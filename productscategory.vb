Imports MySql.Data.MySqlClient

Public Class productscategory
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub productscategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCategoriesData()
        ClearFields()
    End Sub

    Private Sub LoadCategoriesData()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = "SELECT categoryId, category FROM productCategories ORDER BY categoryId"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Categories")
            DataGridView1.DataSource = ds.Tables("Categories")
            SetFormalHeaders()
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub SetFormalHeaders()
        ' Set formal column headers for DataGridView
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("categoryId").HeaderText = "Category ID"
            DataGridView1.Columns("category").HeaderText = "Category Name"
        End If
    End Sub

    Private Sub ClearFields()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox2.Focus()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'INSERT
        Try
            If String.IsNullOrWhiteSpace(TextBox2.Text.Trim()) Then
                MessageBox.Show("Please enter a category name.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
                Return
            End If

            ' Check if ID is manually entered and if it exists
            If Not String.IsNullOrWhiteSpace(TextBox1.Text.Trim()) Then
                Dim enteredId As String = TextBox1.Text.Trim()
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                
                ' Check if the entered ID already exists
                query = $"SELECT COUNT(*) FROM productCategories WHERE categoryId = {enteredId}"
                cmd = New MySqlCommand(query, conn)
                Dim idCount = Convert.ToInt32(cmd.ExecuteScalar())
                If idCount > 0 Then
                    MessageBox.Show($"Category ID {enteredId} already exists. Please clear the ID field or use a different ID.", "ID Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            Dim categoryName As String = TextBox2.Text.Trim()

            ' Show confirmation with summary
            Dim summary As String = $"INSERT NEW CATEGORY:" & vbCrLf & vbCrLf &
                                   $"Category Name: {categoryName}" & vbCrLf & vbCrLf &
                                   $"Do you want to insert this category?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Return

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if category name already exists
            query = $"SELECT COUNT(*) FROM productCategories WHERE category = '{categoryName}'"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Category name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Insert new category
            query = $"INSERT INTO productCategories (category) VALUES ('{categoryName}')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the inserted category ID
            query = "SELECT LAST_INSERT_ID();"
            cmd = New MySqlCommand(query, conn)
            Dim newCategoryId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            MessageBox.Show($"Category inserted successfully!" & vbCrLf & vbCrLf &
                          $"Category ID: {newCategoryId}" & vbCrLf &
                          $"Category Name: {categoryName}", "Insert Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCategoriesData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding category: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'UPDATE
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Please select a category to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrWhiteSpace(TextBox2.Text.Trim()) Then
                MessageBox.Show("Please enter a category name.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
                Return
            End If

            Dim categoryId As String = TextBox1.Text
            Dim categoryName As String = TextBox2.Text.Trim()

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if category name already exists (excluding current category)
            query = $"SELECT COUNT(*) FROM productCategories WHERE category = '{categoryName}' AND categoryId != {categoryId}"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Category name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Check if there are any changes by comparing with current database values
            query = $"SELECT category FROM productCategories WHERE categoryId = {categoryId}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim hasChanges As Boolean = False
            Dim changesList As New List(Of String)
            If reader.Read() Then
                Dim currentCategoryName As String = reader("category").ToString()

                ' Check if any field has changed and build changes list
                If currentCategoryName <> categoryName Then
                    hasChanges = True
                    changesList.Add($"Category Name: {currentCategoryName} → {categoryName}")
                End If
            End If
            reader.Close()

            If Not hasChanges Then
                MessageBox.Show("No changes detected. Category information is already up to date.")
                Return
            End If

            ' Show confirmation with changes summary
            Dim summary As String = $"UPDATE CATEGORY:" & vbCrLf & vbCrLf &
                                   $"Category ID: {categoryId}" & vbCrLf & vbCrLf &
                                   $"CHANGES TO BE MADE:" & vbCrLf
            For Each change In changesList
                summary &= change & vbCrLf
            Next
            summary &= vbCrLf & "Do you want to update this category?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Return

            ' Update category
            query = $"UPDATE productCategories SET category = '{categoryName}' WHERE categoryId = {categoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Show success message with summary of changes
            Dim successMsg As String = $"Category updated successfully!" & vbCrLf & vbCrLf &
                                      $"Category ID: {categoryId}" & vbCrLf & vbCrLf &
                                      $"CHANGES MADE:" & vbCrLf
            For Each change In changesList
                successMsg &= change & vbCrLf
            Next

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCategoriesData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating category: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'DELETE
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                MessageBox.Show("Please select a category to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim categoryId As String = TextBox1.Text
            Dim categoryName As String = TextBox2.Text

            ' Show confirmation with category details
            Dim summary As String = $"DELETE CATEGORY:" & vbCrLf & vbCrLf &
                                   $"Category ID: {categoryId}" & vbCrLf &
                                   $"Category Name: {categoryName}" & vbCrLf & vbCrLf &
                                   $"WARNING: This action cannot be undone!" & vbCrLf &
                                   $"Do you want to delete this category?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if category is being used in products
            query = $"SELECT COUNT(*) FROM products WHERE productCategories_categoryId = {categoryId}"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Cannot delete category. It is being used in products.", "Delete Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Delete category
            query = $"DELETE FROM productCategories WHERE categoryId = {categoryId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"Category deleted successfully!" & vbCrLf & vbCrLf &
                          $"DELETED CATEGORY DETAILS:" & vbCrLf &
                          $"Category ID: {categoryId}" & vbCrLf &
                          $"Category Name: {categoryName}", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadCategoriesData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error deleting category: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'SEARCH
        PerformSearch()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'AUTO SEARCH
        If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then
            PerformSearch()
        Else
            LoadCategoriesData()
        End If
    End Sub

    Private Sub PerformSearch()
        Try
            If String.IsNullOrWhiteSpace(TextBox3.Text) Then
                LoadCategoriesData()
                Return
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim searchTerm = TextBox3.Text.Trim()
            query = $"SELECT categoryId, category FROM productCategories WHERE category LIKE '%{searchTerm}%' ORDER BY categoryId"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Categories")
            DataGridView1.DataSource = ds.Tables("Categories")
            SetFormalHeaders()

        Catch ex As Exception
            MessageBox.Show("Error performing search: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        'REFRESH
        TextBox3.Clear()
        LoadCategoriesData()
        ClearFields()
        MessageBox.Show("Data refreshed successfully.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Load selected category data to textboxes
        Try
            If e.RowIndex >= 0 Then
                Dim row = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells("categoryId").Value.ToString()
                TextBox2.Text = row.Cells("category").Value.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading category data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        'Allow only letters, spaces, and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class