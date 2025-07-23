Public Class auditor
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        MessageBox.Show("Logging Out...", "Goodbye, auditor.", MessageBoxButtons.OK, MessageBoxIcon.Information)
        home.Show()
    End Sub
End Class