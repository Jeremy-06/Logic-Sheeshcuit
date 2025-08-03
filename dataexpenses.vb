Imports MySql.Data.MySqlClient

'wait lang 'di pa yan tapos, icommit ko na raw kasi sabi ni Jirimiah Primavera 

Public Class dataexpenses

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    ' Flag to track if DateTimePicker has been modified
    Private isDateFilterActive As Boolean = False

    Private Sub dataexpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set DateTimePicker to show no date (null value)
        DateTimePicker2.Value = DateTimePicker2.MinDate
        ' Set max date to current date
        DateTimePicker1.MaxDate = DateTime.Today
        DateTimePicker2.MaxDate = DateTime.Today
        LoadInventoryData()
        LoadCategories()
        LoadSearchComboBox()
        ClearFields()
        TextBox1.ReadOnly = True ' Make ID field read-only since it's auto-increment
    End Sub

    Private Sub LoadInventoryData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            Dim query As String = ""

            ' Check if date filter is active
            If isDateFilterActive AndAlso DateTimePicker2.Value <> DateTimePicker2.MinDate Then
                ' SQL query with date filter
                query = $"SELECT 
                            expensesId, 
                            expenseDate, 
                            expenseDescription, 
                            expenseAmount,
                            expenseCategory
                         FROM expenses
                         WHERE DATE(expenseDate) = '{DateTimePicker2.Value:yyyy-MM-dd}'
                         ORDER BY expensesId;"
            Else
                ' SQL query to show all expenses by default
                query = $"SELECT 
                            expensesId, 
                            expenseDate, 
                            expenseDescription, 
                            expenseAmount,
                            expenseCategory
                         FROM expenses
                         ORDER BY expensesId;"
            End If

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Expenses")
            DataGridView1.DataSource = ds.Tables("Expenses")

            ' Set formal column headers
            DataGridView1.Columns(0).HeaderText = "Expense ID"
            DataGridView1.Columns(1).HeaderText = "Date"
            DataGridView1.Columns(2).HeaderText = "Description"
            DataGridView1.Columns(3).HeaderText = "Amount"
            DataGridView1.Columns(4).HeaderText = "Category"

            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadCategories()
        Try
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Office Supplies")
            ComboBox1.Items.Add("Utilities")
            ComboBox1.Items.Add("Inventory")
            ComboBox1.Items.Add("Travel")
            ComboBox1.Items.Add("Marketing")
            ComboBox1.Items.Add("Maintenance")
            ComboBox1.Items.Add("Other")
        Catch ex As Exception
            MessageBox.Show("Error loading categories: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadSearchComboBox()
        Try
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("Expense ID")
            ComboBox2.Items.Add("Category")
            ComboBox2.Items.Add("Month")
            ComboBox2.Items.Add("Year")
            ComboBox2.SelectedIndex = 0 ' Default to first item
        Catch ex As Exception
            MessageBox.Show("Error loading search combo box: " & ex.Message)
        End Try
    End Sub

    Private Sub ClearFields()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = 0
        ' Don't reset DateTimePicker here as it's used for filtering
        ' DateTimePicker1.Value = DateTime.Now
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBox1.Text = row.Cells("expensesId").Value.ToString()
            TextBox2.Text = row.Cells("expenseDescription").Value.ToString()
            TextBox3.Text = row.Cells("expenseAmount").Value.ToString()
            DateTimePicker1.Value = Convert.ToDateTime(row.Cells("expenseDate").Value)

            ' Set category in combo box
            Dim category As String = row.Cells("expenseCategory").Value.ToString()
            ComboBox1.SelectedItem = category
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' INSERT operation
        Try
            If String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Please fill in all required fields (Description, Amount, and Category)")
                Return
            End If

            Dim amount As Decimal
            If Not Decimal.TryParse(TextBox3.Text, amount) Then
                MessageBox.Show("Please enter a valid amount")
                Return
            End If

            ' Show confirmation with summary
            Dim expenseId As String = TextBox1.Text.Trim()
            Dim description As String = TextBox2.Text
            Dim amountStr As String = TextBox3.Text
            Dim category As String = ComboBox1.SelectedItem.ToString()
            Dim dateStr As String = DateTimePicker1.Value.ToString("MM/dd/yyyy")

            Dim summary As String = $"INSERT NEW EXPENSE:" & vbCrLf & vbCrLf &
                                   $"Expense ID: {(If(String.IsNullOrWhiteSpace(expenseId), "Auto-increment", expenseId))}" & vbCrLf &
                                   $"Description: {description}" & vbCrLf &
                                   $"Amount: {amountStr}" & vbCrLf &
                                   $"Category: {category}" & vbCrLf &
                                   $"Date: {dateStr}" & vbCrLf & vbCrLf &
                                   $"Do you want to insert this expense?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then Exit Sub

            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Check if expense ID is provided and if it already exists
            If Not String.IsNullOrWhiteSpace(expenseId) Then
                query = $"SELECT COUNT(*) FROM expenses WHERE expensesId = {expenseId}"
                cmd = New MySqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If count > 0 Then
                    MessageBox.Show("Expense ID already exists. Please leave Expense ID blank for auto-increment or use a different ID.")
                    If conn.State = ConnectionState.Open Then conn.Close()
                    Exit Sub
                End If
            End If

            ' Insert with or without expense ID
            If String.IsNullOrWhiteSpace(expenseId) Then
                ' Auto increment - don't specify expensesId
                query = $"INSERT INTO expenses (expenseDate, expenseDescription, expenseAmount, expenseCategory) VALUES ('{DateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{TextBox2.Text}', {amount}, '{ComboBox1.SelectedItem.ToString()}')"
            Else
                ' Use specified expense ID
                query = $"INSERT INTO expenses (expensesId, expenseDate, expenseDescription, expenseAmount, expenseCategory) VALUES ({expenseId}, '{DateTimePicker1.Value.ToString("yyyy-MM-dd")}', '{TextBox2.Text}', {amount}, '{ComboBox1.SelectedItem.ToString()}')"
            End If

            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the inserted expense ID
            Dim newExpenseId As Integer
            If String.IsNullOrWhiteSpace(expenseId) Then
                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                newExpenseId = Convert.ToInt32(cmd.ExecuteScalar())
            Else
                newExpenseId = Convert.ToInt32(expenseId)
            End If

            If conn.State = ConnectionState.Open Then conn.Close()

            MessageBox.Show($"Expense added successfully!" & vbCrLf & vbCrLf &
                          $"Expense ID: {newExpenseId}" & vbCrLf &
                          $"Description: {description}" & vbCrLf &
                          $"Amount: {amountStr}" & vbCrLf &
                          $"Category: {category}" & vbCrLf &
                          $"Date: {dateStr}", "Insert Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error inserting expense: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' UPDATE operation
        Try
            If String.IsNullOrEmpty(TextBox1.Text) Then
                MessageBox.Show("Please select an expense to update")
                Return
            End If

            If String.IsNullOrEmpty(TextBox2.Text) Or String.IsNullOrEmpty(TextBox3.Text) Or ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Please fill in all required fields (Description, Amount, and Category)")
                Return
            End If

            Dim amount As Decimal
            If Not Decimal.TryParse(TextBox3.Text, amount) Then
                MessageBox.Show("Please enter a valid amount")
                Return
            End If

            conn.Open()

            ' Check if there are any changes by comparing with current database values
            query = $"SELECT expenseDate, expenseDescription, expenseAmount, expenseCategory FROM expenses WHERE expensesId = {TextBox1.Text}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            Dim hasChanges As Boolean = False
            Dim changesList As New List(Of String)
            If reader.Read() Then
                Dim currentDate As String = Convert.ToDateTime(reader("expenseDate")).ToString("MM/dd/yyyy")
                Dim currentDescription As String = reader("expenseDescription").ToString()
                Dim currentAmount As String = reader("expenseAmount").ToString()
                Dim currentCategory As String = reader("expenseCategory").ToString()

                ' Check if any field has changed and build changes list
                If currentDescription <> TextBox2.Text Then
                    hasChanges = True
                    changesList.Add($"Description: {currentDescription} → {TextBox2.Text}")
                End If
                If currentAmount <> TextBox3.Text Then
                    hasChanges = True
                    changesList.Add($"Amount: {currentAmount} → {TextBox3.Text}")
                End If
                If currentCategory <> ComboBox1.SelectedItem.ToString() Then
                    hasChanges = True
                    changesList.Add($"Category: {currentCategory} → {ComboBox1.SelectedItem.ToString()}")
                End If
                If currentDate <> DateTimePicker1.Value.ToString("MM/dd/yyyy") Then
                    hasChanges = True
                    changesList.Add($"Date: {currentDate} → {DateTimePicker1.Value.ToString("MM/dd/yyyy")}")
                End If
            End If
            reader.Close()

            If Not hasChanges Then
                MessageBox.Show("No changes detected. Expense information is already up to date.")
                If conn.State = ConnectionState.Open Then conn.Close()
                Exit Sub
            End If

            ' Show confirmation with changes summary
            Dim summary As String = $"UPDATE EXPENSE:" & vbCrLf & vbCrLf &
                                   $"Expense ID: {TextBox1.Text}" & vbCrLf & vbCrLf &
                                   $"CHANGES TO BE MADE:" & vbCrLf
            For Each change In changesList
                summary &= change & vbCrLf
            Next
            summary &= vbCrLf & "Do you want to update this expense?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                If conn.State = ConnectionState.Open Then conn.Close()
                Exit Sub
            End If

            query = $"UPDATE expenses SET expenseDate = '{DateTimePicker1.Value.ToString("yyyy-MM-dd")}', expenseDescription = '{TextBox2.Text}', expenseAmount = {amount}, expenseCategory = '{ComboBox1.SelectedItem.ToString()}' WHERE expensesId = {TextBox1.Text}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()

            ' Show success message with summary of changes
            Dim successMsg As String = $"Expense updated successfully!" & vbCrLf & vbCrLf &
                                      $"Expense ID: {TextBox1.Text}" & vbCrLf & vbCrLf &
                                      $"CHANGES MADE:" & vbCrLf
            For Each change In changesList
                successMsg &= change & vbCrLf
            Next

            MessageBox.Show(successMsg, "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error updating expense: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' DELETE operation
        Try
            If String.IsNullOrEmpty(TextBox1.Text) Then
                MessageBox.Show("Please select an expense to delete")
                Return
            End If

            Dim expenseId As String = TextBox1.Text
            Dim description As String = TextBox2.Text
            Dim amount As String = TextBox3.Text
            Dim category As String = ComboBox1.SelectedItem.ToString()
            Dim dateStr As String = DateTimePicker1.Value.ToString("MM/dd/yyyy")

            ' Show confirmation with expense details
            Dim summary As String = $"DELETE EXPENSE:" & vbCrLf & vbCrLf &
                                   $"Expense ID: {expenseId}" & vbCrLf &
                                   $"Description: {description}" & vbCrLf &
                                   $"Amount: {amount}" & vbCrLf &
                                   $"Category: {category}" & vbCrLf &
                                   $"Date: {dateStr}" & vbCrLf & vbCrLf &
                                   $"WARNING: This action cannot be undone!" & vbCrLf &
                                   $"Do you want to delete this expense?"

            Dim result As DialogResult = MessageBox.Show(summary, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then Exit Sub

            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"DELETE FROM expenses WHERE expensesId = {TextBox1.Text}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            If conn.State = ConnectionState.Open Then conn.Close()

            MessageBox.Show($"Expense deleted successfully!" & vbCrLf & vbCrLf &
                          $"DELETED EXPENSE DETAILS:" & vbCrLf &
                          $"Expense ID: {expenseId}" & vbCrLf &
                          $"Description: {description}" & vbCrLf &
                          $"Amount: {amount}" & vbCrLf &
                          $"Category: {category}" & vbCrLf &
                          $"Date: {dateStr}", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()
            RefreshData()
        Catch ex As Exception
            MessageBox.Show("Error deleting expense: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' SEARCH FUNCTION
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Search button functionality (same as text changed)
        TextBox4_TextChanged(sender, e)
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        ' Search functionality based on selected combo box item - works independently of date filter
        Try
            If Not String.IsNullOrEmpty(TextBox4.Text) Then
                If conn.State <> ConnectionState.Open Then conn.Open()

                Dim searchType As String = If(ComboBox2.SelectedItem IsNot Nothing, ComboBox2.SelectedItem.ToString(), "ID")
                Dim searchCondition As String = ""

                Select Case searchType
                    Case "Expense ID"
                        ' Only allow numbers for ID
                        If IsNumeric(TextBox4.Text) Then
                            searchCondition = $"expensesId = {TextBox4.Text}"
                        Else
                            MessageBox.Show("ID must be a number only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBox4.Text = ""
                            If conn.State = ConnectionState.Open Then conn.Close()
                            Return
                        End If
                    Case "Category"
                        searchCondition = $"expenseCategory LIKE '%{TextBox4.Text}%'"
                    Case "Month"
                        ' Allow both numbers (1-12) and month names
                        Dim monthNumber As Integer = 0
                        If IsNumeric(TextBox4.Text) Then
                            ' If it's a number, check if it's valid (1-12)
                            monthNumber = Convert.ToInt32(TextBox4.Text)
                            If monthNumber >= 1 AndAlso monthNumber <= 12 Then
                                searchCondition = $"MONTH(expenseDate) = {monthNumber}"
                            Else
                                MessageBox.Show("Month number must be between 1 and 12.", "Invalid Month", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBox4.Text = ""
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        Else
                            ' If it's text, try to convert month name to number
                            monthNumber = GetMonthNumber(TextBox4.Text)
                            If monthNumber > 0 Then
                                searchCondition = $"MONTH(expenseDate) = {monthNumber}"
                            Else
                                ' Don't show error message for partial month names, just don't search
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        End If
                    Case "Year"
                        ' Only allow numbers for Year and only search when exactly 4 digits
                        If IsNumeric(TextBox4.Text) Then
                            If TextBox4.Text.Length = 4 Then
                                searchCondition = $"YEAR(expenseDate) = {TextBox4.Text}"
                            ElseIf TextBox4.Text.Length > 4 Then
                                MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBox4.Text = ""
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            Else
                                ' Don't search if less than 4 digits, just return
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        Else
                            MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBox4.Text = ""
                            If conn.State = ConnectionState.Open Then conn.Close()
                            Return
                        End If
                    Case Else
                        searchCondition = $"expensesId = {TextBox4.Text}"
                End Select

                ' Build the WHERE clause considering both search condition and date filter
                Dim whereClause As String = searchCondition
                If isDateFilterActive AndAlso DateTimePicker2.Value <> DateTimePicker2.MinDate Then
                    whereClause = $"{searchCondition} AND DATE(expenseDate) = '{DateTimePicker2.Value:yyyy-MM-dd}'"
                End If

                query = $"SELECT 
                            expensesId, 
                            expenseDate, 
                            expenseDescription, 
                            expenseAmount,
                            expenseCategory
                         FROM expenses
                         WHERE {whereClause}
                         ORDER BY expensesId;"
                cmd = New MySqlCommand(query, conn)
                da = New MySqlDataAdapter(cmd)
                ds = New DataSet()
                da.Fill(ds, "Expenses")
                DataGridView1.DataSource = ds.Tables("Expenses")

                ' Set formal column headers
                DataGridView1.Columns(0).HeaderText = "Expense ID"
                DataGridView1.Columns(1).HeaderText = "Date"
                DataGridView1.Columns(2).HeaderText = "Description"
                DataGridView1.Columns(3).HeaderText = "Amount"
                DataGridView1.Columns(4).HeaderText = "Category"

                If conn.State = ConnectionState.Open Then conn.Close()
            Else
                LoadInventoryData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " & ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ' If there's text in the search box, perform search with new criteria
        If Not String.IsNullOrEmpty(TextBox4.Text) Then
            TextBox4_TextChanged(sender, e)
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Reset to initial state
        isDateFilterActive = False
        DateTimePicker2.Value = DateTimePicker2.MinDate
        ClearFields()
        RefreshData()
    End Sub

    ' DateTimePicker value changed event
    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        ' Set flag to indicate date filter is active
        isDateFilterActive = True
        LoadInventoryData()
        TextBox4.Clear()
        ClearFields()
    End Sub

    Private Function GetMonthNumber(monthName As String) As Integer
        Dim monthLower As String = monthName.ToLower().Trim()

        Select Case monthLower
            Case "january", "jan"
                Return 1
            Case "february", "feb"
                Return 2
            Case "march", "mar"
                Return 3
            Case "april", "apr"
                Return 4
            Case "may"
                Return 5
            Case "june", "jun"
                Return 6
            Case "july", "jul"
                Return 7
            Case "august", "aug"
                Return 8
            Case "september", "sept", "sep"
                Return 9
            Case "october", "oct"
                Return 10
            Case "november", "nov"
                Return 11
            Case "december", "dec"
                Return 12
        End Select

        If monthLower.Length >= 3 Then
            Select Case monthLower
                Case "jan", "janu", "janua", "januar"
                    Return 1
                Case "feb", "febr", "febru", "februa", "februar"
                    Return 2
                Case "mar", "marc"
                    Return 3
                Case "apr", "apri"
                    Return 4
                Case "may"
                    Return 5
                Case "jun", "june"
                    Return 6
                Case "jul", "july"
                    Return 7
                Case "aug", "augu", "augus"
                    Return 8
                Case "sep", "sept", "septe", "septem", "septemb", "septembe"
                    Return 9
                Case "oct", "octo", "octob", "octobe"
                    Return 10
                Case "nov", "nove", "novem", "novemb", "novembe"
                    Return 11
                Case "dec", "dece", "decem", "decemb", "decembe"
                    Return 12
            End Select
        End If

        Return 0
    End Function

    Private Sub expeensesReport_btn_Click(sender As Object, e As EventArgs) Handles expeensesReport_btn.Click
        expensesreport.Show()
    End Sub
End Class
