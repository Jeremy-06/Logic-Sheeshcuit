Imports MySql.Data.MySqlClient

Public Class dataproducts
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub dataproducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conn.Open()
            query = "SELECT 
                      p.productId,
                      s.supplierName, 
                      pc.category, 
                      p.productName, 
                      p.productPrice, 
                      i.productStock FROM inventory i
                    JOIN products p ON i.products_productId = p.productId
                    JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                    JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                    ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "products")
            DataGridView1.DataSource = ds.Tables("products")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub RefreshData()
        Try
            conn.Open()
            query = "SELECT 
                          p.productId,
                          s.supplierName, 
                          pc.category, 
                          p.productName, 
                          p.productPrice, 
                          i.productStock FROM inventory i
                        JOIN products p ON i.products_productId = p.productId
                        JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                        JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                        ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "products")
            DataGridView1.DataSource = ds.Tables("products")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    ' Add this event handler to show the selected row's values in TextBoxes when a cell is clicked.
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(0).Value.ToString() ' productId
                ComboBox1.Text = row.Cells(1).Value.ToString() ' category
                ComboBox2.Text = row.Cells(2).Value.ToString() ' productName
                TextBox4.Text = row.Cells(3).Value.ToString() ' productPrice
                TextBox5.Text = row.Cells(4).Value.ToString() ' productStock
                TextBox6.Text = row.Cells(5).Value.ToString() ' supplierName for TextBox6
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub
    'INSERT
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Validate required fields
            If ComboBox1.SelectedIndex = -1 OrElse
               ComboBox2.SelectedIndex = -1 OrElse
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox6.Text) Then
                MessageBox.Show("Please fill in all fields before inserting.")
                Exit Sub
            End If

            conn.Open()
            Dim productId As String = TextBox1.Text
            Dim supplierName As String = ComboBox1.Text
            Dim category As String = ComboBox2.Text
            Dim productName As String = TextBox4.Text
            Dim productPrice As String = TextBox5.Text
            Dim productStock As String = TextBox6.Text

            ' After inserting into products, get the last inserted productId using LAST_INSERT_ID()
            query = $"INSERT INTO products (productName, productPrice, productCategories_categoryId) VALUES ('{productName}', '{productPrice}', (SELECT categoryId FROM productCategories WHERE category = '{category}'))"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the auto-incremented productId
            query = "SELECT LAST_INSERT_ID();"
            cmd = New MySqlCommand(query, conn)
            Dim newProductId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            ' Now insert into inventory using the new productId
            query = $"INSERT INTO inventory (products_productId, suppliers_supplierId, productStock) VALUES ({newProductId}, (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), '{productStock}')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product inserted successfully.")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        ' Add this method to refresh the DataGridView data from the database.
        RefreshData()
    End Sub

    'UPDATE
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Validate required fields
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
               ComboBox1.SelectedIndex = -1 OrElse
               ComboBox2.SelectedIndex = -1 OrElse
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox6.Text) Then
                MessageBox.Show("Please select a product and fill in all fields before updating.")
                Exit Sub
            End If

            conn.Open()
            Dim productId As String = TextBox1.Text
            Dim supplierName As String = ComboBox1.Text
            Dim category As String = ComboBox2.Text
            Dim productName As String = TextBox4.Text
            Dim productPrice As String = TextBox5.Text
            Dim productStock As String = TextBox6.Text

            ' Update products table
            query = $"UPDATE products SET productName = '{productName}', productPrice = '{productPrice}', productCategories_categoryId = (SELECT categoryId FROM productCategories WHERE category = '{category}') WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Update inventory table
            query = $"UPDATE inventory SET suppliers_supplierId = (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), productStock = '{productStock}' WHERE products_productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product updated successfully.")
            conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    'DELETE
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Validate that a product is selected
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Please select a product to delete.")
                Exit Sub
            End If

            Dim productId As String = TextBox1.Text

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then
                Exit Sub
            End If

            conn.Open()

            ' Delete from inventory first due to foreign key constraints
            query = $"DELETE FROM inventory WHERE products_productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Delete from products table
            query = $"DELETE FROM products WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Product deleted successfully.")
            conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    'SEARCH
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim searchText As String = TextBox2.Text.Trim()
            If String.IsNullOrWhiteSpace(searchText) Then
                MessageBox.Show("Please enter a product name to search.")
                Exit Sub
            End If

            conn.Open()
            query = "SELECT 
                        p.productId,
                        s.supplierName, 
                        pc.category, 
                        p.productName, 
                        p.productPrice, 
                        i.productStock 
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
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
End Class