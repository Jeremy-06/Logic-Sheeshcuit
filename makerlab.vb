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
    Dim product7Qty As Integer = 0



    Private Sub makerlab_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Back.Click
        Me.Close()
        home.Show()
    End Sub

    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        Dim customerId = login.customerId
        Dim productId As Integer = 1 ' Change this if needed
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox1.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If

        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If

        Try
            conn.Open()

            ' Check if there's an existing cart item for this product and customer
            query = $"SELECT cartId, productQty 
                 FROM cart 
                 WHERE products_productId = {productId} 
                   AND customers_customerId = {customerId}
                 LIMIT 1"

            cmd = New MySqlCommand(query, conn)
            Dim existingCartId = cmd.ExecuteScalar()

            If existingCartId IsNot Nothing Then
                ' Found existing cart item - update it (add to existing quantity)
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {existingCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")
            Else
                ' No cart item exists for this product - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) 
                     VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            conn.Close()
        End Try
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