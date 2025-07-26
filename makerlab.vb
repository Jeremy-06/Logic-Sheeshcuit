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
    Dim product9Qty As Integer = 0
    Dim product12Qty As Integer = 0

    Private Sub makerlab_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
                ' Customer doesn't exist, redirect to signup
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

    Private Sub clearQty()
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Back.Click
        Me.Close()
        home.Show()
    End Sub

    ' PRODUCT 1 
    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 1
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

            ' First, find cart items for this product and customer
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        product1Qty += 1
        TextBox1.Text = product1Qty.ToString()
        minus_btn.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product1Qty > 0 Then
            product1Qty -= 1
            TextBox1.Text = product1Qty.ToString()
            If product1Qty <= 0 Then
                minus_btn.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 2
    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 2
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

            ' First, find cart items for this product and customer that have remaining quantity
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn1_Click(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product2Qty += 1
        TextBox2.Text = product2Qty.ToString()
        minus_btn1.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn1_Click(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product2Qty > 0 Then
            product2Qty -= 1
            TextBox2.Text = product2Qty.ToString()
            If product2Qty <= 0 Then
                minus_btn1.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 3
    Private Sub addtocart_btn2_Click(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 3
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

            ' First, find cart items for this product and customer that have remaining quantity
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub

    Private Sub plus_btn2_Click(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product3Qty += 1
        TextBox3.Text = product3Qty.ToString()
        minus_btn2.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn2_Click(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product3Qty > 0 Then
            product3Qty -= 1
            TextBox3.Text = product3Qty.ToString()
            If product3Qty <= 0 Then
                minus_btn2.Enabled = False
            End If
        End If
    End Sub

    'PRODUCT 7
    Private Sub addtocart_btn3_Click(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 7
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

            ' First, find cart items for this product and customer that have remaining quantity
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub

    Private Sub plus_btn3_Click(sender As Object, e As EventArgs) Handles plus_btn3.Click
        product7Qty += 1
        TextBox4.Text = product7Qty.ToString()
        minus_btn3.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn3_Click(sender As Object, e As EventArgs) Handles minus_btn3.Click
        If product7Qty > 0 Then
            product7Qty -= 1
            TextBox4.Text = product7Qty.ToString()
            If product7Qty <= 0 Then
                minus_btn3.Enabled = False
            End If
        End If
    End Sub

    'PRODUCT 9
    Private Sub addtocart_btn4_Click(sender As Object, e As EventArgs) Handles addtocart_btn4.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 9
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox5.Text, newQty) Then
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

            ' First, find cart items for this product and customer that have remaining quantity
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub

    Private Sub plus_btn4_Click(sender As Object, e As EventArgs) Handles plus_btn4.Click
        product9Qty += 1
        TextBox5.Text = product9Qty.ToString()
        minus_btn4.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn4_Click(sender As Object, e As EventArgs) Handles minus_btn4.Click
        If product9Qty > 0 Then
            product9Qty -= 1
            TextBox5.Text = product9Qty.ToString()
            If product9Qty <= 0 Then
                minus_btn4.Enabled = False
            End If
        End If
    End Sub

    'PRODUCT 12
    Private Sub addtocart_btn5_Click(sender As Object, e As EventArgs) Handles addtocart_btn5.Click
        ' Validate customer first
        If Not ValidateCustomer() Then
            Return
        End If

        Dim customerId = login.customerId
        Dim productId As Integer = 12
        Dim newQty As Integer

        If Not Integer.TryParse(TextBox6.Text, newQty) Then
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

            ' First, find cart items for this product and customer that have remaining quantity
            query = $"SELECT ca.cartId, ca.productQty AS remainingQty FROM cart ca WHERE ca.products_productId = {productId} AND ca.customers_customerId = {customerId} ORDER BY ca.cartId DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "cartcheck")

            Dim foundAvailableCart As Boolean = False
            Dim availableCartId As Integer = 0

            ' Check if any cart item has remaining quantity > 0
            For Each row As DataRow In ds.Tables("cartcheck").Rows
                If Convert.ToInt32(row("remainingQty")) > 0 Then
                    foundAvailableCart = True
                    availableCartId = Convert.ToInt32(row("cartId"))
                    Exit For
                End If
            Next

            If foundAvailableCart Then
                ' Found existing cart item with remaining quantity - update it
                query = $"UPDATE cart SET productQty = productQty + {newQty} WHERE cartId = {availableCartId}"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Updated cart! Added {newQty} more items to existing cart item.")

            Else
                ' No available cart item found - insert new one
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES ({productId}, {customerId}, {newQty})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show($"Product added to cart successfully! Quantity: {newQty}")
            End If

            ' Refresh cart display
            cart.refreshData()

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            clearQty()
        End Try
    End Sub

    Private Sub plus_btn5_Click(sender As Object, e As EventArgs) Handles plus_btn5.Click
        product12Qty += 1
        TextBox6.Text = product12Qty.ToString()
        minus_btn5.Enabled = True ' Always enable minus button when quantity > 0
    End Sub

    Private Sub minus_btn5_Click(sender As Object, e As EventArgs) Handles minus_btn5.Click
        If product12Qty > 0 Then
            product12Qty -= 1
            TextBox6.Text = product12Qty.ToString()
            If product12Qty <= 0 Then
                minus_btn5.Enabled = False
            End If
        End If
    End Sub
End Class
