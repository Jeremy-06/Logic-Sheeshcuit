Imports MySql.Data.MySqlClient

Public Class checkout
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim customerId As Integer = 0

    Private Sub checkout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize customer ID
        Try
            If login.customerId > 0 Then
                customerId = login.customerId
                LoadCheckoutOrders()
            Else
                MessageBox.Show("Please log in first to access checkout.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error initializing checkout: " & ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub LoadCheckoutOrders()
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Get all checkout orders for the current customer
            query = $"SELECT 
                        o.orderId,
                        o.orderDate,
                        o.orderStatus,
                        p.productId,
                        p.productName,
                        p.productPrice,
                        oi.productQty,
                        (p.productPrice * oi.productQty) AS totalPrice
                    FROM orders o
                    INNER JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    INNER JOIN products p ON oi.products_productId = p.productId
                    WHERE o.customers_customerId = {customerId} AND o.orderStatus = 'checkout'
                    ORDER BY o.orderDate DESC, p.productName"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "checkout")

            DataGridView1.DataSource = ds.Tables("checkout")

            ' Configure DataGridView
            DataGridView1.Columns("orderId").Visible = False
            DataGridView1.Columns("productId").Visible = False
            DataGridView1.Columns("orderDate").HeaderText = "Date"
            DataGridView1.Columns("orderStatus").HeaderText = "Status"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("productQty").HeaderText = "Quantity"
            DataGridView1.Columns("totalPrice").HeaderText = "Total"

            ' Calculate total
            CalculateTotal()

        Catch ex As Exception
            MessageBox.Show("Error loading checkout orders: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        CalculateTotal()
    End Sub

    Private Sub CalculateTotal()
        Dim total As Decimal = 0
        
        ' Calculate total from selected rows only
        For Each row As DataGridViewRow In DataGridView1.SelectedRows
            total += Convert.ToDecimal(row.Cells("totalPrice").Value)
        Next
        
        lblTotal.Text = $"Total: ₱{total:N2}"
    End Sub

    Private Sub btnCheckout_Click(sender As Object, e As EventArgs) Handles btnCheckout.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select items to pay.")
            Return
        End If

        ' Confirm payment for selected items
        Dim selectedCount As Integer = DataGridView1.SelectedRows.Count
        Dim confirm As DialogResult = MessageBox.Show($"Are you sure you want to pay for {selectedCount} selected item(s)?", "Confirm Payment", MessageBoxButtons.YesNo)
        If confirm = DialogResult.No Then Return

        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Create a single paid order
            query = $"INSERT INTO orders (orderDate, orderStatus, customers_customerId) VALUES (CURDATE(), 'paid', {customerId})"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            ' Get the new order ID
            query = "SELECT LAST_INSERT_ID()"
            cmd = New MySqlCommand(query, conn)
            Dim newOrderId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            ' Move only selected checkout items to the new paid order
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                Dim productId As Integer = Convert.ToInt32(row.Cells("productId").Value)
                Dim quantity As Integer = Convert.ToInt32(row.Cells("productQty").Value)
                Dim oldOrderId As Integer = Convert.ToInt32(row.Cells("orderId").Value)

                ' Update orderitems to point to new order
                query = $"UPDATE orderitems SET orders_orderId = {newOrderId} WHERE orders_orderId = {oldOrderId} AND products_productId = {productId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Subtract purchased quantity from inventory stock
                query = $"UPDATE inventory SET productStock = productStock - {quantity} WHERE products_productId = {productId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
            Next

            ' Delete checkout orders that have no more items
            query = $"DELETE FROM orders WHERE customers_customerId = {customerId} AND orderStatus = 'checkout' AND orderId NOT IN (SELECT DISTINCT orders_orderId FROM orderitems)"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"Payment successful! Order ID: {newOrderId}")

            ' Refresh the form
            LoadCheckoutOrders()

        Catch ex As Exception
            MessageBox.Show("Error processing payment: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select items to remove.")
            Return
        End If

        ' Confirm removal of selected items
        Dim selectedCount As Integer = DataGridView1.SelectedRows.Count
        Dim confirm As DialogResult = MessageBox.Show($"Are you sure you want to return {selectedCount} selected item(s) to cart?", "Confirm Return to Cart", MessageBoxButtons.YesNo)
        If confirm = DialogResult.No Then Return

        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Open()

            ' Return selected items back to cart
            For Each row As DataGridViewRow In DataGridView1.SelectedRows
                Dim orderId As Integer = Convert.ToInt32(row.Cells("orderId").Value)
                Dim productId As Integer = Convert.ToInt32(row.Cells("productId").Value)
                Dim quantity As Integer = Convert.ToInt32(row.Cells("productQty").Value)

                ' Check if item already exists in cart
                query = $"SELECT cartId, productQty FROM cart WHERE customers_customerId = {customerId} AND products_productId = {productId}"
                cmd = New MySqlCommand(query, conn)
                reader = cmd.ExecuteReader()

                If reader.Read() Then
                    ' Item exists in cart, update quantity
                    Dim cartId As Integer = Convert.ToInt32(reader("cartId"))
                    Dim currentQty As Integer = Convert.ToInt32(reader("productQty"))
                    Dim newQty As Integer = currentQty + quantity
                    reader.Close()

                    query = $"UPDATE cart SET productQty = {newQty} WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                Else
                    ' Item doesn't exist in cart, add new entry
                    reader.Close()
                    query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {quantity})"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End If

                ' Remove specific item from orderitems
                query = $"DELETE FROM orderitems WHERE orders_orderId = {orderId} AND products_productId = {productId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
            Next

            ' Delete checkout orders that have no more items
            query = $"DELETE FROM orders WHERE customers_customerId = {customerId} AND orderStatus = 'checkout' AND orderId NOT IN (SELECT DISTINCT orders_orderId FROM orderitems)"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()

            MessageBox.Show($"{selectedCount} item(s) returned to cart.")

            ' Refresh the form
            LoadCheckoutOrders()

        Catch ex As Exception
            MessageBox.Show("Error returning items to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        LoadCheckoutOrders()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cartForm As New cart()
        cartForm.Show()
        Me.Close()
    End Sub

    Private Sub btnBackToCart_Click(sender As Object, e As EventArgs) Handles btnBackToCart.Click
        ' Show the cart form and hide this form
        Dim cartForm As New cart()
        cartForm.Show()
        Me.Hide()
    End Sub
End Class