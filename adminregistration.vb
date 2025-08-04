Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class adminregistration
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim query As String

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim sb As New StringBuilder()
            For Each b As Byte In hash
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    Private Sub adminregistration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Populate ComboBox1 with admin roles
        ComboBox1.Items.Clear()
        ComboBox1.Items.AddRange(New Object() {"admin", "super_admin", "inventory_manager", "order_manager", "financial_auditor"})
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RegisterAdmin()
    End Sub

    Private Sub RegisterAdmin()
        Try
            ' Validate input fields
            If String.IsNullOrEmpty(ComboBox1.Text.Trim()) Then
                MessageBox.Show("Please select an admin role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ComboBox1.Focus()
                Return
            End If

            If String.IsNullOrEmpty(TextBox1.Text.Trim()) Then
                MessageBox.Show("Please enter the full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox1.Focus()
                Return
            End If

            If String.IsNullOrEmpty(TextBox2.Text.Trim()) Then
                MessageBox.Show("Please enter the email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
                Return
            End If

            If String.IsNullOrEmpty(TextBox3.Text.Trim()) Then
                MessageBox.Show("Please enter the phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox3.Focus()
                Return
            End If

            If String.IsNullOrEmpty(TextBox4.Text.Trim()) Then
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox4.Focus()
                Return
            End If

            If String.IsNullOrEmpty(TextBox5.Text.Trim()) Then
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox5.Focus()
                Return
            End If

            ' Check if username already exists
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            query = "SELECT COUNT(*) FROM users WHERE username = '" & TextBox4.Text.Trim() & "'"
            cmd = New MySqlCommand(query, conn)
            Dim userCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If userCount > 0 Then
                MessageBox.Show("Username already exists. Please choose a different username.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox4.Focus()
                Return
            End If

            ' Check if email already exists
            query = "SELECT COUNT(*) FROM admin_users WHERE email = '" & TextBox2.Text.Trim() & "'"
            cmd = New MySqlCommand(query, conn)
            Dim emailCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            If emailCount > 0 Then
                MessageBox.Show("Email address already exists. Please use a different email.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Focus()
                Return
            End If

            ' Start transaction for data consistency
            Dim transaction As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Hash the password
                Dim hashedPassword As String = HashPassword(TextBox5.Text.Trim())

                ' Insert into users table first with userRole fixed to "admin" and hashed password
                query = "INSERT INTO users (username, password, userRole) VALUES ('" & TextBox4.Text.Trim() & "', '" & hashedPassword & "', 'admin')"
                cmd = New MySqlCommand(query, conn, transaction)
                cmd.ExecuteNonQuery()

                ' Get the generated userId
                query = "SELECT LAST_INSERT_ID()"
                cmd = New MySqlCommand(query, conn, transaction)
                Dim userId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Insert into admin_users table with adminRole from ComboBox1
                query = "INSERT INTO admin_users (userId, adminRole, fullName, email, phone, isActive) VALUES (" & userId & ", '" & ComboBox1.Text.Trim() & "', '" & TextBox1.Text.Trim() & "', '" & TextBox2.Text.Trim() & "', '" & TextBox3.Text.Trim() & "', '1')"
                cmd = New MySqlCommand(query, conn, transaction)
                cmd.ExecuteNonQuery()

                ' Commit transaction
                transaction.Commit()

                MessageBox.Show("Admin registered successfully!" & vbCrLf & vbCrLf &
                              "Admin Role: " & ComboBox1.Text.Trim() & vbCrLf &
                              "Full Name: " & TextBox1.Text.Trim() & vbCrLf &
                              "Email: " & TextBox2.Text.Trim() & vbCrLf &
                              "Phone: " & TextBox3.Text.Trim() & vbCrLf &
                              "Username: " & TextBox4.Text.Trim(),
                              "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Clear form fields
                ClearForm()

            Catch ex As Exception
                ' Rollback transaction on error
                transaction.Rollback()
                MessageBox.Show("Error during registration: " & ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub ClearForm()
        ComboBox1.SelectedIndex = 0
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Clear form button
        ClearForm()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Cancel/Close button
        Me.Close()
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBox2.Leave
        ' Basic email validation
        If Not String.IsNullOrEmpty(TextBox2.Text.Trim()) Then
            If Not IsValidEmail(TextBox2.Text.Trim()) Then
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TextBox2.Focus()
            End If
        End If
    End Sub

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        ' Allow only numbers, spaces, dashes, and parentheses for phone number
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso
           e.KeyChar <> " "c AndAlso e.KeyChar <> "-"c AndAlso e.KeyChar <> "("c AndAlso e.KeyChar <> ")"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        ' Allow only alphanumeric characters and underscore for username
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetterOrDigit(e.KeyChar) AndAlso e.KeyChar <> "_"c Then
            e.Handled = True
        End If
    End Sub
End Class