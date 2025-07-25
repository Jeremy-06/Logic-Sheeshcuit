﻿Imports MySql.Data.MySqlClient

Public Class userprofile
    ' Database connection objects
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet
    Private reader As MySqlDataReader
    Private query As String

    Private Sub userprofile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUserProfile()
    End Sub

    Private Sub LoadUserProfile()
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

            ' Get customer information using the logged-in customer ID
            query = $"SELECT customerId, customerFname, customerLname, customerAddress, customerPhone FROM customers WHERE customerId = {login.customerId}"
            cmd = New MySqlCommand(query, conn)
            reader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Set Label1 to Customer ID
                Label1.Text = "Customer ID: " & reader.GetInt32("customerId").ToString()

                ' Set Label2 to First Name + Last Name (concatenated)
                Dim firstName As String = reader.GetString("customerFname")
                Dim lastName As String = reader.GetString("customerLname")
                Label2.Text = "Name: " & firstName & " " & lastName

                ' Set Label3 to Address
                Label3.Text = "Address: " & reader.GetString("customerAddress")

                ' Set Label4 to Phone
                Label4.Text = "Phone: " & reader.GetString("customerPhone")
            Else
                MessageBox.Show("Customer information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            reader.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading user profile: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If reader IsNot Nothing AndAlso Not reader.IsClosed Then
                reader.Close()
            End If
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Log out functionality
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            login.customerId = 0 ' Reset customer ID to indicate not logged in
            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information)
            login.Show()
            Me.Hide()
        End If
    End Sub
End Class