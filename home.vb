
Public Class home
    Private smoothScrollTarget As Integer = 0
    Private WithEvents smoothScrollTimer As New Timer With {.Interval = 10}

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        Application.Exit()
    End Sub

    Private Sub ICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ICToolStripMenuItem.Click
        products0.Show()
    End Sub

End Class

