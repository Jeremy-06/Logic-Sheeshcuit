﻿Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class login
    Public Shared customerId As Integer = 0

    ' Database connection objects
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String

    ' Ensure password is hidden by default when the form loads
    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.UseSystemPasswordChar = True
    End Sub

    ' Hash password using SHA256, preserving original casing and whitespace
    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim sb As New StringBuilder()
            For Each b As Byte In hash
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.Hide()
        signup.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'log in button
        Dim username As String = TextBox1.Text.Trim().ToLower()
        Dim password As String = TextBox2.Text
        Dim hashedPassword As String = HashPassword(password)

        ' Check if username and password are not empty
        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Check if username and password match in users table (hashed)
            query = $"SELECT userId FROM users WHERE username = '{username}' AND password = '{hashedPassword}'"
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim userId As Integer = reader.GetInt32("userId")
                reader.Close()

                ' Get customer ID from customers table
                query = $"SELECT customerId FROM customers WHERE userId = {userId}"
                cmd = New MySqlCommand(query, conn)
                reader = cmd.ExecuteReader()

                If reader.Read() Then
                    customerId = reader.GetInt32("customerId") ' Set the shared variable
                    reader.Close()

                    ' Clear the textboxes
                    TextBox1.Clear()
                    TextBox2.Clear()

                    ' Show success message and proceed to home
                    MessageBox.Show("Login successful!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Hide()
                    home.Show()
                Else
                    reader.Close()
                    MessageBox.Show("Customer information not found.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                reader.Close()
                MessageBox.Show("Invalid username or password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error during login: " & ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If reader IsNot Nothing AndAlso Not reader.IsClosed Then
                reader.Close()
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' Show password if checkbox is checked, otherwise hide it
        If CheckBox1.Checked Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub
End Class