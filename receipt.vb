Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.InteropServices

Public Class receipt
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Private Sub receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Form will be populated by LoadReceipt method
    End Sub

    Public Sub LoadReceipt(orderId As Integer)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Get order details with proper date handling and customer address
            query = $"SELECT 
                            o.orderId,
                            IFNULL(NULLIF(o.orderDate, '0000-00-00'), NULL) AS orderDate,
                            o.orderStatus,
                            c.customerFName,
                            c.customerLName,
                            c.customerPhone,
                            c.customerAddress
                        FROM orders o
                        INNER JOIN customers c ON o.customers_customerId = c.customerId
                        WHERE o.orderId = {orderId}"

            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Populate order header with safe date conversion
                lblOrderId.Text = $"{reader("orderId")}"

                ' Safe date conversion with null checking
                Dim orderDateValue As Object = reader("orderDate")
                If orderDateValue IsNot Nothing AndAlso Not Convert.IsDBNull(orderDateValue) AndAlso Not String.IsNullOrEmpty(orderDateValue.ToString()) Then
                    Try
                        Dim orderDate As DateTime = Convert.ToDateTime(orderDateValue)
                        lblOrderDate.Text = $"Date: {orderDate.ToString("MMMM dd, yyyy")}"
                    Catch ex As Exception
                        lblOrderDate.Text = "Date: N/A"
                    End Try
                Else
                    lblOrderDate.Text = "Date: N/A"
                End If

                lblCustomerName.Text = $"Customer: {reader("customerFName")} {reader("customerLName")}"
                lblCustomerPhone.Text = $"Phone: {reader("customerPhone")}"

                ' Add customer address if available
                Dim customerAddress As Object = reader("customerAddress")
                If customerAddress IsNot Nothing AndAlso Not Convert.IsDBNull(customerAddress) AndAlso Not String.IsNullOrEmpty(customerAddress.ToString()) Then
                    lblAddress.Text = $"Address: {customerAddress.ToString()}"
                Else
                    lblAddress.Text = "Address: N/A"
                End If

                ' Store orderStatus before closing reader
                Dim orderStatus As String = reader("orderStatus").ToString()

                reader.Close()

                ' Get order items
                query = $"SELECT 
                                p.productName,
                                p.productPrice,
                                oi.productQty,
                                (p.productPrice * oi.productQty) AS itemTotal
                            FROM orderitems oi
                            INNER JOIN products p ON oi.products_productId = p.productId
                            WHERE oi.orders_orderId = {orderId}
                            ORDER BY p.productName"

                cmd = New MySqlCommand(query, conn)
                da = New MySqlDataAdapter(cmd)
                ds = New DataSet()
                da.Fill(ds, "items")

                DataGridView1.DataSource = ds.Tables("items")

                ' Configure DataGridView
                DataGridView1.Columns("productName").HeaderText = "Product"
                DataGridView1.Columns("productPrice").HeaderText = "Price"
                DataGridView1.Columns("productQty").HeaderText = "Qty"
                DataGridView1.Columns("itemTotal").HeaderText = "Total"

                ' Format price columns
                DataGridView1.Columns("productPrice").DefaultCellStyle.Format = "₱#,##0.00"
                DataGridView1.Columns("itemTotal").DefaultCellStyle.Format = "₱#,##0.00"

                ' Calculate total with better error handling
                Dim total As Decimal = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        Try
                            Dim itemTotalValue As Object = row.Cells("itemTotal").Value
                            If itemTotalValue IsNot Nothing AndAlso Not Convert.IsDBNull(itemTotalValue) Then
                                total += Convert.ToDecimal(itemTotalValue)
                            End If
                        Catch ex As Exception
                            ' Skip invalid values
                            Continue For
                        End Try
                    End If
                Next

                lblTotal.Text = $"₱{total:N2}"

                ' Check if order is completed and add to sales if needed
                If orderStatus = "completed" Then
                    AddToSales(orderId, total)
                End If

            Else
                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading receipt: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub AddToSales(orderId As Integer, totalAmount As Decimal)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Check if sales record already exists
            query = $"SELECT salesId FROM sales WHERE orderId = {orderId}"
            cmd = New MySqlCommand(query, conn)
            Dim existingSales = cmd.ExecuteScalar()

            If existingSales Is Nothing Then
                ' Create sales record with total amount
                query = $"INSERT INTO sales (salesDate, orderId, totalAmount) VALUES (CURDATE(), {orderId}, {totalAmount})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Order completed and added to sales records.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error adding to sales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Try multiple methods to capture the exact form appearance
        Try
            SaveReceiptUsingPrintWindow()
        Catch ex As Exception
            Try
                SaveReceiptUsingAlternativeMethod()
            Catch ex2 As Exception
                MessageBox.Show("Error saving image: " & ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Try
    End Sub

    Private Sub SaveReceiptUsingPrintWindow()
        Try
            ' Force complete rendering of all controls
            Me.Refresh()
            Application.DoEvents()

            ' Force DataGridView to update and ensure all data is visible
            DataGridView1.Refresh()
            DataGridView1.Update()
            Application.DoEvents()

            ' Ensure all labels are updated
            lblOrderId.Refresh()
            lblOrderDate.Refresh()
            lblCustomerName.Refresh()
            lblCustomerPhone.Refresh()
            lblAddress.Refresh()
            lblTotal.Refresh()
            Application.DoEvents()

            ' Add delay to ensure everything is rendered
            System.Threading.Thread.Sleep(300)

            ' Get PictureBox1 bounds (receipt area)
            Dim pictureBoxBounds As Rectangle = PictureBox1.Bounds

            ' Create bitmap with PictureBox1 dimensions
            Using bmp As New Bitmap(pictureBoxBounds.Width, pictureBoxBounds.Height)
                ' Create graphics object for the bitmap
                Using g As Graphics = Graphics.FromImage(bmp)
                    ' Set high quality rendering
                    g.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

                    ' Clear the bitmap with white background first
                    g.Clear(Color.White)

                    ' Draw the PictureBox1 background image first
                    If PictureBox1.BackgroundImage IsNot Nothing Then
                        g.DrawImage(PictureBox1.BackgroundImage, 0, 0, pictureBoxBounds.Width, pictureBoxBounds.Height)
                    End If

                    ' Now draw all the receipt details on top of the background
                    DrawReceiptDetails(g, pictureBoxBounds)
                End Using

                Dim sfd As New SaveFileDialog()
                sfd.Filter = "PNG Image|*.png"
                sfd.Title = "Save Complete Receipt as Image"
                sfd.FileName = $"Receipt_Complete_{lblOrderId.Text}.png"

                If sfd.ShowDialog() = DialogResult.OK Then
                    bmp.Save(sfd.FileName, Imaging.ImageFormat.Png)
                    MessageBox.Show("Complete receipt saved as image successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DrawReceiptDetails(g As Graphics, pictureBoxBounds As Rectangle)
        Try
            ' Draw receipt title
            Using titleFont As New Font("Sitka Subheading", 20, FontStyle.Bold)
                g.DrawString("RECEIPT", titleFont, Brushes.Black, 117 - pictureBoxBounds.X, 19 - pictureBoxBounds.Y)
            End Using

            ' Draw order ID
            Using orderFont As New Font("Bahnschrift", 18, FontStyle.Bold)
                g.DrawString(lblOrderId.Text, orderFont, Brushes.Black, 203 - pictureBoxBounds.X, 70 - pictureBoxBounds.Y)
            End Using

            ' Draw customer details
            Using detailFont As New Font("Microsoft Yi Baiti", 12)
                g.DrawString(lblOrderDate.Text, detailFont, Brushes.Black, 174 - pictureBoxBounds.X, 176 - pictureBoxBounds.Y)
                g.DrawString(lblCustomerName.Text, detailFont, Brushes.Black, 44 - pictureBoxBounds.X, 176 - pictureBoxBounds.Y)
                g.DrawString(lblCustomerPhone.Text, detailFont, Brushes.Black, 41 - pictureBoxBounds.X, 134 - pictureBoxBounds.Y)
                g.DrawString(lblAddress.Text, detailFont, Brushes.Black, 41 - pictureBoxBounds.X, 114 - pictureBoxBounds.Y)
            End Using

            ' Draw DataGridView content
            DrawDataGridViewContent(g, pictureBoxBounds)

            ' Draw total
            Using totalFont As New Font("Calibri", 18, FontStyle.Bold)
                g.DrawString(lblTotal.Text, totalFont, Brushes.White, 86 - pictureBoxBounds.X, 411 - pictureBoxBounds.Y)
            End Using

        Catch ex As Exception
            ' Ignore errors in drawing details
        End Try
    End Sub

    Private Sub SaveReceiptUsingAlternativeMethod()
        Try
            ' Force complete rendering of all controls
            Me.Refresh()
            Application.DoEvents()

            ' Force DataGridView to update and ensure all data is visible
            DataGridView1.Refresh()
            DataGridView1.Update()
            Application.DoEvents()

            ' Ensure all labels are updated
            lblOrderId.Refresh()
            lblOrderDate.Refresh()
            lblCustomerName.Refresh()
            lblCustomerPhone.Refresh()
            lblAddress.Refresh()
            lblTotal.Refresh()
            Application.DoEvents()

            ' Add delay to ensure everything is rendered
            System.Threading.Thread.Sleep(300)

            ' Get PictureBox1 bounds (receipt area)
            Dim pictureBoxBounds As Rectangle = PictureBox1.Bounds

            ' Create bitmap with PictureBox1 dimensions
            Using bmp As New Bitmap(pictureBoxBounds.Width, pictureBoxBounds.Height)
                ' Create graphics object for the bitmap
                Using g As Graphics = Graphics.FromImage(bmp)
                    ' Set high quality rendering
                    g.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

                    ' Clear the bitmap with white background first
                    g.Clear(Color.White)

                    ' Draw the PictureBox1 background image first
                    If PictureBox1.BackgroundImage IsNot Nothing Then
                        g.DrawImage(PictureBox1.BackgroundImage, 0, 0, pictureBoxBounds.Width, pictureBoxBounds.Height)
                    End If

                    ' Now draw all the receipt details on top of the background
                    DrawReceiptDetails(g, pictureBoxBounds)
                End Using

                Dim sfd As New SaveFileDialog()
                sfd.Filter = "PNG Image|*.png"
                sfd.Title = "Save Complete Receipt as Image"
                sfd.FileName = $"Receipt_Complete_{lblOrderId.Text}.png"

                If sfd.ShowDialog() = DialogResult.OK Then
                    bmp.Save(sfd.FileName, Imaging.ImageFormat.Png)
                    MessageBox.Show("Complete receipt saved as image successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Windows API declaration for PrintWindow
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function PrintWindow(hwnd As IntPtr, hdcBlt As IntPtr, nFlags As Integer) As Boolean
    End Function

    Private Sub SaveReceiptAsImageManual()
        Try
            ' Force complete rendering of all controls
            Me.Refresh()
            Application.DoEvents()

            ' Force DataGridView to update
            DataGridView1.Refresh()
            Application.DoEvents()

            ' Ensure all labels are updated
            lblOrderId.Refresh()
            lblOrderDate.Refresh()
            lblCustomerName.Refresh()
            lblCustomerPhone.Refresh()
            lblAddress.Refresh()
            lblTotal.Refresh()
            Application.DoEvents()

            ' Add longer delay to ensure everything is rendered
            System.Threading.Thread.Sleep(300)

            Dim bounds As Rectangle = Me.Bounds

            ' Create bitmap with form dimensions
            Using bmp As New Bitmap(bounds.Width, bounds.Height)
                ' Create graphics object for the bitmap
                Using g As Graphics = Graphics.FromImage(bmp)
                    ' Set high quality rendering
                    g.SmoothingMode = Drawing.Drawing2D.SmoothingMode.AntiAlias
                    g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
                    g.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic

                    ' Clear the bitmap with white background first
                    g.Clear(Color.White)

                    ' Draw the entire form to the bitmap
                    Me.DrawToBitmap(bmp, New Rectangle(0, 0, bounds.Width, bounds.Height))
                End Using

                Dim sfd As New SaveFileDialog()
                sfd.Filter = "PNG Image|*.png"
                sfd.Title = "Save Receipt as Image"
                sfd.FileName = $"Receipt_{lblOrderId.Text}.png"

                If sfd.ShowDialog() = DialogResult.OK Then
                    bmp.Save(sfd.FileName, Imaging.ImageFormat.Png)
                    MessageBox.Show("Receipt saved as image successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error saving image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DrawDataGridViewContent(g As Graphics, pictureBoxBounds As Rectangle)
        Try
            ' Get DataGridView position relative to PictureBox1
            Dim gridLocation As Point = DataGridView1.Location
            Dim relativeX As Integer = gridLocation.X - pictureBoxBounds.X
            Dim relativeY As Integer = gridLocation.Y - pictureBoxBounds.Y

            ' Draw DataGridView content
            Using gridFont As New Font("Microsoft Sans Serif", 10)
                Dim rowHeight As Integer = 24
                Dim startY As Integer = relativeY + DataGridView1.ColumnHeadersHeight

                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If Not DataGridView1.Rows(i).IsNewRow Then
                        Dim row As DataGridViewRow = DataGridView1.Rows(i)
                        Dim currentY As Integer = startY + (i * rowHeight)

                        ' Draw each cell content
                        For j As Integer = 0 To DataGridView1.Columns.Count - 1
                            Dim cell As DataGridViewCell = row.Cells(j)
                            Dim column As DataGridViewColumn = DataGridView1.Columns(j)

                            If cell.Value IsNot Nothing Then
                                Dim cellText As String = cell.Value.ToString()
                                Dim cellX As Integer = relativeX + column.DisplayIndex * (DataGridView1.Width / DataGridView1.Columns.Count)

                                ' Draw cell text
                                g.DrawString(cellText, gridFont, Brushes.Black, cellX + 5, currentY + 2)
                            End If
                        Next
                    End If
                Next
            End Using
        Catch ex As Exception
            ' Ignore errors in drawing DataGridView content
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class