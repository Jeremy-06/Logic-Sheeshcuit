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

    Private Sub cart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        refreshData()
    End Sub

    Public Sub refreshData()
        Try
            conn.Open()
            query = "SELECT 
                        ca.cartId,
                        c.customerId,
                        p.productName,
                        p.productPrice,
                        ca.productQty - IFNULL(SUM(oi.productQty), 0) AS remainingQty
                    FROM cart ca
                    INNER JOIN customers c ON ca.customers_customerId = c.customerId
                    INNER JOIN products p ON ca.products_productId = p.productId
                    LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                    GROUP BY ca.cartId
                    HAVING remainingQty > 0
                    ORDER BY p.productId;"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cart")
            DataGridView1.DataSource = ds.Tables("cart")

            DataGridView1.Columns("cartId").Visible = False ' Hide cartId
            DataGridView1.Columns("customerId").HeaderText = "Customer ID"
            DataGridView1.Columns("productName").HeaderText = "Product Name"
            DataGridView1.Columns("productPrice").HeaderText = "Price"
            DataGridView1.Columns("remainingQty").HeaderText = "Quantity"
        Catch ex As Exception
            MessageBox.Show("Error loading cart: " & ex.Message)
        Finally
            conn.Close()
        End Try
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
            conn.Close()
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim customerId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("customerId").Value)
            Dim productName As String = DataGridView1.SelectedRows(0).Cells("productName").Value.ToString()

            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this item from the cart?", "Confirm Delete", MessageBoxButtons.YesNo)
            If confirm = DialogResult.No Then Exit Sub

            Try
                conn.Open()
                query = $"SELECT productId FROM products WHERE productName = '{productName}'"
                cmd = New MySqlCommand(query, conn)
                Dim productId As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                query = $"DELETE FROM cart WHERE customers_customerId = '{customerId}' AND products_productId = '{productId}'"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Item deleted successfully.")
            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message)
            Finally
                conn.Close()
                refreshData()
            End Try
        Else
            MessageBox.Show("Please select a row to delete.")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        orders.Show()

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
                ' Clear all input fields and picture box
                TextBox1.Clear() ' Product name
                TextBox2.Clear() ' Product price
                TextBox3.Clear() ' Quantity
                PictureBox1.Image = Nothing
                plus_btn.Enabled = False
                minus_btn.Enabled = False

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