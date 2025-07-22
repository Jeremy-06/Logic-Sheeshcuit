Imports MySql.Data.MySqlClient

Public Class cart
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String
    Private Sub cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conn.Open()
            query = "SELECT 
                        c.customerId,
                        p.productName, 
                        p.productPrice, 
                        ca.productQty FROM customers c
                    JOIN cart ca ON c.customerId = ca.customers_customerId
                    JOIN products p ON ca.products_productId = p.productId
                    ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cart")
            DataGridView1.DataSource = ds.Tables("cart")
            DataGridView1.Columns("customerId").HeaderText = "Customer ID"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("productQty").HeaderText = "Quantity"
        Catch ex As Exception
            MessageBox.Show("Error loading cart data: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class