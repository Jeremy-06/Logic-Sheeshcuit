Imports MySql.Data.MySqlClient

Public Class circuitrocks
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product10Qty As Integer = 0
    Dim product11Qty As Integer = 0
    Dim product13Qty As Integer = 0
    Dim product14Qty As Integer = 0
    Dim product15Qty As Integer = 0
    Dim product16Qty As Integer = 0
    Dim product17Qty As Integer = 0
    Dim product18Qty As Integer = 0
    Dim product19Qty As Integer = 0
    Dim product20Qty As Integer = 0
    Dim product21Qty As Integer = 0
    Dim product22Qty As Integer = 0
    Dim product23Qty As Integer = 0
    Dim product24Qty As Integer = 0
    Dim product25Qty As Integer = 0
    Dim product26Qty As Integer = 0
    Dim product27Qty As Integer = 0
    Dim product28Qty As Integer = 0
    Dim product29Qty As Integer = 0
    Dim product30Qty As Integer = 0
    Dim product31Qty As Integer = 0
    Dim product32Qty As Integer = 0
    Dim product33Qty As Integer = 0
    Dim product34Qty As Integer = 0
    Dim product37Qty As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    ' Clears all product quantity textboxes
    Private Sub ClearAllProductTextBoxes()
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
        TextBox7.Text = 0
        TextBox8.Text = 0
        TextBox9.Text = 0
        TextBox10.Text = 0
        TextBox11.Text = 0
        TextBox12.Text = 0
        TextBox13.Text = 0
        TextBox14.Text = 0
        TextBox15.Text = 0
        TextBox16.Text = 0
        TextBox17.Text = 0
        TextBox18.Text = 0
        TextBox19.Text = 0
        TextBox20.Text = 0
        TextBox21.Text = 0
        TextBox22.Text = 0
        TextBox23.Text = 0
        TextBox24.Text = 0
        TextBox25.Text = 0
    End Sub

    Private Function HasSufficientStock(productId As Integer, requestedQty As Integer) As Boolean
        Dim availableStock As Integer = 0
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT productStock FROM inventory WHERE products_productId = {productId} LIMIT 1"
            cmd = New MySqlCommand(query, conn)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                availableStock = Convert.ToInt32(result)
            End If
            If requestedQty > availableStock Then
                MessageBox.Show($"Not enough stock available. Only {availableStock} left in stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error checking stock: " & ex.Message)
            Return False
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Function

    'PRODUCT 10
    Private Sub addtocart_btn_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 10 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 10
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox1.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn_Click_1(sender As Object, e As EventArgs) Handles plus_btn.Click
        product10Qty += 1
        TextBox1.Text = product10Qty.ToString()
        minus_btn.Enabled = True
    End Sub

    Private Sub minus_btn_Click_1(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product10Qty > 0 Then
            product10Qty -= 1
            TextBox1.Text = product10Qty.ToString()
            If product10Qty <= 0 Then minus_btn.Enabled = False
        End If
    End Sub
    'PRODUCT 11
    Private Sub addtocart_btn1_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 11 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 11
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox2.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn1_Click_1(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product11Qty += 1
        TextBox2.Text = product11Qty.ToString()
        minus_btn1.Enabled = True
    End Sub

    Private Sub minus_btn1_Click_1(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product11Qty > 0 Then
            product11Qty -= 1
            TextBox2.Text = product11Qty.ToString()
            If product11Qty <= 0 Then minus_btn1.Enabled = False
        End If
    End Sub
    'PRODUCT 13
    Private Sub addtocart_btn2_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 13 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 13
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox3.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn2_Click_1(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product13Qty += 1
        TextBox3.Text = product13Qty.ToString()
        minus_btn2.Enabled = True
    End Sub

    Private Sub minus_btn2_Click_1(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product13Qty > 0 Then
            product13Qty -= 1
            TextBox3.Text = product13Qty.ToString()
            If product13Qty <= 0 Then minus_btn2.Enabled = False
        End If
    End Sub
    'PRODUCT 14
    Private Sub addtocart_btn3_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 14 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 14
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox4.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn3_Click_1(sender As Object, e As EventArgs) Handles plus_btn3.Click
        product14Qty += 1
        TextBox4.Text = product14Qty.ToString()
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If product14Qty > 0 Then
            product14Qty -= 1
            TextBox4.Text = product14Qty.ToString()
            If product14Qty <= 0 Then Button2.Enabled = False
        End If
    End Sub
    'PRODUCT 15
    Private Sub addtocart_btn4_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn4.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 15 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 15
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox5.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn4_Click_1(sender As Object, e As EventArgs) Handles plus_btn4.Click
        product15Qty += 1
        TextBox5.Text = product15Qty.ToString()
        minus_btn4.Enabled = True
    End Sub

    Private Sub minus_btn4_Click_1(sender As Object, e As EventArgs) Handles minus_btn4.Click
        If product15Qty > 0 Then
            product15Qty -= 1
            TextBox5.Text = product15Qty.ToString()
            If product15Qty <= 0 Then minus_btn4.Enabled = False
        End If
    End Sub
    'PRODUCT 16 
    Private Sub addtocart_btn5_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn5.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 16 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 16
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox6.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn5_Click_1(sender As Object, e As EventArgs) Handles plus_btn5.Click
        product16Qty += 1
        TextBox6.Text = product16Qty.ToString()
        minus_btn5.Enabled = True
    End Sub

    Private Sub minus_btn5_Click_1(sender As Object, e As EventArgs) Handles minus_btn5.Click
        If product16Qty > 0 Then
            product16Qty -= 1
            TextBox6.Text = product16Qty.ToString()
            If product16Qty <= 0 Then minus_btn5.Enabled = False
        End If
    End Sub
    'PRODUCT 17
    Private Sub addtocart_btn6_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn6.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 17 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 17
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox7.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn6_Click_1(sender As Object, e As EventArgs) Handles plus_btn6.Click
        product17Qty += 1
        TextBox7.Text = product17Qty.ToString()
        minus_btn6.Enabled = True
    End Sub

    Private Sub minus_btn6_Click_1(sender As Object, e As EventArgs) Handles minus_btn6.Click
        If product17Qty > 0 Then
            product17Qty -= 1
            TextBox7.Text = product17Qty.ToString()
            If product17Qty <= 0 Then minus_btn6.Enabled = False
        End If
    End Sub
    'PRODUCT 18
    Private Sub addtocart_btn7_Click(sender As Object, e As EventArgs) Handles addtocart_btn7.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 18 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 18
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox8.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn7_Click(sender As Object, e As EventArgs) Handles plus_btn7.Click
        product18Qty += 1
        TextBox8.Text = product18Qty.ToString()
        minus_btn7.Enabled = True
    End Sub

    Private Sub minus_btn7_Click(sender As Object, e As EventArgs) Handles minus_btn7.Click
        If product18Qty > 0 Then
            product18Qty -= 1
            TextBox8.Text = product18Qty.ToString()
            If product18Qty <= 0 Then minus_btn7.Enabled = False
        End If
    End Sub
    'PRODUCT 19
    Private Sub addtocart_btn8_Click(sender As Object, e As EventArgs) Handles addtocart_btn8.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 19 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 19
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox9.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn8_Click(sender As Object, e As EventArgs) Handles plus_btn8.Click
        product19Qty += 1
        TextBox9.Text = product19Qty.ToString()
        minus_btn8.Enabled = True
    End Sub

    Private Sub minus_btn8_Click(sender As Object, e As EventArgs) Handles minus_btn8.Click
        If product19Qty > 0 Then
            product19Qty -= 1
            TextBox9.Text = product19Qty.ToString()
            If product19Qty <= 0 Then minus_btn8.Enabled = False
        End If
    End Sub
    'PRODUCT 20
    Private Sub addtocart_btn9_Click(sender As Object, e As EventArgs) Handles addtocart_btn9.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 20 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 20
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox10.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn9_Click(sender As Object, e As EventArgs) Handles plus_btn9.Click
        product20Qty += 1
        TextBox10.Text = product20Qty.ToString()
        minus_btn9.Enabled = True
    End Sub

    Private Sub minus_btn9_Click(sender As Object, e As EventArgs) Handles minus_btn9.Click
        If product20Qty > 0 Then
            product20Qty -= 1
            TextBox10.Text = product20Qty.ToString()
            If product20Qty <= 0 Then minus_btn9.Enabled = False
        End If
    End Sub
    'PRODUCT 21
    Private Sub addtocart_btn10_Click(sender As Object, e As EventArgs) Handles addtocart_btn10.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 21 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 21
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox11.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn10_Click(sender As Object, e As EventArgs) Handles plus_btn10.Click
        product21Qty += 1
        TextBox11.Text = product21Qty.ToString()
        minus_btn10.Enabled = True
    End Sub

    Private Sub minus_btn10_Click(sender As Object, e As EventArgs) Handles minus_btn10.Click
        If product21Qty > 0 Then
            product21Qty -= 1
            TextBox11.Text = product21Qty.ToString()
            If product21Qty <= 0 Then minus_btn10.Enabled = False
        End If
    End Sub
    'PRODUCT 22
    Private Sub addtocart_btn11_Click(sender As Object, e As EventArgs) Handles addtocart_btn11.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 22 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 22
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox12.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn11_Click(sender As Object, e As EventArgs) Handles plus_btn11.Click
        product22Qty += 1
        TextBox12.Text = product22Qty.ToString()
        minus_btn11.Enabled = True
    End Sub

    Private Sub minus_btn11_Click(sender As Object, e As EventArgs) Handles minus_btn11.Click
        If product22Qty > 0 Then
            product22Qty -= 1
            TextBox12.Text = product22Qty.ToString()
            If product22Qty <= 0 Then minus_btn11.Enabled = False
        End If
    End Sub
    'PRODUCT 23
    Private Sub addtocart_btn12_Click(sender As Object, e As EventArgs) Handles addtocart_btn12.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 23 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 23
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox13.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn12_Click(sender As Object, e As EventArgs) Handles plus_btn12.Click
        product23Qty += 1
        TextBox13.Text = product23Qty.ToString()
        minus_btn12.Enabled = True
    End Sub

    Private Sub minus_btn12_Click(sender As Object, e As EventArgs) Handles minus_btn12.Click
        If product23Qty > 0 Then
            product23Qty -= 1
            TextBox13.Text = product23Qty.ToString()
            If product23Qty <= 0 Then minus_btn12.Enabled = False
        End If
    End Sub
    'PRODUCT 24
    Private Sub addtocart_btn13_Click(sender As Object, e As EventArgs) Handles addtocart_btn13.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 24 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 24
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox14.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn13_Click(sender As Object, e As EventArgs) Handles plus_btn13.Click
        product24Qty += 1
        TextBox14.Text = product24Qty.ToString()
        minus_btn13.Enabled = True
    End Sub

    Private Sub minus_btn13_Click(sender As Object, e As EventArgs) Handles minus_btn13.Click
        If product24Qty > 0 Then
            product24Qty -= 1
            TextBox14.Text = product24Qty.ToString()
            If product24Qty <= 0 Then minus_btn13.Enabled = False
        End If
    End Sub
    'PRODUCT 25
    Private Sub addtocart_btn14_Click(sender As Object, e As EventArgs) Handles addtocart_btn14.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 25 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 25
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox15.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn14_Click(sender As Object, e As EventArgs) Handles plus_btn14.Click
        product25Qty += 1
        TextBox15.Text = product25Qty.ToString()
        minus_btn14.Enabled = True
    End Sub

    Private Sub minus_btn14_Click(sender As Object, e As EventArgs) Handles minus_btn14.Click
        If product25Qty > 0 Then
            product25Qty -= 1
            TextBox15.Text = product25Qty.ToString()
            If product25Qty <= 0 Then minus_btn14.Enabled = False
        End If
    End Sub
    'PRODUCT 26
    Private Sub addtocart_btn15_Click(sender As Object, e As EventArgs) Handles addtocart_btn15.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 26 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 26
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox16.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn15_Click(sender As Object, e As EventArgs) Handles plus_btn15.Click
        product26Qty += 1
        TextBox16.Text = product26Qty.ToString()
        minus_btn15.Enabled = True
    End Sub

    Private Sub minus_btn15_Click(sender As Object, e As EventArgs) Handles minus_btn15.Click
        If product26Qty > 0 Then
            product26Qty -= 1
            TextBox16.Text = product26Qty.ToString()
            If product26Qty <= 0 Then minus_btn15.Enabled = False
        End If
    End Sub
    'PRODUCT 27
    Private Sub addtocart_btn16_Click(sender As Object, e As EventArgs) Handles addtocart_btn16.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 27 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 27
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox17.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn16_Click(sender As Object, e As EventArgs) Handles plus_btn16.Click
        product27Qty += 1
        TextBox17.Text = product27Qty.ToString()
        minus_btn16.Enabled = True
    End Sub

    Private Sub minus_btn16_Click(sender As Object, e As EventArgs) Handles minus_btn16.Click
        If product27Qty > 0 Then
            product27Qty -= 1
            TextBox17.Text = product27Qty.ToString()
            If product27Qty <= 0 Then minus_btn16.Enabled = False
        End If
    End Sub

    'PRODUCT 28
    Private Sub addtocart_btn17_Click(sender As Object, e As EventArgs) Handles addtocart_btn17.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 28 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 28
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox18.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub
    Private Sub plus_btn17_Click(sender As Object, e As EventArgs) Handles plus_btn17.Click
        product28Qty += 1
        TextBox18.Text = product28Qty.ToString()
        minus_btn17.Enabled = True
    End Sub

    Private Sub minus_btn17_Click(sender As Object, e As EventArgs) Handles minus_btn17.Click
        If product28Qty > 0 Then
            product28Qty -= 1
            TextBox18.Text = product28Qty.ToString()
            If product28Qty <= 0 Then minus_btn17.Enabled = False
        End If
    End Sub
    'PRODUCT 29
    Private Sub addtocart_btn18_Click(sender As Object, e As EventArgs) Handles addtocart_btn18.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 29 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 29
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox19.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn18_Click(sender As Object, e As EventArgs) Handles plus_btn18.Click
        product29Qty += 1
        TextBox19.Text = product29Qty.ToString()
        minus_btn18.Enabled = True
    End Sub

    Private Sub minus_btn18_Click(sender As Object, e As EventArgs) Handles minus_btn18.Click
        If product29Qty > 0 Then
            product29Qty -= 1
            TextBox19.Text = product29Qty.ToString()
            If product29Qty <= 0 Then minus_btn18.Enabled = False
        End If
    End Sub
    'PRODUCT 30
    Private Sub addtocart_btn19_Click(sender As Object, e As EventArgs) Handles addtocart_btn19.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 30 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 30
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox20.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn19_Click(sender As Object, e As EventArgs) Handles plus_btn19.Click
        product30Qty += 1
        TextBox20.Text = product30Qty.ToString()
        minus_btn19.Enabled = True
    End Sub

    Private Sub minus_btn19_Click(sender As Object, e As EventArgs) Handles minus_btn19.Click
        If product30Qty > 0 Then
            product30Qty -= 1
            TextBox20.Text = product30Qty.ToString()
            If product30Qty <= 0 Then minus_btn19.Enabled = False
        End If
    End Sub
    'PRODUCT 31
    Private Sub addtocart_btn20_Click(sender As Object, e As EventArgs) Handles addtocart_btn20.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 31 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 31
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox21.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn20_Click(sender As Object, e As EventArgs) Handles plus_btn20.Click
        product31Qty += 1
        TextBox21.Text = product31Qty.ToString()
        minus_btn20.Enabled = True
    End Sub

    Private Sub minus_btn20_Click(sender As Object, e As EventArgs) Handles minus_btn20.Click
        If product31Qty > 0 Then
            product31Qty -= 1
            TextBox21.Text = product31Qty.ToString()
            If product31Qty <= 0 Then minus_btn20.Enabled = False
        End If
    End Sub
    'PRODUCT 32
    Private Sub addtocart_btn21_Click(sender As Object, e As EventArgs) Handles addtocart_btn21.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 32 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 32
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox22.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn21_Click(sender As Object, e As EventArgs) Handles plus_btn21.Click
        product32Qty += 1
        TextBox22.Text = product32Qty.ToString()
        minus_btn21.Enabled = True
    End Sub

    Private Sub minus_btn21_Click(sender As Object, e As EventArgs) Handles minus_btn21.Click
        If product32Qty > 0 Then
            product32Qty -= 1
            TextBox22.Text = product32Qty.ToString()
            If product32Qty <= 0 Then minus_btn21.Enabled = False
        End If
    End Sub
    'PRODUCT 33
    Private Sub addtocart_btn22_Click(sender As Object, e As EventArgs) Handles addtocart_btn22.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 33 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 33
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox23.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn22_Click(sender As Object, e As EventArgs) Handles plus_btn22.Click
        product33Qty += 1
        TextBox23.Text = product33Qty.ToString()
        minus_btn22.Enabled = True
    End Sub

    Private Sub minus_btn22_Click(sender As Object, e As EventArgs) Handles minus_btn22.Click
        If product33Qty > 0 Then
            product33Qty -= 1
            TextBox23.Text = product33Qty.ToString()
            If product33Qty <= 0 Then minus_btn22.Enabled = False
        End If
    End Sub
    'PRODUCT 34
    Private Sub addtocart_btn23_Click(sender As Object, e As EventArgs) Handles addtocart_btn23.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 34 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 34
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox24.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn23_Click(sender As Object, e As EventArgs) Handles plus_btn23.Click
        product34Qty += 1
        TextBox24.Text = product34Qty.ToString()
        minus_btn23.Enabled = True
    End Sub

    Private Sub minus_btn23_Click(sender As Object, e As EventArgs) Handles minus_btn23.Click
        If product34Qty > 0 Then
            product34Qty -= 1
            TextBox24.Text = product34Qty.ToString()
            If product34Qty <= 0 Then minus_btn23.Enabled = False
        End If
    End Sub
    'PRODUCT 37
    Private Sub addtocart_btn24_Click(sender As Object, e As EventArgs) Handles addtocart_btn24.Click
        If Not ValidateCustomer() Then Return
        ' Adds Product 37 to the cart
        Dim customerId = login.customerId
        Dim productId As Integer = 37
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox25.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then
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
            ' Clear all quantity textboxes after adding to cart
            ClearAllProductTextBoxes()
        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub plus_btn24_Click(sender As Object, e As EventArgs) Handles plus_btn24.Click
        product37Qty += 1
        TextBox25.Text = product37Qty.ToString()
        minus_btn24.Enabled = True
    End Sub

    Private Sub minus_btn24_Click(sender As Object, e As EventArgs) Handles minus_btn24.Click
        If product37Qty > 0 Then
            product37Qty -= 1
            TextBox25.Text = product37Qty.ToString()
            If product37Qty <= 0 Then minus_btn24.Enabled = False
        End If
    End Sub

    Private Sub circuitrocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize all minus buttons as enabled by default
        minus_btn.Enabled = True
        minus_btn1.Enabled = True
        minus_btn2.Enabled = True
        Button2.Enabled = True
        minus_btn4.Enabled = True
        minus_btn5.Enabled = True
        minus_btn6.Enabled = True
        minus_btn7.Enabled = True
        minus_btn8.Enabled = True
        minus_btn9.Enabled = True
        minus_btn10.Enabled = True
        minus_btn11.Enabled = True
        minus_btn12.Enabled = True
        minus_btn13.Enabled = True
        minus_btn14.Enabled = True
        minus_btn15.Enabled = True
        minus_btn16.Enabled = True
        minus_btn17.Enabled = True
        minus_btn18.Enabled = True
        minus_btn19.Enabled = True
        minus_btn20.Enabled = True
        minus_btn21.Enabled = True
        minus_btn22.Enabled = True
        minus_btn23.Enabled = True
        minus_btn24.Enabled = True
    End Sub
End Class