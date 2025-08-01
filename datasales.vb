Imports MySql.Data.MySqlClient

Public Class datasales
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datasales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSalesData()
    End Sub

    Private Sub LoadSalesData()
        Try
            conn.Open()
            query = "SELECT 
                        s.salesId,
                        s.salesDate,
                        o.orderId,
                        o.orderStatus,
                        o.orderDate,
                        o.customers_customerId AS customerId,
                        SUM(oi.productQty * p.productPrice) AS totalSalesAmount
                    FROM 
                        sales s
                    JOIN 
                        orders o ON s.orderId = o.orderId
                    JOIN 
                        orderitems oi ON o.orderId = oi.orders_orderId
                    JOIN 
                        products p ON oi.products_productId = p.productId
                    WHERE 
                        LOWER(o.orderStatus) = 'completed'
                    GROUP BY 
                        s.salesId, s.salesDate, o.orderId, o.orderStatus, o.orderDate, o.customers_customerId;"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "SalesData")
            DataGridView1.DataSource = ds.Tables("SalesData")

            ' Format the DataGridView columns
            FormatDataGridView()

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading sales data: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub FormatDataGridView()
        ' Set column headers
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns(0).HeaderText = "Sales ID"
            DataGridView1.Columns(1).HeaderText = "Sales Date"
            DataGridView1.Columns(2).HeaderText = "Order ID"
            DataGridView1.Columns(3).HeaderText = "Order Status"
            DataGridView1.Columns(4).HeaderText = "Order Date"
            DataGridView1.Columns(5).HeaderText = "Customer ID"
            DataGridView1.Columns(6).HeaderText = "Total Sales Amount"

            ' Format the total sales amount column to show currency
            DataGridView1.Columns(6).DefaultCellStyle.Format = "C2"

            ' Auto-size columns
            DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        End If
    End Sub

    Private Sub RefreshData()
        LoadSalesData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                ' You can add textboxes or labels to display selected row data
                ' For example:
                ' TextBox1.Text = row.Cells(0).Value.ToString() ' Sales ID
                ' TextBox2.Text = row.Cells(6).Value.ToString() ' Total Amount
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' Refresh button
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        RefreshData()
    End Sub

    ' Export to CSV (optional)
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim saveFileDialog As New SaveFileDialog()
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv"
            saveFileDialog.Title = "Export Sales Data"
            saveFileDialog.FileName = "SalesData_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".csv"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                ExportToCSV(saveFileDialog.FileName)
                MessageBox.Show("Sales data exported successfully to: " & saveFileDialog.FileName)
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message)
        End Try
    End Sub

    Private Sub ExportToCSV(filePath As String)
        Try
            Dim csv As New System.Text.StringBuilder()

            ' Add headers
            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                csv.Append(DataGridView1.Columns(i).HeaderText)
                If i < DataGridView1.Columns.Count - 1 Then
                    csv.Append(",")
                End If
            Next
            csv.AppendLine()

            ' Add data rows
            For Each row As DataGridViewRow In DataGridView1.Rows
                For i As Integer = 0 To DataGridView1.Columns.Count - 1
                    Dim value As String = ""
                    If row.Cells(i).Value IsNot Nothing Then
                        value = row.Cells(i).Value.ToString()
                    End If
                    csv.Append(value)
                    If i < DataGridView1.Columns.Count - 1 Then
                        csv.Append(",")
                    End If
                Next
                csv.AppendLine()
            Next

            ' Write to file
            System.IO.File.WriteAllText(filePath, csv.ToString())
        Catch ex As Exception
            Throw New Exception("Error writing CSV file: " & ex.Message)
        End Try
    End Sub
End Class