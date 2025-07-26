Imports MySql.Data.MySqlClient

Public Class oscilloscopes
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product35Qty As Integer = 0
    Dim product36Qty As Integer = 0

    Private Sub oscilloscopes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = 0
        TextBox2.Text = 0
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

    Private Sub addtocart_btn_Click(sender As Object, e As EventArgs) Handles addtocart_btn.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 35
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

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product35Qty = 0
            TextBox1.Text = 0
            minus_btn.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn_Click(sender As Object, e As EventArgs) Handles plus_btn.Click
        product35Qty += 1
        TextBox1.Text = product35Qty.ToString()
        minus_btn.Enabled = True
    End Sub

    Private Sub minus_btn_Click(sender As Object, e As EventArgs) Handles minus_btn.Click
        If product35Qty > 0 Then
            product35Qty -= 1
            TextBox1.Text = product35Qty.ToString()
            If product35Qty <= 0 Then
                minus_btn.Enabled = False
            End If
        End If
    End Sub

    Private Sub addtocart_btn1_Click(sender As Object, e As EventArgs) Handles addtocart_btn1.Click
        If Not ValidateCustomer() Then Return
        Dim customerId = login.customerId
        Dim productId As Integer = 36
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

        Catch ex As Exception
            MessageBox.Show("Error adding product to cart: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            product36Qty = 0
            TextBox2.Text = 0
            minus_btn1.Enabled = False
        End Try
    End Sub

    Private Sub plus_btn1_Click(sender As Object, e As EventArgs) Handles plus_btn1.Click
        product36Qty += 1
        TextBox2.Text = product36Qty.ToString()
        minus_btn1.Enabled = True
    End Sub

    Private Sub minus_btn1_Click(sender As Object, e As EventArgs) Handles minus_btn1.Click
        If product36Qty > 0 Then
            product36Qty -= 1
            TextBox2.Text = product36Qty.ToString()
            If product36Qty <= 0 Then
                minus_btn1.Enabled = False
            End If
        End If
    End Sub
End Class