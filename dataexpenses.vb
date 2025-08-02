Imports MySql.Data.MySqlClient

'wait lang 'di pa yan tapos, icommit ko na raw kasi sabi ni Jirimiah Primavera 

Public Class dataexpenses

    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
        Dim cmd As MySqlCommand
        Dim da As MySqlDataAdapter
        Dim ds As DataSet
        Dim query As String

    Private Sub dataexpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryData()
    End Sub

    Private Sub LoadInventoryData()
        Try
            conn.Open()
            query = "SELECT 
                        expensesId, 
                        expenseDate, 
                        expenseDescription, 
                        expenseAmount,
                        expenseCategory
                     FROM expenses;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Expenses")
            DataGridView1.DataSource = ds.Tables("Expenses")
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub RefreshData()
        LoadInventoryData()
    End Sub
End Class
