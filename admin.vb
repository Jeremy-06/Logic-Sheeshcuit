Imports MySql.Data.MySqlClient

Public Class admin
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            conn.Open()
            Select Case ComboBox1.Text.ToLower()
                Case "cart", "customers", "expenses", "inventory", "orderitems",
                     "orders", "productcategories", "products", "sales", "suppliers", "users"

                    query = $"SELECT * FROM {ComboBox1.Text}"
                    cmd = New MySqlCommand(query, conn)
                    da = New MySqlDataAdapter(cmd)
                    ds = New DataSet()
                    da.Fill(ds, ComboBox1.Text)
                    DataGridView1.DataSource = ds.Tables(ComboBox1.Text)

                Case Else
                    MessageBox.Show("Please select a valid table from the dropdown.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Select
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            login.adminId = 0
            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Me.Close()
            datasets.Close()
            home.Button1.Visible = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        datasets.Show()
    End Sub

    'meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow
    'meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow
    'meow meow meow meow meow

End Class