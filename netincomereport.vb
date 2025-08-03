Imports MySql.Data.MySqlClient
Imports System.Data

Public Class netincomereport
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet

    Private Sub netincomereport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set date constraints first
        DateTimePicker1.MinDate = DateTime.Today.AddYears(-5) ' Allow up to 5 years in the past
        DateTimePicker1.MaxDate = DateTime.Today ' Cannot select future dates
        DateTimePicker2.MinDate = DateTime.Today.AddYears(-5) ' Allow up to 5 years in the past
        DateTimePicker2.MaxDate = DateTime.Today ' Cannot select future dates

        ' Set default dates (current date and 30 days ago)
        Dim defaultStartDate As DateTime = DateTime.Today.AddDays(-30)
        Dim defaultEndDate As DateTime = DateTime.Today

        ' Ensure dates are within allowed range
        If defaultStartDate < DateTimePicker1.MinDate Then
            defaultStartDate = DateTimePicker1.MinDate
        End If
        If defaultEndDate > DateTimePicker1.MaxDate Then
            defaultEndDate = DateTimePicker1.MaxDate
        End If

        DateTimePicker1.Value = defaultStartDate
        DateTimePicker2.Value = defaultEndDate

        ' Set maximum date for DateTimePicker2 to 30 days after DateTimePicker1
        DateTimePicker2.MaxDate = DateTimePicker1.Value.AddDays(30)

        ' Initialize ComboBoxes for Chart 1
        LoadMonthComboBoxes()
        LoadYearComboBoxes()

        ' Initialize charts
        SetupCharts()

        ' Load sample data for Charts 2 and 3 (August)
        Dim augustDate As DateTime = New DateTime(DateTime.Now.Year, 8, 1)
        LoadExpensesBreakdownData(augustDate)
        LoadSalesByCategoryData(augustDate)

        ' Set chart title for Chart 1
        Chart1.Titles.Clear()
        Chart1.Titles.Add("Net Income Report")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)
    End Sub

    Private Sub LoadMonthComboBoxes()
        ' Load months for ComboBox1 and ComboBox2
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()

        Dim months As String() = {"January", "February", "March", "April", "May", "June",
                                 "July", "August", "September", "October", "November", "December"}

        For Each monthItem In months
            ComboBox1.Items.Add(monthItem)
            ComboBox2.Items.Add(monthItem)
        Next

        ' Set default selections
        ComboBox1.SelectedIndex = DateTime.Now.Month - 1 ' Current month
        ComboBox2.SelectedIndex = DateTime.Now.Month - 1 ' Current month
    End Sub

    Private Sub LoadYearComboBoxes()
        ' Load years for ComboBox3 (current year and 2 years before)
        ComboBox3.Items.Clear()
        Dim currentYear As Integer = DateTime.Now.Year
        For i As Integer = currentYear - 2 To currentYear
            ComboBox3.Items.Add(i.ToString())
        Next

        ' Set default selection to current year
        ComboBox3.SelectedIndex = 2 ' Current year (index 2: currentYear-2, currentYear-1, currentYear)

        ' Load years for ComboBox4 based on ComboBox3 selection
        LoadYearComboBox4()
    End Sub

    Private Sub LoadYearComboBox4()
        ComboBox4.Items.Clear()

        If ComboBox3.SelectedItem IsNot Nothing Then
            Dim selectedYear As Integer = Convert.ToInt32(ComboBox3.SelectedItem.ToString())

            ' Add 4 years ahead from the year AFTER the selected year in ComboBox3
            For i As Integer = selectedYear + 1 To selectedYear + 4
                ComboBox4.Items.Add(i.ToString())
            Next

            ' Set default selection to the first year in the range (year after selected year)
            ComboBox4.SelectedIndex = 0 ' First year in the range
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ' When ComboBox3 selection changes, update ComboBox4
        LoadYearComboBox4()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        ' Update DateTimePicker2 maximum date to 30 days after selected date
        DateTimePicker2.MaxDate = DateTimePicker1.Value.AddDays(30)

        ' If current DateTimePicker2 value exceeds the new maximum, adjust it
        If DateTimePicker2.Value > DateTimePicker2.MaxDate Then
            DateTimePicker2.Value = DateTimePicker2.MaxDate
        End If
    End Sub

    Private Sub SetupCharts()
        ' Setup Chart 1: Net Income (Sales vs Expenses)
        SetupNetIncomeChart()

        ' Setup Chart 2: Expenses Breakdown
        SetupExpensesBreakdownChart()

        ' Setup Chart 3: Sales by Product Category
        SetupSalesByCategoryChart()
    End Sub

    Private Sub SetupNetIncomeChart()
        ' Clear existing data
        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()
        Chart1.Titles.Clear()

        ' Add chart area and legend
        Chart1.ChartAreas.Add("ChartArea1")
        Chart1.Legends.Add("Legend1")

        ' Set chart title
        Chart1.Titles.Add("Net Income Report")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)
    End Sub

    Private Sub SetupExpensesBreakdownChart()
        ' Clear existing data
        Chart2.Series.Clear()
        Chart2.ChartAreas.Clear()
        Chart2.Legends.Clear()
        Chart2.Titles.Clear()

        ' Add chart area and legend
        Chart2.ChartAreas.Add("ChartArea1")
        Chart2.Legends.Add("Legend1")

        ' Create Expenses Breakdown series
        Dim expensesSeries As New DataVisualization.Charting.Series("Expenses")
        expensesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Pie
        expensesSeries.ChartArea = "ChartArea1"
        expensesSeries.Legend = "Legend1"
        Chart2.Series.Add(expensesSeries)

        ' Set chart title
        Chart2.Titles.Add("Expenses Breakdown by Category (August)")
        Chart2.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)
    End Sub

    Private Sub SetupSalesByCategoryChart()
        ' Clear existing data
        Chart3.Series.Clear()
        Chart3.ChartAreas.Clear()
        Chart3.Legends.Clear()
        Chart3.Titles.Clear()

        ' Add chart area and legend
        Chart3.ChartAreas.Add("ChartArea1")
        Chart3.Legends.Add("Legend1")

        ' Create Sales by Category series
        Dim salesSeries As New DataVisualization.Charting.Series("Sales by Category")
        salesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
        salesSeries.ChartArea = "ChartArea1"
        salesSeries.Legend = "Legend1"
        salesSeries.Color = Color.Blue
        Chart3.Series.Add(salesSeries)

        ' Set chart title
        Chart3.Titles.Add("Sales by Product Category (August)")
        Chart3.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

        ' Format chart
        Chart3.ChartAreas("ChartArea1").AxisX.Title = "Product Category"
        Chart3.ChartAreas("ChartArea1").AxisY.Title = "Sales Amount (₱)"
        Chart3.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"
    End Sub

    ' ===== CHART 1 CONTROLS (Net Income) =====
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            ' Validate date range (maximum 30 days)
            Dim dateDiff As TimeSpan = DateTimePicker2.Value - DateTimePicker1.Value
            If dateDiff.Days > 30 Then
                MessageBox.Show("Date range cannot exceed 30 days. Please select a smaller range.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Clear existing chart data
            Chart1.Series.Clear()
            Chart1.ChartAreas.Clear()
            Chart1.Legends.Clear()

            ' Add chart area and legend
            Chart1.ChartAreas.Add("ChartArea1")
            Chart1.Legends.Add("Legend1")

            ' Create series for daily sales
            Dim salesSeries As New DataVisualization.Charting.Series("Daily Sales")
            salesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            salesSeries.ChartArea = "ChartArea1"
            salesSeries.Legend = "Legend1"
            salesSeries.Color = Color.Green
            Chart1.Series.Add(salesSeries)

            ' Create series for daily expenses
            Dim expensesSeries As New DataVisualization.Charting.Series("Daily Expenses")
            expensesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            expensesSeries.ChartArea = "ChartArea1"
            expensesSeries.Legend = "Legend1"
            expensesSeries.Color = Color.Red
            Chart1.Series.Add(expensesSeries)

            ' Load daily net income data
            LoadDailyNetIncomeData()

        Catch ex As Exception
            MessageBox.Show($"Error generating daily net income chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDailyNetIncomeData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Check if start and end dates are the same
            Dim isSameDate As Boolean = DateTimePicker1.Value.Date = DateTimePicker2.Value.Date

            Dim salesQuery As String
            Dim expensesQuery As String

            If isSameDate Then
                ' If same date, show single day totals
                salesQuery = $"
                    SELECT 
                        DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS SaleDate,
                        SUM(ROUND(oi.productQty * p.productPrice, 2)) AS DailySales
                    FROM sales s
                    JOIN orders o ON s.orderId = o.orderId
                    JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    JOIN products p ON oi.products_productId = p.productId
                    WHERE LOWER(o.orderStatus) = 'completed'
                    AND DATE(s.salesDate) = '{DateTimePicker1.Value:yyyy-MM-dd}'
                    GROUP BY DATE_FORMAT(s.salesDate, '%Y-%m-%d')"

                expensesQuery = $"
                    SELECT 
                        DATE_FORMAT(expenseDate, '%Y-%m-%d') AS ExpenseDate,
                        SUM(ROUND(expenseAmount, 2)) AS DailyExpenses
                    FROM expenses
                    WHERE DATE(expenseDate) = '{DateTimePicker1.Value:yyyy-MM-dd}'
                    GROUP BY DATE_FORMAT(expenseDate, '%Y-%m-%d')"
            Else
                ' If different dates, show daily breakdown
                salesQuery = $"
                    SELECT 
                        DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS SaleDate,
                        SUM(ROUND(oi.productQty * p.productPrice, 2)) AS DailySales
                    FROM sales s
                    JOIN orders o ON s.orderId = o.orderId
                    JOIN orderitems oi ON o.orderId = oi.orders_orderId
                    JOIN products p ON oi.products_productId = p.productId
                    WHERE LOWER(o.orderStatus) = 'completed'
                    AND s.salesDate >= '{DateTimePicker1.Value:yyyy-MM-dd}'
                    AND s.salesDate <= '{DateTimePicker2.Value:yyyy-MM-dd}'
                    GROUP BY DATE_FORMAT(s.salesDate, '%Y-%m-%d')
                    ORDER BY SaleDate"

                expensesQuery = $"
                    SELECT 
                        DATE_FORMAT(expenseDate, '%Y-%m-%d') AS ExpenseDate,
                        SUM(ROUND(expenseAmount, 2)) AS DailyExpenses
                    FROM expenses
                    WHERE expenseDate >= '{DateTimePicker1.Value:yyyy-MM-dd}'
                    AND expenseDate <= '{DateTimePicker2.Value:yyyy-MM-dd}'
                    GROUP BY DATE_FORMAT(expenseDate, '%Y-%m-%d')
                    ORDER BY ExpenseDate"
            End If

            ' Execute sales query
            cmd = New MySqlCommand(salesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "DailySales")

            ' Execute expenses query
            cmd = New MySqlCommand(expensesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "DailyExpenses")

            ' Clear existing data points
            Chart1.Series("Daily Sales").Points.Clear()
            Chart1.Series("Daily Expenses").Points.Clear()

            ' Calculate totals
            Dim totalSales As Decimal = 0
            Dim totalExpenses As Decimal = 0

            If isSameDate Then
                ' Process single day data - position bars in the middle
                ' Add sales data point to chart (position at X=1)
                If ds.Tables("DailySales").Rows.Count > 0 Then
                    Dim row As DataRow = ds.Tables("DailySales").Rows(0)
                    Dim saleDate As String = row("SaleDate").ToString()
                    Dim dailySales As Decimal = Convert.ToDecimal(row("DailySales"))
                    totalSales = dailySales

                    ' Add point to chart at position 1 (middle)
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.XValue = 1
                    point.YValues = {CDbl(dailySales)}
                    point.ToolTip = $"Date: {saleDate}{Environment.NewLine}Sales: ₱{dailySales:F2}"
                    point.AxisLabel = DateTimePicker1.Value.ToString("MM/dd/yyyy")
                    point.Label = $"₱{dailySales:F0}" ' Add data label on bar

                    Chart1.Series("Daily Sales").Points.Add(point)
                End If

                ' Add expenses data point to chart (position at X=1)
                If ds.Tables("DailyExpenses").Rows.Count > 0 Then
                    Dim row As DataRow = ds.Tables("DailyExpenses").Rows(0)
                    Dim expenseDate As String = row("ExpenseDate").ToString()
                    Dim dailyExpenses As Decimal = Convert.ToDecimal(row("DailyExpenses"))
                    totalExpenses = dailyExpenses

                    ' Add point to chart at position 1 (middle)
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.XValue = 1
                    point.YValues = {CDbl(dailyExpenses)}
                    point.ToolTip = $"Date: {expenseDate}{Environment.NewLine}Expenses: ₱{dailyExpenses:F2}"
                    point.AxisLabel = DateTimePicker1.Value.ToString("MM/dd/yyyy")
                    point.Label = $"₱{dailyExpenses:F0}" ' Add data label on bar

                    Chart1.Series("Daily Expenses").Points.Add(point)
                End If
            Else
                ' Process daily data for different dates
                ' Add sales data points to chart
                For Each row As DataRow In ds.Tables("DailySales").Rows
                    Dim saleDate As String = row("SaleDate").ToString()
                    Dim dailySales As Decimal = Convert.ToDecimal(row("DailySales"))
                    totalSales += dailySales

                    ' Add point to chart
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.XValue = DateTime.Parse(saleDate).ToOADate() ' Convert to OLE Automation Date
                    point.YValues = {CDbl(dailySales)}
                    point.ToolTip = $"Date: {saleDate}{Environment.NewLine}Sales: ₱{dailySales:F2}"
                    point.AxisLabel = DateTime.Parse(saleDate).ToString("MM/dd/yyyy")
                    point.Label = $"₱{dailySales:F0}" ' Add data label on bar

                    Chart1.Series("Daily Sales").Points.Add(point)
                Next

                ' Add expenses data points to chart
                For Each row As DataRow In ds.Tables("DailyExpenses").Rows
                    Dim expenseDate As String = row("ExpenseDate").ToString()
                    Dim dailyExpenses As Decimal = Convert.ToDecimal(row("DailyExpenses"))
                    totalExpenses += dailyExpenses

                    ' Add point to chart
                    Dim point As New DataVisualization.Charting.DataPoint()
                    point.XValue = DateTime.Parse(expenseDate).ToOADate() ' Convert to OLE Automation Date
                    point.YValues = {CDbl(dailyExpenses)}
                    point.ToolTip = $"Date: {expenseDate}{Environment.NewLine}Expenses: ₱{dailyExpenses:F2}"
                    point.AxisLabel = DateTime.Parse(expenseDate).ToString("MM/dd/yyyy")
                    point.Label = $"₱{dailyExpenses:F0}" ' Add data label on bar

                    Chart1.Series("Daily Expenses").Points.Add(point)
                Next
            End If

            ' Update summary labels if they exist
            If TextBox1 IsNot Nothing Then
                TextBox1.Text = $"₱{totalSales:F2}"
            End If
            If TextBox2 IsNot Nothing Then
                TextBox2.Text = $"₱{totalExpenses:F2}"
            End If
            If TextBox3 IsNot Nothing Then
                Dim netIncome As Decimal = totalSales - totalExpenses
                TextBox3.Text = $"₱{netIncome:F2}"
            End If

            ' Format chart based on data type
            If isSameDate Then
                ' Format for single day data
                Chart1.ChartAreas("ChartArea1").AxisX.Title = "Date"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Amount (₱)"
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
                Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
                Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

                ' Set X-axis to show centered single day with padding
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = -0.5
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 2.5

                ' Add chart area margins for better spacing
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Auto = False
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.X = 10
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Y = 10
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Width = 80
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Height = 80

                ' Set chart title for single day view
                Chart1.Titles.Clear()
                Chart1.Titles.Add($"Daily Net Income Report ({DateTimePicker1.Value:MM/dd/yyyy})")
                Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)
            Else
                ' Format for daily data
                Chart1.ChartAreas("ChartArea1").AxisX.Title = "Date"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Amount (₱)"
                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "MM/dd/yyyy"
                Chart1.ChartAreas("ChartArea1").AxisX.IntervalType = DataVisualization.Charting.DateTimeIntervalType.Days
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
                Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
                Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

                ' Set X-axis to show all dates in range with padding
                Dim startDate As DateTime = DateTimePicker1.Value.AddDays(-1)
                Dim endDate As DateTime = DateTimePicker2.Value.AddDays(1)
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = startDate.ToOADate()
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = endDate.ToOADate()

                ' Add chart area margins for better spacing
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Auto = False
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.X = 10
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Y = 10
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Width = 80
                Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Height = 80

                ' Set chart title for daily view
                Chart1.Titles.Clear()
                Chart1.Titles.Add($"Daily Net Income Report ({DateTimePicker1.Value:MM/dd/yyyy} to {DateTimePicker2.Value:MM/dd/yyyy})")
                Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)
            End If

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading daily net income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Validate ComboBox selections
            If ComboBox1.SelectedItem Is Nothing Or ComboBox2.SelectedItem Is Nothing Then
                MessageBox.Show("Please select both start and end months.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Clear existing chart data
            Chart1.Series.Clear()
            Chart1.ChartAreas.Clear()
            Chart1.Legends.Clear()

            ' Add chart area and legend
            Chart1.ChartAreas.Add("ChartArea1")
            Chart1.Legends.Add("Legend1")

            ' Create series for monthly sales
            Dim salesSeries As New DataVisualization.Charting.Series("Monthly Sales")
            salesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            salesSeries.ChartArea = "ChartArea1"
            salesSeries.Legend = "Legend1"
            salesSeries.Color = Color.Green
            Chart1.Series.Add(salesSeries)

            ' Create series for monthly expenses
            Dim expensesSeries As New DataVisualization.Charting.Series("Monthly Expenses")
            expensesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            expensesSeries.ChartArea = "ChartArea1"
            expensesSeries.Legend = "Legend1"
            expensesSeries.Color = Color.Red
            Chart1.Series.Add(expensesSeries)

            ' Load monthly net income data
            LoadMonthlyNetIncomeData()

        Catch ex As Exception
            MessageBox.Show($"Error generating monthly net income chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMonthlyNetIncomeData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Get selected months
            Dim startMonth As Integer = ComboBox1.SelectedIndex + 1 ' Convert to 1-based month number
            Dim endMonth As Integer = ComboBox2.SelectedIndex + 1

            ' SQL query to get monthly sales totals
            Dim salesQuery As String = $"
                SELECT 
                    MONTH(s.salesDate) AS SaleMonth,
                    CASE MONTH(s.salesDate)
                        WHEN 1 THEN 'January'
                        WHEN 2 THEN 'February'
                        WHEN 3 THEN 'March'
                        WHEN 4 THEN 'April'
                        WHEN 5 THEN 'May'
                        WHEN 6 THEN 'June'
                        WHEN 7 THEN 'July'
                        WHEN 8 THEN 'August'
                        WHEN 9 THEN 'September'
                        WHEN 10 THEN 'October'
                        WHEN 11 THEN 'November'
                        WHEN 12 THEN 'December'
                    END AS MonthName,
                    SUM(ROUND(oi.productQty * p.productPrice, 2)) AS MonthlySales
                FROM sales s
                JOIN orders o ON s.orderId = o.orderId
                JOIN orderitems oi ON o.orderId = oi.orders_orderId
                JOIN products p ON oi.products_productId = p.productId
                WHERE LOWER(o.orderStatus) = 'completed'
                AND MONTH(s.salesDate) >= {startMonth}
                AND MONTH(s.salesDate) <= {endMonth}
                GROUP BY MONTH(s.salesDate)
                ORDER BY SaleMonth"

            ' SQL query to get monthly expenses totals
            Dim expensesQuery As String = $"
                SELECT 
                    MONTH(expenseDate) AS ExpenseMonth,
                    SUM(ROUND(expenseAmount, 2)) AS MonthlyExpenses
                FROM expenses
                WHERE MONTH(expenseDate) >= {startMonth}
                AND MONTH(expenseDate) <= {endMonth}
                GROUP BY MONTH(expenseDate)
                ORDER BY ExpenseMonth"

            ' Execute sales query
            cmd = New MySqlCommand(salesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "MonthlySales")

            ' Execute expenses query
            cmd = New MySqlCommand(expensesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "MonthlyExpenses")

            ' Clear existing data points
            Chart1.Series("Monthly Sales").Points.Clear()
            Chart1.Series("Monthly Expenses").Points.Clear()

            ' Calculate totals
            Dim totalSales As Decimal = 0
            Dim totalExpenses As Decimal = 0

            ' Add sales data points to chart
            For Each row As DataRow In ds.Tables("MonthlySales").Rows
                Dim monthNumber As Integer = Convert.ToInt32(row("SaleMonth"))
                Dim monthText As String = row("MonthName").ToString()
                Dim monthlySales As Decimal = Convert.ToDecimal(row("MonthlySales"))
                totalSales += monthlySales

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = monthNumber
                point.YValues = {CDbl(monthlySales)}
                point.ToolTip = $"Month: {monthText}{Environment.NewLine}Sales: ₱{monthlySales:F2}"
                point.AxisLabel = monthText
                point.Label = $"₱{monthlySales:F0}" ' Add data label on bar

                Chart1.Series("Monthly Sales").Points.Add(point)
            Next

            ' Add expenses data points to chart
            For Each row As DataRow In ds.Tables("MonthlyExpenses").Rows
                Dim monthNumber As Integer = Convert.ToInt32(row("ExpenseMonth"))
                Dim monthlyExpenses As Decimal = Convert.ToDecimal(row("MonthlyExpenses"))
                totalExpenses += monthlyExpenses

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = monthNumber
                point.YValues = {CDbl(monthlyExpenses)}
                point.ToolTip = $"Month: {monthNumber}{Environment.NewLine}Expenses: ₱{monthlyExpenses:F2}"
                point.AxisLabel = monthNumber.ToString()
                point.Label = $"₱{monthlyExpenses:F0}" ' Add data label on bar

                Chart1.Series("Monthly Expenses").Points.Add(point)
            Next

            ' Update summary labels if they exist
            If TextBox1 IsNot Nothing Then
                TextBox1.Text = $"₱{totalSales:F2}"
            End If
            If TextBox2 IsNot Nothing Then
                TextBox2.Text = $"₱{totalExpenses:F2}"
            End If
            If TextBox3 IsNot Nothing Then
                Dim netIncome As Decimal = totalSales - totalExpenses
                TextBox3.Text = $"₱{netIncome:F2}"
            End If

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Month"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Amount (₱)"
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show selected month range with padding
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = startMonth - 0.5
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = endMonth + 0.5

            ' Add chart area margins for better spacing
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Auto = False
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.X = 10
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Y = 10
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Width = 80
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Height = 80

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add($"Monthly Net Income Report ({ComboBox1.SelectedItem} to {ComboBox2.SelectedItem})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading monthly net income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            ' Validate ComboBox selections
            If ComboBox3.SelectedItem Is Nothing Or ComboBox4.SelectedItem Is Nothing Then
                MessageBox.Show("Please select both start and end years.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Clear existing chart data
            Chart1.Series.Clear()
            Chart1.ChartAreas.Clear()
            Chart1.Legends.Clear()

            ' Add chart area and legend
            Chart1.ChartAreas.Add("ChartArea1")
            Chart1.Legends.Add("Legend1")

            ' Create series for yearly sales
            Dim salesSeries As New DataVisualization.Charting.Series("Yearly Sales")
            salesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            salesSeries.ChartArea = "ChartArea1"
            salesSeries.Legend = "Legend1"
            salesSeries.Color = Color.Green
            Chart1.Series.Add(salesSeries)

            ' Create series for yearly expenses
            Dim expensesSeries As New DataVisualization.Charting.Series("Yearly Expenses")
            expensesSeries.ChartType = DataVisualization.Charting.SeriesChartType.Column
            expensesSeries.ChartArea = "ChartArea1"
            expensesSeries.Legend = "Legend1"
            expensesSeries.Color = Color.Red
            Chart1.Series.Add(expensesSeries)

            ' Load yearly net income data
            LoadYearlyNetIncomeData()

        Catch ex As Exception
            MessageBox.Show($"Error generating yearly net income chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadYearlyNetIncomeData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Get selected years
            Dim startYear As Integer = Convert.ToInt32(ComboBox3.SelectedItem.ToString())
            Dim endYear As Integer = Convert.ToInt32(ComboBox4.SelectedItem.ToString())

            ' SQL query to get yearly sales totals
            Dim salesQuery As String = $"
                SELECT 
                    YEAR(s.salesDate) AS SaleYear,
                    SUM(ROUND(oi.productQty * p.productPrice, 2)) AS YearlySales
                FROM sales s
                JOIN orders o ON s.orderId = o.orderId
                JOIN orderitems oi ON o.orderId = oi.orders_orderId
                JOIN products p ON oi.products_productId = p.productId
                WHERE LOWER(o.orderStatus) = 'completed'
                AND YEAR(s.salesDate) >= {startYear}
                AND YEAR(s.salesDate) <= {endYear}
                GROUP BY YEAR(s.salesDate)
                ORDER BY SaleYear"

            ' SQL query to get yearly expenses totals
            Dim expensesQuery As String = $"
                SELECT 
                    YEAR(expenseDate) AS ExpenseYear,
                    SUM(ROUND(expenseAmount, 2)) AS YearlyExpenses
                FROM expenses
                WHERE YEAR(expenseDate) >= {startYear}
                AND YEAR(expenseDate) <= {endYear}
                GROUP BY YEAR(expenseDate)
                ORDER BY ExpenseYear"

            ' Execute sales query
            cmd = New MySqlCommand(salesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "YearlySales")

            ' Execute expenses query
            cmd = New MySqlCommand(expensesQuery, conn)
            da = New MySqlDataAdapter(cmd)
            da.Fill(ds, "YearlyExpenses")

            ' Clear existing data points
            Chart1.Series("Yearly Sales").Points.Clear()
            Chart1.Series("Yearly Expenses").Points.Clear()

            ' Calculate totals
            Dim totalSales As Decimal = 0
            Dim totalExpenses As Decimal = 0

            ' Add sales data points to chart
            For Each row As DataRow In ds.Tables("YearlySales").Rows
                Dim yearNumber As Integer = Convert.ToInt32(row("SaleYear"))
                Dim yearlySales As Decimal = Convert.ToDecimal(row("YearlySales"))
                totalSales += yearlySales

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = yearNumber
                point.YValues = {CDbl(yearlySales)}
                point.ToolTip = $"Year: {yearNumber}{Environment.NewLine}Sales: ₱{yearlySales:F2}"
                point.AxisLabel = yearNumber.ToString()
                point.Label = $"₱{yearlySales:F0}" ' Add data label on bar

                Chart1.Series("Yearly Sales").Points.Add(point)
            Next

            ' Add expenses data points to chart
            For Each row As DataRow In ds.Tables("YearlyExpenses").Rows
                Dim yearNumber As Integer = Convert.ToInt32(row("ExpenseYear"))
                Dim yearlyExpenses As Decimal = Convert.ToDecimal(row("YearlyExpenses"))
                totalExpenses += yearlyExpenses

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = yearNumber
                point.YValues = {CDbl(yearlyExpenses)}
                point.ToolTip = $"Year: {yearNumber}{Environment.NewLine}Expenses: ₱{yearlyExpenses:F2}"
                point.AxisLabel = yearNumber.ToString()
                point.Label = $"₱{yearlyExpenses:F0}" ' Add data label on bar

                Chart1.Series("Yearly Expenses").Points.Add(point)
            Next

            ' Update summary labels if they exist
            If TextBox1 IsNot Nothing Then
                TextBox1.Text = $"₱{totalSales:F2}"
            End If
            If TextBox2 IsNot Nothing Then
                TextBox2.Text = $"₱{totalExpenses:F2}"
            End If
            If TextBox3 IsNot Nothing Then
                Dim netIncome As Decimal = totalSales - totalExpenses
                TextBox3.Text = $"₱{netIncome:F2}"
            End If

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Year"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Amount (₱)"
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show selected year range with padding
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = startYear - 0.5
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = endYear + 0.5

            ' Add chart area margins for better spacing
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Auto = False
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.X = 10
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Y = 10
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Width = 80
            Chart1.ChartAreas("ChartArea1").InnerPlotPosition.Height = 80

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add($"Yearly Net Income Report ({startYear} - {endYear})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading yearly net income data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        ' This can be used for additional validation if needed
    End Sub

    ' ===== SAMPLE DATA METHODS FOR CHARTS 2 AND 3 =====
    Private Sub LoadExpensesBreakdownData(targetMonth As DateTime)
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' SQL query to get expenses breakdown by category
            Dim query As String = $"
                SELECT 
                    expenseCategory,
                    SUM(ROUND(expenseAmount, 2)) AS CategoryTotal
                FROM expenses
                WHERE MONTH(expenseDate) = {targetMonth.Month}
                AND YEAR(expenseDate) = {targetMonth.Year}
                GROUP BY expenseCategory
                ORDER BY CategoryTotal DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "ExpensesBreakdown")

            ' Clear existing data points
            Chart2.Series("Expenses").Points.Clear()

            ' Define colors for different categories
            Dim colors() As Color = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.Pink, Color.Brown}

            ' Add data points
            Dim colorIndex As Integer = 0
            For Each row As DataRow In ds.Tables("ExpensesBreakdown").Rows
                Dim category As String = row("expenseCategory").ToString()
                Dim categoryTotal As Decimal = Convert.ToDecimal(row("CategoryTotal"))

                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = colorIndex
                point.YValues = {CDbl(categoryTotal)}
                point.ToolTip = $"Category: {category}{Environment.NewLine}Amount: ₱{categoryTotal:F2}"
                point.LegendText = category
                point.Color = colors(colorIndex Mod colors.Length)

                Chart2.Series("Expenses").Points.Add(point)
                colorIndex += 1
            Next

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading expenses breakdown data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub LoadSalesByCategoryData(targetMonth As DateTime)
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' SQL query to get sales by product category
            Dim query As String = $"
                SELECT 
                    pc.category,
                    SUM(ROUND(oi.productQty * p.productPrice, 2)) AS CategorySales
                FROM sales s
                JOIN orders o ON s.orderId = o.orderId
                JOIN orderitems oi ON o.orderId = oi.orders_orderId
                JOIN products p ON oi.products_productId = p.productId
                JOIN productCategories pc ON p.productCategories_categoryId = pc.categoryId
                WHERE LOWER(o.orderStatus) = 'completed'
                AND MONTH(s.salesDate) = {targetMonth.Month}
                AND YEAR(s.salesDate) = {targetMonth.Year}
                GROUP BY pc.category
                ORDER BY CategorySales DESC"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "SalesByCategory")

            ' Clear existing data points
            Chart3.Series("Sales by Category").Points.Clear()

            ' Add data points
            Dim categoryIndex As Integer = 0
            For Each row As DataRow In ds.Tables("SalesByCategory").Rows
                Dim category As String = row("category").ToString()
                Dim categorySales As Decimal = Convert.ToDecimal(row("CategorySales"))

                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = categoryIndex
                point.YValues = {CDbl(categorySales)}
                point.ToolTip = $"Category: {category}{Environment.NewLine}Sales: ₱{categorySales:F2}"
                point.AxisLabel = category
                point.Label = $"₱{categorySales:F0}" ' Add data label on bar

                Chart3.Series("Sales by Category").Points.Add(point)
                categoryIndex += 1
            Next

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading sales by category data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class