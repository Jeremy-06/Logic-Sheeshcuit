Public Class datasets
    Private Sub dsInventory_btn_Click(sender As Object, e As EventArgs) Handles dsInventory_btn.Click
        datainventory.Show()
    End Sub

    Private Sub dsCart_btn_Click(sender As Object, e As EventArgs) Handles dsCart_btn.Click
        datacart.Show()
    End Sub

    Private Sub dsManage_orders_btn_Click(sender As Object, e As EventArgs) Handles dsManage_orders_btn.Click
        dataorders.Show()
    End Sub

    Private Sub dsSales_btn_Click(sender As Object, e As EventArgs) Handles dsSales_btn.Click
        datasales.Show()
    End Sub

    Private Sub dsExpenses_btn_Click(sender As Object, e As EventArgs) Handles dsExpenses_btn.Click
        dataexpenses.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

    End Sub

    Private Sub dsUsers_btn_Click(sender As Object, e As EventArgs) Handles dsUsers_btn.Click
        datausers.Show()
    End Sub
End Class