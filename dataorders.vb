'Imports MySql.Data.MySqlClient

'Public Class dataorders
'    Dim conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
'    Dim cmd As MySqlCommand
'    Dim da As MySqlDataAdapter
'    Dim ds As DataSet
'    Dim query As String

'    Private Sub dataorders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        LoadAllTables()
'        SetupDataGridViews()
'    End Sub

'    Private Sub LoadAllTables()
'        LoadOrderProcessing()
'        LoadCustomerOrderMgmt()
'        LoadSales()
'        LoadCustomers()
'        LoadProducts()
'        LoadCartMonitoring()
'    End Sub

'    Private Sub SetupDataGridViews()
'        dgvOrderProcessing.ReadOnly = False
'        dgvCustomerOrderMgmt.ReadOnly = False
'        dgvSales.ReadOnly = False
'        dgvCustomers.ReadOnly = True
'        dgvProducts.ReadOnly = True
'        dgvCartMonitoring.ReadOnly = True
'        dgvOrderProcessing.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'        dgvCustomerOrderMgmt.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'        dgvSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'        dgvCartMonitoring.SelectionMode = DataGridViewSelectionMode.FullRowSelect
'    End Sub

'    Private Sub LoadOrderProcessing()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT o.orderId, o.orderDate, o.orderStatus, o.customers_customerId, oi.orderItemsId, oi.productQty, oi.orders_orderId, oi.products_productId, s.salesId, s.salesDate, s.orderId as salesOrderId FROM orders o JOIN orderitems oi ON o.orderId = oi.orders_orderId LEFT JOIN sales s ON o.orderId = s.orderId;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "OrderProcessing")
'            dgvOrderProcessing.DataSource = ds.Tables("OrderProcessing")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Order Processing: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub LoadCustomerOrderMgmt()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT o.orderId, o.orderDate, o.orderStatus, o.customers_customerId, oi.orderItemsId, oi.productQty, oi.orders_orderId, oi.products_productId, c.customerId, c.customerFname, c.customerLname, c.customerAddress, c.customerPhone, c.userId, p.productId, p.productName, p.productPrice, p.productCategories_categoryId FROM orders o JOIN orderitems oi ON o.orderId = oi.orders_orderId JOIN customers c ON o.customers_customerId = c.customerId JOIN products p ON oi.products_productId = p.productId;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "CustomerOrderMgmt")
'            dgvCustomerOrderMgmt.DataSource = ds.Tables("CustomerOrderMgmt")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Customer Order Management: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub LoadSales()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT * FROM sales;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "Sales")
'            dgvSales.DataSource = ds.Tables("Sales")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Sales: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub LoadCustomers()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT * FROM customers;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "Customers")
'            dgvCustomers.DataSource = ds.Tables("Customers")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Customers: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub LoadProducts()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT * FROM products;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "Products")
'            dgvProducts.DataSource = ds.Tables("Products")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Products: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub LoadCartMonitoring()
'        Try
'            If conn.State = ConnectionState.Open Then conn.Close()
'            conn.Open()
'            query = "SELECT c.cartId, c.products_productId, c.customers_customerId, c.productQty, p.productId, p.productName, p.productPrice, p.productCategories_categoryId, cu.customerId, cu.customerFname, cu.customerLname, cu.customerAddress, cu.customerPhone, cu.userId FROM cart c JOIN products p ON c.products_productId = p.productId JOIN customers cu ON c.customers_customerId = cu.customerId;"
'            cmd = New MySqlCommand(query, conn)
'            da = New MySqlDataAdapter(cmd)
'            ds = New DataSet()
'            da.Fill(ds, "CartMonitoring")
'            dgvCartMonitoring.DataSource = ds.Tables("CartMonitoring")
'        Catch ex As Exception
'            MessageBox.Show("Error loading Cart Monitoring: " & ex.Message)
'        Finally
'            If conn.State = ConnectionState.Open Then conn.Close()
'        End Try
'    End Sub

'    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
'        LoadAllTables()
'    End Sub

'    ' Example CRUD for Order Processing (implement similar for other full-modify tables as needed)
'    Private Sub btnInsertOrder_Click(sender As Object, e As EventArgs) Handles btnInsertOrder.Click
'        ' TODO: Implement insert logic for orders/orderitems/sales
'        MessageBox.Show("Insert Order logic goes here.")
'    End Sub

'    Private Sub btnUpdateOrder_Click(sender As Object, e As EventArgs) Handles btnUpdateOrder.Click
'        ' TODO: Implement update logic for orders/orderitems/sales
'        MessageBox.Show("Update Order logic goes here.")
'    End Sub

'    Private Sub btnDeleteOrder_Click(sender As Object, e As EventArgs) Handles btnDeleteOrder.Click
'        ' TODO: Implement delete logic for orders/orderitems/sales
'        MessageBox.Show("Delete Order logic goes here.")
'    End Sub
'End Class