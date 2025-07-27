Imports MySql.Data.MySqlClient

Public Class resistor
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product22Qty As Integer = 0
    Dim product23Qty As Integer = 0
    Dim product24Qty As Integer = 0
    Dim product25Qty As Integer = 0
    Dim product26Qty As Integer = 0
    Dim product27Qty As Integer = 0

    Private Sub resistor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
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

    ' PRODUCT 22
    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 22
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
            product22Qty = 0
            TextBox1.Text = 0
            minus_btn.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs)
        product22Qty += 1
        TextBox1.Text = product22Qty.ToString()
        minus_btn.Enabled = True
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs)
        If product22Qty > 0 Then
            product22Qty -= 1
            TextBox1.Text = product22Qty.ToString()
            If product22Qty <= 0 Then
                minus_btn.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 23
    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 23
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
            product23Qty = 0
            TextBox2.Text = 0
            minus_btn1.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn1_Click(sender As Object, e As EventArgs)
        product23Qty += 1
        TextBox2.Text = product23Qty.ToString()
        minus_btn1.Enabled = True
    End Sub

    Private Sub minus_btn1_Click(sender As Object, e As EventArgs)
        If product23Qty > 0 Then
            product23Qty -= 1
            TextBox2.Text = product23Qty.ToString()
            If product23Qty <= 0 Then
                minus_btn1.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 24
    Private Sub addtocart_btn2_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 24
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
            product24Qty = 0
            TextBox3.Text = 0
            minus_btn2.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn2_Click(sender As Object, e As EventArgs)
        product24Qty += 1
        TextBox3.Text = product24Qty.ToString()
        minus_btn2.Enabled = True
    End Sub

    Private Sub minus_btn2_Click(sender As Object, e As EventArgs)
        If product24Qty > 0 Then
            product24Qty -= 1
            TextBox3.Text = product24Qty.ToString()
            If product24Qty <= 0 Then
                minus_btn2.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 25
    Private Sub addtocart_btn3_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 25
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
            product25Qty = 0
            TextBox4.Text = 0
            minus_btn3.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn3_Click(sender As Object, e As EventArgs)
        product25Qty += 1
        TextBox4.Text = product25Qty.ToString()
        minus_btn3.Enabled = True
    End Sub

    Private Sub minus_btn3_Click(sender As Object, e As EventArgs)
        If product25Qty > 0 Then
            product25Qty -= 1
            TextBox4.Text = product25Qty.ToString()
            If product25Qty <= 0 Then
                minus_btn3.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 26
    Private Sub addtocart_btn4_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 26
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
            product26Qty = 0
            TextBox5.Text = 0
            minus_btn4.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn4_Click(sender As Object, e As EventArgs)
        product26Qty += 1
        TextBox5.Text = product26Qty.ToString()
        minus_btn4.Enabled = True
    End Sub

    Private Sub minus_btn4_Click(sender As Object, e As EventArgs)
        If product26Qty > 0 Then
            product26Qty -= 1
            TextBox5.Text = product26Qty.ToString()
            If product26Qty <= 0 Then
                minus_btn4.Enabled = False
            End If
        End If
    End Sub

    ' PRODUCT 27
    Private Sub addtocart_btn5_Click(sender As Object, e As EventArgs)
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 27
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
            product27Qty = 0
            TextBox6.Text = 0
            minus_btn5.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn5_Click(sender As Object, e As EventArgs)
        product27Qty += 1
        TextBox6.Text = product27Qty.ToString()
        minus_btn5.Enabled = True
    End Sub

    Private Sub minus_btn5_Click(sender As Object, e As EventArgs)
        If product27Qty > 0 Then
            product27Qty -= 1
            TextBox6.Text = product27Qty.ToString()
            If product27Qty <= 0 Then
                minus_btn5.Enabled = False
            End If
        End If
    End Sub
End Class