Imports MySql.Data.MySqlClient

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

            ' Get order details
            query = $"SELECT 
                        o.orderId,
                        o.orderDate,
                        o.orderStatus,
                        c.customerFName,
                        c.customerLName,
                        c.customerEmail,
                        c.customerPhone
                    FROM orders o
                    INNER JOIN customers c ON o.customers_customerId = c.customerId
                    WHERE o.orderId = {orderId}"
            
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()
            
            If reader.Read() Then
                ' Populate order header
                lblOrderId.Text = $"Order ID: {reader("orderId")}"
                lblOrderDate.Text = $"Date: {Convert.ToDateTime(reader("orderDate")).ToString("MMMM dd, yyyy")}"
                lblCustomerName.Text = $"Customer: {reader("customerFName")} {reader("customerLName")}"
                lblCustomerEmail.Text = $"Email: {reader("customerEmail")}"
                lblCustomerPhone.Text = $"Phone: {reader("customerPhone")}"
                lblStatus.Text = $"Status: {reader("orderStatus")}"
                
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
                
                ' Calculate total
                Dim total As Decimal = 0
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        total += Convert.ToDecimal(row.Cells("itemTotal").Value)
                    End If
                Next
                
                lblTotal.Text = $"Total: ₱{total:N2}"
                
                ' If order is completed, add to sales
                If lblStatus.Text.Contains("completed") Then
                    AddToSales(orderId, total)
                End If
                
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
                ' Create sales record
                query = $"INSERT INTO sales (salesDate, orderId) VALUES (CURDATE(), {orderId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                
                MessageBox.Show("Order completed and added to sales records.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error adding to sales: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        ' Print functionality can be added here
        MessageBox.Show("Print functionality would be implemented here.")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class 