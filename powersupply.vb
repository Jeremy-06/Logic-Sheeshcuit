Imports MySql.Data.MySqlClient

Public Class powersupply
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product8Qty As Integer = 0
    Dim product9Qty As Integer = 0
    Dim product10Qty As Integer = 0
    Dim product11Qty As Integer = 0

    Private Sub powersupply_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
    End Sub

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

    ' Utility to clear all product quantities and textboxes
    Private Sub clearQty()
        product8Qty = 0
        product9Qty = 0
        product10Qty = 0
        product11Qty = 0
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        minus_btn.Enabled = False
        minus_btn1.Enabled = False
        minus_btn2.Enabled = False
        minus_btn3.Enabled = False
    End Sub

    ' PRODUCT 8
    Private Sub addtocart_btn_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 8
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox1.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then Return
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
        product8Qty += 1
        TextBox1.Text = product8Qty.ToString()
        minus_btn.Enabled = True
    End Sub
    Private Sub minus_btn_Click_1(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product8Qty > 0 Then
            product8Qty -= 1
            TextBox1.Text = product8Qty.ToString()
            If product8Qty <= 0 Then minus_btn.Enabled = False
        End If
    End Sub

    ' PRODUCT 9
    Private Sub addtocart_btn1_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 9
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox2.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then Return
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
        product9Qty += 1
        TextBox2.Text = product9Qty.ToString()
        minus_btn1.Enabled = True
    End Sub
    Private Sub minus_btn1_Click_1(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product9Qty > 0 Then
            product9Qty -= 1
            TextBox2.Text = product9Qty.ToString()
            If product9Qty <= 0 Then minus_btn1.Enabled = False
        End If
    End Sub

    ' PRODUCT 10
    Private Sub addtocart_btn2_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn2.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 10
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox3.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then Return
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
        product10Qty += 1
        TextBox3.Text = product10Qty.ToString()
        minus_btn2.Enabled = True
    End Sub
    Private Sub minus_btn2_Click_1(sender As Object, e As EventArgs) Handles minus_btn2.Click
        If product10Qty > 0 Then
            product10Qty -= 1
            TextBox3.Text = product10Qty.ToString()
            If product10Qty <= 0 Then minus_btn2.Enabled = False
        End If
    End Sub

    ' PRODUCT 11
    Private Sub addtocart_btn3_Click_1(sender As Object, e As EventArgs) Handles addtocart_btn3.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 11
        Dim newQty As Integer
        If Not Integer.TryParse(TextBox4.Text, newQty) Then
            MessageBox.Show("Please enter a valid number.")
            Return
        End If
        If newQty <= 0 Then
            MessageBox.Show("Please enter a quantity greater than 0.")
            Return
        End If
        If Not HasSufficientStock(productId, newQty) Then Return
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
        product11Qty += 1
        TextBox4.Text = product11Qty.ToString()
        minus_btn3.Enabled = True
    End Sub
    Private Sub minus_btn3_Click_1(sender As Object, e As EventArgs) Handles minus_btn3.Click
        If product11Qty > 0 Then
            product11Qty -= 1
            TextBox4.Text = product11Qty.ToString()
            If product11Qty <= 0 Then minus_btn3.Enabled = False
        End If
    End Sub
End Class