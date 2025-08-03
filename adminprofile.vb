Imports MySql.Data.MySqlClient

Public Class adminprofile
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String
    Private isEditing As Boolean = False

    Private Sub adminprofile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAdminProfile()
        SetTextBoxesFromLabels()
        SetTextBoxesReadOnly(True)
    End Sub

    Private Sub LoadAdminProfile()

        AddHandler firstNameTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler LastNameTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler EmailTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler PhoneTextBox.KeyDown, AddressOf EditTextBox_KeyDown

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            query = $"SELECT adminId, fullName, email, phone, lastLogin, createdAt FROM admin_users WHERE adminId = {login.adminId}"
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                adminIdlbl.Text = reader.GetInt32("adminId").ToString()
                usernamelbl.Text = reader.GetString("fullName")
                emaillbl.Text = reader.GetString("email")
                phonelbl.Text = reader.GetString("phone")
                lastLoginlbl.Text = reader.GetString("lastLogin")
                createdAtlbl.Text = reader.GetString("createdAt")
                rolelbl.Text = login.userRole

                ' Set textboxes
                Dim names = reader.GetString("fullName").Split(" "c)
                If names.Length > 1 Then
                    firstNameTextBox.Text = names(0)
                    LastNameTextBox.Text = String.Join(" ", names.Skip(1))
                Else
                    firstNameTextBox.Text = reader.GetString("fullName")
                    LastNameTextBox.Text = ""
                End If
                EmailTextBox.Text = reader.GetString("email")
                PhoneTextBox.Text = reader.GetString("phone")
            Else
                MessageBox.Show("Admin information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading admin profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If reader IsNot Nothing AndAlso Not reader.IsClosed Then
                reader.Close()
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub SetTextBoxesFromLabels()
        ' Set textboxes from label values (if needed)
        Dim names = usernamelbl.Text.Split(" "c)
        If names.Length > 1 Then
            firstNameTextBox.Text = names(0)
            LastNameTextBox.Text = String.Join(" ", names.Skip(1))
        Else
            firstNameTextBox.Text = usernamelbl.Text
            LastNameTextBox.Text = ""
        End If
        EmailTextBox.Text = emaillbl.Text
        PhoneTextBox.Text = phonelbl.Text
    End Sub

    Private Sub EditTextBox_KeyDown(sender As Object, e As KeyEventArgs)
        If isEditing AndAlso e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to save changes?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ConfirmEditAndUpdate()
            End If
        End If
    End Sub


    Private Sub SetTextBoxesReadOnly(readOnlyState As Boolean)
        Dim cursorType As Cursor = If(readOnlyState, Cursors.Default, Cursors.IBeam)

        For Each tb As TextBox In {firstNameTextBox, LastNameTextBox, EmailTextBox, PhoneTextBox}
            tb.ReadOnly = readOnlyState
            tb.TabStop = Not readOnlyState
            tb.Cursor = cursorType

            ' Intercept click to prevent focus when read-only
            If readOnlyState Then
                AddHandler tb.MouseDown, AddressOf PreventClick
            Else
                RemoveHandler tb.MouseDown, AddressOf PreventClick
            End If
        Next

        If readOnlyState Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub PreventClick(sender As Object, e As MouseEventArgs)
        Me.ActiveControl = Nothing
    End Sub

    Private Sub lighttheme_Click(sender As Object, e As EventArgs) Handles lightTheme.Click
        Me.BackgroundImage = My.Resources.Admin_Light_Theme
        usernamelbl.ForeColor = Color.Gray
        adminIdlbl.ForeColor = Color.Gray
        rolelbl.ForeColor = Color.Gray

        Label1.ForeColor = Color.Gray
        Label2.ForeColor = Color.Gray
        createdAtlbl.ForeColor = Color.Gray
        lastLoginlbl.ForeColor = Color.Gray

        Label8.ForeColor = Color.White
        Label9.ForeColor = Color.White
        Label10.ForeColor = Color.White
        Label11.ForeColor = Color.White

        emaillbl.ForeColor = Color.White
        phonelbl.ForeColor = Color.White

        Button1.BackgroundImage = My.Resources.Light_Back_Button
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button2.BackgroundImage = My.Resources.Light_Log_Out_Button
        Button2.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub darktheme_Click(sender As Object, e As EventArgs) Handles darkTheme.Click
        Me.BackgroundImage = My.Resources.Admin_Dark_Theme
        usernamelbl.ForeColor = Color.White
        adminIdlbl.ForeColor = Color.White
        rolelbl.ForeColor = Color.White

        Label1.ForeColor = Color.White
        Label2.ForeColor = Color.White
        createdAtlbl.ForeColor = Color.White
        lastLoginlbl.ForeColor = Color.White

        Label8.ForeColor = Color.Gray
        Label9.ForeColor = Color.Gray
        Label10.ForeColor = Color.Gray
        Label11.ForeColor = Color.Gray

        emaillbl.ForeColor = Color.Gray
        phonelbl.ForeColor = Color.Gray

        Button1.BackgroundImage = My.Resources.Dark_Back_Button
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button2.BackgroundImage = My.Resources.Dark_Log_Out_Button
        Button2.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub editDatalbl_Click(sender As Object, e As EventArgs) Handles editDatalbl.Click
        ToggleEdit()
    End Sub

    Private Sub editDataIcon_Click(sender As Object, e As EventArgs) Handles editDataIcon.Click
        ToggleEdit()
    End Sub

    Private Sub ToggleEdit()
        If Not isEditing Then
            SetTextBoxesReadOnly(False)
            firstNameTextBox.Focus()
            isEditing = True
        Else
            ' Save changes to labels
            Dim firstName As String = firstNameTextBox.Text.Trim()
            Dim lastName As String = LastNameTextBox.Text.Trim()
            usernamelbl.Text = firstNameTextBox.Text & " " & LastNameTextBox.Text
            emaillbl.Text = EmailTextBox.Text
            phonelbl.Text = PhoneTextBox.Text
            SetTextBoxesReadOnly(True)
            isEditing = False

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to save changes?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ConfirmEditAndUpdate()
            End If
        End If
    End Sub

    Private Sub ConfirmEditAndUpdate()
        ' Get values from textboxes
        Dim fullName As String = (firstNameTextBox.Text.Trim() & " " & LastNameTextBox.Text.Trim()).Trim()
        Dim email As String = EmailTextBox.Text.Trim()
        Dim phone As String = PhoneTextBox.Text.Trim()

        ' Update labels
        usernamelbl.Text = fullName
        emaillbl.Text = email
        phonelbl.Text = phone

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Update admin_users table 
            query = $"UPDATE admin_users SET fullName='{fullName}', email='{email}', phone='{phone}' WHERE adminId={Convert.ToInt32(adminIdlbl.Text)}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Profile updated successfully.", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error updating profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        LoadAdminProfile()
        SetTextBoxesFromLabels()
        SetTextBoxesReadOnly(True)
        isEditing = False
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            login.adminId = 0
            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Me.Hide()
        End If
    End Sub
End Class