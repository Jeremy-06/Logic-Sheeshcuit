Imports MySql.Data.MySqlClient

Public Class orders
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim customerId As Integer = 0

    Private Sub orders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize customer ID
        Try
            If login.customerId > 0 Then
                customerId = login.customerId
                LoadOrders()
            Else
                MessageBox.Show("Please log in first to access orders.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error initializing orders: " & ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub LoadOrders(Optional statusFilter As String = "")
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Build query based on status filter
            Dim whereClause As String = $"WHERE o.customers_customerId = {customerId} AND o.orderStatus <> 'checkout'"
            If Not String.IsNullOrEmpty(statusFilter) Then
                whereClause += $" AND o.orderStatus = '{statusFilter}'"
            End If

            query = $"SELECT 
                        o.orderId,
                        p.productName,
                        oi.productQty AS numberOfItems,
                        p.productPrice,
                        (p.productPrice * oi.productQty) AS itemTotal,
                        IFNULL(NULLIF(o.orderDate, '0000-00-00'), NULL) AS orderDate,
                        o.orderStatus
                    FROM orders o
                    INNER JOIN customers c ON o.customers_customerId = c.customerId
                    INNER JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    INNER JOIN products p ON oi.products_productId = p.productId
                    {whereClause}
                    ORDER BY o.orderDate DESC, o.orderId, p.productName"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "orders")

            DataGridView1.DataSource = ds.Tables("orders")

            ' Configure DataGridView
            DataGridView1.Columns("orderId").HeaderText = "Order ID"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("numberOfItems").HeaderText = "Number of Items"
            DataGridView1.Columns("productPrice").HeaderText = "Unit Price"
            DataGridView1.Columns("itemTotal").HeaderText = "Item Total"
            DataGridView1.Columns("orderDate").HeaderText = "Date"
            DataGridView1.Columns("orderStatus").HeaderText = "Status"

            ' Format price columns
            DataGridView1.Columns("productPrice").DefaultCellStyle.Format = "₱#,##0.00"
            DataGridView1.Columns("itemTotal").DefaultCellStyle.Format = "₱#,##0.00"

            ' Enable multi-selection for highlighting similar order IDs
            DataGridView1.MultiSelect = True
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Catch ex As Exception
            MessageBox.Show("Error loading orders: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an order to cancel.")
            Return
        End If

        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("orderId").Value)
        Dim orderStatus As String = selectedRow.Cells("orderStatus").Value.ToString()

        If orderStatus = "completed" Then
            MessageBox.Show("Cannot cancel completed orders.")
            Return
        End If

        If orderStatus = "cancelled" Then
            MessageBox.Show("This order is already cancelled.")
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to cancel this order?", "Confirm Cancellation", MessageBoxButtons.YesNo)
        If confirm = DialogResult.No Then Return

        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Update order status to cancelled
            query = $"UPDATE orders SET orderStatus = 'cancelled' WHERE orderId = {orderId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show("Order cancelled successfully.")
            LoadOrders()

        Catch ex As Exception
            MessageBox.Show("Error cancelling order: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnViewReceipt_Click(sender As Object, e As EventArgs) Handles btnViewReceipt.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an order to view receipt.")
            Return
        End If

        Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
        Dim orderId As Integer = Convert.ToInt32(selectedRow.Cells("orderId").Value)
        Dim orderStatus As String = selectedRow.Cells("orderStatus").Value.ToString()

        If orderStatus <> "completed" Then
            MessageBox.Show("Receipt is only available for completed orders.")
            Return
        End If

        ' Show receipt form
        Dim receiptForm As New receipt()
        receiptForm.LoadReceipt(orderId)
        receiptForm.Show()
    End Sub

    Private Sub btnAllOrders_Click(sender As Object, e As EventArgs) Handles btnAllOrders.Click
        LoadOrders()
    End Sub

    Private Sub btnPaidOrders_Click(sender As Object, e As EventArgs) Handles btnPaidOrders.Click
        LoadOrders("paid")
    End Sub

    Private Sub btnCancelledOrders_Click(sender As Object, e As EventArgs) Handles btnCancelledOrders.Click
        LoadOrders("cancelled")
    End Sub

    Private Sub btnCompletedOrders_Click(sender As Object, e As EventArgs) Handles btnCompletedOrders.Click
        LoadOrders("completed")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        LoadOrders()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            HighlightSameOrderId(e.RowIndex)
        End If
    End Sub

    Private Sub HighlightSameOrderId(selectedRowIndex As Integer)
        Try
            ' Get the Order ID of the selected row
            Dim selectedOrderId As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("orderId").Value)

            ' Clear all selections first
            DataGridView1.ClearSelection()

            ' Select all rows with the same Order ID
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim rowOrderId As Integer = Convert.ToInt32(row.Cells("orderId").Value)
                If rowOrderId = selectedOrderId Then
                    row.Selected = True
                End If
            Next

        Catch ex As Exception
            ' Ignore errors in highlighting
        End Try
    End Sub
End Class