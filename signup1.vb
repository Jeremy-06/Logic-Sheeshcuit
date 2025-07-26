Public Class signup1
    Public Address As String
    Public Phone As String

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' Only allow letters, numbers, spaces, and common address characters
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetterOrDigit(e.KeyChar) AndAlso e.KeyChar <> " "c AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> "#"c AndAlso e.KeyChar <> "-"c Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if any TextBox is empty before proceeding
        Dim emptyFound As Boolean = False
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                If String.IsNullOrWhiteSpace(CType(ctrl, TextBox).Text) Then
                    emptyFound = True
                    Exit For
                End If
            End If
        Next

        If emptyFound Then
            MessageBox.Show("Please fill in all fields before proceeding.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Assign instance variables
        Address = TextBox1.Text.Trim()
        Phone = TextBox2.Text.Trim()
        Me.Hide()
        signup2.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Hide()
        signup.Show()
    End Sub
End Class