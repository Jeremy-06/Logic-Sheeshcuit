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
        ' Check if user has been logged out from other forms
        If login.adminId = 0 AndAlso Not String.IsNullOrEmpty(login.userRole) AndAlso login.userRole.ToLower() = "admin" Then
            ' Clear all session data
            login.userRole = ""
            login.customerId = 0
            login.adminRole = ""
            MessageBox.Show("You have been logged out from all sessions.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Me.Close()
            Return
        End If

        LoadAdminProfile()
        SetTextBoxesFromLabels()
        SetTextBoxesReadOnly(True)
    End Sub

    Private Sub LoadAdminProfile()

        AddHandler fullNameTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler EmailTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler PhoneTextBox.KeyDown, AddressOf EditTextBox_KeyDown

        ' Check if admin is still logged in
        If login.adminId <= 0 Then
            ClearProfileData()
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Debug: Check what admin records exist
            Dim debugQuery As String = "SELECT adminId, fullName, email, phone FROM admin_users"
            Dim debugCmd As New MySqlCommand(debugQuery, conn)
            Dim debugReader As MySqlDataReader = debugCmd.ExecuteReader()

            Console.WriteLine("Debug: All admin records in database:")
            While debugReader.Read()
                Console.WriteLine($"Admin ID: {debugReader.GetInt32("adminId")}, Name: {debugReader.GetString("fullName")}, Email: {debugReader.GetString("email")}")
            End While
            debugReader.Close()

            ' Get admin information using the logged-in admin ID
            query = $"SELECT adminId, fullName, email, phone FROM admin_users WHERE adminId = {login.adminId}"
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            ' Debug: Check if we found any records
            If reader.HasRows Then
                Console.WriteLine($"Debug: Found admin record with ID {login.adminId}")
            Else
                Console.WriteLine($"Debug: No admin record found for ID {login.adminId}")
            End If

            If reader.Read() Then
                adminIdlbl.Text = reader.GetInt32("adminId").ToString()

                ' Get fullName and format it properly
                Dim fullName As String = reader.GetString("fullName")
                usernamelbl.Text = fullName

                emaillbl.Text = reader.GetString("email")
                phonelbl.Text = reader.GetString("phone")
                rolelbl.Text = login.adminRole
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
        Try
            If Not String.IsNullOrEmpty(usernamelbl.Text) AndAlso usernamelbl.Text <> "N/A" Then
                ' Set the full name directly to the textbox
                fullNameTextBox.Text = usernamelbl.Text.Trim()
            Else
                fullNameTextBox.Text = ""
            End If

            EmailTextBox.Text = If(emaillbl.Text = "N/A", "", emaillbl.Text)
            PhoneTextBox.Text = If(phonelbl.Text = "N/A", "", phonelbl.Text)
        Catch ex As Exception
            fullNameTextBox.Text = ""
            EmailTextBox.Text = ""
            PhoneTextBox.Text = ""
        End Try
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

        For Each tb As TextBox In {fullNameTextBox, EmailTextBox, PhoneTextBox}
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

        Label8.ForeColor = Color.White
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

        Label8.ForeColor = Color.Gray
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
            fullNameTextBox.Focus()
            isEditing = True
        Else
            ' Save changes to labels
            usernamelbl.Text = fullNameTextBox.Text.Trim()
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
        Dim fullName As String = fullNameTextBox.Text.Trim()
        Dim email As String = EmailTextBox.Text.Trim()
        Dim phone As String = PhoneTextBox.Text.Trim()

        ' Validate full name (should contain at least first and last name)
        If String.IsNullOrEmpty(fullName) Then
            MessageBox.Show("Please enter a valid full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Update labels
        usernamelbl.Text = fullName
        emaillbl.Text = email
        phonelbl.Text = phone

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"UPDATE admin_users SET fullName='{fullName}', email='{email}', phone='{phone}' WHERE adminId={adminIdlbl.Text}"
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

    Private Sub ClearProfileData()
        ' Clear all profile data
        adminIdlbl.Text = ""
        usernamelbl.Text = ""
        emaillbl.Text = ""
        phonelbl.Text = ""
        rolelbl.Text = ""

        ' Clear textboxes
        fullNameTextBox.Text = ""
        EmailTextBox.Text = ""
        PhoneTextBox.Text = ""
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
            Me.Close()
            home.Button1.Visible = False
        End If
    End Sub
End Class