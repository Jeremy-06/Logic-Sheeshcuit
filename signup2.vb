Public Class signup2
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        MessageBox.Show("Sign Up Successful! Proceeding to Log In...", "Sign Up Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        login.Show()
    End Sub
End Class