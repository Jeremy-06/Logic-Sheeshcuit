Imports MySql.Data.MySqlClient
Imports System.Data

Public Class datasales
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet

    ' Flag to prevent search when populating textboxes from row selection
    Private isPopulatingFromRow As Boolean = False
    ' Flag to track if DateTimePicker has been modified
    Private isDateFilterActive As Boolean = False

    Private Sub datasales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set DateTimePicker to show no date (null value)
        DateTimePicker1.Value = DateTimePicker1.MinDate
        ' Set max date to current date
        DateTimePicker1.MaxDate = DateTime.Today
        LoadSalesData()
        PopulateSearchComboBox()
    End Sub

    Private Sub PopulateSearchComboBox()
        Try
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Product Name")
            ComboBox1.Items.Add("Month")
            ComboBox1.Items.Add("Year")
            ComboBox1.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show("Error populating search combo box: " + ex.Message)
        End Try
    End Sub

    Private Sub LoadSalesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            Dim query As String = ""

            ' Check if date filter is active
            If isDateFilterActive AndAlso DateTimePicker1.Value <> DateTimePicker1.MinDate Then
                ' SQL query with date filter
                query = $"
                    SELECT 
                        s.salesId AS 'Sales ID',
                        DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS 'Date',
                        o.orderId AS 'Order ID',
                        CONCAT (c.customerFname, ' ', c.customerLname) AS 'Customer Name',
                        p.productName AS 'Product Name',
                        oi.productQty AS 'Quantity',
                        p.productPrice AS 'Price',
                        ROUND(oi.productQty * p.productPrice, 2) AS 'Total'
                    FROM sales s
                    JOIN orders o ON s.orderId = o.orderId
                    JOIN customers c ON o.customers_customerId = c.customerId
                    JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    JOIN products p ON oi.products_productId = p.productId
                    WHERE LOWER(o.orderStatus) = 'completed'
                    AND DATE(s.salesDate) = '{DateTimePicker1.Value:yyyy-MM-dd}'
                    ORDER BY s.salesDate DESC"
            Else
                ' SQL query to show all sales by default
                query = $"
                    SELECT 
                        s.salesId AS 'Sales ID',
                        DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS 'Date',
                        o.orderId AS 'Order ID',
                        CONCAT (c.customerFname, ' ', c.customerLname) AS 'Customer Name',
                        p.productName AS 'Product Name',
                        oi.productQty AS 'Quantity',
                        p.productPrice AS 'Price',
                        ROUND(oi.productQty * p.productPrice, 2) AS 'Total'
                    FROM sales s
                    JOIN orders o ON s.orderId = o.orderId
                    JOIN customers c ON o.customers_customerId = c.customerId
                    JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    JOIN products p ON oi.products_productId = p.productId
                    WHERE LOWER(o.orderStatus) = 'completed'
                    ORDER BY s.salesDate DESC"
            End If

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "SalesData")

            DataGridView1.DataSource = ds.Tables("SalesData")
            FormatDataGridView()
            UpdateTotalAmount()

            If conn.State = ConnectionState.Open Then conn.Close()
        Catch ex As Exception
            MessageBox.Show($"Error loading sales data: {ex.Message}")
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub FormatDataGridView()
        If DataGridView1.Columns.Count > 0 Then
            ' Format currency columns
            DataGridView1.Columns("Price").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("Price").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DataGridView1.Columns("Total").DefaultCellStyle.Format = "C2"
            DataGridView1.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            ' Format quantity column
            DataGridView1.Columns("Quantity").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' Format date column
            DataGridView1.Columns("Date").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            ' Auto-size columns
            DataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

            ' Clear any existing row highlighting
            ClearRowHighlighting()
        End If
    End Sub

    ' Method to clear all row highlighting
    Private Sub ClearRowHighlighting()
        Try
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.DefaultCellStyle.BackColor = Color.White
            Next
        Catch ex As Exception
            ' Handle any errors silently
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

    ' Search button functionality
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox8_TextChanged(sender, e)
    End Sub

    ' Refresh button functionality
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Reset to initial state
        isDateFilterActive = False
        DateTimePicker1.Value = DateTimePicker1.MinDate
        LoadSalesData()
        TextBox8.Clear()
        ClearTextboxes()
    End Sub

    ' DateTimePicker value changed event
    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ' Set flag to indicate date filter is active
        isDateFilterActive = True
        LoadSalesData()
        TextBox8.Clear()
        ClearTextboxes()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' If there's text in the search box, perform search with new criteria
        If Not String.IsNullOrEmpty(TextBox8.Text) Then
            TextBox8_TextChanged(sender, e)
        End If
    End Sub

    ' Search functionality - works independently of date filter
    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Try
            If Not String.IsNullOrEmpty(TextBox8.Text) Then
                ' Clear input textboxes when search starts
                ClearInputTextboxes()

                ' Only perform search if not populating from row selection
                If Not isPopulatingFromRow Then
                    If conn.State <> ConnectionState.Open Then conn.Open()

                    Dim searchType As String = If(ComboBox1.SelectedItem IsNot Nothing, ComboBox1.SelectedItem.ToString(), "Product Name")
                    Dim searchCondition As String = ""

                    Select Case searchType
                        Case "Product Name"
                            searchCondition = $"p.productName LIKE '%{TextBox8.Text}%'"
                        Case "Month"
                            ' Allow both numbers (1-12) and month names
                            Dim monthNumber As Integer = 0
                            If IsNumeric(TextBox8.Text) Then
                                ' If it's a number, check if it's valid (1-12)
                                monthNumber = Convert.ToInt32(TextBox8.Text)
                                If monthNumber >= 1 AndAlso monthNumber <= 12 Then
                                    searchCondition = $"MONTH(s.salesDate) = {monthNumber}"
                                Else
                                    MessageBox.Show("Month number must be between 1 and 12.", "Invalid Month", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    TextBox8.Text = ""
                                    If conn.State = ConnectionState.Open Then conn.Close()
                                    Return
                                End If
                            Else
                                ' If it's text, try to convert month name to number
                                monthNumber = GetMonthNumber(TextBox8.Text)
                                If monthNumber > 0 Then
                                    searchCondition = $"MONTH(s.salesDate) = {monthNumber}"
                                Else
                                    ' Don't show error message for partial month names, just don't search
                                    If conn.State = ConnectionState.Open Then conn.Close()
                                    Return
                                End If
                            End If
                        Case "Year"
                            ' Only allow numbers for Year and only search when exactly 4 digits
                            If IsNumeric(TextBox8.Text) Then
                                If TextBox8.Text.Length = 4 Then
                                    searchCondition = $"YEAR(s.salesDate) = {TextBox8.Text}"
                                ElseIf TextBox8.Text.Length > 4 Then
                                    MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    TextBox8.Text = ""
                                    If conn.State = ConnectionState.Open Then conn.Close()
                                    Return
                                Else
                                    ' Don't search if less than 4 digits, just return
                                    If conn.State = ConnectionState.Open Then conn.Close()
                                    Return
                                End If
                            Else
                                MessageBox.Show("Year must be a 4-digit number (e.g., 2024).", "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                TextBox8.Text = ""
                                If conn.State = ConnectionState.Open Then conn.Close()
                                Return
                            End If
                        Case Else
                            searchCondition = $"p.productName LIKE '%{TextBox8.Text}%'"
                    End Select

                    Dim query As String = $"
                        SELECT 
                            s.salesId AS 'Sales ID',
                            DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS 'Date',
                            o.orderId AS 'Order ID',
                            CONCAT (c.customerFname, ' ', c.customerLname) AS 'Customer Name',
                            p.productName AS 'Product Name',
                            oi.productQty AS 'Quantity',
                            p.productPrice AS 'Price',
                            ROUND(oi.productQty * p.productPrice, 2) AS 'Total'
                        FROM sales s
                        JOIN orders o ON s.orderId = o.orderId
                        JOIN customers c ON o.customers_customerId = c.customerId
                        JOIN orderitems oi ON o.orderId = oi.orders_orderId
                        JOIN products p ON oi.products_productId = p.productId
                        WHERE LOWER(o.orderStatus) = 'completed' 
                        AND {searchCondition}
                        ORDER BY s.salesDate DESC"

                    cmd = New MySqlCommand(query, conn)
                    da = New MySqlDataAdapter(cmd)
                    ds = New DataSet()
                    da.Fill(ds, "SalesData")
                    DataGridView1.DataSource = ds.Tables("SalesData")
                    FormatDataGridView()
                    UpdateTotalAmount()

                    If conn.State = ConnectionState.Open Then conn.Close()
                End If
            Else
                ' Only reload data if not populating from row selection
                If Not isPopulatingFromRow Then
                    LoadSalesData()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error searching: " + ex.Message)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub ClearTextboxes()
        isPopulatingFromRow = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        salesDate.Clear()
        isPopulatingFromRow = False
    End Sub

    Private Sub ClearInputTextboxes()
        isPopulatingFromRow = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        salesDate.Clear()
        isPopulatingFromRow = False
    End Sub

    ' Refresh data
    Private Sub RefreshData()
        LoadSalesData()
    End Sub

    ' Cell click event
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            ' Set flag to prevent search from triggering
            isPopulatingFromRow = True

            ' Populate textboxes with selected row data using column indices
            TextBox1.Text = row.Cells(0).Value?.ToString() ' Sales ID
            TextBox2.Text = row.Cells(3).Value?.ToString() ' Customer Name
            TextBox3.Text = row.Cells(2).Value?.ToString() ' Order ID
            TextBox4.Text = row.Cells(4).Value?.ToString() ' Product Name
            TextBox5.Text = row.Cells(5).Value?.ToString() ' Quantity
            TextBox6.Text = row.Cells(6).Value?.ToString() ' Price
            TextBox7.Text = row.Cells(7).Value?.ToString() ' Total
            salesDate.Text = row.Cells(1).Value?.ToString() ' Date

            ' Highlight rows with the same Sales ID
            HighlightRowsWithSameSalesId(row.Cells(0).Value?.ToString())

            ' Reset flag after populating
            isPopulatingFromRow = False
        End If
    End Sub

    ' Method to highlight rows with the same Sales ID
    Private Sub HighlightRowsWithSameSalesId(selectedSalesId As String)
        Try
            ' Clear all row highlighting first
            For Each row As DataGridViewRow In DataGridView1.Rows
                row.DefaultCellStyle.BackColor = Color.White
            Next

            ' If no sales ID selected, return
            If String.IsNullOrEmpty(selectedSalesId) Then
                Return
            End If

            ' Highlight rows with the same Sales ID
            For Each row As DataGridViewRow In DataGridView1.Rows
                Dim rowSalesId As String = row.Cells(0).Value?.ToString()
                If rowSalesId = selectedSalesId Then
                    row.DefaultCellStyle.BackColor = Color.LightGray
                End If
            Next
        Catch ex As Exception
            ' Handle any errors silently
        End Try
    End Sub

    Private Sub UpdateTotalAmount()
        Try
            Dim total As Decimal = 0
            For Each row As DataRow In ds.Tables("SalesData").Rows
                If Not IsDBNull(row("Total")) Then
                    total += Convert.ToDecimal(row("Total"))
                End If
            Next
            Label11.Text = total.ToString("C2")
        Catch ex As Exception
            Label11.Text = "$0.00"
        End Try
    End Sub

    Private Sub salesReport_btn_Click(sender As Object, e As EventArgs) Handles salesReport_btn.Click
        salesreport.Show()
    End Sub
End Class