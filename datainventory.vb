Imports MySql.Data.MySqlClient

Public Class datainventory
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datainventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryData()
    End Sub

    Private Sub LoadInventoryData()
        Try
            conn.Open()
            query = "SELECT 
                        i.inventoryId,
                        p.productId,
                        p.productName,
                        pc.category,  
                        p.productPrice,
                        i.productStock,
                        s.supplierName 
                     FROM inventory i
                     JOIN products p ON i.products_productId = p.productId
                     JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                     JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                     ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Inventory")
            DataGridView1.DataSource = ds.Tables("Inventory")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(0).Value.ToString() ' inventoryId
                TextBox2.Text = row.Cells(1).Value.ToString() ' productId
                TextBox3.Text = row.Cells(2).Value.ToString() ' productName
                ComboBox1.Text = row.Cells(3).Value.ToString() ' category
                TextBox4.Text = row.Cells(4).Value.ToString() ' productPrice
                TextBox5.Text = row.Cells(5).Value.ToString() ' productStock
                ComboBox2.Text = row.Cells(6).Value.ToString() ' supplierName 
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' INSERT
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If ComboBox1.SelectedIndex = -1 OrElse
               ComboBox2.SelectedIndex = -1 OrElse
               String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) Then
                MessageBox.Show("Please fill in all fields before inserting.")
                Exit Sub
            End If

            conn.Open()
            Dim supplierName As String = ComboBox1.Text
            Dim category As String = ComboBox2.Text
            Dim productName As String = TextBox3.Text
            Dim productPrice As String = TextBox4.Text
            Dim productStock As String = TextBox5.Text

            query = $"INSERT INTO products (productName, productPrice, productCategories_categoryId) 
                      VALUES ('{productName}', '{productPrice}', 
                      (SELECT categoryId FROM productCategories WHERE category = '{category}'))"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            query = "SELECT LAST_INSERT_ID();"
            cmd = New MySqlCommand(query, conn)
            Dim newProductId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            query = $"INSERT INTO inventory (products_productId, suppliers_supplierId, productStock) 
                      VALUES ({newProductId}, 
                      (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), 
                      '{productStock}')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product inserted successfully.")
            conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' CLEAR
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        RefreshData()
    End Sub

    ' UPDATE
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
               ComboBox1.SelectedIndex = -1 OrElse
               ComboBox2.SelectedIndex = -1 OrElse
               String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) Then
                MessageBox.Show("Please select a product and fill in all fields before updating.")
                Exit Sub
            End If

            conn.Open()
            Dim inventoryId As String = TextBox1.Text
            Dim productId As String = TextBox2.Text
            Dim supplierName As String = ComboBox1.Text
            Dim category As String = ComboBox2.Text
            Dim productName As String = TextBox3.Text
            Dim productPrice As String = TextBox4.Text
            Dim productStock As String = TextBox5.Text

            query = $"UPDATE products 
                      SET productName = '{productName}', 
                          productPrice = '{productPrice}', 
                          productCategories_categoryId = (SELECT categoryId FROM productCategories WHERE category = '{category}') 
                      WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            query = $"UPDATE inventory 
                      SET suppliers_supplierId = (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), 
                          productStock = '{productStock}' 
                      WHERE inventoryId = {inventoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product updated successfully.")
            conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' DELETE
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Please select a product to delete.")
                Exit Sub
            End If

            Dim inventoryId As String = TextBox1.Text
            Dim productId As String = TextBox2.Text
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Exit Sub

            conn.Open()
            query = $"DELETE FROM inventory WHERE inventoryId= {inventoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            query = $"DELETE FROM products WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product deleted successfully.")
            conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' SEARCH
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim searchText As String = TextBox6.Text.Trim()
            If String.IsNullOrWhiteSpace(searchText) Then
                MessageBox.Show("Please enter a product name to search.")
                Exit Sub
            End If

            conn.Open()
            query = "SELECT 
                        i.inventoryId,
                        p.productId,
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
