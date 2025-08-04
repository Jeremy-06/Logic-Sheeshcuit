Imports MySql.Data.MySqlClient

Public Class home

    Private smoothScrollTarget As Integer = 0
    Private WithEvents smoothScrollTimer As New Timer With {.Interval = 10}
    Private currentOpenForm As Form = Nothing

    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        Application.Exit()
    End Sub

    Private Sub HideCurrentForm()
        If currentOpenForm IsNot Nothing AndAlso Not currentOpenForm.IsDisposed Then
            currentOpenForm.Hide()
        End If
    End Sub

    Private Sub MakerlabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakerlabToolStripMenuItem.Click
        HideCurrentForm()
        makerlab.Show()
        currentOpenForm = makerlab
    End Sub

    Private Sub CircuitrocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircuitrocksToolStripMenuItem.Click
        HideCurrentForm()
        circuitrocks.Show()
        currentOpenForm = circuitrocks
    End Sub

    Private Sub Element14ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Element14ToolStripMenuItem.Click
        HideCurrentForm()
        element14.Show()
        currentOpenForm = element14
    End Sub

    Private Sub RSComponentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RSComponentsToolStripMenuItem.Click
        HideCurrentForm()
        rscomponents.Show()
        currentOpenForm = rscomponents
    End Sub

    Private Sub DisplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayToolStripMenuItem.Click
        HideCurrentForm()
        digitaldisplays.Show()
        currentOpenForm = digitaldisplays
    End Sub

    Private Sub ICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ICToolStripMenuItem.Click
        HideCurrentForm()
        integratedcircuits.Show()
        currentOpenForm = integratedcircuits
    End Sub

    Private Sub LEDsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEDsToolStripMenuItem.Click
        HideCurrentForm()
        led.Show()
        currentOpenForm = led
    End Sub

    Private Sub PowerSupplyAndModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PowerSupplyAndModuleToolStripMenuItem.Click
        HideCurrentForm()
        powersupply.Show()
        currentOpenForm = powersupply
    End Sub

    Private Sub WiresAndCablesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WiresAndCablesToolStripMenuItem.Click
        HideCurrentForm()
        wires.Show()
        currentOpenForm = wires
    End Sub

    Private Sub BreadboardsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BreadboardsToolStripMenuItem.Click
        HideCurrentForm()
        breadboards.Show()
        currentOpenForm = breadboards
    End Sub

    Private Sub SwitchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchesToolStripMenuItem.Click
        HideCurrentForm()
        switches.Show()
        currentOpenForm = switches
    End Sub

    Private Sub ResistorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResistorsToolStripMenuItem.Click
        HideCurrentForm()
        resistor.Show()
        currentOpenForm = resistor
    End Sub

    Private Sub CapacitorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CapacitorsToolStripMenuItem.Click
        HideCurrentForm()
        capacitors.Show()
        currentOpenForm = capacitors
    End Sub

    Private Sub OscilloscopesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OscilloscopesToolStripMenuItem.Click
        HideCurrentForm()
        oscilloscopes.Show()
        currentOpenForm = oscilloscopes
    End Sub

    Private Sub ScrollToCurrentResult()
        If currentResultIndex >= 0 AndAlso currentResultIndex < searchResults.Count Then
            Dim lbl As Label = searchResults(currentResultIndex)
            Panel1.ScrollControlIntoView(lbl)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim searchTerm As String = TextBox1.Text.Trim().ToLower()
        If String.IsNullOrWhiteSpace(searchTerm) Then Return

        If searchTerm = lastSearchTerm AndAlso searchResults.Count > 0 Then
            ' Next result
            currentResultIndex = (currentResultIndex + 1) Mod searchResults.Count
            ScrollToCurrentResult()
        Else
            ' New search
            lastSearchTerm = searchTerm
            SearchAndScroll(searchTerm)
        End If
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click, PictureBox38.Click, PictureBox42.Click
        RefreshHomePanel()
    End Sub

    Private Sub RefreshHomePanel()
        TextBox1.Clear()
        searchResults.Clear()
        currentResultIndex = -1
        lastSearchTerm = ""

        For Each lbl As Label In previousResults
            lbl.BackColor = SystemColors.Control
            lbl.ForeColor = SystemColors.ControlText
        Next
        previousResults.Clear()

        Try
            If conn.State = ConnectionState.Closed Then conn.Open()
            Dim countQuery As String = "SELECT COUNT(*) FROM products"
            Dim countCmd As New MySqlCommand(countQuery, conn)
            Dim totalProducts As Integer = Convert.ToInt32(countCmd.ExecuteScalar())
            Label2.Text = totalProducts.ToString()
        Catch ex As Exception
            Label2.Text = "0"
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try

        Panel1.Refresh()
        Panel1.VerticalScroll.Value = 0
        Panel1.PerformLayout()
        Me.Invalidate()
        Me.Update()
    End Sub

    'Products
    Private Sub PictureBox40_Click(sender As Object, e As EventArgs) Handles PictureBox40.Click, PictureBox43.Click
        ProductsToolStripMenuItem.Select()
        ProductsToolStripMenuItem.ShowDropDown()
    End Sub

    'Cart
    Private Sub CartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CartToolStripMenuItem.Click, PictureBox39.Click, PictureBox44.Click
        Dim cartForm As New cart()
        cartForm.Show()
    End Sub

    'Profile
    Private Sub usericon_Click(sender As Object, e As EventArgs) Handles usericon.Click
        ' Check if user has been logged out from admin forms (adminId = 0 means logged out)
        If login.adminId = 0 AndAlso Not String.IsNullOrEmpty(login.userRole) AndAlso login.userRole.ToLower() = "admin" Then
            ' Clear all session data
            login.userRole = ""
            login.customerId = 0
            login.adminRole = ""
            MessageBox.Show("You have been logged out from all sessions.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Return
        End If

        ' Check if user is logged in
        If String.IsNullOrEmpty(login.userRole) Then
            MessageBox.Show("Please log in first to view your profile.")
            login.Show()
            Return
        End If

        ' Check user role and show appropriate profile
        If login.userRole.ToLower() = "admin" Then
            ' Show admin profile
            adminprofile.Show()
        ElseIf login.userRole.ToLower() = "customer" Then
            ' Check if customer exists in the database
            Try
                If login.customerId <= 0 Then
                    MessageBox.Show("Please log in or sign up first to view your profile.")
                    login.Show()
                    Return
                End If

                ' Use the shared/public connection object (assumed declared outside the form)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Dim query As String = $"SELECT COUNT(*) FROM customers WHERE customerId = {login.customerId}"
                Dim cmd As New MySqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If count = 0 Then
                    MessageBox.Show("Please sign up first to view your profile.")
                    signup.Show()
                    Return
                End If

                customerprofile.Show()
            Catch ex As Exception
                MessageBox.Show("Error checking user: " & ex.Message)
                login.Show()
            End Try
        Else
            ' For other roles (auditor, etc.), show appropriate message
            MessageBox.Show($"Profile access for {login.userRole} role is not yet implemented.")
        End If
    End Sub

    Private Sub PictureBox45_Click(sender As Object, e As EventArgs) Handles PictureBox45.Click
        ' Check if user has been logged out from admin forms (adminId = 0 means logged out)
        If login.adminId = 0 AndAlso Not String.IsNullOrEmpty(login.userRole) AndAlso login.userRole.ToLower() = "admin" Then
            ' Clear all session data
            login.userRole = ""
            login.customerId = 0
            login.adminRole = ""
            MessageBox.Show("You have been logged out from all sessions.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Return
        End If

        ' Check if user is logged in
        If String.IsNullOrEmpty(login.userRole) Then
            MessageBox.Show("Please log in first to view your profile.")
            login.Show()
            Return
        End If

        ' Check user role and show appropriate profile
        If login.userRole.ToLower() = "admin" Then
            ' Show admin profile
            adminprofile.Show()
        ElseIf login.userRole.ToLower() = "customer" Then
            ' Check if customer exists in the database
            Try
                If login.customerId <= 0 Then
                    MessageBox.Show("Please log in or sign up first to view your profile.")
                    login.Show()
                    Return
                End If

                ' Use the shared/public connection object (assumed declared outside the form)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Dim query As String = $"SELECT COUNT(*) FROM customers WHERE customerId = {login.customerId}"
                Dim cmd As New MySqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If count = 0 Then
                    MessageBox.Show("Please sign up first to view your profile.")
                    signup.Show()
                    Return
                End If

                customerprofile.Show()
            Catch ex As Exception
                MessageBox.Show("Error checking user: " & ex.Message)
                login.Show()
            End Try
        Else
            ' For other roles (auditor, etc.), show appropriate message
            MessageBox.Show($"Profile access for {login.userRole} role is not yet implemented.")
        End If
    End Sub

    Private Sub ProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfileToolStripMenuItem.Click
        ' Check if user has been logged out from admin forms (adminId = 0 means logged out)
        If login.adminId = 0 AndAlso Not String.IsNullOrEmpty(login.userRole) AndAlso login.userRole.ToLower() = "admin" Then
            ' Clear all session data
            login.userRole = ""
            login.customerId = 0
            login.adminRole = ""
            MessageBox.Show("You have been logged out from all sessions.", "Session Expired", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Return
        End If

        ' Check if user is logged in
        If String.IsNullOrEmpty(login.userRole) Then
            MessageBox.Show("Please log in first to view your profile.")
            login.Show()
            Return
        End If

        ' Check user role and show appropriate profile
        If login.userRole.ToLower() = "admin" Then
            ' Show admin profile
            adminprofile.Show()
        ElseIf login.userRole.ToLower() = "customer" Then
            ' Check if customer exists in the database
            Try
                If login.customerId <= 0 Then
                    MessageBox.Show("Please log in or sign up first to view your profile.")
                    login.Show()
                    Return
                End If

                ' Use the shared/public connection object (assumed declared outside the form)
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Dim query As String = $"SELECT COUNT(*) FROM customers WHERE customerId = {login.customerId}"
                Dim cmd As New MySqlCommand(query, conn)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If count = 0 Then
                    MessageBox.Show("Please sign up first to view your profile.")
                    signup.Show()
                    Return
                End If

                customerprofile.Show()
            Catch ex As Exception
                MessageBox.Show("Error checking user: " & ex.Message)
                login.Show()
            End Try
        Else
            ' For other roles (auditor, etc.), show appropriate message
            MessageBox.Show($"Profile access for {login.userRole} role is not yet implemented.")
        End If
    End Sub

    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Dim countQuery As String = "SELECT COUNT(*) FROM products"
            Dim countCmd As New MySqlCommand(countQuery, conn)
            Dim totalProducts As Integer = Convert.ToInt32(countCmd.ExecuteScalar())
            Label2.Text = totalProducts.ToString()
        Catch ex As Exception
            Label2.Text = 0
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

        ' Disable Button1 by default, only enable for non-customer users
        Button1.Visible = False
        Button1.Enabled = False

    End Sub

    Private searchResults As New List(Of Label)
    Private currentResultIndex As Integer = -1
    Private previousResults As New List(Of Label)

    Private Function LevenshteinDistance(s As String, t As String) As Integer
        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim d(n + 1, m + 1) As Integer

        If n = 0 Then Return m
        If m = 0 Then Return n

        For i As Integer = 0 To n
            d(i, 0) = i
        Next

        For j As Integer = 0 To m
            d(0, j) = j
        Next

        For i As Integer = 1 To n
            For j As Integer = 1 To m
                Dim cost As Integer = If(t(j - 1) = s(i - 1), 0, 1)
                d(i, j) = Math.Min(Math.Min(
                                d(i - 1, j) + 1,
                                d(i, j - 1) + 1),
                                d(i - 1, j - 1) + cost)
            Next
        Next

        Return d(n, m)
    End Function

    Public Sub SearchAndScroll(searchText As String)
        ' Clear previous highlights
        For Each lbl As Label In previousResults
            lbl.BackColor = SystemColors.Control
            lbl.ForeColor = SystemColors.ControlText
        Next
        previousResults.Clear()

        searchResults.Clear()
        currentResultIndex = -1
        searchText = searchText.ToLower().Trim()

        ' Target labels to search
        Dim targetNumbers As String() = {"3", "4", "5", "6", "7", "8", "9", "10", "14", "13", "12", "11", "18", "17", "16", "15", "22", "21", "20", "19", "26", "25", "24", "23", "30", "29", "28", "27", "34", "33", "32", "31", "38", "37", "36", "35", "39"}

        ' Search labels
        For Each num As String In targetNumbers
            Dim labelName As String = "Label" & num
            Dim lbl As Label = TryCast(Panel1.Controls(labelName), Label)
            If lbl IsNot Nothing Then
                Dim labelText As String = lbl.Text.ToLower().Trim()
                If labelText.Contains(searchText) Then
                    searchResults.Add(lbl)
                    previousResults.Add(lbl)
                End If
            End If
        Next

        ' Sort by position
        searchResults = searchResults.OrderBy(Function(lbl) lbl.Top).ToList()
        previousResults = searchResults.ToList()

        ' Highlight results
        For Each lbl As Label In searchResults
            lbl.BackColor = Color.Black
            lbl.ForeColor = Color.White
        Next

        ' Show first result
        If searchResults.Count > 0 Then
            currentResultIndex = 0
            ScrollToCurrentResult()
        Else
            MessageBox.Show("No matching label found.")
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3.PerformClick()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            ' Clear highlights
            For Each lbl As Label In previousResults
                lbl.BackColor = SystemColors.Control
                lbl.ForeColor = SystemColors.ControlText
            Next
            previousResults.Clear()
            searchResults.Clear()
            currentResultIndex = -1
            Timer1.Stop()
        Else
            Timer1.Stop()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        For Each lbl As Label In previousResults
            lbl.BackColor = SystemColors.Control
            lbl.ForeColor = SystemColors.ControlText
        Next
        previousResults.Clear()
    End Sub

    Private lastSearchTerm As String = ""

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if user is not a customer
        If login.userRole = "customer" Then
            MessageBox.Show("Access denied. Admin privileges required.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check admin role and show appropriate form
        Select Case login.adminRole.ToLower()
            Case "super_admin"
                admin.Show()
            Case "admin"
                admin.Show()
            Case "inventory_manager"
                inventorymanager.Show()
            Case "order_manager"
                ordermanager.Show()
            Case "financial_auditor"
                auditor.Show()
            Case Else
                MessageBox.Show("Unknown admin role. Please contact system administrator.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Select
    End Sub
End Class

