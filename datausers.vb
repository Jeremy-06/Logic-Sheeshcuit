Imports MySql.Data.MySqlClient

Public Class datausers

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datausers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set default selection to "Users"
        ComboBox1.SelectedIndex = 0
        LoadUserData()
    End Sub

    Private Sub LoadUserData()
        Try
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            If ComboBox1.Text = "Customers" Then
                query = "SELECT 
                            u.userId,
                            u.username,
                            u.userRole,
                            c.customerId,
                            c.customerFname,
                            c.customerLname,
                            c.customerAddress,
                            c.customerPhone
                         FROM users u
                         LEFT JOIN customers c ON u.userId = c.userId
                         WHERE u.userRole = 'customer'
                         ORDER BY u.userId;"
            ElseIf ComboBox1.Text = "Admins" Then
                query = "SELECT 
                              u.userId,
                              u.username,
                              u.userRole,
                              a.adminId,
                              a.adminRole,
                              a.fullName,
                              a.email,
                              a.phone,
                              a.isActive
                           FROM users u
                           LEFT JOIN admin_users a ON u.userId = a.userId
                           WHERE u.userRole IN ('admin', 'inventory_manager', 'order_manager', 'financial_auditor')
                           ORDER BY u.userId;"
            ElseIf ComboBox1.Text = "Users" Then
                ' Query for all users (basic user information)
                query = "SELECT 
                             u.userId,
                             u.username,
                             u.userRole
                          FROM users u
                          ORDER BY u.userId;"
            Else
                ' Default query for all users when nothing is selected
                query = "SELECT 
                            u.userId,
                            u.username,
                            u.userRole
                         FROM users u
                         ORDER BY u.userId;"
            End If

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Users")
            DataGridView1.DataSource = ds.Tables("Users")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub RefreshData()
        LoadUserData()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Update ComboBox2 items based on selected user type
        UpdateSearchOptions()
        LoadUserData()
    End Sub

    Private Sub UpdateSearchOptions()
        ComboBox2.Items.Clear()

        If ComboBox1.Text = "Users" Then
            ComboBox2.Items.AddRange(New Object() {"Username", "User ID"})
        ElseIf ComboBox1.Text = "Customers" Then
            ComboBox2.Items.AddRange(New Object() {"Customer Name", "Customer ID", "Address"})
        ElseIf ComboBox1.Text = "Admins" Then
            ComboBox2.Items.AddRange(New Object() {"Admin ID", "Admin Name", "Role"})
        End If

        ' Set default selection
        If ComboBox2.Items.Count > 0 Then
            ComboBox2.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SearchUsers()
    End Sub

    Private Sub SearchUsers()
        Try
            If String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
                LoadUserData()
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            Dim searchTerm As String = TextBox1.Text.Trim()

            If ComboBox1.Text = "Users" Then
                If ComboBox2.Text = "Username" Then
                    query = "SELECT 
                                 u.userId,
                                 u.username,
                                 u.userRole
                              FROM users u
                              WHERE u.username LIKE '%" & searchTerm & "%'
                              ORDER BY u.userId;"
                ElseIf ComboBox2.Text = "User ID" Then
                    query = "SELECT 
                                 u.userId,
                                 u.username,
                                 u.userRole
                              FROM users u
                              WHERE u.userId LIKE '%" & searchTerm & "%'
                              ORDER BY u.userId;"
                End If
            ElseIf ComboBox1.Text = "Customers" Then
                If ComboBox2.Text = "Customer Name" Then
                    query = "SELECT 
                                u.userId,
                                u.username,
                                u.userRole,
                                c.customerId,
                                c.customerFname,
                                c.customerLname,
                                c.customerAddress,
                                c.customerPhone
                             FROM users u
                             LEFT JOIN customers c ON u.userId = c.userId
                             WHERE u.userRole = 'customer' 
                             AND (c.customerFname LIKE '%" & searchTerm & "%' OR c.customerLname LIKE '%" & searchTerm & "%')
                             ORDER BY u.userId;"
                ElseIf ComboBox2.Text = "Customer ID" Then
                    query = "SELECT 
                                u.userId,
                                u.username,
                                u.userRole,
                                c.customerId,
                                c.customerFname,
                                c.customerLname,
                                c.customerAddress,
                                c.customerPhone
                             FROM users u
                             LEFT JOIN customers c ON u.userId = c.userId
                             WHERE u.userRole = 'customer' 
                             AND c.customerId LIKE '%" & searchTerm & "%'
                             ORDER BY u.userId;"
                ElseIf ComboBox2.Text = "Address" Then
                    query = "SELECT 
                                u.userId,
                                u.username,
                                u.userRole,
                                c.customerId,
                                c.customerFname,
                                c.customerLname,
                                c.customerAddress,
                                c.customerPhone
                             FROM users u
                             LEFT JOIN customers c ON u.userId = c.userId
                             WHERE u.userRole = 'customer' 
                             AND c.customerAddress LIKE '%" & searchTerm & "%'
                             ORDER BY u.userId;"
                End If
            ElseIf ComboBox1.Text = "Admins" Then
                If ComboBox2.Text = "Admin ID" Then
                    query = "SELECT 
                                  u.userId,
                                  u.username,
                                  u.userRole,
                                  a.adminId,
                                  a.adminRole,
                                  a.fullName,
                                  a.email,
                                  a.phone,
                                  a.isActive
                               FROM users u
                               LEFT JOIN admin_users a ON u.userId = a.userId
                               WHERE u.userRole IN ('admin', 'inventory_manager', 'order_manager', 'financial_auditor')
                               AND a.adminId LIKE '%" & searchTerm & "%'
                               ORDER BY u.userId;"
                ElseIf ComboBox2.Text = "Admin Name" Then
                    query = "SELECT 
                                  u.userId,
                                  u.username,
                                  u.userRole,
                                  a.adminId,
                                  a.adminRole,
                                  a.fullName,
                                  a.email,
                                  a.phone,
                                  a.isActive
                               FROM users u
                               LEFT JOIN admin_users a ON u.userId = a.userId
                               WHERE u.userRole IN ('admin', 'inventory_manager', 'order_manager', 'financial_auditor')
                               AND a.fullName LIKE '%" & searchTerm & "%'
                               ORDER BY u.userId;"
                ElseIf ComboBox2.Text = "Role" Then
                    query = "SELECT 
                                  u.userId,
                                  u.username,
                                  u.userRole,
                                  a.adminId,
                                  a.adminRole,
                                  a.fullName,
                                  a.email,
                                  a.phone,
                                  a.isActive
                               FROM users u
                               LEFT JOIN admin_users a ON u.userId = a.userId
                               WHERE u.userRole IN ('admin', 'inventory_manager', 'order_manager', 'financial_auditor')
                               AND a.adminRole LIKE '%" & searchTerm & "%'
                               ORDER BY u.userId;"
                End If
            End If

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Users")
            DataGridView1.DataSource = ds.Tables("Users")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Allow search when Enter key is pressed
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SearchUsers()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        UpdateUserData()
    End Sub

    Private Sub UpdateUserData()
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a row to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim userId As String = selectedRow.Cells("userId").Value.ToString()
            Dim username As String = selectedRow.Cells("username").Value.ToString()

            ' Check if any changes were made by comparing with original data
            Dim hasChanges As Boolean = False

            If ComboBox1.Text = "Users" Then
                ' For Users, check if username or userRole changed
                Dim currentUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim currentUserRole As String = selectedRow.Cells("userRole").Value.ToString()

                ' Get original values from database
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim originalQuery As String = "SELECT username, userRole FROM users WHERE userId = '" & userId & "'"
                Dim originalCmd As New MySqlCommand(originalQuery, conn)
                Dim originalReader As MySqlDataReader = originalCmd.ExecuteReader()

                If originalReader.Read() Then
                    Dim originalUsername As String = originalReader("username").ToString()
                    Dim originalUserRole As String = originalReader("userRole").ToString()

                    If currentUsername <> originalUsername OrElse currentUserRole <> originalUserRole Then
                        hasChanges = True
                    End If
                End If
                originalReader.Close()

            ElseIf ComboBox1.Text = "Customers" Then
                ' For Customers, check if any customer fields changed
                Dim currentUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim currentUserRole As String = selectedRow.Cells("userRole").Value.ToString()
                Dim currentCustomerFname As String = selectedRow.Cells("customerFname").Value.ToString()
                Dim currentCustomerLname As String = selectedRow.Cells("customerLname").Value.ToString()
                Dim currentCustomerAddress As String = selectedRow.Cells("customerAddress").Value.ToString()
                Dim currentCustomerPhone As String = selectedRow.Cells("customerPhone").Value.ToString()
                Dim customerId As String = selectedRow.Cells("customerId").Value.ToString()

                ' Get original values from database
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim originalQuery As String = "SELECT u.username, u.userRole, c.customerFname, c.customerLname, c.customerAddress, c.customerPhone " &
                                            "FROM users u LEFT JOIN customers c ON u.userId = c.userId " &
                                            "WHERE u.userId = '" & userId & "' AND c.customerId = '" & customerId & "'"
                Dim originalCmd As New MySqlCommand(originalQuery, conn)
                Dim originalReader As MySqlDataReader = originalCmd.ExecuteReader()

                If originalReader.Read() Then
                    Dim originalUsername As String = originalReader("username").ToString()
                    Dim originalUserRole As String = originalReader("userRole").ToString()
                    Dim originalCustomerFname As String = originalReader("customerFname").ToString()
                    Dim originalCustomerLname As String = originalReader("customerLname").ToString()
                    Dim originalCustomerAddress As String = originalReader("customerAddress").ToString()
                    Dim originalCustomerPhone As String = originalReader("customerPhone").ToString()

                    If currentUsername <> originalUsername OrElse currentUserRole <> originalUserRole OrElse
                       currentCustomerFname <> originalCustomerFname OrElse currentCustomerLname <> originalCustomerLname OrElse
                       currentCustomerAddress <> originalCustomerAddress OrElse currentCustomerPhone <> originalCustomerPhone Then
                        hasChanges = True
                    End If
                End If
                originalReader.Close()

            ElseIf ComboBox1.Text = "Admins" Then
                ' For Admins, check if any admin fields changed
                Dim currentUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim currentUserRole As String = selectedRow.Cells("userRole").Value.ToString()
                Dim currentAdminRole As String = selectedRow.Cells("adminRole").Value.ToString()
                Dim currentFullName As String = selectedRow.Cells("fullName").Value.ToString()
                Dim currentEmail As String = selectedRow.Cells("email").Value.ToString()
                Dim currentPhone As String = selectedRow.Cells("phone").Value.ToString()
                Dim currentIsActive As String = selectedRow.Cells("isActive").Value.ToString()
                Dim adminId As String = selectedRow.Cells("adminId").Value.ToString()

                ' Get original values from database
                If conn.State <> ConnectionState.Open Then
                    conn.Open()
                End If

                Dim originalQuery As String = "SELECT u.username, u.userRole, a.adminRole, a.fullName, a.email, a.phone, a.isActive " &
                                            "FROM users u LEFT JOIN admin_users a ON u.userId = a.userId " &
                                            "WHERE u.userId = '" & userId & "' AND a.adminId = '" & adminId & "'"
                Dim originalCmd As New MySqlCommand(originalQuery, conn)
                Dim originalReader As MySqlDataReader = originalCmd.ExecuteReader()

                If originalReader.Read() Then
                    Dim originalUsername As String = originalReader("username").ToString()
                    Dim originalUserRole As String = originalReader("userRole").ToString()
                    Dim originalAdminRole As String = originalReader("adminRole").ToString()
                    Dim originalFullName As String = originalReader("fullName").ToString()
                    Dim originalEmail As String = originalReader("email").ToString()
                    Dim originalPhone As String = originalReader("phone").ToString()
                    Dim originalIsActive As String = originalReader("isActive").ToString()

                    If currentUsername <> originalUsername OrElse currentUserRole <> originalUserRole OrElse
                       currentAdminRole <> originalAdminRole OrElse currentFullName <> originalFullName OrElse
                       currentEmail <> originalEmail OrElse currentPhone <> originalPhone OrElse
                       currentIsActive <> originalIsActive Then
                        hasChanges = True
                    End If
                End If
                originalReader.Close()
            End If

            ' If no changes were made, show message and return
            If Not hasChanges Then
                MessageBox.Show("No changes were made to the selected record.", "No Changes", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Create confirmation message based on user type
            Dim confirmationMessage As String = ""
            If ComboBox1.Text = "Users" Then
                confirmationMessage = $"Are you sure you want to update the following user?" & vbCrLf & vbCrLf &
                                    $"User ID: {userId}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Role: {selectedRow.Cells("userRole").Value.ToString()}"
            ElseIf ComboBox1.Text = "Customers" Then
                Dim customerName As String = $"{selectedRow.Cells("customerFname").Value.ToString()} {selectedRow.Cells("customerLname").Value.ToString()}"
                confirmationMessage = $"Are you sure you want to update the following customer?" & vbCrLf & vbCrLf &
                                    $"Customer ID: {selectedRow.Cells("customerId").Value.ToString()}" & vbCrLf &
                                    $"Name: {customerName}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Address: {selectedRow.Cells("customerAddress").Value.ToString()}" & vbCrLf &
                                    $"Phone: {selectedRow.Cells("customerPhone").Value.ToString()}"
            ElseIf ComboBox1.Text = "Admins" Then
                confirmationMessage = $"Are you sure you want to update the following admin?" & vbCrLf & vbCrLf &
                                    $"Admin ID: {selectedRow.Cells("adminId").Value.ToString()}" & vbCrLf &
                                    $"Name: {selectedRow.Cells("fullName").Value.ToString()}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Role: {selectedRow.Cells("adminRole").Value.ToString()}" & vbCrLf &
                                    $"Email: {selectedRow.Cells("email").Value.ToString()}" & vbCrLf &
                                    $"Phone: {selectedRow.Cells("phone").Value.ToString()}" & vbCrLf &
                                    $"Active: {selectedRow.Cells("isActive").Value.ToString()}"
            End If

            Dim result As DialogResult = MessageBox.Show(confirmationMessage, "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            If ComboBox1.Text = "Users" Then
                ' Update basic user information
                Dim updatedUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim updatedUserRole As String = selectedRow.Cells("userRole").Value.ToString()

                query = "UPDATE users SET username = '" & updatedUsername & "', userRole = '" & updatedUserRole & "' WHERE userId = '" & userId & "'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

            ElseIf ComboBox1.Text = "Customers" Then
                ' Update customer information
                Dim updatedUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim updatedUserRole As String = selectedRow.Cells("userRole").Value.ToString()
                Dim customerId As String = selectedRow.Cells("customerId").Value.ToString()
                Dim customerFname As String = selectedRow.Cells("customerFname").Value.ToString()
                Dim customerLname As String = selectedRow.Cells("customerLname").Value.ToString()
                Dim customerAddress As String = selectedRow.Cells("customerAddress").Value.ToString()
                Dim customerPhone As String = selectedRow.Cells("customerPhone").Value.ToString()

                ' Update users table
                query = "UPDATE users SET username = '" & updatedUsername & "', userRole = '" & updatedUserRole & "' WHERE userId = '" & userId & "'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Update customers table
                query = "UPDATE customers SET customerFname = '" & customerFname & "', customerLname = '" & customerLname & "', customerAddress = '" & customerAddress & "', customerPhone = '" & customerPhone & "' WHERE customerId = '" & customerId & "'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

            ElseIf ComboBox1.Text = "Admins" Then
                ' Update admin information
                Dim updatedUsername As String = selectedRow.Cells("username").Value.ToString()
                Dim updatedUserRole As String = selectedRow.Cells("userRole").Value.ToString()
                Dim adminId As String = selectedRow.Cells("adminId").Value.ToString()
                Dim adminRole As String = selectedRow.Cells("adminRole").Value.ToString()
                Dim fullName As String = selectedRow.Cells("fullName").Value.ToString()
                Dim email As String = selectedRow.Cells("email").Value.ToString()
                Dim phone As String = selectedRow.Cells("phone").Value.ToString()
                Dim isActive As String = selectedRow.Cells("isActive").Value.ToString()

                ' Update users table
                query = "UPDATE users SET username = '" & updatedUsername & "', userRole = '" & updatedUserRole & "' WHERE userId = '" & userId & "'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Update admin_users table
                query = "UPDATE admin_users SET adminRole = '" & adminRole & "', fullName = '" & fullName & "', email = '" & email & "', phone = '" & phone & "', isActive = '" & isActive & "' WHERE adminId = '" & adminId & "'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
            End If

            MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the data to show updated values
            LoadUserData()

        Catch ex As Exception
            MessageBox.Show("Error updating data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        ' Track when cell values are changed
        If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
            ' You can add additional logic here if needed
            ' For example, highlight the row to show it has been modified
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightYellow
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DeleteUserData()
    End Sub

    Private Sub DeleteUserData()
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            Dim userId As String = selectedRow.Cells("userId").Value.ToString()
            Dim username As String = selectedRow.Cells("username").Value.ToString()

            ' Create confirmation message based on user type
            Dim confirmationMessage As String = ""
            If ComboBox1.Text = "Users" Then
                confirmationMessage = $"Are you sure you want to delete the following user?" & vbCrLf & vbCrLf &
                                    $"User ID: {userId}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Role: {selectedRow.Cells("userRole").Value.ToString()}"
            ElseIf ComboBox1.Text = "Customers" Then
                Dim customerName As String = $"{selectedRow.Cells("customerFname").Value.ToString()} {selectedRow.Cells("customerLname").Value.ToString()}"
                confirmationMessage = $"Are you sure you want to delete the following customer?" & vbCrLf & vbCrLf &
                                    $"Customer ID: {selectedRow.Cells("customerId").Value.ToString()}" & vbCrLf &
                                    $"Name: {customerName}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Address: {selectedRow.Cells("customerAddress").Value.ToString()}"
            ElseIf ComboBox1.Text = "Admins" Then
                confirmationMessage = $"Are you sure you want to delete the following admin?" & vbCrLf & vbCrLf &
                                    $"Admin ID: {selectedRow.Cells("adminId").Value.ToString()}" & vbCrLf &
                                    $"Name: {selectedRow.Cells("fullName").Value.ToString()}" & vbCrLf &
                                    $"Username: {username}" & vbCrLf &
                                    $"Role: {selectedRow.Cells("adminRole").Value.ToString()}"
            End If

            confirmationMessage &= vbCrLf & vbCrLf & "This action cannot be undone!"

            Dim result As DialogResult = MessageBox.Show(confirmationMessage, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.No Then
                Return
            End If

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            Try
                If ComboBox1.Text = "Users" Then
                    ' Delete from users table
                    query = "DELETE FROM users WHERE userId = '" & userId & "'"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                ElseIf ComboBox1.Text = "Customers" Then
                    Dim customerId As String = selectedRow.Cells("customerId").Value.ToString()

                    ' Delete from customers table first (child table)
                    query = "DELETE FROM customers WHERE customerId = '" & customerId & "'"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    ' Then delete from users table (parent table)
                    query = "DELETE FROM users WHERE userId = '" & userId & "'"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                ElseIf ComboBox1.Text = "Admins" Then
                    Dim adminId As String = selectedRow.Cells("adminId").Value.ToString()

                    ' Delete from admin_users table first (child table)
                    query = "DELETE FROM admin_users WHERE adminId = '" & adminId & "'"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    ' Then delete from users table (parent table)
                    query = "DELETE FROM users WHERE userId = '" & userId & "'"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End If

                MessageBox.Show("Data deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Refresh the data to show updated values
                LoadUserData()

            Catch ex As MySqlException
                If ex.Number = 1451 Then ' Foreign key constraint error
                    MessageBox.Show("Cannot delete this record because it is referenced by other records in the database." & vbCrLf & vbCrLf &
                                  "Please remove all related records first before deleting this user.",
                                  "Foreign Key Constraint", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("Error deleting data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Finally
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        ' Handle cell click events if needed
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Refresh the data when PictureBox is clicked
        LoadUserData()
        ' Clear any search text
        TextBox1.Clear()
        ' Reset row highlighting
        For Each row As DataGridViewRow In DataGridView1.Rows
            row.DefaultCellStyle.BackColor = Color.White
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        adminregistration.Show()
    End Sub
End Class