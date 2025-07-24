Imports MySql.Data.MySqlClient

Public Class cart
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String
    Private Sub cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshData()
    End Sub
    Public Sub refreshData()
        Try
            conn.Open()
            query = "SELECT 
                    c.customerId,
                    p.productName,
                    p.productPrice,
                    ca.productQty
                FROM customers c
                INNER JOIN cart ca ON c.customerId = ca.customers_customerId
                INNER JOIN products p ON ca.products_productId = p.productId
                LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                WHERE oi.cart_cartId IS NULL
                ORDER BY p.productId;"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cart")
            DataGridView1.DataSource = ds.Tables("cart")

            DataGridView1.Columns("customerId").HeaderText = "Customer ID"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("productQty").HeaderText = "Quantity"
        Catch ex As Exception
            MessageBox.Show("Error loading cart: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim productId As Integer
        Dim productName As String
        Try
            conn.Open()
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                TextBox1.Text = row.Cells(1).Value.ToString()
                TextBox2.Text = row.Cells(2).Value.ToString()
                TextBox3.Text = row.Cells(3).Value.ToString()

                productName = row.cells(1).Value.ToString()
                ' Get the productId from productName

                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                productId = Convert.ToInt32(cmd.ExecuteScalar())
                If productId = 1 Then
                    PictureBox1.Image = My.Resources.Product_1
                ElseIf productId = 2 Then
                    PictureBox1.Image = My.Resources.Product_2
                ElseIf productId = 3 Then
                    PictureBox1.Image = My.Resources.Product_3
                ElseIf productId = 4 Then
                    PictureBox1.Image = My.Resources.Product_4
                ElseIf productId = 5 Then
                    PictureBox1.Image = My.Resources.Product_5
                ElseIf productId = 6 Then
                    PictureBox1.Image = My.Resources.Product_6
                ElseIf productId = 7 Then
                    PictureBox1.Image = My.Resources.Product_7
                ElseIf productId = 8 Then
                    PictureBox1.Image = My.Resources.Product_8
                ElseIf productId = 9 Then
                    PictureBox1.Image = My.Resources.Product_9
                ElseIf productId = 10 Then
                    PictureBox1.Image = My.Resources.Product_10
                ElseIf productId = 11 Then
                    PictureBox1.Image = My.Resources.Product_11
                ElseIf productId = 12 Then
                    PictureBox1.Image = My.Resources.Product_12
                ElseIf productId = 13 Then
                    PictureBox1.Image = My.Resources.Product_13
                ElseIf productId = 14 Then
                    PictureBox1.Image = My.Resources.Product_14
                ElseIf productId = 15 Then
                    PictureBox1.Image = My.Resources.Product_15
                ElseIf productId = 16 Then
                    PictureBox1.Image = My.Resources.Product_16
                ElseIf productId = 17 Then
                    PictureBox1.Image = My.Resources.Product_17
                ElseIf productId = 18 Then
                    PictureBox1.Image = My.Resources.Product_18
                ElseIf productId = 19 Then
                    PictureBox1.Image = My.Resources.Product_19
                ElseIf productId = 20 Then
                    PictureBox1.Image = My.Resources.Product_20
                ElseIf productId = 21 Then
                    PictureBox1.Image = My.Resources.Product_21
                ElseIf productId = 22 Then
                    PictureBox1.Image = My.Resources.Product_22
                ElseIf productId = 23 Then
                    PictureBox1.Image = My.Resources.Product_23
                ElseIf productId = 24 Then
                    PictureBox1.Image = My.Resources.Product_24
                ElseIf productId = 25 Then
                    PictureBox1.Image = My.Resources.Product_25
                ElseIf productId = 26 Then
                    PictureBox1.Image = My.Resources.Product_26
                ElseIf productId = 27 Then
                    PictureBox1.Image = My.Resources.Product_27
                ElseIf productId = 28 Then
                    PictureBox1.Image = My.Resources.Product_28
                ElseIf productId = 29 Then
                    PictureBox1.Image = My.Resources.Product_29
                ElseIf productId = 30 Then
                    PictureBox1.Image = My.Resources.Product_30
                ElseIf productId = 31 Then
                    PictureBox1.Image = My.Resources.Product_31
                ElseIf productId = 32 Then
                    PictureBox1.Image = My.Resources.Product_32
                ElseIf productId = 33 Then
                    PictureBox1.Image = My.Resources.Product_33
                ElseIf productId = 34 Then
                    PictureBox1.Image = My.Resources.Product_34
                ElseIf productId = 35 Then
                    PictureBox1.Image = My.Resources.Product_35
                ElseIf productId = 36 Then
                    PictureBox1.Image = My.Resources.Product_36
                ElseIf productId = 37 Then
                    PictureBox1.Image = My.Resources.Product_37
                Else
                    PictureBox1.Image = Nothing
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
        conn.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected values
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("customerId").Value)
            Dim productName As String = DataGridView1.SelectedRows(0).Cells("productName").Value.ToString()

            ' Optional: Confirm delete
            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this item from the cart?", "Confirm Delete", MessageBoxButtons.YesNo)
            If confirm = DialogResult.No Then Exit Sub

            Try
                conn.Open()

                ' Get the productId from productName
                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)

                Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Now delete from cart using customerId and productId
                query = $"DELETE FROM cart WHERE customers_customerId = '{customerId}' AND products_productId = '{productId}'"
                cmd = New MySqlCommand(query, conn)

                cmd.ExecuteNonQuery()

                MessageBox.Show("Item deleted successfully.")
                refreshData()
            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
        refreshData()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Me.Hide()
        orders.Show()

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("customerId").Value)
            Dim productName As String = DataGridView1.SelectedRows(0).Cells("productName").Value.ToString()
            Dim productQty As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("productQty").Value)

            Try
                conn.Open()

                ' Get productId
                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Get cartId
                query = $"SELECT cartId FROM cart WHERE products_productId = {productId} AND customers_customerId = {customerId} AND productQty = {productQty}"
                cmd = New MySqlCommand(query, conn)
                Dim cartIdObj = cmd.ExecuteScalar()
                If cartIdObj Is Nothing Then
                    MessageBox.Show("Cart item not found.")
                    conn.Close()
                    Exit Sub
                End If
                Dim cartId As Integer = Convert.ToInt32(cartIdObj)

                ' Insert into sales
                query = "INSERT INTO sales (salesDate) VALUES (CURDATE())"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Get salesId
                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                Dim salesId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Insert into orders
                query = $"INSERT INTO orders (orderDate, orderStatus, sales_salesId) VALUES (CURDATE(), 'Pending', {salesId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Get orderId
                query = "SELECT LAST_INSERT_ID();"
                cmd = New MySqlCommand(query, conn)
                Dim orderId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Insert into orderitems
                query = "INSERT INTO orderitems (productQty, orders_orderId, cart_cartId, cart_products_productId, cart_customers_customerId) " &
                        $"VALUES ({productQty}, {orderId}, {cartId}, {productId}, {customerId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Order placed successfully.")

            Catch ex As Exception
                MessageBox.Show("Error placing order: " & ex.Message)
            Finally
                conn.Close()
                refreshData() ' Refresh grid to hide ordered item
            End Try
        Else
            MessageBox.Show("Please select an item to order.")
        End If
    End Sub

End Class