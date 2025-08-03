Imports System.Reflection.Emit
Imports MySql.Data.MySqlClient

Public Class customerprofile
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String
    Private isEditing As Boolean = False

    Private Sub customerprofile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCustomerProfile()
        SetTextBoxesFromLabels()
        SetTextBoxesReadOnly(True)
    End Sub

    Private Sub LoadCustomerProfile()

        AddHandler firstNameTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler lastNameTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler addressTextBox.KeyDown, AddressOf EditTextBox_KeyDown
        AddHandler phoneTextBox.KeyDown, AddressOf EditTextBox_KeyDown

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Get customer information using the logged-in customer ID
            query = $"SELECT customerId, customerFname, customerLname, customerAddress, customerPhone FROM customers WHERE customerId = {login.customerId}"
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                customerIdlbl.Text = reader.GetInt32("customerId").ToString()

                ' Set Label2 to First Name + Last Name (concatenated)
                Dim firstName As String = reader.GetString("customerFname")
                Dim lastName As String = reader.GetString("customerLname")
                usernamelbl.Text = firstName & " " & lastName

                addresslbl.Text = reader.GetString("customerAddress")
                phonelbl.Text = reader.GetString("customerPhone")
                rolelbl.Text = login.userRole
            Else
                MessageBox.Show("Customer information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading user profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            lastNameTextBox.Text = String.Join(" ", names.Skip(1))
        Else
            firstNameTextBox.Text = usernamelbl.Text
            lastNameTextBox.Text = ""
        End If
        addressTextBox.Text = addresslbl.Text
        phoneTextBox.Text = phonelbl.Text
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

        For Each tb As TextBox In {firstNameTextBox, lastNameTextBox, addressTextBox, phoneTextBox}
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

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Log out functionality
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            login.customerId = 0 ' Reset customer ID to indicate not logged in
            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Me.Hide()
            home.Button1.Visible = False
        End If
    End Sub

    Private Sub lighttheme_Click(sender As Object, e As EventArgs) Handles lightTheme.Click
        Me.BackgroundImage = My.Resources.Customer_Light_Theme
        usernamelbl.ForeColor = Color.Gray
        customerIdlbl.ForeColor = Color.Gray
        rolelbl.ForeColor = Color.Gray

        Label8.ForeColor = Color.White
        Label9.ForeColor = Color.White
        Label10.ForeColor = Color.White
        Label11.ForeColor = Color.White

        addresslbl.ForeColor = Color.White
        phonelbl.ForeColor = Color.White

        Button1.BackgroundImage = My.Resources.Light_Back_Button
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button2.BackgroundImage = My.Resources.Light_Log_Out_Button
        Button2.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub darktheme_Click(sender As Object, e As EventArgs) Handles darkTheme.Click
        Me.BackgroundImage = My.Resources.Customer_Dark_Theme
        usernamelbl.ForeColor = Color.White
        customerIdlbl.ForeColor = Color.White
        rolelbl.ForeColor = Color.Gray

        Label8.ForeColor = Color.Gray
        Label9.ForeColor = Color.Gray
        Label10.ForeColor = Color.Gray
        Label11.ForeColor = Color.Gray

        usernamelbl.ForeColor = Color.White
        customerIdlbl.ForeColor = Color.White
        rolelbl.ForeColor = Color.White

        addresslbl.ForeColor = Color.Gray
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
            Dim lastName As String = lastNameTextBox.Text.Trim()
            usernamelbl.Text = firstNameTextBox.Text & " " & lastNameTextBox.Text
            addresslbl.Text = addressTextBox.Text
            phonelbl.Text = phoneTextBox.Text
            SetTextBoxesReadOnly(True)
            isEditing = False

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to save changes?", "Confirm Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                ConfirmEditAndUpdate()
            End If
        End If
    End Sub

    Private Sub ConfirmEditAndUpdate()
        Dim firstName As String = firstNameTextBox.Text.Trim()
        Dim lastName As String = lastNameTextBox.Text.Trim()
        Dim address As String = addressTextBox.Text.Trim()
        Dim phone As String = phoneTextBox.Text.Trim()

        ' Update labels
        usernamelbl.Text = If(lastName = "", firstName, firstName & " " & lastName)
        addresslbl.Text = address
        phonelbl.Text = phone

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"UPDATE customers SET customerFname='{firstName}', customerLname='{lastName}', customerAddress='{address}', customerPhone='{phone}' WHERE customerId={Convert.ToInt32(customerIdlbl.Text)}"
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

        LoadCustomerProfile()
        SetTextBoxesFromLabels()

        SetTextBoxesReadOnly(True)
        isEditing = False

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class