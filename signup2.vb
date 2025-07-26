Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class signup2
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Function ShowPasswordInputBox(prompt As String, title As String) As String
        ' Custom input box with light gray background and contrasting font color for password confirmation
        Dim inputForm As New Form()
        Dim inputTextBox As New TextBox()
        Dim okButton As New Button()
        Dim cancelButton As New Button()
        Dim lblPrompt As New Label()
        Dim result As String = ""
        inputForm.FormBorderStyle = FormBorderStyle.FixedDialog
        inputForm.MinimizeBox = False
        inputForm.MaximizeBox = False
        inputForm.StartPosition = FormStartPosition.CenterParent
        inputForm.Width = 400
        inputForm.Height = 180
        inputForm.Text = title
        inputForm.ShowInTaskbar = False
        inputForm.ControlBox = False

        ' Set light gray background and contrasting font color (black) for the form
        inputForm.BackColor = Color.LightGray
        inputForm.ForeColor = Color.Black

        lblPrompt.Text = prompt
        lblPrompt.SetBounds(10, 10, 370, 30)
        lblPrompt.Font = New Font("Microsoft Sans Serif", 12)
        lblPrompt.BackColor = Color.Transparent
        lblPrompt.ForeColor = Color.Black

        inputTextBox.SetBounds(10, 50, 370, 35)
        inputTextBox.Font = New Font("Microsoft Sans Serif", 14)
        inputTextBox.UseSystemPasswordChar = True
        inputTextBox.BackColor = Color.White
        inputTextBox.ForeColor = Color.Black

        okButton.Text = "OK"
        okButton.SetBounds(210, 100, 80, 30)
        okButton.BackColor = Color.White
        okButton.ForeColor = Color.Black
        AddHandler okButton.Click, Sub(sender, e)
                                       result = inputTextBox.Text
                                       inputForm.DialogResult = DialogResult.OK
                                       inputForm.Close()
                                   End Sub

        cancelButton.Text = "Cancel"
        cancelButton.SetBounds(300, 100, 80, 30)
        cancelButton.BackColor = Color.White
        cancelButton.ForeColor = Color.Black
        AddHandler cancelButton.Click, Sub(sender, e)
                                           result = ""
                                           inputForm.DialogResult = DialogResult.Cancel
                                           inputForm.Close()
                                       End Sub

        inputForm.Controls.AddRange(New Control() {lblPrompt, inputTextBox, okButton, cancelButton})
        inputForm.AcceptButton = okButton
        inputForm.CancelButton = cancelButton
        inputForm.ShowDialog()
        Return result
    End Function

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if any TextBox is empty before proceeding
        If String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Please fill in all fields before proceeding.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Confirm password with custom input box
        Dim confirmPassword As String = ShowPasswordInputBox("Please confirm your password:", "Confirm Password")
        If confirmPassword <> TextBox2.Text Then
            MessageBox.Show("Passwords do not match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim username As String = TextBox1.Text.Trim().ToLower()
            Dim password As String = TextBox2.Text
            Dim hashedPassword As String = HashPassword(password)
            query = $"INSERT INTO users (username, password, userRole) VALUES ('{username}', '{hashedPassword}', 'customer')"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
            ' Get the new userId
            cmd = New MySqlCommand("SELECT LAST_INSERT_ID();", conn)
            Dim userId As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            ' Get data from previous forms
            Dim firstName As String = signup.FirstName
            Dim lastName As String = signup.LastName
            Dim address As String = signup1.Address
            Dim phone As String = signup1.Phone
            ' Insert into customers table (no parameterized query)
            query = $"INSERT INTO customers (customerFname, customerLname, customerAddress, customerPhone, userId) VALUES ('{firstName}', '{lastName}', '{address}', '{phone}', {userId})"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error during sign up: " & ex.Message, "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
            Return
        End Try
        If conn.State = ConnectionState.Open Then conn.Close()
        ' Clear all input fields in signup and signup1
        For Each ctrl As Control In signup.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Clear()
            End If
        Next
        For Each ctrl As Control In signup1.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Clear()
            End If
        Next
        Me.Hide()
        MessageBox.Show("Sign Up Successful! Proceeding to Log In...", "Sign Up Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        login.Show()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Only allow letters, numbers, and underscore for username
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetterOrDigit(e.KeyChar) AndAlso e.KeyChar <> "_"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        ' Only allow letters, numbers, and underscore for password
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetterOrDigit(e.KeyChar) AndAlso e.KeyChar <> "_"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        signup1.Show()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' Toggle password character visibility based on a checkbox (assume CheckBox1 is "Show Password")
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub signup2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub
End Class