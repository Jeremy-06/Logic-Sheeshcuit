Imports MySql.Data.MySqlClient

Public Class datausers

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub datausers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUserData()
    End Sub

    Private Sub LoadUserData()
        Try
            conn.Open()

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
                            a.isActive,
                            a.lastLogin,
                            a.createdAt
                         FROM users u
                         LEFT JOIN admin_users a ON u.userId = a.userId
                         WHERE u.userRole IN ('admin', 'inventory_manager', 'order_manager', 'financial_auditor')
                         ORDER BY u.userId;"
            Else
                ' Default query for all users
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
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshData()
        LoadUserData()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LoadUserData()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class