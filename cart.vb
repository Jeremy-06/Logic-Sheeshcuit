Imports MySql.Data.MySqlClient

Public Class cart
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String

    Private cartQty As Integer = 0
    Private orderQty As Integer = 0
    Private selectedCartId As Integer = 0
    Private customerId As Integer = 0

    Private Sub cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeCart()
    End Sub

    Private Sub InitializeCart()
        Try
            If login IsNot Nothing AndAlso login.customerId > 0 Then
                customerId = login.customerId
            Else
                customerId = 0
            End If

            LoadCartData()
        Catch ex As Exception
            customerId = 0
            LoadCartData()
        End Try
    End Sub

    Private Sub LoadCartData()
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            If customerId > 0 Then
                ' User is logged in - load their cart items
                query = $"SELECT 
                            ca.cartId,
                            c.customerId,
                            p.productName,
                            p.productPrice,
                            ca.productQty AS remainingQty
                        FROM cart ca
                        INNER JOIN customers c ON ca.customers_customerId = c.customerId
                        INNER JOIN products p ON ca.products_productId = p.productId
                        WHERE c.customerId = {customerId}
                        ORDER BY p.productName"
            Else
                ' User not logged in - show empty cart
                query = "SELECT 
                            NULL as cartId,
                            NULL as customerId,
                            'No items in cart' as productName,
                            0 as productPrice,
                            0 as remainingQty
                        WHERE 1 = 0"
            End If

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cart")

            DataGridView1.DataSource = ds.Tables("cart")
            ConfigureDataGridView()

        Catch ex As Exception
            MessageBox.Show("Error loading cart: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub ConfigureDataGridView()
        Try
            ' Hide technical columns
            DataGridView1.Columns("cartId").Visible = False
            DataGridView1.Columns("customerId").Visible = False

            ' Set display names
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("remainingQty").HeaderText = "Quantity"

            ' Format price column
            DataGridView1.Columns("productPrice").DefaultCellStyle.Format = "₱#,##0.00"

        Catch ex As Exception
            ' Ignore errors in grid configuration
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If Not IsUserLoggedIn() Then
            ShowLoginMessage()
            Return
        End If

        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("No items in cart to select.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            If e.RowIndex >= 0 AndAlso e.RowIndex < DataGridView1.Rows.Count Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

                If row IsNot Nothing AndAlso row.Cells("productName").Value IsNot Nothing Then
                    LoadSelectedItem(row)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSelectedItem(row As DataGridViewRow)
        Try
            ' Load item details into form controls
            TextBox1.Text = row.Cells("productName").Value.ToString()
            TextBox2.Text = row.Cells("productPrice").Value.ToString()

            selectedCartId = Convert.ToInt32(row.Cells("cartId").Value)
            cartQty = Convert.ToInt32(row.Cells("remainingQty").Value)
            orderQty = cartQty
            TextBox3.Text = orderQty.ToString()

            ' Enable quantity controls
            plus_btn.Enabled = True
            minus_btn.Enabled = True
            Button2.Enabled = True

            ' Load product image
            LoadProductImage(row.Cells("productName").Value.ToString())

        Catch ex As Exception
            MessageBox.Show("Error loading item details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadProductImage(productName As String)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            query = $"SELECT productId FROM products WHERE productName = '{productName}'"
            cmd = New MySqlCommand(query, conn)
            Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            ' Set product image based on product ID
            Select Case productId
                Case 1 To 37
                    PictureBox1.Image = CType(My.Resources.ResourceManager.GetObject($"Product_{productId}"), Image)
                Case Else
                    PictureBox1.Image = Nothing
            End Select

        Catch ex As Exception
            PictureBox1.Image = Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Move to Checkout button
        If Not IsUserLoggedIn() Then
            ShowLoginMessage()
            Return
        End If

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item to move to checkout.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim qtyToMove As Integer
        If Not Integer.TryParse(TextBox3.Text, qtyToMove) OrElse qtyToMove <= 0 Then
            MessageBox.Show("Please enter a valid quantity greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        MoveItemToCheckout(qtyToMove)
    End Sub

    Private Sub MoveItemToCheckout(qtyToMove As Integer)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            If selectedRow IsNot Nothing AndAlso selectedRow.Cells("cartId").Value IsNot Nothing Then
                Dim cartId As Integer = Convert.ToInt32(selectedRow.Cells("cartId").Value)
                Dim productName As String = selectedRow.Cells("productName").Value.ToString()
                Dim totalQty As Integer = Convert.ToInt32(selectedRow.Cells("remainingQty").Value)

                If qtyToMove > totalQty Then
                    MessageBox.Show($"Cannot move {qtyToMove} items. Only {totalQty} items are available in cart.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                ' Get product ID
                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Check if item already exists in checkout
                Dim existingCheckoutItem = GetExistingCheckoutItem(productId)

                If existingCheckoutItem IsNot Nothing Then
                    ' Update existing checkout item
                    UpdateCheckoutItem(existingCheckoutItem, qtyToMove)
                Else
                    ' Create new checkout item
                    CreateNewCheckoutItem(productId, qtyToMove)
                End If

                ' Update cart quantity
                UpdateCartQuantity(cartId, totalQty, qtyToMove)

                MessageBox.Show($"{qtyToMove} {productName} moved to checkout.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                LoadCartData()
                ClearInputs()
            End If

        Catch ex As Exception
            MessageBox.Show("Error moving item to checkout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Function GetExistingCheckoutItem(productId As Integer) As Object
        query = $"SELECT oi.orderItemsId, oi.productQty, o.orderId 
                 FROM orderitems oi 
                 INNER JOIN orders o ON oi.orders_orderId = o.orderId 
                 WHERE o.customers_customerId = {customerId} 
                 AND o.orderStatus = 'checkout' 
                 AND oi.products_productId = {productId}"
        cmd = New MySqlCommand(query, conn)
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            Dim result = New With {
                .orderItemsId = Convert.ToInt32(reader("orderItemsId")),
                .currentQty = Convert.ToInt32(reader("productQty")),
                .orderId = Convert.ToInt32(reader("orderId"))
            }
            reader.Close()
            Return result
        Else
            reader.Close()
            Return Nothing
        End If
    End Function

    Private Sub UpdateCheckoutItem(existingItem As Object, qtyToMove As Integer)
        Dim newQty As Integer = existingItem.currentQty + qtyToMove
        query = $"UPDATE orderitems SET productQty = {newQty} WHERE orderItemsId = {existingItem.orderItemsId}"
        cmd = New MySqlCommand(query, conn)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub CreateNewCheckoutItem(productId As Integer, qtyToMove As Integer)
        ' Create new checkout order
        query = $"INSERT INTO orders (orderDate, orderStatus, customers_customerId) VALUES (CURDATE(), 'checkout', {customerId})"
        cmd = New MySqlCommand(query, conn)
        cmd.ExecuteNonQuery()

        ' Get the new order ID
        query = "SELECT LAST_INSERT_ID()"
        cmd = New MySqlCommand(query, conn)
        Dim orderId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

        ' Add to orderitems
        query = $"INSERT INTO orderitems (productQty, orders_orderId, products_productId) VALUES ({qtyToMove}, {orderId}, {productId})"
        cmd = New MySqlCommand(query, conn)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub UpdateCartQuantity(cartId As Integer, totalQty As Integer, qtyToMove As Integer)
        Dim remainingQty As Integer = totalQty - qtyToMove

        If remainingQty > 0 Then
            ' Update cart with remaining quantity
            query = $"UPDATE cart SET productQty = {remainingQty} WHERE cartId = {cartId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
        Else
            ' Delete cart record if all items moved
            query = $"DELETE FROM cart WHERE cartId = {cartId}"
            cmd = New MySqlCommand(query, conn)
            cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Remove Item button
        If Not IsUserLoggedIn() Then
            ShowLoginMessage()
            Return
        End If

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an item to remove.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim qtyToRemove As Integer
        If Not Integer.TryParse(TextBox3.Text, qtyToRemove) OrElse qtyToRemove <= 0 Then
            MessageBox.Show("Please enter a valid quantity greater than 0.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        RemoveItemFromCart(qtyToRemove)
    End Sub

    Private Sub RemoveItemFromCart(qtyToRemove As Integer)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If

            conn.Open()

            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)

            If selectedRow IsNot Nothing AndAlso selectedRow.Cells("cartId").Value IsNot Nothing Then
                Dim cartId As Integer = Convert.ToInt32(selectedRow.Cells("cartId").Value)
                Dim remainingQty As Integer = Convert.ToInt32(selectedRow.Cells("remainingQty").Value)
                Dim productName As String = selectedRow.Cells("productName").Value.ToString()

                If qtyToRemove > remainingQty Then
                    MessageBox.Show($"Cannot remove {qtyToRemove} items. Only {remainingQty} items are available in cart.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim confirm As DialogResult = MessageBox.Show($"Are you sure you want to remove {qtyToRemove} {productName} from your cart?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.No Then Return

                Dim newCartQty As Integer = remainingQty - qtyToRemove

                If newCartQty > 0 Then
                    ' Update cart quantity
                    query = $"UPDATE cart SET productQty = {newCartQty} WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show($"Removed {qtyToRemove} items from cart. Remaining quantity: {newCartQty}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ' Delete cart record
                    query = $"DELETE FROM cart WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("All items removed from cart.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                LoadCartData()
                ClearInputs()
            End If

        Catch ex As Exception
            MessageBox.Show("Error removing item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub btnGoToStaging_Click(sender As Object, e As EventArgs) Handles btnGoToStaging.Click
        ' Go to Checkout button
        If Not IsUserLoggedIn() Then
            ShowLoginMessage()
            Return
        End If

        Try
            Dim checkoutForm As New checkout()
            checkoutForm.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Error opening checkout: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOrderManager_Click(sender As Object, e As EventArgs) Handles btnOrderManager.Click
        ' Order Manager button
        If Not IsUserLoggedIn() Then
            ShowLoginMessage()
            Return
        End If

        Try
            Dim orderManagerForm As New orders()
            orderManagerForm.Show()
        Catch ex As Exception
            MessageBox.Show("Error opening order manager: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        If orderQty < cartQty Then
            orderQty += 1
            TextBox3.Text = orderQty.ToString()
        End If
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        If orderQty > 1 Then
            orderQty -= 1
            TextBox3.Text = orderQty.ToString()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Back button
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Refresh button
        LoadCartData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Close button
        Me.Close()
    End Sub

    ' Helper methods
    Private Function IsUserLoggedIn() As Boolean
        Try
            Return login IsNot Nothing AndAlso login.customerId > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ShowLoginMessage()
        MessageBox.Show("Please log in to perform this action.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub ClearInputs()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        PictureBox1.Image = Nothing
        plus_btn.Enabled = False
        minus_btn.Enabled = False
        Button2.Enabled = False
    End Sub

    ' Public method to refresh cart from other forms
    Public Sub RefreshCartData()
        LoadCartData()
    End Sub

    ' Public method for backward compatibility with other forms
    Public Sub refreshData()
        LoadCartData()
    End Sub
End Class
