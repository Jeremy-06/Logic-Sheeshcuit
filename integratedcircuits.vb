Imports MySql.Data.MySqlClient

Public Class integratedcircuits
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product3Qty As Integer = 0
    Dim product4Qty As Integer = 0
    Dim product5Qty As Integer = 0
    Dim product6Qty As Integer = 0

    Private Sub integratedcircuits_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
    End Sub

    Private Sub Back_Click(sender As Object, e As EventArgs) Handles Back.Click
        Me.Hide()
        home.Show()
    End Sub

    Private Function ValidateCustomer() As Boolean
        ' Check if user is logged in
        If login.customerId = 0 Then
            MessageBox.Show("Please log in first to add items to cart.")
            Me.Hide()
            login.Show()
            Return False
        End If
        
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT COUNT(*) FROM customers WHERE customerId = {login.customerId}"
            cmd = New MySqlCommand(query, conn)
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            If count = 0 Then
                MessageBox.Show("Please create an account first to add items to cart.")
                Me.Hide()
                signup.Show()
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error validating customer: " & ex.Message)
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Function

    ' PRODUCT 3
    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 3
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
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")
            Else
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product3Qty = 0
            TextBox1.Text = 0
            minus_btn.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        product3Qty += 1
        TextBox1.Text = product3Qty.ToString()
        minus_btn.Enabled = True
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product3Qty > 0 Then
            product3Qty -= 1
            TextBox1.Text = product3Qty.ToString()
            If product3Qty <= 0 Then
                minus_btn.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 4
    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 4
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox2.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If

        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")
            Else
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product4Qty = 0
            TextBox2.Text = 0
            minus_btn1.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn1_Click(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product4Qty += 1
        TextBox2.Text = product4Qty.ToString()
        minus_btn1.Enabled = True
    End Sub

    Private Sub minus_btn1_Click(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product4Qty > 0 Then
            product4Qty -= 1
            TextBox2.Text = product4Qty.ToString()
            If product4Qty <= 0 Then
                minus_btn1.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 5
    Private Sub addtocart_btn2_Click(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 5
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox3.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If

        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")
            Else
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product5Qty = 0
            TextBox3.Text = 0
            minus_btn2.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn2_Click(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product5Qty += 1
        TextBox3.Text = product5Qty.ToString()
        minus_btn2.Enabled = True
    End Sub

    Private Sub minus_btn2_Click(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product5Qty > 0 Then
            product5Qty -= 1
            TextBox3.Text = product5Qty.ToString()
            If product5Qty <= 0 Then
                minus_btn2.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 6
    Private Sub addtocart_btn3_Click(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 6
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox4.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If

        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")
            Else
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product6Qty = 0
            TextBox4.Text = 0
            minus_btn3.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn3_Click(sender As Object, e As EventArgs) Handles plus_btn3.Click
        product6Qty += 1
        TextBox4.Text = product6Qty.ToString()
        minus_btn3.Enabled = True
    End Sub

    Private Sub minus_btn3_Click(sender As Object, e As EventArgs) Handles minus_btn3.Click
        If product6Qty > 0 Then
            product6Qty -= 1
            TextBox4.Text = product6Qty.ToString()
            If product6Qty <= 0 Then
                minus_btn3.Enabled = False
            End If
        End If
    End Sub
End Class