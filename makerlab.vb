Imports MySql.Data.MySqlClient

Public Class makerlab
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim reader As MySqlDataReader
    Dim query As String

    Dim product1Qty As Integer
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
            ' product1Qty now contains the integer value from the textbox
            Try
                conn.Open()
                query = $"INSERT INTO cart (products_productId, customers_customerId, productQty) VALUES (1, 1, '{product1Qty}')"
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


End Class