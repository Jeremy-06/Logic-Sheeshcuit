Imports MySql.Data.MySqlClient

Public Class breadboards
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product14Qty As Integer = 0
    Dim product15Qty As Integer = 0
    Dim product16Qty As Integer = 0
    Dim product17Qty As Integer = 0
    Dim product18Qty As Integer = 0
    Dim product19Qty As Integer = 0
    Dim product20Qty As Integer = 0

    Private Sub breadboards_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
        TextBox7.Text = 0
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

    ' Utility to clear all product quantities and textboxes
    Private Sub clearQty()
        product14Qty = 0
        product15Qty = 0
        product16Qty = 0
        product17Qty = 0
        product18Qty = 0
        product19Qty = 0
        product20Qty = 0
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
        TextBox7.Text = 0
        minus_btn.Enabled = False
        minus_btn1.Enabled = False
        minus_btn2.Enabled = False
        minus_btn3.Enabled = False
        minus_btn4.Enabled = False
        minus_btn5.Enabled = False
        minus_btn6.Enabled = False
    End Sub

    ' PRODUCT 14
    Private Sub addtocart_btn_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 14
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn_Click_1(sender As Object, e As EventArgs) Handles plus_btn.Click
        product14Qty += 1
        TextBox1.Text = product14Qty.ToString()
        minus_btn.Enabled = True
    End Sub
    Private Sub minus_btn_Click_1(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product14Qty > 0 Then
            product14Qty -= 1
            TextBox1.Text = product14Qty.ToString()
            If product14Qty <= 0 Then minus_btn.Enabled = False
        End If
    End Sub

    ' PRODUCT 15
    Private Sub addtocart_btn1_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 15
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn1_Click_1(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product15Qty += 1
        TextBox2.Text = product15Qty.ToString()
        minus_btn1.Enabled = True
    End Sub
    Private Sub minus_btn1_Click_1(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product15Qty > 0 Then
            product15Qty -= 1
            TextBox2.Text = product15Qty.ToString()
            If product15Qty <= 0 Then minus_btn1.Enabled = False
        End If
    End Sub

    ' PRODUCT 16
    Private Sub addtocart_btn2_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 16
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn2_Click_1(sender As Object, e As EventArgs) Handles plus_btn2.Click
        product16Qty += 1
        TextBox3.Text = product16Qty.ToString()
        minus_btn2.Enabled = True
    End Sub
    Private Sub minus_btn2_Click_1(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product16Qty > 0 Then
            product16Qty -= 1
            TextBox3.Text = product16Qty.ToString()
            If product16Qty <= 0 Then minus_btn2.Enabled = False
        End If
    End Sub

    ' PRODUCT 17
    Private Sub addtocart_btn3_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 17
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn3_Click_1(sender As Object, e As EventArgs) Handles plus_btn3.Click
        product17Qty += 1
        TextBox4.Text = product17Qty.ToString()
        minus_btn3.Enabled = True
    End Sub
    Private Sub minus_btn3_Click_1(sender As Object, e As EventArgs) Handles minus_btn3.Click
        If product17Qty > 0 Then
            product17Qty -= 1
            TextBox4.Text = product17Qty.ToString()
            If product17Qty <= 0 Then minus_btn3.Enabled = False
        End If
    End Sub

    ' PRODUCT 18
    Private Sub addtocart_btn4_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn4.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 18
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn4_Click_1(sender As Object, e As EventArgs) Handles plus_btn4.Click
        product18Qty += 1
        TextBox5.Text = product18Qty.ToString()
        minus_btn4.Enabled = True
    End Sub
    Private Sub minus_btn4_Click_1(sender As Object, e As EventArgs) Handles minus_btn4.Click
        If product18Qty > 0 Then
            product18Qty -= 1
            TextBox5.Text = product18Qty.ToString()
            If product18Qty <= 0 Then minus_btn4.Enabled = False
        End If
    End Sub

    ' PRODUCT 19
    Private Sub addtocart_btn5_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn5.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 19
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn5_Click_1(sender As Object, e As EventArgs) Handles plus_btn5.Click
        product19Qty += 1
        TextBox6.Text = product19Qty.ToString()
        minus_btn5.Enabled = True
    End Sub
    Private Sub minus_btn5_Click_1(sender As Object, e As EventArgs) Handles minus_btn5.Click
        If product19Qty > 0 Then
            product19Qty -= 1
            TextBox6.Text = product19Qty.ToString()
            If product19Qty <= 0 Then minus_btn5.Enabled = False
        End If
    End Sub

    ' PRODUCT 20
    Private Sub addtocart_btn6_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn6.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 20
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
            clearQty()
        End Try
    End Sub
    Private Sub plus_btn6_Click_1(sender As Object, e As EventArgs) Handles plus_btn6.Click
        product20Qty += 1
        TextBox7.Text = product20Qty.ToString()
        minus_btn6.Enabled = True
    End Sub
    Private Sub minus_btn6_Click_1(sender As Object, e As EventArgs) Handles minus_btn6.Click
        If product20Qty > 0 Then
            product20Qty -= 1
            TextBox7.Text = product20Qty.ToString()
            If product20Qty <= 0 Then minus_btn6.Enabled = False
        End If
    End Sub
End Class