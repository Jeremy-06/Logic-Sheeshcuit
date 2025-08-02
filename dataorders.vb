Imports MySql.Data.MySqlClient

Public Class dataorders
    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim query As String

    Private Sub dataorders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadOrdersData()
        PopulateStatusComboBox()
        PopulateSearchComboBox()

        ' Add event handler for sorting to maintain colors
        AddHandler DataGridView1.Sorted, AddressOf DataGridView1_Sorted
    End Sub

    Private Sub LoadOrdersData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"SELECT 
                         o.orderId,
                         CONCAT(c.customerFname, ' ', c.customerLname) AS customerName,
                         c.customerPhone,
                         c.customerAddress,
                         p.productName,
                         oi.productQty,
                         (oi.productQty * p.productPrice) AS totalAmount,
                         o.orderStatus,
                         DATE_FORMAT(o.orderDate, '%m/%d/%Y') AS orderDate
                      FROM orders o
                      JOIN customers c ON o.customers_customerId = c.customerId
                      JOIN orderitems oi ON o.orderId = oi.orders_orderId
                      JOIN products p ON oi.products_productId = p.productId
                      ORDER BY o.orderId DESC;"
            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "Orders")
            DataGridView1.DataSource = ds.Tables("Orders")

            ' Set column headers for cleaner display
            DataGridView1.Columns(0).HeaderText = "Order ID"
            DataGridView1.Columns(1).HeaderText = "Customer Name"
            DataGridView1.Columns(2).HeaderText = "Phone"
            DataGridView1.Columns(3).HeaderText = "Address"
            DataGridView1.Columns(4).HeaderText = "Product"
            DataGridView1.Columns(5).HeaderText = "Quantity"
            DataGridView1.Columns(6).HeaderText = "Total Amount"
            DataGridView1.Columns(7).HeaderText = "Status"
            DataGridView1.Columns(8).HeaderText = "Order Date"

            ' Color code rows based on status
            ColorCodeRows()

            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PopulateStatusComboBox()
        Try
            status.Items.Clear()
            status.Items.Add("checkout")
            status.Items.Add("paid")
            status.Items.Add("completed")
            status.Items.Add("cancelled")
        Catch ex As Exception
            MessageBox.Show("Error populating status: " + ex.Message)
        End Try
    End Sub

    Private Sub PopulateSearchComboBox()
        Try
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Product Name")
            ComboBox1.Items.Add("Customer Name")
            ComboBox1.Items.Add("Order ID")
            ComboBox1.Items.Add("Status")
            ComboBox1.Items.Add("Month")
            ComboBox1.Items.Add("Year")
            ComboBox1.SelectedIndex = 0 ' Default to first item
        Catch ex As Exception
            MessageBox.Show("Error populating search combo box: " + ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                orderID.Text = row.Cells(0).Value.ToString()
                customerName.Text = row.Cells(1).Value.ToString()
                phone.Text = row.Cells(2).Value.ToString()
                address.Text = row.Cells(3).Value.ToString()
                productName.Text = row.Cells(4).Value.ToString()
                total.Text = row.Cells(6).Value.ToString()
                orderDate.Text = row.Cells(8).Value.ToString()
                status.Text = row.Cells(7).Value.ToString()

                ' Get customer ID for the selected order
                GetCustomerID(row.Cells(0).Value.ToString())
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting row: " + ex.Message)
        End Try
    End Sub

    Private Sub GetCustomerID(orderId As String)
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"SELECT customers_customerId FROM orders WHERE orderId = {orderId}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                customerID.Text = reader("customers_customerId").ToString()
            End If
            reader.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error getting customer ID: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub GetOrderStatus(orderId As String)
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"SELECT orderStatus FROM orders WHERE orderId = {orderId}"
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                status.Text = reader("orderStatus").ToString()
            End If
            reader.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error getting order status: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub update_Click(sender As Object, e As EventArgs) Handles update.Click
        Try
            If String.IsNullOrEmpty(orderID.Text) Then
                MessageBox.Show("Please select an order to update")
                Return
            End If

            ' Check current order status before updating
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"SELECT orderStatus FROM orders WHERE orderId = {orderID.Text}"
            cmd = New MySqlCommand(query, conn)
            Dim currentStatus As String = If(cmd.ExecuteScalar() IsNot Nothing, cmd.ExecuteScalar().ToString(), "")
            If conn.State = ConnectionState.Open Then conn.Close()

            ' Check if order is already cancelled
            If currentStatus.ToLower() = "cancelled" Then
                MessageBox.Show("Cannot update orders that are already cancelled.", "Cannot Update", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Check if trying to update to "checkout" status
            If status.Text.ToLower() = "checkout" Then
                ' Prevent paid orders from going back to checkout
                If currentStatus.ToLower() = "paid" Then
                    MessageBox.Show("Cannot change paid orders back to checkout status.", "Cannot Update", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            ' Check if trying to update to "completed" status
            If status.Text.ToLower() = "completed" Then
                ' Only allow "paid" orders to be updated to "completed"
                If currentStatus.ToLower() <> "paid" Then
                    MessageBox.Show("Only paid orders can be updated to completed status. Current order status: " + currentStatus, "Cannot Update", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            End If

            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"UPDATE orders SET orderStatus = '{status.Text}' WHERE orderId = {orderID.Text}"
            cmd = New MySqlCommand(query, conn)

            Dim result = cmd.ExecuteNonQuery()
            If result > 0 Then
                ' If status is changed to "completed", add to sales
                If status.Text.ToLower() = "completed" Then
                    Dim totalAmount As Decimal = 0
                    If Decimal.TryParse(total.Text.Replace("₱", "").Replace(",", ""), totalAmount) Then
                        AddToSales(Convert.ToInt32(orderID.Text), totalAmount)
                    Else
                        MessageBox.Show("Warning: Could not parse total amount for sales record.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End If

                MessageBox.Show("Order updated successfully!")
                If conn.State = ConnectionState.Open Then conn.Close()

                RefreshDataGrid()

                ClearInputs()
            Else
                MessageBox.Show("Failed to update order")
                If conn.State = ConnectionState.Open Then conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating order: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub AddToSales(orderId As Integer, totalAmount As Decimal)
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Check if sales record already exists
            query = $"SELECT salesId FROM sales WHERE orderId = {orderId}"
            cmd = New MySqlCommand(query, conn)
            Dim existingSales = cmd.ExecuteScalar()

            If existingSales Is Nothing Then
                ' Create sales record (without totalAmount since it's not in the table structure)
                query = $"INSERT INTO sales (salesDate, orderId) VALUES (CURDATE(), {orderId})"
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                MessageBox.Show("Order completed and added to sales records.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("Error adding to sales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub RefreshDataGrid()
        Try
            ' Clear existing data
            DataGridView1.DataSource = Nothing

            ' Reload the data
            LoadOrdersData()

            ' Force the grid to refresh
            DataGridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error refreshing data: " + ex.Message)
        End Try
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click
        Try
            If String.IsNullOrEmpty(orderID.Text) Then
                MessageBox.Show("Please select an order to delete")
                Return
            End If

            ' Check if order status is cancelled
            If conn.State <> ConnectionState.Open Then conn.Open()
            query = $"SELECT orderStatus FROM orders WHERE orderId = {orderID.Text}"
            cmd = New MySqlCommand(query, conn)
            Dim orderStatus As String = If(cmd.ExecuteScalar() IsNot Nothing, cmd.ExecuteScalar().ToString(), Nothing)
            If conn.State = ConnectionState.Open Then conn.Close()

            If orderStatus IsNot Nothing AndAlso orderStatus.ToLower() = "cancelled" Then
                Dim confirm = MessageBox.Show("Are you sure you want to delete this cancelled order?", "Confirm Delete", MessageBoxButtons.YesNo)
                If confirm = DialogResult.Yes Then
                    If conn.State <> ConnectionState.Open Then conn.Open()

                    ' Delete order items first (foreign key constraint)
                    query = $"DELETE FROM orderitems WHERE orders_orderId = {orderID.Text}"
                    cmd = New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()

                    ' Delete the order
                    query = $"DELETE FROM orders WHERE orderId = {orderID.Text}"
                    cmd = New MySqlCommand(query, conn)

                    Dim result = cmd.ExecuteNonQuery()
                    If result > 0 Then
                        MessageBox.Show("Cancelled order deleted successfully!")
                        If conn.State = ConnectionState.Open Then conn.Close()

                        ' Refresh the data grid
                        RefreshDataGrid()

                        ' Clear the form inputs
                        ClearInputs()
                    Else
                        MessageBox.Show("Failed to delete order")
                        If conn.State = ConnectionState.Open Then conn.Close()
                    End If
                End If
            Else
                MessageBox.Show("Only cancelled orders can be deleted. Current order status: " + If(orderStatus, "Unknown"), "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting order: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        RefreshDataGrid()
    End Sub

    Private Sub ClearInputs()
        orderID.Clear()
        customerID.Clear()
        customerName.Clear()
        phone.Clear()
        address.Clear()
        productName.Clear()
        total.Clear()
        orderDate.Clear()
        status.Text = ""
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        ' Search functionality based on selected combo box item
        Try
            If Not String.IsNullOrEmpty(TextBox6.Text) Then
                If conn.State <> ConnectionState.Open Then conn.Open()

                Dim searchType As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "Product Name")
                Dim searchCondition As String = ""

                Select Case searchType
                    Case "Product Name"
                        searchCondition = $"p.productName LIKE '%{TextBox6.Text}%'"
                    Case "Customer Name"
                        searchCondition = $"CONCAT(c.customerFname, ' ', c.customerLname) LIKE '%{TextBox6.Text}%'"
                    Case "Order ID"
                        ' Only allow numbers for Order ID
                        If IsNumeric(TextBox6.Text) Then
                            searchCondition = $"o.orderId = {TextBox6.Text}"
                        Else
                            MessageBox.Show("Order ID must be a number only.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBox6.Text = ""
                            If conn.State = ConnectionState.Open Then conn.Close()
                            Return
                        End If
                    Case "Status"
                        searchCondition = $"o.orderStatus LIKE '%{TextBox6.Text}%'"
                    Case "Month"
                        ' Allow both numbers (1-12) and month names
                        Dim monthNumber As Integer = 0
                        If IsNumeric(TextBox6.Text) Then
                            ' If it's a number, check if it's valid (1-12)
                            monthNumber = Convert.ToInt32(TextBox6.Text)
                            If monthNumber >= 1 AndAlso monthNumber <= 12 Then
                                searchCondition = $"MONTH(o.orderDate) = {monthNumber}"
                            Else
                                MessageBox.Show("Month number must be between 1 and 12.", "Invalid Month", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBox6.Text = ""
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        Else
                            ' If it's text, try to convert month name to number
                            monthNumber = GetMonthNumber(TextBox6.Text)
                            If monthNumber > 0 Then
                                searchCondition = $"MONTH(o.orderDate) = {monthNumber}"
                            Else
                                ' Don't show error message for partial month names, just don't search
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        End If
                    Case "Year"
                        ' Only allow numbers for Year and only search when exactly 4 digits
                        If IsNumeric(TextBox6.Text) Then
                            If TextBox6.Text.Length = 4 Then
                                searchCondition = $"YEAR(o.orderDate) = {TextBox6.Text}"
                            ElseIf TextBox6.Text.Length > 4 Then
                                MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBox6.Text = ""
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            Else
                                ' Don't search if less than 4 digits, just return
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        Else
                            MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            TextBox6.Text = ""
                            If conn.State = ConnectionState.Open Then conn.Close()
                            Return
                        End If
                    Case Else
                        searchCondition = $"p.productName LIKE '%{TextBox6.Text}%'"
                End Select

                query = $"SELECT 
                             o.orderId,
                             CONCAT(c.customerFname, ' ', c.customerLname) AS customerName,
                             c.customerPhone,
                             c.customerAddress,
                             p.productName,
                             oi.productQty,
                             (oi.productQty * p.productPrice) AS totalAmount,
                             o.orderStatus,
                             DATE_FORMAT(o.orderDate, '%m/%d/%Y') AS orderDate
                          FROM orders o
                          JOIN customers c ON o.customers_customerId = c.customerId
                          JOIN orderitems oi ON o.orderId = oi.orders_orderId
                          JOIN products p ON oi.products_productId = p.productId
                          WHERE {searchCondition}
                          ORDER BY o.orderId DESC;"
                cmd = New MySqlCommand(query, conn)
                da = New MySqlDataAdapter(cmd)
                ds = New DataSet()
                da.Fill(ds, "Orders")
                DataGridView1.DataSource = ds.Tables("Orders")

                ' Color code rows based on status
                ColorCodeRows()

                If conn.State = ConnectionState.Open Then conn.Close()
            Else
                LoadOrdersData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Function GetMonthNumber(monthName As String) As Integer
        Dim monthLower As String = monthName.ToLower().Trim()

        Select Case monthLower
            Case "january", "jan"
                Return 1
            Case "february", "feb"
                Return 2
            Case "march", "mar"
                Return 3
            Case "april", "apr"
                Return 4
            Case "may"
                Return 5
            Case "june", "jun"
                Return 6
            Case "july", "jul"
                Return 7
            Case "august", "aug"
                Return 8
            Case "september", "sept", "sep"
                Return 9
            Case "october", "oct"
                Return 10
            Case "november", "nov"
                Return 11
            Case "december", "dec"
                Return 12
        End Select

        If monthLower.Length >= 3 Then
            Select Case monthLower
                Case "jan", "janu", "janua", "januar"
                    Return 1
                Case "feb", "febr", "febru", "februa", "februar"
                    Return 2
                Case "mar", "marc"
                    Return 3
                Case "apr", "apri"
                    Return 4
                Case "may"
                    Return 5
                Case "jun", "june"
                    Return 6
                Case "jul", "july"
                    Return 7
                Case "aug", "augu", "augus"
                    Return 8
                Case "sep", "sept", "septe", "septem", "septemb", "septembe"
                    Return 9
                Case "oct", "octo", "octob", "octobe"
                    Return 10
                Case "nov", "nove", "novem", "novemb", "novembe"
                    Return 11
                Case "dec", "dece", "decem", "decemb", "decembe"
                    Return 12
            End Select
        End If

        Return 0
    End Function

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        ' Search button functionality (same as text changed)
        TextBox6_TextChanged(sender, e)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Refresh data and clear inputs
        RefreshDataGrid()
        ClearInputs()
        TextBox6.Clear()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' If there's text in the search box, perform search with new criteria
        If Not String.IsNullOrEmpty(TextBox6.Text) Then
            TextBox6_TextChanged(sender, e)
        End If
    End Sub

    Private Sub ColorCodeRows()
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(7).Value IsNot Nothing Then
                    Dim status As String = row.Cells(7).Value.ToString().ToLower()

                    Select Case status
                        Case "checkout"
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 245, 200) ' Pastel Orange/Yellow
                        Case "paid"
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 255, 230) ' Pastel Green
                        Case "completed"
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230, 255) ' Pastel Blue
                        Case "cancelled"
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 230, 230) ' Pastel Red
                        Case Else
                            row.DefaultCellStyle.BackColor = Color.White ' Default white
                    End Select
                End If
            Next
        Catch ex As Exception
            ' Silently handle any coloring errors
        End Try
    End Sub

    Private Sub DataGridView1_Sorted(sender As Object, e As EventArgs)
        ColorCodeRows()
    End Sub
End Class