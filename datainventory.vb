Imports MySql.Data.MySqlClient

Public Class datainventory
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datainventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateCategoryComboBox()
        PopulateSupplierComboBox()
        PopulateSearchComboBox()
        LoadInventoryData()

        ' Add event handlers to maintain highlighting
        AddHandler DataGridView1.Sorted, AddressOf DataGridView1_Sorted
        AddHandler DataGridView1.DataSourceChanged, AddressOf DataGridView1_DataSourceChanged
        
        ' Add event handlers for numeric validation
        AddHandler TextBox3.KeyPress, AddressOf TextBox3_KeyPress ' Product Price
        AddHandler TextBox5.KeyPress, AddressOf TextBox5_KeyPress ' Product Stock
    End Sub

    Private Sub PopulateCategoryComboBox()
        Try
            ComboBox1.Items.Clear()
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = "SELECT category FROM productCategories ORDER BY categoryId"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ComboBox1.Items.Add(reader("category").ToString())
            End While
            reader.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PopulateSupplierComboBox()
        Try
            ComboBox2.Items.Clear()
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = "SELECT supplierName FROM suppliers ORDER BY supplierId"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                ComboBox2.Items.Add(reader("supplierName").ToString())
            End While
            reader.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading suppliers: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PopulateSearchComboBox()
        Try
            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("Product Name")
            ComboBox3.Items.Add("Product ID")
            ComboBox3.Items.Add("Category")
            ComboBox3.Items.Add("Supplier Name")
            ComboBox3.SelectedIndex = 0 ' Default to first item
        Catch ex As Exception
            MessageBox.Show("Error populating search combo box: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadInventoryData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = "SELECT 
                        p.productId, p.productName, p.productPrice,
                        pc.categoryId, pc.category,
                        i.inventoryId, i.productStock,
                        s.supplierId, s.supplierName
                     FROM products p
                     JOIN productcategories pc ON p.productCategories_categoryId = pc.categoryId
                     JOIN inventory i ON p.productId = i.products_productId
                     JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                     ORDER BY p.productId;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Inventory")
            DataGridView1.DataSource = ds.Tables("Inventory")

            ' Set formal column headers
            DataGridView1.Columns(0).HeaderText = "Product ID"
            DataGridView1.Columns(1).HeaderText = "Product Name"
            DataGridView1.Columns(2).HeaderText = "Product Price"
            DataGridView1.Columns(3).HeaderText = "Category ID"
            DataGridView1.Columns(4).HeaderText = "Category"
            DataGridView1.Columns(5).HeaderText = "Inventory ID"
            DataGridView1.Columns(6).HeaderText = "Product Stock"
            DataGridView1.Columns(7).HeaderText = "Supplier ID"
            DataGridView1.Columns(8).HeaderText = "Supplier Name"

            HighlightLowStockItems()

            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Highlights rows with stock < 10 in light red
    Private Sub HighlightLowStockItems()
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow AndAlso row.Cells.Count > 6 Then
                    Dim stockValue As Object = row.Cells(6).Value

                    If stockValue IsNot Nothing AndAlso IsNumeric(stockValue) Then
                        Dim stock As Integer = Convert.ToInt32(stockValue)

                        If stock < 10 Then
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 230, 230) ' Pastel Red
                        Else
                            row.DefaultCellStyle.BackColor = Color.White
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error highlighting low stock items: " & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_Sorted(sender As Object, e As EventArgs)
        HighlightLowStockItems()
    End Sub

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs)
        HighlightLowStockItems()
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
        HighlightLowStockItems()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Clear all inputs
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = 0 ' Reset search type to default
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        RefreshData()
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(0).Value.ToString() ' Product ID
                TextBox2.Text = row.Cells(1).Value.ToString() ' Product Name
                TextBox3.Text = row.Cells(2).Value.ToString() ' Product Price
                TextBox4.Text = row.Cells(5).Value.ToString() ' Inventory ID
                TextBox5.Text = row.Cells(6).Value.ToString() ' Product Stock
                ComboBox1.Text = row.Cells(4).Value.ToString() ' Category
                ComboBox2.Text = row.Cells(8).Value.ToString() ' Supplier Name
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    ' INSERT
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
               String.IsNullOrWhiteSpace(ComboBox1.Text) OrElse
               String.IsNullOrWhiteSpace(ComboBox2.Text) Then
                MessageBox.Show("Please fill in all fields before inserting.")
                Exit Sub
            End If

            ' Show confirmation with summary
            Dim productId As String = TextBox1.Text.Trim()
            Dim productName As String = TextBox2.Text
            Dim productPrice As String = TextBox3.Text
            Dim productStock As String = TextBox5.Text
            Dim category As String = ComboBox1.Text
            Dim supplierName As String = ComboBox2.Text

            Dim summary As String = $"INSERT NEW PRODUCT:" & vbCrLf & vbCrLf &
                                   $"Product ID: {(If(String.IsNullOrWhiteSpace(productId), "Auto-increment", productId))}" & vbCrLf &
                                   $"Product Name: {productName}" & vbCrLf &
                                   $"Product Price: {productPrice}" & vbCrLf &
                                   $"Product Stock: {productStock}" & vbCrLf &
                                   $"Category: {category}" & vbCrLf &
                                   $"Supplier: {supplierName}" & vbCrLf & vbCrLf &
                                   $"Do you want to insert this product?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Exit Sub

            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Check if product ID is provided and if it already exists
            If Not String.IsNullOrWhiteSpace(productId) Then
                query = $"SELECT COUNT(*) FROM products WHERE productId = {productId}"
                cmd = New MySqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("Product ID already exists. Please leave Product ID blank for auto-increment or use a different ID.")
                    conn.Close()
                    Exit Sub
                End If
            End If

            ' Insert with or without product ID
            If String.IsNullOrWhiteSpace(productId) Then
                ' Auto increment - don't specify productId
                query = $"INSERT INTO products (productName, productPrice, productCategories_categoryId) 
                          VALUES ('{productName}', '{productPrice}', 
                          (SELECT categoryId FROM productCategories WHERE category = '{category}'))"
            Else
                ' Use specified product ID
                query = $"INSERT INTO products (productId, productName, productPrice, productCategories_categoryId) 
                          VALUES ({productId}, '{productName}', '{productPrice}', 
                          (SELECT categoryId FROM productCategories WHERE category = '{category}'))"
            End If

            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the inserted product ID
            Dim newProductId As Integer
            If String.IsNullOrWhiteSpace(productId) Then
                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                newProductId = Convert.ToInt32(cmd.ExecuteScalar())
            Else
                newProductId = Convert.ToInt32(productId)
            End If

            query = $"INSERT INTO inventory (products_productId, suppliers_supplierId, productStock) 
                      VALUES ({newProductId}, 
                      (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), 
                      '{productStock}')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"Product inserted successfully!" & vbCrLf & vbCrLf &
                          $"Product ID: {newProductId}" & vbCrLf &
                          $"Product Name: {productName}" & vbCrLf &
                          $"Product Price: {productPrice}" & vbCrLf &
                          $"Product Stock: {productStock}" & vbCrLf &
                          $"Category: {category}" & vbCrLf &
                          $"Supplier: {supplierName}", "Insert Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If conn.State = ConnectionState.Open Then conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub


    ' UPDATE
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
               String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
               String.IsNullOrWhiteSpace(ComboBox1.Text) OrElse
               String.IsNullOrWhiteSpace(ComboBox2.Text) Then
                MessageBox.Show("Please select a product and fill in all fields before updating.")
                Exit Sub
            End If

            If conn.State <> ConnectionState.Open Then conn.Open()
            ' Check if category exists
            query = $"SELECT categoryId FROM productCategories WHERE category = '{ComboBox1.Text}'"
            cmd = New MySqlCommand(query, conn)
            Dim catId = cmd.ExecuteScalar()
            If catId Is Nothing Then
                MessageBox.Show("Selected category does not exist in the database.")
                If conn.State = ConnectionState.Open Then conn.Close()
                Exit Sub
            End If

            Dim productId As String = TextBox1.Text
            Dim productName As String = TextBox2.Text
            Dim productPrice As String = TextBox3.Text
            Dim inventoryId As String = TextBox4.Text
            Dim productStock As String = TextBox5.Text
            Dim category As String = ComboBox1.Text
            Dim supplierName As String = ComboBox2.Text

            ' Check if there are any changes by comparing with current database values
            query = $"SELECT p.productName, p.productPrice, pc.category, i.productStock, s.supplierName
                      FROM products p
                      JOIN productcategories pc ON p.productCategories_categoryId = pc.categoryId
                      JOIN inventory i ON p.productId = i.products_productId
                      JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                      WHERE p.productId = {productId} AND i.inventoryId = {inventoryId}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim hasChanges As Boolean = False
            Dim changesList As New List(Of String)
            If reader.Read() Then
                Dim currentProductName As String = reader("productName").ToString()
                Dim currentProductPrice As String = reader("productPrice").ToString()
                Dim currentCategory As String = reader("category").ToString()
                Dim currentProductStock As String = reader("productStock").ToString()
                Dim currentSupplierName As String = reader("supplierName").ToString()

                ' Check if any field has changed and build changes list
                If currentProductName <> productName Then
                    hasChanges = True
                    changesList.Add($"Product Name: {currentProductName} → {productName}")
                End If
                If currentProductPrice <> productPrice Then
                    hasChanges = True
                    changesList.Add($"Product Price: {currentProductPrice} → {productPrice}")
                End If
                If currentCategory <> category Then
                    hasChanges = True
                    changesList.Add($"Category: {currentCategory} → {category}")
                End If
                If currentProductStock <> productStock Then
                    hasChanges = True
                    changesList.Add($"Product Stock: {currentProductStock} → {productStock}")
                End If
                If currentSupplierName <> supplierName Then
                    hasChanges = True
                    changesList.Add($"Supplier: {currentSupplierName} → {supplierName}")
                End If
            End If
            reader.Close()

            If Not hasChanges Then
                MessageBox.Show("No changes detected. Product information is already up to date.")
                If conn.State = ConnectionState.Open Then conn.Close()
                Exit Sub
            End If

            ' Show confirmation with changes summary
            Dim summary As String = $"UPDATE PRODUCT:" & vbCrLf & vbCrLf &
                                   $"Product ID: {productId}" & vbCrLf &
                                   $"Inventory ID: {inventoryId}" & vbCrLf & vbCrLf &
                                   $"CHANGES TO BE MADE:" & vbCrLf
            For Each change In changesList
                summary &= change & vbCrLf
            Next
            summary &= vbCrLf & "Do you want to update this product?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                If conn.State = ConnectionState.Open Then conn.Close()
                Exit Sub
            End If

            query = $"UPDATE products 
                      SET productName = '{productName}', 
                          productPrice = '{productPrice}', 
                          productCategories_categoryId = {catId} 
                      WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            query = $"UPDATE inventory 
                      SET suppliers_supplierId = (SELECT supplierId FROM suppliers WHERE supplierName = '{supplierName}'), 
                          productStock = '{productStock}' 
                      WHERE inventoryId = {inventoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Show success message with summary of changes
            Dim successMsg As String = $"Product updated successfully!" & vbCrLf & vbCrLf &
                                      $"Product ID: {productId}" & vbCrLf &
                                      $"Inventory ID: {inventoryId}" & vbCrLf & vbCrLf &
                                      $"CHANGES MADE:" & vbCrLf
            For Each change In changesList
                successMsg &= change & vbCrLf
            Next

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If conn.State = ConnectionState.Open Then conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' DELETE
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox4.Text) Then
                MessageBox.Show("Please select a product to delete.")
                Exit Sub
            End If

            Dim productId As String = TextBox1.Text
            Dim inventoryId As String = TextBox4.Text
            Dim productName As String = TextBox2.Text
            Dim productPrice As String = TextBox3.Text
            Dim productStock As String = TextBox5.Text
            Dim category As String = ComboBox1.Text
            Dim supplierName As String = ComboBox2.Text

            ' Show confirmation with product details
            Dim summary As String = $"DELETE PRODUCT:" & vbCrLf & vbCrLf &
                                   $"Product ID: {productId}" & vbCrLf &
                                   $"Inventory ID: {inventoryId}" & vbCrLf &
                                   $"Product Name: {productName}" & vbCrLf &
                                   $"Product Price: {productPrice}" & vbCrLf &
                                   $"Product Stock: {productStock}" & vbCrLf &
                                   $"Category: {category}" & vbCrLf &
                                   $"Supplier: {supplierName}" & vbCrLf & vbCrLf &
                                   $"WARNING: This action cannot be undone!" & vbCrLf &
                                   $"Do you want to delete this product?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Exit Sub

            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"DELETE FROM inventory WHERE inventoryId = {inventoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            query = $"DELETE FROM products WHERE productId = {productId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"Product deleted successfully!" & vbCrLf & vbCrLf &
                          $"DELETED PRODUCT DETAILS:" & vbCrLf &
                          $"Product ID: {productId}" & vbCrLf &
                          $"Inventory ID: {inventoryId}" & vbCrLf &
                          $"Product Name: {productName}" & vbCrLf &
                          $"Product Price: {productPrice}" & vbCrLf &
                          $"Product Stock: {productStock}" & vbCrLf &
                          $"Category: {category}" & vbCrLf &
                          $"Supplier: {supplierName}", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If conn.State = ConnectionState.Open Then conn.Close()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' SEARCH
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Search button functionality (same as text changed)
        TextBox6_TextChanged(sender, e)
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        ' Search functionality based on selected combo box item
        Try
            If Not String.IsNullOrEmpty(TextBox6.Text) Then
                If conn.State <> ConnectionState.Open Then conn.Open()

                Dim searchType As String = If(ComboBox3.SelectedItem IsNot Nothing, ComboBox3.SelectedItem.ToString(), "Product Name")
                Dim searchCondition As String = ""

                Select Case searchType
                    Case "Product Name"
                        searchCondition = $"p.productName LIKE '%{TextBox6.Text}%'"
                    Case "Product ID"
                        ' Only allow numbers for Product ID
                        If IsNumeric(TextBox6.Text) Then
                            searchCondition = $"p.productId = {TextBox6.Text}"
                        Else
                            MessageBox.Show("Product ID must be a number only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBox6.Text = ""
                            If conn.State = ConnectionState.Open Then conn.Close()
                            Return
                        End If
                    Case "Category"
                        searchCondition = $"pc.category LIKE '%{TextBox6.Text}%'"
                    Case "Supplier Name"
                        searchCondition = $"s.supplierName LIKE '%{TextBox6.Text}%'"
                    Case Else
                        searchCondition = $"p.productName LIKE '%{TextBox6.Text}%'"
                End Select

                query = $"SELECT 
                            p.productId, p.productName, p.productPrice,
                            pc.categoryId, pc.category,
                            i.inventoryId, i.productStock,
                            s.supplierId, s.supplierName
                         FROM products p
                         JOIN productcategories pc ON p.productCategories_categoryId = pc.categoryId
                         JOIN inventory i ON p.productId = i.products_productId
                         JOIN suppliers s ON i.suppliers_supplierId = s.supplierId
                         WHERE {searchCondition}
                         ORDER BY p.productId;"
                cmd = New MySqlCommand(query, conn)
                da = New MySqlDataAdapter(cmd)
                ds = New DataSet()
                da.Fill(ds, "Inventory")
                DataGridView1.DataSource = ds.Tables("Inventory")

                ' Set formal column headers
                DataGridView1.Columns(0).HeaderText = "Product ID"
                DataGridView1.Columns(1).HeaderText = "Product Name"
                DataGridView1.Columns(2).HeaderText = "Product Price"
                DataGridView1.Columns(3).HeaderText = "Category ID"
                DataGridView1.Columns(4).HeaderText = "Category"
                DataGridView1.Columns(5).HeaderText = "Inventory ID"
                DataGridView1.Columns(6).HeaderText = "Product Stock"
                DataGridView1.Columns(7).HeaderText = "Supplier ID"
                DataGridView1.Columns(8).HeaderText = "Supplier Name"

                HighlightLowStockItems()
                If conn.State = ConnectionState.Open Then conn.Close()
            Else
                LoadInventoryData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ' If there's text in the search box, perform search with new criteria
        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            TextBox6_TextChanged(sender, e)
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow numbers, decimal point, backspace, and delete
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        
        ' Allow only one decimal point
        If e.KeyChar = "."c AndAlso DirectCast(sender, TextBox).Text.IndexOf("."c) > -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow only numbers, backspace, and delete
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class
