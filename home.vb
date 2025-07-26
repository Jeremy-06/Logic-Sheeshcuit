
Public Class home
    Private smoothScrollTarget As Integer = 0
    Private WithEvents smoothScrollTimer As New Timer With {.Interval = 10}
    Private currentOpenForm As Form = Nothing

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        MyBase.OnFormClosing(e)
        Application.Exit()
    End Sub

    Private Sub HideCurrentForm()
        If currentOpenForm IsNot Nothing AndAlso Not currentOpenForm.IsDisposed Then
            currentOpenForm.Hide()
        End If
    End Sub

    Private Sub MakerlabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakerlabToolStripMenuItem.Click
        HideCurrentForm()
        makerlab.Show()
        currentOpenForm = makerlab
    End Sub

    Private Sub CircuitrocksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CircuitrocksToolStripMenuItem.Click
        HideCurrentForm()
        circuitrocks.Show()
        currentOpenForm = circuitrocks
    End Sub

    Private Sub Element14ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Element14ToolStripMenuItem.Click
        HideCurrentForm()
        element14.Show()
        currentOpenForm = element14
    End Sub

    Private Sub RSComponentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RSComponentsToolStripMenuItem.Click
        HideCurrentForm()
        rscomponents.Show()
        currentOpenForm = rscomponents
    End Sub

    Private Sub DisplayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayToolStripMenuItem.Click
        HideCurrentForm()
        digitaldisplays.Show()
        currentOpenForm = digitaldisplays
    End Sub

    Private Sub ICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ICToolStripMenuItem.Click
        HideCurrentForm()
        integratedcircuits.Show()
        currentOpenForm = integratedcircuits
    End Sub

    Private Sub LEDsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LEDsToolStripMenuItem.Click
        HideCurrentForm()
        led.Show()
        currentOpenForm = led
    End Sub

    Private Sub PowerSupplyAndModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PowerSupplyAndModuleToolStripMenuItem.Click
        HideCurrentForm()
        powersupply.Show()
        currentOpenForm = powersupply
    End Sub

    Private Sub WiresAndCablesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WiresAndCablesToolStripMenuItem.Click
        HideCurrentForm()
        wires.Show()
        currentOpenForm = wires
    End Sub

    Private Sub BreadboardsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BreadboardsToolStripMenuItem.Click
        HideCurrentForm()
        breadboards.Show()
        currentOpenForm = breadboards
    End Sub

    Private Sub SwitchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchesToolStripMenuItem.Click
        HideCurrentForm()
        switches.Show()
        currentOpenForm = switches
    End Sub

    Private Sub ResistorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResistorsToolStripMenuItem.Click
        HideCurrentForm()
        resistor.Show()
        currentOpenForm = resistor
    End Sub

    Private Sub CapacitorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CapacitorsToolStripMenuItem.Click
        HideCurrentForm()
        capacitors.Show()
        currentOpenForm = capacitors
    End Sub

    Private Sub OscilloscopesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OscilloscopesToolStripMenuItem.Click
        HideCurrentForm()
        oscilloscopes.Show()
        currentOpenForm = oscilloscopes
    End Sub

    Private Sub ScrollToCurrentResult()
        If currentResultIndex >= 0 AndAlso currentResultIndex < searchResults.Count Then
            Dim lbl As Label = searchResults(currentResultIndex)
            Panel1.ScrollControlIntoView(lbl)

            ' Optional: highlight
            lbl.BackColor = Color.Black
            lbl.ForeColor = Color.White
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If searchResults.Count = 0 Then
            MessageBox.Show("No results to go back to. Please search first.")
            Return
        End If
        ' Move to previous result, wrap around if needed
        currentResultIndex = (currentResultIndex - 1 + searchResults.Count) Mod searchResults.Count
        ScrollToCurrentResult()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If searchResults.Count = 0 Then
            MessageBox.Show("No results to go to. Please search first.")
            Return
        End If

        ' Move to next result, wrap around if needed
        currentResultIndex = (currentResultIndex + 1) Mod searchResults.Count
        ScrollToCurrentResult()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim searchTerm As String = TextBox1.Text.Trim()
        If Not String.IsNullOrWhiteSpace(searchTerm) Then
            SearchAndScroll(searchTerm)
        End If
    End Sub

    Private Sub PictureBox38_Click(sender As Object, e As EventArgs) Handles PictureBox38.Click

    End Sub

    Private Sub PictureBox39_Click(sender As Object, e As EventArgs) Handles PictureBox39.Click
        cart.Show()
    End Sub

    Private Sub PictureBox40_Click(sender As Object, e As EventArgs) Handles PictureBox40.Click

    End Sub

    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private searchResults As New List(Of Label)
    Private currentResultIndex As Integer = -1
    Private previousResults As New List(Of Label)

    Private Function LevenshteinDistance(s As String, t As String) As Integer
        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim d(n + 1, m + 1) As Integer

        If n = 0 Then Return m
        If m = 0 Then Return n

        For i As Integer = 0 To n
            d(i, 0) = i
        Next

        For j As Integer = 0 To m
            d(0, j) = j
        Next

        For i As Integer = 1 To n
            For j As Integer = 1 To m
                Dim cost As Integer = If(t(j - 1) = s(i - 1), 0, 1)
                d(i, j) = Math.Min(Math.Min(
                                d(i - 1, j) + 1,
                                d(i, j - 1) + 1),
                                d(i - 1, j - 1) + cost)
            Next
        Next

        Return d(n, m)
    End Function

    Public Sub SearchAndScroll(searchText As String)
        ' Reset previously highlighted labels
        For Each lbl As Label In previousResults
            lbl.BackColor = SystemColors.Control
            lbl.ForeColor = SystemColors.ControlText
        Next
        previousResults.Clear()

        searchResults.Clear()
        currentResultIndex = -1
        searchText = searchText.ToLower().Trim()

        ' Get all labels sorted top-to-bottom
        Dim orderedControls = Panel1.Controls.Cast(Of Control)().
                          Where(Function(c) TypeOf c Is Label).
                          OrderBy(Function(c) c.Top)

        ' Fuzzy match and collect all results
        For Each ctrl As Control In orderedControls
            Dim lbl As Label = CType(ctrl, Label)
            Dim labelText As String = lbl.Text.ToLower().Trim()

            Dim dist As Integer = LevenshteinDistance(searchText, labelText)
            Dim maxLen As Integer = Math.Max(searchText.Length, labelText.Length)
            Dim similarity As Double = 1 - (dist / maxLen)

            If similarity >= 0.8 Then
                searchResults.Add(lbl)
                previousResults.Add(lbl) ' Track for later reset
            End If
        Next

        ' Scroll to first result
        If searchResults.Count > 0 Then
            currentResultIndex = 0
            ScrollToCurrentResult()
        Else
            MessageBox.Show("No matching label found.")
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3.PerformClick()
        End If
    End Sub


End Class

