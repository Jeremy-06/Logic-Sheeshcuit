Imports MySql.Data.MySqlClient
Imports System.Data

Public Class salesreport
    ' Database connection
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet

    Private Sub salesreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set default dates (current date and 30 days ago)
        DateTimePicker1.Value = DateTime.Now.AddDays(-30)
        DateTimePicker2.Value = DateTime.Now

        ' Set chart title
        Chart1.Titles.Clear()
        Chart1.Titles.Add("Sales Report")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)
    End Sub

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
            Dim series As New DataVisualization.Charting.Series("Daily Sales")
            series.ChartType = DataVisualization.Charting.SeriesChartType.Column
            series.ChartArea = "ChartArea1"
            series.Legend = "Legend1"
            Chart1.Series.Add(series)

            ' Load daily sales data
            LoadDailySalesData()

        Catch ex As Exception
            MessageBox.Show($"Error generating daily sales chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDailySalesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' SQL query to get daily sales totals
            Dim query As String = $"
                SELECT 
                    DATE_FORMAT(s.salesDate, '%Y-%m-%d') AS SaleDate,
                    SUM(ROUND(oi.productQty * p.productPrice, 2)) AS DailyTotal
                FROM sales s
                JOIN orders o ON s.orderId = o.orderId
                JOIN orderitems oi ON o.orderId = oi.orders_orderId
                JOIN products p ON oi.products_productId = p.productId
                WHERE LOWER(o.orderStatus) = 'completed'
                AND s.salesDate >= '{DateTimePicker1.Value:yyyy-MM-dd}'
                AND s.salesDate <= '{DateTimePicker2.Value:yyyy-MM-dd}'
                GROUP BY DATE_FORMAT(s.salesDate, '%Y-%m-%d')
                ORDER BY SaleDate"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "DailySales")

            ' Clear existing data points
            Chart1.Series("Daily Sales").Points.Clear()

            ' Add data points to chart
            For Each row As DataRow In ds.Tables("DailySales").Rows
                Dim saleDate As String = row("SaleDate").ToString()
                Dim dailyTotal As Decimal = Convert.ToDecimal(row("DailyTotal"))

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = DateTime.Parse(saleDate).ToOADate() ' Convert to OLE Automation Date
                point.YValues = {CDbl(dailyTotal)}
                point.ToolTip = $"Date: {saleDate}{Environment.NewLine}Total: ${dailyTotal:F2}"
                point.AxisLabel = DateTime.Parse(saleDate).ToString("MM/dd/yyyy")

                Chart1.Series("Daily Sales").Points.Add(point)
            Next

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Date"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Sales Amount ($)"
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "MM/dd/yyyy"
            Chart1.ChartAreas("ChartArea1").AxisX.IntervalType = DataVisualization.Charting.DateTimeIntervalType.Days
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show all dates in range
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = DateTimePicker1.Value.ToOADate()
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = DateTimePicker2.Value.ToOADate()

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add($"Daily Sales Report ({DateTimePicker1.Value:MM/dd/yyyy} to {DateTimePicker2.Value:MM/dd/yyyy})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading daily sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Clear existing chart data
            Chart1.Series.Clear()
            Chart1.ChartAreas.Clear()
            Chart1.Legends.Clear()

            ' Add chart area and legend
            Chart1.ChartAreas.Add("ChartArea1")
            Chart1.Legends.Add("Legend1")

            ' Create series for monthly sales
            Dim series As New DataVisualization.Charting.Series("Monthly Sales")
            series.ChartType = DataVisualization.Charting.SeriesChartType.Column
            series.ChartArea = "ChartArea1"
            series.Legend = "Legend1"
            Chart1.Series.Add(series)

            ' Load monthly sales data for 2025
            LoadMonthlySalesData()

        Catch ex As Exception
            MessageBox.Show($"Error generating monthly sales chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMonthlySalesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' SQL query to get monthly sales totals for 2025
            Dim query As String = $"
                SELECT 
                    MONTH(s.salesDate) AS SaleMonth,
                    MONTHNAME(s.salesDate) AS MonthName,
                    SUM(ROUND(oi.productQty * p.productPrice, 2)) AS MonthlyTotal
                FROM sales s
                JOIN orders o ON s.orderId = o.orderId
                JOIN orderitems oi ON o.orderId = oi.orders_orderId
                JOIN products p ON oi.products_productId = p.productId
                WHERE LOWER(o.orderStatus) = 'completed'
                AND YEAR(s.salesDate) = 2025
                GROUP BY MONTH(s.salesDate), MONTHNAME(s.salesDate)
                ORDER BY SaleMonth"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "MonthlySales")

            ' Clear existing data points
            Chart1.Series("Monthly Sales").Points.Clear()

            ' Add data points to chart
            For Each row As DataRow In ds.Tables("MonthlySales").Rows
                Dim monthNumber As Integer = Convert.ToInt32(row("SaleMonth"))
                Dim monthName As String = row("MonthName").ToString()
                Dim monthlyTotal As Decimal = Convert.ToDecimal(row("MonthlyTotal"))

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = monthNumber
                point.YValues = {CDbl(monthlyTotal)}
                point.ToolTip = $"Month: {monthName}{Environment.NewLine}Total: ${monthlyTotal:F2}"
                point.AxisLabel = monthName

                Chart1.Series("Monthly Sales").Points.Add(point)
            Next

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Month"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Sales Amount ($)"
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show all months (1-12)
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 1
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 12

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add("Monthly Sales Report - 2025")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading monthly sales data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear the chart
        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()
        Chart1.Titles.Clear()

        ' Reset chart title
        Chart1.Titles.Add("Sales Report")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)
    End Sub
End Class