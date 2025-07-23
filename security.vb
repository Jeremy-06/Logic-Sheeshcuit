Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class security

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            conn.Open()
            Select Case ComboBox1.Text.ToLower()
                Case "users"

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
        Me.Hide()
        MessageBox.Show("Logging Out...", "Goodbye, Data Editor.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        home.Show()
    End Sub
End Class