Imports MySql.Data.MySqlClient

Public Class cart
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim cartQty As Integer = 0
    Dim orderQty As Integer = 0
    Dim selectedCartId As Integer = 0

    Dim customerId As Integer = login.customerId
    Private Sub cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshData()
    End Sub

    Public Sub refreshData()
        Button2.Enabled = False
        Try
            conn.Open()
            query = $"SELECT 
                        ca.cartId,
                        c.customerId,
                        p.productName,
                        p.productPrice,
                        ca.productQty - COALESCE(SUM(oi.productQty), 0) AS remainingQty
                    FROM cart ca
                    INNER JOIN customers c ON ca.customers_customerId = c.customerId
                    INNER JOIN products p ON ca.products_productId = p.productId
                    LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                    WHERE c.customerId = {customerId}
                    GROUP BY ca.cartId, c.customerId, p.productName, p.productPrice, ca.productQty
                    HAVING remainingQty > 0
                    ORDER BY p.productId;"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cart")
            DataGridView1.DataSource = ds.Tables("cart")

            DataGridView1.Columns("cartId").Visible = False ' Hide cartId
            DataGridView1.Columns("customerId").Visible = False
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("remainingQty").HeaderText = "Quantity"

        Catch ex As Exception
            MessageBox.Show("Error loading cart: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
    Private Sub clearinput()
        ' Clear all input fields and picture box
        TextBox1.Clear() ' Product name
        TextBox2.Clear() ' Product price
        TextBox3.Clear() ' Quantity
        PictureBox1.Image = Nothing
        plus_btn.Enabled = False
        minus_btn.Enabled = False

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim productId As Integer
        Dim productName As String
        plus_btn.Enabled = True
        minus_btn.Enabled = True
        Try
            conn.Open()
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells("productName").Value.ToString()
                TextBox2.Text = row.Cells("productPrice").Value.ToString()

                ' MODIFIED: Get cart ID and quantities
                selectedCartId = Convert.ToInt32(row.Cells("cartId").Value)
                cartQty = Convert.ToInt32(row.Cells("remainingQty").Value)
                orderQty = cartQty
                TextBox3.Text = orderQty.ToString()

                productName = row.Cells("productName").Value.ToString()

                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                productId = Convert.ToInt32(cmd.ExecuteScalar())

                ' Set product image
                Select Case productId
                    Case 1 To 37
                        PictureBox1.Image = CType(My.Resources.ResourceManager.GetObject($"Product_{productId}"), Image)
                    Case Else
                        PictureBox1.Image = Nothing
                End Select
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            Button2.Enabled = True
            conn.Close()
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim cartId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("cartId").Value)
            Dim remainingQty As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("remainingQty").Value)
            Dim productName As String = DataGridView1.SelectedRows(0).Cells("productName").Value.ToString()

            ' Get the quantity to remove from the user input (TextBox3)
            Dim qtyToRemove As Integer
            If Not Integer.TryParse(TextBox3.Text, qtyToRemove) Then
                MessageBox.Show("Please enter a valid quantity to remove.")
                Return
            End If

            If qtyToRemove <= 0 Then
                MessageBox.Show("Please enter a quantity greater than 0.")
                Return
            End If

            If qtyToRemove > remainingQty Then
                MessageBox.Show($"Cannot remove {qtyToRemove} items. Only {remainingQty} items are available in cart.")
                Return
            End If

            Dim confirm As DialogResult = MessageBox.Show($"Are you sure you want to remove {qtyToRemove} {productName} from your cart?", "Confirm Remove", MessageBoxButtons.YesNo)
            If confirm = DialogResult.No Then Exit Sub

            Try
                conn.Open()

                ' Get the total ordered quantity for this cart item
                query = $"SELECT COALESCE(SUM(productQty), 0) FROM orderitems WHERE cart_cartId = {cartId}"
                cmd = New MySqlCommand(query, conn)
                Dim totalOrdered As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Calculate new cart quantity after removal
                Dim newCartQty As Integer = (totalOrdered + remainingQty) - qtyToRemove

                If newCartQty > 0 Then
                    ' Update cart quantity
                    query = $"UPDATE cart SET productQty = {newCartQty} WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    Dim newRemainingQty As Integer = newCartQty - totalOrdered
                    MessageBox.Show($"Removed {qtyToRemove} items from cart. Remaining quantity: {newRemainingQty}")
                Else
                    ' If removing all items, delete the entire cart item
                    query = $"DELETE FROM cart WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("All items removed from cart.")
                End If

            Catch ex As Exception
                MessageBox.Show("Error removing item: " & ex.Message)
            Finally
                conn.Close()
                refreshData()
                clearinput()
            End Try
        Else
            MessageBox.Show("Please select a row to remove.")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'orders.Show()

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("customerId").Value)
            Dim productName As String = DataGridView1.SelectedRows(0).Cells("productName").Value.ToString()

            Try
                conn.Open()

                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                Dim cartId As Integer = selectedCartId

                ' Insert into sales
                query = "INSERT INTO sales (salesDate) VALUES (CURDATE())"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                Dim salesId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                query = $"INSERT INTO orders (orderDate, orderStatus, sales_salesId) VALUES (CURDATE(), 'Pending', {salesId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                Dim orderId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Insert into orderitems (just record how much was ordered)
                query = "INSERT INTO orderitems (productQty, orders_orderId, cart_cartId, cart_products_productId, cart_customers_customerId) " &
                    $"VALUES ({orderQty}, {orderId}, {cartId}, {productId}, {customerId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Order placed successfully.")

            Catch ex As Exception
                MessageBox.Show("Error placing order: " & ex.Message)
            Finally
                conn.Close()
                refreshData()
                clearinput()
            End Try
        Else
            MessageBox.Show("Please select an item to order.")
        End If
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
End Class