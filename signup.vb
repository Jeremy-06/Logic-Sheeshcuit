Public Class signup
    Public FirstName As String
    Public LastName As String

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        login.Show()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> " "c Then
            e.Handled = True
        End If
        ' Allow only up to two spaces
        If e.KeyChar = " "c Then
            Dim text = CType(sender, TextBox).Text
            Dim spaceCount = text.Count(Function(c) c = " "c)
            If spaceCount >= 1 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> " "c Then
            e.Handled = True
        End If
        ' Allow only up to two spaces
        If e.KeyChar = " "c Then
            Dim text = CType(sender, TextBox).Text
            Dim spaceCount = text.Count(Function(c) c = " "c)
            If spaceCount >= 2 Then
                e.Handled = True
            End If
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
        
        FirstName = TextBox1.Text.Trim()
        LastName = TextBox2.Text.Trim()
        Me.Hide()
        signup1.Show()
    End Sub

End Class