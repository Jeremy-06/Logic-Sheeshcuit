Imports MySql.Data.MySqlClient
Imports System.Data

Public Class expensesreport
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=sheeshcuit")
    Private cmd As MySqlCommand
    Private da As MySqlDataAdapter
    Private ds As DataSet

    Private Sub expensesreport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ' Initialize ComboBoxes
        LoadMonthComboBoxes()
        LoadYearComboBoxes()

        ' Set chart title
        Chart1.Titles.Clear()
        Chart1.Titles.Add("Expenses Report")
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

            ' Create series for daily expenses
            Dim series As New DataVisualization.Charting.Series("Daily Expenses")
            series.ChartType = DataVisualization.Charting.SeriesChartType.Column
            series.ChartArea = "ChartArea1"
            series.Legend = "Legend1"
            Chart1.Series.Add(series)

            ' Load daily expenses data
            LoadDailyExpensesData()

        Catch ex As Exception
            MessageBox.Show($"Error generating daily expenses chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDailyExpensesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' SQL query to get daily expenses totals
            Dim query As String = $"
                SELECT 
                    DATE_FORMAT(expenseDate, '%Y-%m-%d') AS ExpenseDate,
                    SUM(ROUND(expenseAmount, 2)) AS DailyTotal
                FROM expenses
                WHERE expenseDate >= '{DateTimePicker1.Value:yyyy-MM-dd}'
                AND expenseDate <= '{DateTimePicker2.Value:yyyy-MM-dd}'
                GROUP BY DATE_FORMAT(expenseDate, '%Y-%m-%d')
                ORDER BY ExpenseDate"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "DailyExpenses")

            ' Clear existing data points
            Chart1.Series("Daily Expenses").Points.Clear()

            ' Calculate total amount
            Dim totalAmount As Decimal = 0

            ' Add data points to chart
            For Each row As DataRow In ds.Tables("DailyExpenses").Rows
                Dim expenseDate As String = row("ExpenseDate").ToString()
                Dim dailyTotal As Decimal = Convert.ToDecimal(row("DailyTotal"))
                totalAmount += dailyTotal

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = DateTime.Parse(expenseDate).ToOADate() ' Convert to OLE Automation Date
                point.YValues = {CDbl(dailyTotal)}
                point.ToolTip = $"Date: {expenseDate}{Environment.NewLine}Total: ₱{dailyTotal:F2}"
                point.AxisLabel = DateTime.Parse(expenseDate).ToString("MM/dd/yyyy")

                Chart1.Series("Daily Expenses").Points.Add(point)
            Next

            ' Update TextBox1 with total amount
            TextBox1.Text = $"₱{totalAmount:F2}"

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Date"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Expenses Amount (₱)"
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
            Chart1.Titles.Add($"Daily Expenses Report ({DateTimePicker1.Value:MM/dd/yyyy} to {DateTimePicker2.Value:MM/dd/yyyy})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading daily expenses data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            ' Create series for monthly expenses
            Dim series As New DataVisualization.Charting.Series("Monthly Expenses")
            series.ChartType = DataVisualization.Charting.SeriesChartType.Column
            series.ChartArea = "ChartArea1"
            series.Legend = "Legend1"
            Chart1.Series.Add(series)

            ' Load monthly expenses data based on ComboBox selections
            LoadMonthlyExpensesData()

        Catch ex As Exception
            MessageBox.Show($"Error generating monthly expenses chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMonthlyExpensesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Get selected months
            Dim startMonth As Integer = ComboBox1.SelectedIndex + 1 ' Convert to 1-based month number
            Dim endMonth As Integer = ComboBox2.SelectedIndex + 1

            ' SQL query to get monthly expenses totals for selected month range
            Dim query As String = $"
                SELECT 
                    MONTH(expenseDate) AS ExpenseMonth,
                    CASE MONTH(expenseDate)
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
                    SUM(ROUND(expenseAmount, 2)) AS MonthlyTotal
                FROM expenses
                WHERE MONTH(expenseDate) >= {startMonth}
                AND MONTH(expenseDate) <= {endMonth}
                GROUP BY MONTH(expenseDate)
                ORDER BY ExpenseMonth"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "MonthlyExpenses")

            ' Clear existing data points
            Chart1.Series("Monthly Expenses").Points.Clear()

            ' Calculate total amount
            Dim totalAmount As Decimal = 0

            ' Add data points to chart
            For Each row As DataRow In ds.Tables("MonthlyExpenses").Rows
                Dim monthNumber As Integer = Convert.ToInt32(row("ExpenseMonth"))
                Dim monthText As String = row("MonthName").ToString()
                Dim monthlyTotal As Decimal = Convert.ToDecimal(row("MonthlyTotal"))
                totalAmount += monthlyTotal

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = monthNumber
                point.YValues = {CDbl(monthlyTotal)}
                point.ToolTip = $"Month: {monthText}{Environment.NewLine}Total: ₱{monthlyTotal:F2}"
                point.AxisLabel = monthText

                Chart1.Series("Monthly Expenses").Points.Add(point)
            Next

            ' Update TextBox1 with total amount
            TextBox1.Text = $"₱{totalAmount:F2}"

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Month"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Expenses Amount (₱)"
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show selected month range
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = startMonth
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = endMonth

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add($"Monthly Expenses Report ({ComboBox1.SelectedItem} to {ComboBox2.SelectedItem})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading monthly expenses data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

            ' Create series for yearly expenses
            Dim series As New DataVisualization.Charting.Series("Yearly Expenses")
            series.ChartType = DataVisualization.Charting.SeriesChartType.Column
            series.ChartArea = "ChartArea1"
            series.Legend = "Legend1"
            Chart1.Series.Add(series)

            ' Load yearly expenses data based on ComboBox selections
            LoadYearlyExpensesData()

        Catch ex As Exception
            MessageBox.Show($"Error generating yearly expenses chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadYearlyExpensesData()
        Try
            If conn.State <> ConnectionState.Open Then conn.Open()

            ' Get selected years
            Dim startYear As Integer = Convert.ToInt32(ComboBox3.SelectedItem.ToString())
            Dim endYear As Integer = Convert.ToInt32(ComboBox4.SelectedItem.ToString())

            ' SQL query to get yearly expenses totals for selected year range
            Dim query As String = $"
                SELECT 
                    YEAR(expenseDate) AS ExpenseYear,
                    SUM(ROUND(expenseAmount, 2)) AS YearlyTotal
                FROM expenses
                WHERE YEAR(expenseDate) >= {startYear}
                AND YEAR(expenseDate) <= {endYear}
                GROUP BY YEAR(expenseDate)
                ORDER BY ExpenseYear"

            cmd = New MySqlCommand(query, conn)
            da = New MySqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "YearlyExpenses")

            ' Clear existing data points
            Chart1.Series("Yearly Expenses").Points.Clear()

            ' Calculate total amount
            Dim totalAmount As Decimal = 0

            ' Add data points to chart
            For Each row As DataRow In ds.Tables("YearlyExpenses").Rows
                Dim yearNumber As Integer = Convert.ToInt32(row("ExpenseYear"))
                Dim yearlyTotal As Decimal = Convert.ToDecimal(row("YearlyTotal"))
                totalAmount += yearlyTotal

                ' Add point to chart
                Dim point As New DataVisualization.Charting.DataPoint()
                point.XValue = yearNumber
                point.YValues = {CDbl(yearlyTotal)}
                point.ToolTip = $"Year: {yearNumber}{Environment.NewLine}Total: ₱{yearlyTotal:F2}"
                point.AxisLabel = yearNumber.ToString()

                Chart1.Series("Yearly Expenses").Points.Add(point)
            Next

            ' Update TextBox1 with total amount
            TextBox1.Text = $"₱{totalAmount:F2}"

            ' Format chart
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Year"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Expenses Amount (₱)"
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 1
            Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.LightGray
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "C0"

            ' Set X-axis to show selected year range
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = startYear
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = endYear

            ' Set chart title
            Chart1.Titles.Clear()
            Chart1.Titles.Add($"Yearly Expenses Report ({startYear} - {endYear})")
            Chart1.Titles(0).Font = New Font("Arial", 12, FontStyle.Bold)

            If conn.State = ConnectionState.Open Then conn.Close()

        Catch ex As Exception
            MessageBox.Show($"Error loading yearly expenses data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        ' Clear the chart
        Chart1.Series.Clear()
        Chart1.ChartAreas.Clear()
        Chart1.Legends.Clear()
        Chart1.Titles.Clear()

        ' Clear TextBox1
        TextBox1.Text = ""

        ' Reset chart title
        Chart1.Titles.Add("Expenses Report")
        Chart1.Titles(0).Font = New Font("Arial", 14, FontStyle.Bold)
    End Sub
End Class