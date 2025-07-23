
Public Class home
    Private smoothScrollTarget As Integer = 0
    Private WithEvents smoothScrollTimer As New Timer With {.Interval = 10}

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        Application.Exit()
    End Sub

    Private Sub MakerlabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakerlabToolStripMenuItem.Click
        makerlab.Show()
        Me.Hide()
    End Sub

    Private Sub CircuitrocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircuitrocksToolStripMenuItem.Click
        circuitrocks.Show()
        Me.Hide()
    End Sub

    Private Sub Element14ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Element14ToolStripMenuItem.Click
        element14.Show()
        Me.Hide()
    End Sub

    Private Sub RSComponentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RSComponentsToolStripMenuItem.Click
        rscomponents.Show()
        Me.Hide()
    End Sub

    Private Sub DisplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayToolStripMenuItem.Click
        digitaldisplays.Show()
        Me.Hide()
    End Sub

    Private Sub ICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ICToolStripMenuItem.Click
        integratedcircuits.Show()
        Me.Hide()
    End Sub

    Private Sub LEDsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEDsToolStripMenuItem.Click
        led.Show()
        Me.Hide()
    End Sub

    Private Sub PowerSupplyAndModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PowerSupplyAndModuleToolStripMenuItem.Click
        powersupply.Show()
        Me.Hide()
    End Sub

    Private Sub WiresAndCablesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WiresAndCablesToolStripMenuItem.Click
        wires.Show()
        Me.Hide()
    End Sub

    Private Sub BreadboardsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BreadboardsToolStripMenuItem.Click
        breadboards.Show()
        Me.Hide()
    End Sub

    Private Sub SwitchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchesToolStripMenuItem.Click
        switches.Show()
        Me.Hide()
    End Sub

    Private Sub ResistorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResistorsToolStripMenuItem.Click
        resistor.Show()
        Me.Hide()
    End Sub

    Private Sub CapacitorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CapacitorsToolStripMenuItem.Click
        capacitors.Show()
        Me.Hide()
    End Sub

    Private Sub OscilloscopesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OscilloscopesToolStripMenuItem.Click
        oscilloscopes.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dataproducts.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cart.Show()
    End Sub

    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class

