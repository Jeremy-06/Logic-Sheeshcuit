Imports MySql.Data.MySqlClient

Public Class makerlab
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product1Qty As Integer = 0
    Dim product2Qty As Integer = 0
    Dim product3Qty As Integer = 0

    Private Sub makerlab_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Back.Click
        Me.Close()
        home.Show()
    End Sub

    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Integer.TryParse(TextBox1.Text, product1Qty) Then
            Try
                conn.Open()

                ' Check if there's an existing cart item that hasn't been ordered
                query = "SELECT ca.cartId, ca.productQty
                     FROM cart ca
                     LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                     WHERE ca.products_productId = 1 AND ca.customers_customerId = 1 AND oi.cart_cartId IS NULL
                     LIMIT 1;"
                cmd = New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                If reader.Read() Then
                    ' Unordered cart item exists → update its quantity
                    Dim cartId As Integer = reader.GetInt32("cartId")
                    Dim existingQty As Integer = reader.GetInt32("productQty")
                    Dim newQty As Integer = existingQty + product1Qty
                    reader.Close()

                    query = $"UPDATE cart SET productQty = {newQty} WHERE cartId = {cartId}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                Else
                    ' No unordered cart item → insert new one
                    reader.Close()
                    query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES (1, 1, {product1Qty})"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End If

                MessageBox.Show("Product added to cart successfully!")
                cart.refreshData()
            Catch ex As Exception
                MessageBox.Show("Error adding product to cart: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("Please enter a valid number.")
        End If
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        product1Qty += 1
        TextBox1.Text = product1Qty.ToString()
        If product1Qty <= 0 Then
            product1Qty = 0
            TextBox1.Text = product1Qty.ToString()
            minus_btn.Enabled = False
        Else
            minus_btn.Enabled = True
        End If
    End Sub
    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        product1Qty -= 1
        TextBox1.Text = product1Qty.ToString()
        If product1Qty <= 0 Then
            product1Qty = 0
            TextBox1.Text = product1Qty.ToString()
            minus_btn.Enabled = False
        End If
    End Sub
    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Integer.TryParse(TextBox2.Text, product2Qty) Then
            ' product1Qty now contains the integer value from the textbox
            Try
                conn.Open()
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES (2, 1, '{product2Qty}')"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Product added to cart successfully!")
            Catch ex As Exception
                MessageBox.Show("Error adding product to cart: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("Please enter a valid number.")
        End If
    End Sub

    Private Sub plus_btn1_Click(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product2Qty += 1
        TextBox2.Text = product2Qty.ToString()
        If product2Qty <= 0 Then
            product2Qty = 0
            TextBox1.Text = product2Qty.ToString()
            minus_btn.Enabled = False
        Else
            minus_btn.Enabled = True
        End If
    End Sub
    Private Sub minus_btn1_Click(sender As Object, e As EventArgs) Handles minus_btn1.Click
        product2Qty -= 1
        TextBox2.Text = product2Qty.ToString()
        If product2Qty <= 0 Then
            product2Qty = 0
            TextBox2.Text = product2Qty.ToString()
            minus_btn1.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn2_Click(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        If Integer.TryParse(TextBox3.Text, product3Qty) Then
            ' product1Qty now contains the integer value from the textbox
            Try
                conn.Open()
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES (3, 1, '{product3Qty}')"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Product added to cart successfully!")
            Catch ex As Exception
                MessageBox.Show("Error adding product to cart: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("Please enter a valid number.")
        End If
    End Sub

    Private Sub plus_btn2_Click(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product3Qty += 1
        TextBox3.Text = product3Qty.ToString()
        If product3Qty <= 0 Then
            product3Qty = 0
            TextBox3.Text = product3Qty.ToString()
            minus_btn2.Enabled = False
        Else
            minus_btn2.Enabled = True
        End If
    End Sub
    Private Sub minus_btn2_Click(sender As Object, e As EventArgs) Handles minus_btn2.Click
        product3Qty -= 1
        TextBox3.Text = product3Qty.ToString()
        If product3Qty <= 0 Then
            product3Qty = 0
            TextBox3.Text = product3Qty.ToString()
            minus_btn2.Enabled = False
        End If
    End Sub
End Class