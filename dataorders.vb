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
    End Sub

    Private Sub LoadOrdersData()
        Try
            conn.Open()
            query = "SELECT 
                         o.orderId,
                         CONCAT(c.customerFname, ' ', c.customerLname) AS customerName,
                         c.customerPhone,
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
            DataGridView1.Columns(3).HeaderText = "Product"
            DataGridView1.Columns(4).HeaderText = "Quantity"
            DataGridView1.Columns(5).HeaderText = "Total Amount"
            DataGridView1.Columns(6).HeaderText = "Status"
            DataGridView1.Columns(7).HeaderText = "Order Date"

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading orders: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PopulateStatusComboBox()
        Try
            status.Items.Clear()
            status.Items.Add("pending")
            status.Items.Add("paid")
            status.Items.Add("completed")
            status.Items.Add("cancelled")
        Catch ex As Exception
            MessageBox.Show("Error populating status: " + ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                orderID.Text = row.Cells(0).Value.ToString()
                customerName.Text = row.Cells(1).Value.ToString()
                phone.Text = row.Cells(2).Value.ToString()
                productName.Text = row.Cells(3).Value.ToString()
                total.Text = row.Cells(5).Value.ToString()
                orderDate.Text = row.Cells(7).Value.ToString()
                status.Text = row.Cells(6).Value.ToString()

                ' Get customer ID for the selected order
                GetCustomerID(row.Cells(0).Value.ToString())
            End If
        Catch ex As Exception
            MessageBox.Show("Error selecting row: " + ex.Message)
        End Try
    End Sub

    Private Sub GetCustomerID(orderId As String)
        Try
            conn.Open()
            query = "SELECT customers_customerId FROM orders WHERE orderId = " + orderId
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                customerID.Text = reader("customers_customerId").ToString()
            End If
            reader.Close()
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Error getting customer ID: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub GetOrderStatus(orderId As String)
        Try
            conn.Open()
            query = "SELECT orderStatus FROM orders WHERE orderId = " + orderId
            cmd = New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                status.Text = reader("orderStatus").ToString()
            End If
            reader.Close()
            conn.Close()
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

            conn.Open()
            query = "UPDATE orders SET orderStatus = '" + status.Text + "' WHERE orderId = " + orderID.Text
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
                conn.Close()

                ' Refresh the data grid
                RefreshDataGrid()

                ' Clear the form inputs
                ClearInputs()
            Else
                MessageBox.Show("Failed to update order")
                conn.Close()
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
            conn.Open()

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
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
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

            Dim confirm = MessageBox.Show("Are you sure you want to delete this order?", "Confirm Delete", MessageBoxButtons.YesNo)
            If confirm = DialogResult.Yes Then
                conn.Open()

                ' Delete order items first (foreign key constraint)
                query = "DELETE FROM orderitems WHERE orders_orderId = " + orderID.Text
                cmd = New MySqlCommand(query, conn)
                cmd.ExecuteNonQuery()

                ' Delete the order
                query = "DELETE FROM orders WHERE orderId = " + orderID.Text
                cmd = New MySqlCommand(query, conn)

                Dim result = cmd.ExecuteNonQuery()
                If result > 0 Then
                    MessageBox.Show("Order deleted successfully!")
                    conn.Close()

                    ' Refresh the data grid
                    RefreshDataGrid()

                    ' Clear the form inputs
                    ClearInputs()
                Else
                    MessageBox.Show("Failed to delete order")
                    conn.Close()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error deleting order: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        RefreshDataGrid()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ClearInputs()
    End Sub

    Private Sub ClearInputs()
        orderID.Clear()
        customerID.Clear()
        customerName.Clear()
        phone.Clear()
        productName.Clear()
        total.Clear()
        orderDate.Clear()
        status.Text = ""
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        ' Search functionality
        Try
            If Not String.IsNullOrEmpty(TextBox4.Text) Then
                conn.Open()
                query = "SELECT 
                             o.orderId,
                             CONCAT(c.customerFname, ' ', c.customerLname) AS customerName,
                             c.customerPhone,
                             p.productName,
                             oi.productQty,
                             (oi.productQty * p.productPrice) AS totalAmount,
                             o.orderStatus,
                             DATE_FORMAT(o.orderDate, '%m/%d/%Y') AS orderDate
                          FROM orders o
                          JOIN customers c ON o.customers_customerId = c.customerId
                          JOIN orderitems oi ON o.orderId = oi.orders_orderId
                          JOIN products p ON oi.products_productId = p.productId
                          WHERE o.orderId LIKE '%" + TextBox4.Text + "%' 
                          OR CONCAT(c.customerFname, ' ', c.customerLname) LIKE '%" + TextBox4.Text + "%'
                          OR p.productName LIKE '%" + TextBox4.Text + "%'
                          OR o.orderStatus LIKE '%" + TextBox4.Text + "%'
                          ORDER BY o.orderId DESC;"
                cmd = New MySqlCommand(query, conn)
                da = New MySqlDataAdapter(cmd)
                ds = New DataSet()
                da.Fill(ds, "Orders")
                DataGridView1.DataSource = ds.Tables("Orders")
                conn.Close()
            Else
                LoadOrdersData()
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class