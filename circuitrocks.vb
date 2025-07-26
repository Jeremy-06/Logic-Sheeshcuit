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

    Private Sub circuitrocks_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Function ValidateCustomer() As Boolean
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

    'atc
    Private Sub addtocart_btn2_Click(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn2_Click(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product10Qty += 1
        TextBox1.Text = product10Qty.ToString()
        minus_btn2.Enabled = True
    End Sub

    Private Sub minus_btn2_Click(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product10Qty > 0 Then
            product10Qty -= 1
            TextBox1.Text = product10Qty.ToString()
            If product10Qty <= 0 Then minus_btn2.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn3_Click(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn3_Click(sender As Object, e As EventArgs) Handles plus_btn3.Click
        product11Qty += 1
        TextBox2.Text = product11Qty.ToString()
        minus_btn3.Enabled = True
    End Sub

    Private Sub minus_btn3_Click(sender As Object, e As EventArgs) Handles minus_btn3.Click
        If product11Qty > 0 Then
            product11Qty -= 1
            TextBox2.Text = product11Qty.ToString()
            If product11Qty <= 0 Then minus_btn3.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn1_Click(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product13Qty += 1
        TextBox3.Text = product13Qty.ToString()
        minus_btn1.Enabled = True
    End Sub

    Private Sub minus_btn1_Click(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product13Qty > 0 Then
            product13Qty -= 1
            TextBox3.Text = product13Qty.ToString()
            If product13Qty <= 0 Then minus_btn1.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        product14Qty += 1
        TextBox4.Text = product14Qty.ToString()
        minus_btn4.Enabled = True
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product14Qty > 0 Then
            product14Qty -= 1
            TextBox4.Text = product14Qty.ToString()
            If product14Qty <= 0 Then minus_btn4.Enabled = False
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        product15Qty += 1
        TextBox5.Text = product15Qty.ToString()
        Button8.Enabled = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If product15Qty > 0 Then
            product15Qty -= 1
            TextBox5.Text = product15Qty.ToString()
            If product15Qty <= 0 Then Button8.Enabled = False
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        product16Qty += 1
        TextBox6.Text = product16Qty.ToString()
        Button5.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If product16Qty > 0 Then
            product16Qty -= 1
            TextBox6.Text = product16Qty.ToString()
            If product16Qty <= 0 Then Button5.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        product17Qty += 1
        TextBox7.Text = product17Qty.ToString()
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If product17Qty > 0 Then
            product17Qty -= 1
            TextBox7.Text = product17Qty.ToString()
            If product17Qty <= 0 Then Button2.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn4_Click(sender As Object, e As EventArgs) Handles addtocart_btn4.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn4_Click(sender As Object, e As EventArgs) Handles plus_btn4.Click
        product18Qty += 1
        TextBox8.Text = product18Qty.ToString()
        minus_btn4.Enabled = True
    End Sub

    Private Sub minus_btn4_Click(sender As Object, e As EventArgs) Handles minus_btn4.Click
        If product18Qty > 0 Then
            product18Qty -= 1
            TextBox8.Text = product18Qty.ToString()
            If product18Qty <= 0 Then minus_btn4.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn5_Click(sender As Object, e As EventArgs) Handles addtocart_btn5.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn5_Click(sender As Object, e As EventArgs) Handles plus_btn5.Click
        product19Qty += 1
        TextBox9.Text = product19Qty.ToString()
        minus_btn5.Enabled = True
    End Sub

    Private Sub minus_btn5_Click(sender As Object, e As EventArgs) Handles minus_btn5.Click
        If product19Qty > 0 Then
            product19Qty -= 1
            TextBox9.Text = product19Qty.ToString()
            If product19Qty <= 0 Then minus_btn5.Enabled = False
        End If
    End Sub

    Private Sub addtocart_btn6_Click(sender As Object, e As EventArgs) Handles addtocart_btn6.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub plus_btn6_Click(sender As Object, e As EventArgs) Handles plus_btn6.Click
        product20Qty += 1
        TextBox10.Text = product20Qty.ToString()
        minus_btn6.Enabled = True
    End Sub

    Private Sub minus_btn6_Click(sender As Object, e As EventArgs) Handles minus_btn6.Click
        If product20Qty > 0 Then
            product20Qty -= 1
            TextBox10.Text = product20Qty.ToString()
            If product20Qty <= 0 Then minus_btn6.Enabled = False
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        product21Qty += 1
        TextBox11.Text = product21Qty.ToString()
        Button11.Enabled = True
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If product21Qty > 0 Then
            product21Qty -= 1
            TextBox11.Text = product21Qty.ToString()
            If product21Qty <= 0 Then Button11.Enabled = False
        End If
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        product22Qty += 1
        TextBox12.Text = product22Qty.ToString()
        Button29.Enabled = True
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If product22Qty > 0 Then
            product22Qty -= 1
            TextBox12.Text = product22Qty.ToString()
            If product22Qty <= 0 Then Button29.Enabled = False
        End If
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        product23Qty += 1
        TextBox13.Text = product23Qty.ToString()
        Button26.Enabled = True
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If product23Qty > 0 Then
            product23Qty -= 1
            TextBox13.Text = product23Qty.ToString()
            If product23Qty <= 0 Then Button26.Enabled = False
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        product24Qty += 1
        TextBox14.Text = product24Qty.ToString()
        Button23.Enabled = True
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If product24Qty > 0 Then
            product24Qty -= 1
            TextBox14.Text = product24Qty.ToString()
            If product24Qty <= 0 Then Button23.Enabled = False
        End If
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        product25Qty += 1
        TextBox15.Text = product25Qty.ToString()
        Button20.Enabled = True
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If product25Qty > 0 Then
            product25Qty -= 1
            TextBox15.Text = product25Qty.ToString()
            If product25Qty <= 0 Then Button20.Enabled = False
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        ' Increments quantity for Product 26
        product26Qty += 1
        TextBox16.Text = product26Qty.ToString()
        Button17.Enabled = True
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If product26Qty > 0 Then
            product26Qty -= 1
            TextBox16.Text = product26Qty.ToString()
            If product26Qty <= 0 Then Button17.Enabled = False
        End If
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        product27Qty += 1
        TextBox17.Text = product27Qty.ToString()
        Button14.Enabled = True
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If product27Qty > 0 Then
            product27Qty -= 1
            TextBox17.Text = product27Qty.ToString()
            If product27Qty <= 0 Then Button14.Enabled = False
        End If
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        product28Qty += 1
        TextBox18.Text = product28Qty.ToString()
        Button50.Enabled = True
    End Sub

    Private Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If product28Qty > 0 Then
            product28Qty -= 1
            TextBox18.Text = product28Qty.ToString()
            If product28Qty <= 0 Then Button50.Enabled = False
        End If
    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        product29Qty += 1
        TextBox19.Text = product29Qty.ToString()
        Button47.Enabled = True
    End Sub

    Private Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        If product29Qty > 0 Then
            product29Qty -= 1
            TextBox19.Text = product29Qty.ToString()
            If product29Qty <= 0 Then Button47.Enabled = False
        End If
    End Sub

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        product30Qty += 1
        TextBox20.Text = product30Qty.ToString()
        Button44.Enabled = True
    End Sub

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        If product30Qty > 0 Then
            product30Qty -= 1
            TextBox20.Text = product30Qty.ToString()
            If product30Qty <= 0 Then Button44.Enabled = False
        End If
    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        product31Qty += 1
        TextBox21.Text = product31Qty.ToString()
        Button41.Enabled = True
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        If product31Qty > 0 Then
            product31Qty -= 1
            TextBox21.Text = product31Qty.ToString()
            If product31Qty <= 0 Then Button41.Enabled = False
        End If
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        product32Qty += 1
        TextBox22.Text = product32Qty.ToString()
        Button38.Enabled = True
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If product32Qty > 0 Then
            product32Qty -= 1
            TextBox22.Text = product32Qty.ToString()
            If product32Qty <= 0 Then Button38.Enabled = False
        End If
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        product33Qty += 1
        TextBox23.Text = product33Qty.ToString()
        Button35.Enabled = True
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If product33Qty > 0 Then
            product33Qty -= 1
            TextBox23.Text = product33Qty.ToString()
            If product33Qty <= 0 Then Button35.Enabled = False
        End If

    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        product34Qty += 1
        TextBox24.Text = product34Qty.ToString()
        Button32.Enabled = True
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If product34Qty > 0 Then
            product34Qty -= 1
            TextBox24.Text = product34Qty.ToString()
            If product34Qty <= 0 Then Button32.Enabled = False
        End If
    End Sub

    Private Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
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
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            query = $"SELECT ca.cartId, (ca.productQty - COALESCE(SUM(oi.productQty), 0)) AS remainingQty
                  FROM cart ca
                  LEFT JOIN orderitems oi ON ca.cartId = oi.cart_cartId
                  WHERE ca.products_productId = {productId} 
                    AND ca.customers_customerId = {customerId}
                  GROUP BY ca.cartId, ca.productQty
                  ORDER BY ca.cartId DESC"
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

    Private Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        product37Qty += 1
        TextBox25.Text = product37Qty.ToString()
        Button53.Enabled = True
    End Sub

    Private Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        If product37Qty > 0 Then
            product37Qty -= 1
            TextBox25.Text = product37Qty.ToString()
            If product37Qty <= 0 Then Button53.Enabled = False
        End If
    End Sub

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
End Class