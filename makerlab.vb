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
        '  Try
        '      conn.Open()
        '      query = "SELECT productPrice FROM products WHERE productId = 1"
        '      cmd = New MySqlCommand(query, conn)
        '      reader = cmd.ExecuteReader()
        '      If reader.Read() Then
        '          product1price = reader.GetDecimal("productPrice")
        '      End If
        '  Catch ex As Exception
        '      MessageBox.Show("Error loading product price: " & ex.Message)
        '  Finally
        '      conn.Close()
        '  End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Back.Click
        Me.Close()
        home.Show()
    End Sub

    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Integer.TryParse(TextBox1.Text, product1Qty) Then
            Try
                conn.Open()

                ' Check if the product is already in the cart for the customer
                query = "SELECT productQty FROM cart WHERE products_productId = 1 AND customers_customerId = 1"
                cmd = New MySqlCommand(query, conn)
                Dim existingQty As Object = cmd.ExecuteScalar()

                If existingQty IsNot Nothing Then
                    ' Product exists, update the quantity
                    Dim newQty As Integer = CInt(existingQty) + product1Qty
                    query = $"UPDATE cart SET productQty = {newQty} WHERE products_productId = 1 AND customers_customerId = 1"
                Else
                    ' Product does not exist, insert new row
                    query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES (1, 1, {product1Qty})"
                End If

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