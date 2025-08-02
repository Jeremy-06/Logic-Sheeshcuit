Imports MySql.Data.MySqlClient

Public Class datacart
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datacart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryData()
    End Sub

    Private Sub LoadInventoryData()
        Try
            conn.Open()
            query = "SELECT 
                        c.cartId,
                        CONCAT (cu.customerFName,' ', cu.customerLName) AS customerFullName,
                        p.productName,
                        pc.category,  
                        p.productPrice,
                        c.productQty,
                        s.supplierName 
                     FROM cart c
                     JOIN customers cu ON c.customers_customerId = cu.customerId
                     JOIN products p ON c.products_productId = p.productId
                     JOIN inventory i ON i.products_productId = p.productId
                     JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                     JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                     ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Cart")
            DataGridView1.DataSource = ds.Tables("Cart")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(0).Value.ToString() ' cartId
                TextBox2.Text = row.Cells(1).Value.ToString() ' customerName
                TextBox3.Text = row.Cells(2).Value.ToString() ' productName
                ComboBox1.Text = row.Cells(3).Value.ToString() ' category
                TextBox4.Text = row.Cells(4).Value.ToString() ' productPrice
                TextBox5.Text = row.Cells(5).Value.ToString() ' productQuantity
                ComboBox2.Text = row.Cells(6).Value.ToString() ' supplierName 
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' CLEAR
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        RefreshData()
    End Sub

    ' SEARCH
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim searchText As String = TextBox6.Text.Trim()
            If String.IsNullOrWhiteSpace(searchText) Then
                MessageBox.Show("Please enter a product name to search.")
                Exit Sub
            End If

            conn.Open()
            query = "SELECT 
                        i.inventoryId,
                        u.username,
                        p.productName,
                        pc.category,  
                        p.productPrice,
                        i.productStock,
                        s.supplierName 
                     FROM inventory i
                     JOIN products p ON i.products_productId = p.productId
                     JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                     JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                     WHERE p.productName LIKE '%" & searchText & "%'
                     ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "products")
            DataGridView1.DataSource = ds.Tables("products")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class
