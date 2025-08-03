Imports MySql.Data.MySqlClient

Public Class datacart
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datacart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryData()
        LoadComboBoxItems()
    End Sub

    Private Sub LoadInventoryData()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
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
            SetFormalHeaders()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub LoadComboBoxItems()
        ' Set the search categories for ComboBox1
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Product Name")
        ComboBox1.Items.Add("Supplier")
        ComboBox1.Items.Add("Product Category")
        ComboBox1.Items.Add("Product ID")
    End Sub

    Private Sub SetFormalHeaders()
        ' Set formal column headers for DataGridView
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("cartId").HeaderText = "Cart ID"
            DataGridView1.Columns("customerFullName").HeaderText = "Customer Name"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("category").HeaderText = "Product Category"
            DataGridView1.Columns("productPrice").HeaderText = "Unit Price"
            DataGridView1.Columns("productQty").HeaderText = "Quantity"
            DataGridView1.Columns("supplierName").HeaderText = "Supplier"
        End If
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'DELETE SELECTED
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow = DataGridView1.SelectedRows(0)
            Dim cartId = selectedRow.Cells("cartId").Value

            Dim result = MessageBox.Show("Are you sure you want to delete this cart item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                query = $"DELETE FROM cart WHERE cartId = {cartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Cart item deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadInventoryData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting cart item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        'DELETE ALL DISPLAYED DATA
        Try
            If DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("No cart items to delete.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim result = MessageBox.Show($"Are you sure you want to delete ALL {DataGridView1.Rows.Count} displayed cart items? This action cannot be undone.", "Confirm Delete All", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                ' Get all cart IDs from the displayed DataGridView
                Dim cartIds As New List(Of String)
                For Each row As DataGridViewRow In DataGridView1.Rows
                    If Not row.IsNewRow Then
                        cartIds.Add(row.Cells("cartId").Value.ToString())
                    End If
                Next

                ' Delete only the displayed items
                If cartIds.Count > 0 Then
                    Dim cartIdsString = String.Join(",", cartIds)
                    query = $"DELETE FROM cart WHERE cartId IN ({cartIdsString})"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show($"{cartIds.Count} cart items deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadInventoryData()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting displayed cart items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'SEARCH BUTTON
        PerformSearch()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'AUTO SEARCH
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso ComboBox1.SelectedItem IsNot Nothing Then
            PerformSearch()
        ElseIf String.IsNullOrWhiteSpace(TextBox1.Text) Then
            LoadInventoryData()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'AUTO SEARCH WHEN CATEGORY CHANGES
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) AndAlso ComboBox1.SelectedItem IsNot Nothing Then
            PerformSearch()
        End If
    End Sub

    Private Sub PerformSearch()
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                LoadInventoryData()
                Return
            End If

            If ComboBox1.SelectedItem Is Nothing Then
                Return
            End If

            Dim searchTerm = TextBox1.Text.Trim()
            Dim searchCategory = ComboBox1.SelectedItem.ToString()

            ' Validate Product ID input - numbers only
            If searchCategory = "Product ID" Then
                If Not IsNumeric(searchTerm) Then
                    MessageBox.Show("Product ID must be a number only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Select Case searchCategory
                Case "Product Name"
                    query = $"SELECT 
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
                             WHERE p.productName LIKE '%{searchTerm}%'
                             ORDER BY p.productId"
                Case "Supplier"
                    query = $"SELECT 
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
                             WHERE s.supplierName LIKE '%{searchTerm}%'
                             ORDER BY p.productId"
                Case "Product Category"
                    query = $"SELECT 
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
                             WHERE pc.category LIKE '%{searchTerm}%'
                             ORDER BY p.productId"
                Case "Product ID"
                    query = $"SELECT 
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
                             WHERE p.productId = {searchTerm}
                             ORDER BY p.productId"
                Case Else
                    Return
            End Select

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Cart")
            DataGridView1.DataSource = ds.Tables("Cart")
            SetFormalHeaders()

        Catch ex As Exception
            MessageBox.Show("Error performing search: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'REFRESH
        TextBox1.Clear()
        ComboBox1.SelectedIndex = -1
        LoadInventoryData()
        MessageBox.Show("Data refreshed successfully.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
