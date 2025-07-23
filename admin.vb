Imports MySql.Data.MySqlClient

Public Class admin
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
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
    'meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow
    'meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow meow
    'meow meow meow meow meo
End Class