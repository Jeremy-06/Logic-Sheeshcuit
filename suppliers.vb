Imports MySql.Data.MySqlClient

Public Class suppliers
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub suppliers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSuppliersData()
        ClearFields()
    End Sub

    Private Sub LoadSuppliersData()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = "SELECT supplierId, supplierName FROM suppliers ORDER BY supplierId"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Suppliers")
            DataGridView1.DataSource = ds.Tables("Suppliers")
            SetFormalHeaders()
        Catch ex As Exception
            MessageBox.Show("Error loading suppliers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub SetFormalHeaders()
        ' Set formal column headers for DataGridView
        If DataGridView1.Columns.Count > 0 Then
            DataGridView1.Columns("supplierId").HeaderText = "Supplier ID"
            DataGridView1.Columns("supplierName").HeaderText = "Supplier Name"
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
                MessageBox.Show("Please enter a supplier name.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                query = $"SELECT COUNT(*) FROM suppliers WHERE supplierId = {enteredId}"
                cmd = New MySqlCommand(query, conn)
                Dim idCount = Convert.ToInt32(cmd.ExecuteScalar())
                If idCount > 0 Then
                    MessageBox.Show($"Supplier ID {enteredId} already exists. Please clear the ID field or use a different ID.", "ID Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            Dim supplierName As String = TextBox2.Text.Trim()

            ' Show confirmation with summary
            Dim summary As String = $"INSERT NEW SUPPLIER:" & vbCrLf & vbCrLf &
                                   $"Supplier Name: {supplierName}" & vbCrLf & vbCrLf &
                                   $"Do you want to insert this supplier?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Return

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if supplier name already exists
            query = $"SELECT COUNT(*) FROM suppliers WHERE supplierName = '{supplierName}'"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Supplier name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Insert new supplier
            query = $"INSERT INTO suppliers (supplierName) VALUES ('{supplierName}')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the inserted supplier ID
            query = "SELECT LAST_INSERT_ID();"
            cmd = New MySqlCommand(query, conn)
            Dim newSupplierId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            MessageBox.Show($"Supplier inserted successfully!" & vbCrLf & vbCrLf &
                          $"Supplier ID: {newSupplierId}" & vbCrLf &
                          $"Supplier Name: {supplierName}", "Insert Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSuppliersData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error adding supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show("Please select a supplier to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If String.IsNullOrWhiteSpace(TextBox2.Text.Trim()) Then
                MessageBox.Show("Please enter a supplier name.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
                Return
            End If

            Dim supplierId As String = TextBox1.Text
            Dim supplierName As String = TextBox2.Text.Trim()

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if supplier name already exists (excluding current supplier)
            query = $"SELECT COUNT(*) FROM suppliers WHERE supplierName = '{supplierName}' AND supplierId != {supplierId}"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Supplier name already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Check if there are any changes by comparing with current database values
            query = $"SELECT supplierName FROM suppliers WHERE supplierId = {supplierId}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim hasChanges As Boolean = False
            Dim changesList As New List(Of String)
            If reader.Read() Then
                Dim currentSupplierName As String = reader("supplierName").ToString()

                ' Check if any field has changed and build changes list
                If currentSupplierName <> supplierName Then
                    hasChanges = True
                    changesList.Add($"Supplier Name: {currentSupplierName} → {supplierName}")
                End If
            End If
            reader.Close()

            If Not hasChanges Then
                MessageBox.Show("No changes detected. Supplier information is already up to date.")
                Return
            End If

            ' Show confirmation with changes summary
            Dim summary As String = $"UPDATE SUPPLIER:" & vbCrLf & vbCrLf &
                                   $"Supplier ID: {supplierId}" & vbCrLf & vbCrLf &
                                   $"CHANGES TO BE MADE:" & vbCrLf
            For Each change In changesList
                summary &= change & vbCrLf
            Next
            summary &= vbCrLf & "Do you want to update this supplier?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Return

            ' Update supplier
            query = $"UPDATE suppliers SET supplierName = '{supplierName}' WHERE supplierId = {supplierId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Show success message with summary of changes
            Dim successMsg As String = $"Supplier updated successfully!" & vbCrLf & vbCrLf &
                                      $"Supplier ID: {supplierId}" & vbCrLf & vbCrLf &
                                      $"CHANGES MADE:" & vbCrLf
            For Each change In changesList
                successMsg &= change & vbCrLf
            Next

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSuppliersData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error updating supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show("Please select a supplier to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim supplierId As String = TextBox1.Text
            Dim supplierName As String = TextBox2.Text

            ' Show confirmation with supplier details
            Dim summary As String = $"DELETE SUPPLIER:" & vbCrLf & vbCrLf &
                                   $"Supplier ID: {supplierId}" & vbCrLf &
                                   $"Supplier Name: {supplierName}" & vbCrLf & vbCrLf &
                                   $"WARNING: This action cannot be undone!" & vbCrLf &
                                   $"Do you want to delete this supplier?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Return

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if supplier is being used in inventory
            query = $"SELECT COUNT(*) FROM inventory WHERE suppliers_supplierId = {supplierId}"
            cmd = New MySqlCommand(query, conn)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            If count > 0 Then
                MessageBox.Show("Cannot delete supplier. It is being used in inventory.", "Delete Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Delete supplier
            query = $"DELETE FROM suppliers WHERE supplierId = {supplierId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"Supplier deleted successfully!" & vbCrLf & vbCrLf &
                          $"DELETED SUPPLIER DETAILS:" & vbCrLf &
                          $"Supplier ID: {supplierId}" & vbCrLf &
                          $"Supplier Name: {supplierName}", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadSuppliersData()
            ClearFields()
        Catch ex As Exception
            MessageBox.Show("Error deleting supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            LoadSuppliersData()
        End If
    End Sub

    Private Sub PerformSearch()
        Try
            If String.IsNullOrWhiteSpace(TextBox3.Text) Then
                LoadSuppliersData()
                Return
            End If

            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            Dim searchTerm = TextBox3.Text.Trim()
            query = $"SELECT supplierId, supplierName FROM suppliers WHERE supplierName LIKE '%{searchTerm}%' ORDER BY supplierId"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Suppliers")
            DataGridView1.DataSource = ds.Tables("Suppliers")
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
        LoadSuppliersData()
        ClearFields()
        MessageBox.Show("Data refreshed successfully.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Load selected supplier data to textboxes
        Try
            If e.RowIndex >= 0 Then
                Dim row = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells("supplierId").Value.ToString()
                TextBox2.Text = row.Cells("supplierName").Value.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading supplier data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        'Allow only letters, spaces, and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsWhiteSpace(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class