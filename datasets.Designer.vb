<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class datasets
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dsInventory_btn = New System.Windows.Forms.Button()
        Me.dsCart_btn = New System.Windows.Forms.Button()
        Me.dsManage_orders_btn = New System.Windows.Forms.Button()
        Me.dsSales_btn = New System.Windows.Forms.Button()
        Me.dsExpenses_btn = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.dsUsers_btn = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dsInventory_btn
        '
        Me.dsInventory_btn.Location = New System.Drawing.Point(42, 61)
        Me.dsInventory_btn.Name = "dsInventory_btn"
        Me.dsInventory_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsInventory_btn.TabIndex = 0
        Me.dsInventory_btn.Text = "Product Inventory Management"
        Me.dsInventory_btn.UseVisualStyleBackColor = True
        '
        'dsCart_btn
        '
        Me.dsCart_btn.Location = New System.Drawing.Point(42, 172)
        Me.dsCart_btn.Name = "dsCart_btn"
        Me.dsCart_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsCart_btn.TabIndex = 1
        Me.dsCart_btn.Text = "Cart Monitoring"
        Me.dsCart_btn.UseVisualStyleBackColor = True
        '
        'dsManage_orders_btn
        '
        Me.dsManage_orders_btn.Location = New System.Drawing.Point(42, 228)
        Me.dsManage_orders_btn.Name = "dsManage_orders_btn"
        Me.dsManage_orders_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsManage_orders_btn.TabIndex = 5
        Me.dsManage_orders_btn.Text = "Order Processing"
        Me.dsManage_orders_btn.UseVisualStyleBackColor = True
        '
        'dsSales_btn
        '
        Me.dsSales_btn.Location = New System.Drawing.Point(42, 284)
        Me.dsSales_btn.Name = "dsSales_btn"
        Me.dsSales_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsSales_btn.TabIndex = 3
        Me.dsSales_btn.Text = "Sales Summary"
        Me.dsSales_btn.UseVisualStyleBackColor = True
        '
        'dsExpenses_btn
        '
        Me.dsExpenses_btn.Location = New System.Drawing.Point(42, 340)
        Me.dsExpenses_btn.Name = "dsExpenses_btn"
        Me.dsExpenses_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsExpenses_btn.TabIndex = 8
        Me.dsExpenses_btn.Text = "Expenses List"
        Me.dsExpenses_btn.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(42, 452)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(300, 50)
        Me.Button8.TabIndex = 7
        Me.Button8.Text = "Net Income Report"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'dsUsers_btn
        '
        Me.dsUsers_btn.Location = New System.Drawing.Point(42, 396)
        Me.dsUsers_btn.Name = "dsUsers_btn"
        Me.dsUsers_btn.Size = New System.Drawing.Size(300, 50)
        Me.dsUsers_btn.TabIndex = 6
        Me.dsUsers_btn.Text = "User Management"
        Me.dsUsers_btn.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(42, 117)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(145, 50)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Product Categories"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(196, 117)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(145, 50)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "Suppliers"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'datasets
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 561)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dsExpenses_btn)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.dsUsers_btn)
        Me.Controls.Add(Me.dsManage_orders_btn)
        Me.Controls.Add(Me.dsSales_btn)
        Me.Controls.Add(Me.dsCart_btn)
        Me.Controls.Add(Me.dsInventory_btn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "datasets"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "datasets"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dsInventory_btn As Button
    Friend WithEvents dsCart_btn As Button
    Friend WithEvents dsManage_orders_btn As Button
    Friend WithEvents dsSales_btn As Button
    Friend WithEvents dsExpenses_btn As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents dsUsers_btn As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
