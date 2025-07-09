Public Class logo
    Private splashTimer As Timer

    Private Sub logo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set form properties for splash screen effect
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.TopMost = True

        ' Initialize and start timer
        splashTimer = New Timer()
        splashTimer.Interval = 3000 '
        AddHandler splashTimer.Tick, AddressOf SplashTimer_Tick
        splashTimer.Start()
    End Sub

    Private Sub SplashTimer_Tick(sender As Object, e As EventArgs)
        splashTimer.Stop()
        splashTimer.Dispose()
        Me.Hide()
        Form1.Show() ' Show the main form after the splash screen
    End Sub
End Class