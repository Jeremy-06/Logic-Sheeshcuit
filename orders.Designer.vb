﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class orders
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnCancelOrder = New System.Windows.Forms.Button()
        Me.btnViewReceipt = New System.Windows.Forms.Button()
        Me.btnAllOrders = New System.Windows.Forms.Button()
        Me.btnPaidOrders = New System.Windows.Forms.Button()
        Me.btnCancelledOrders = New System.Windows.Forms.Button()
        Me.btnCompletedOrders = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ControlText
        Me.DataGridView1.Location = New System.Drawing.Point(25, 80)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(776, 400)
        Me.DataGridView1.TabIndex = 0
        '
        'btnCancelOrder
        '
        Me.btnCancelOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelOrder.Location = New System.Drawing.Point(25, 500)
        Me.btnCancelOrder.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancelOrder.Name = "btnCancelOrder"
        Me.btnCancelOrder.Size = New System.Drawing.Size(150, 50)
        Me.btnCancelOrder.TabIndex = 1
        Me.btnCancelOrder.Text = "Cancel Order"
        Me.btnCancelOrder.UseVisualStyleBackColor = True
        '
        'btnViewReceipt
        '
        Me.btnViewReceipt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewReceipt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewReceipt.Location = New System.Drawing.Point(185, 500)
        Me.btnViewReceipt.Margin = New System.Windows.Forms.Padding(2)
        Me.btnViewReceipt.Name = "btnViewReceipt"
        Me.btnViewReceipt.Size = New System.Drawing.Size(150, 50)
        Me.btnViewReceipt.TabIndex = 2
        Me.btnViewReceipt.Text = "View Receipt"
        Me.btnViewReceipt.UseVisualStyleBackColor = True
        '
        'btnAllOrders
        '
        Me.btnAllOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllOrders.Location = New System.Drawing.Point(371, 500)
        Me.btnAllOrders.Margin = New System.Windows.Forms.Padding(2)
        Me.btnAllOrders.Name = "btnAllOrders"
        Me.btnAllOrders.Size = New System.Drawing.Size(100, 50)
        Me.btnAllOrders.TabIndex = 3
        Me.btnAllOrders.Text = "All Orders"
        Me.btnAllOrders.UseVisualStyleBackColor = True
        '
        'btnPaidOrders
        '
        Me.btnPaidOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPaidOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPaidOrders.Location = New System.Drawing.Point(481, 500)
        Me.btnPaidOrders.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPaidOrders.Name = "btnPaidOrders"
        Me.btnPaidOrders.Size = New System.Drawing.Size(100, 50)
        Me.btnPaidOrders.TabIndex = 4
        Me.btnPaidOrders.Text = "Paid"
        Me.btnPaidOrders.UseVisualStyleBackColor = True
        '
        'btnCancelledOrders
        '
        Me.btnCancelledOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelledOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelledOrders.Location = New System.Drawing.Point(591, 500)
        Me.btnCancelledOrders.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCancelledOrders.Name = "btnCancelledOrders"
        Me.btnCancelledOrders.Size = New System.Drawing.Size(100, 50)
        Me.btnCancelledOrders.TabIndex = 5
        Me.btnCancelledOrders.Text = "Cancelled"
        Me.btnCancelledOrders.UseVisualStyleBackColor = True
        '
        'btnCompletedOrders
        '
        Me.btnCompletedOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompletedOrders.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompletedOrders.Location = New System.Drawing.Point(701, 500)
        Me.btnCompletedOrders.Margin = New System.Windows.Forms.Padding(2)
        Me.btnCompletedOrders.Name = "btnCompletedOrders"
        Me.btnCompletedOrders.Size = New System.Drawing.Size(100, 50)
        Me.btnCompletedOrders.TabIndex = 6
        Me.btnCompletedOrders.Text = "Completed"
        Me.btnCompletedOrders.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(597, 20)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(100, 50)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(701, 20)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(2)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(100, 50)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 29)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Orders"
        '
        'orders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 571)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnCompletedOrders)
        Me.Controls.Add(Me.btnCancelledOrders)
        Me.Controls.Add(Me.btnPaidOrders)
        Me.Controls.Add(Me.btnAllOrders)
        Me.Controls.Add(Me.btnViewReceipt)
        Me.Controls.Add(Me.btnCancelOrder)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "orders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Orders"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnCancelOrder As Button
    Friend WithEvents btnViewReceipt As Button
    Friend WithEvents btnAllOrders As Button
    Friend WithEvents btnPaidOrders As Button
    Friend WithEvents btnCancelledOrders As Button
    Friend WithEvents btnCompletedOrders As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents Label1 As Label
End Class
