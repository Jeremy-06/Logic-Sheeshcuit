Public Class datasets
    Private Sub datasets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Check admin role and enable/disable buttons accordingly
        CheckAdminPermissions()
    End Sub

    Private Sub CheckAdminPermissions()
        ' Disable all buttons by default
        dsInventory_btn.Enabled = False
        dsCart_btn.Enabled = False
        dsManage_orders_btn.Enabled = False
        dsSales_btn.Enabled = False
        dsExpenses_btn.Enabled = False
        Button8.Enabled = False
        dsUsers_btn.Enabled = False

        ' Enable buttons based on admin role
        Select Case login.adminRole.ToLower()
            Case "inventory_manager"
                ' Inventory manager can access inventory and cart
                dsInventory_btn.Enabled = True
                dsCart_btn.Enabled = True
            Case "order_manager"
                ' Order manager can access orders and cart
                dsManage_orders_btn.Enabled = True
                dsCart_btn.Enabled = True
            Case "financial_auditor"
                ' Financial auditor can access sales, expenses, and net income report
                dsSales_btn.Enabled = True
                dsExpenses_btn.Enabled = True
                Button8.Enabled = True
            Case "admin"
                ' Full admin can access everything
                dsInventory_btn.Enabled = True
                dsCart_btn.Enabled = True
                dsManage_orders_btn.Enabled = True
                dsSales_btn.Enabled = True
                dsExpenses_btn.Enabled = True
                Button8.Enabled = True
                dsUsers_btn.Enabled = True
            Case "super_admin"
                ' Super admin has access to everything
                dsInventory_btn.Enabled = True
                dsCart_btn.Enabled = True
                dsManage_orders_btn.Enabled = True
                dsSales_btn.Enabled = True
                dsExpenses_btn.Enabled = True
                Button8.Enabled = True
                dsUsers_btn.Enabled = True
        End Select
    End Sub

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
        netincomereport.Show()
    End Sub

    Private Sub dsUsers_btn_Click(sender As Object, e As EventArgs) Handles dsUsers_btn.Click
        datausers.Show()
    End Sub
End Class