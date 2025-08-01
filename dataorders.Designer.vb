<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dataorders
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
        Me.status = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.total = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.address = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.productName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.customerID = New System.Windows.Forms.TextBox()
        Me.phone = New System.Windows.Forms.MaskedTextBox()
        Me.orderDate = New System.Windows.Forms.MaskedTextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.customerName = New System.Windows.Forms.TextBox()
        Me.orderID = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.delete = New System.Windows.Forms.Button()
        Me.update = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'status
        '
        Me.status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.status.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status.FormattingEnabled = True
        Me.status.Location = New System.Drawing.Point(748, 471)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(146, 28)
        Me.status.TabIndex = 194
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(428, 513)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 13)
        Me.Label10.TabIndex = 193
        Me.Label10.Text = "TOTAL AMOUNT"
        '
        'total
        '
        Me.total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.total.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total.Location = New System.Drawing.Point(430, 528)
        Me.total.Margin = New System.Windows.Forms.Padding(2)
        Me.total.Name = "total"
        Me.total.Size = New System.Drawing.Size(146, 26)
        Me.total.TabIndex = 192
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(428, 458)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 191
        Me.Label9.Text = "ADDRESS"
        '
        'address
        '
        Me.address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.address.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.address.Location = New System.Drawing.Point(431, 473)
        Me.address.Margin = New System.Windows.Forms.Padding(2)
        Me.address.Name = "address"
        Me.address.Size = New System.Drawing.Size(146, 26)
        Me.address.TabIndex = 190
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(137, 513)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 13)
        Me.Label8.TabIndex = 189
        Me.Label8.Text = "PRODUCT NAME"
        '
        'productName
        '
        Me.productName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.productName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.productName.Location = New System.Drawing.Point(140, 528)
        Me.productName.Margin = New System.Windows.Forms.Padding(2)
        Me.productName.Name = "productName"
        Me.productName.Size = New System.Drawing.Size(279, 26)
        Me.productName.TabIndex = 188
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(37, 458)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 187
        Me.Label7.Text = "CUSTOMER ID"
        '
        'customerID
        '
        Me.customerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.customerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerID.Location = New System.Drawing.Point(44, 473)
        Me.customerID.Margin = New System.Windows.Forms.Padding(2)
        Me.customerID.Name = "customerID"
        Me.customerID.Size = New System.Drawing.Size(84, 26)
        Me.customerID.TabIndex = 186
        '
        'phone
        '
        Me.phone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.phone.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.phone.Location = New System.Drawing.Point(589, 473)
        Me.phone.Mask = "+63 000-000-0000"
        Me.phone.Name = "phone"
        Me.phone.Size = New System.Drawing.Size(147, 26)
        Me.phone.TabIndex = 185
        '
        'orderDate
        '
        Me.orderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.orderDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orderDate.Location = New System.Drawing.Point(587, 528)
        Me.orderDate.Mask = "0000/00/00"
        Me.orderDate.Name = "orderDate"
        Me.orderDate.Size = New System.Drawing.Size(147, 26)
        Me.orderDate.TabIndex = 184
        '
        'Button4
        '
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(710, 23)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(31, 28)
        Me.Button4.TabIndex = 173
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(584, 457)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 13)
        Me.Label6.TabIndex = 183
        Me.Label6.Text = "CUSTOMER PHONE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(137, 458)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 13)
        Me.Label5.TabIndex = 182
        Me.Label5.Text = "CUSTOMER NAME"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(745, 457)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 181
        Me.Label4.Text = "ORDER STATUS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(584, 513)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 180
        Me.Label3.Text = "ORDER DATE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 513)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 179
        Me.Label2.Text = "ORDER ID"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("NSimSun", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(44, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 27)
        Me.Label1.TabIndex = 178
        Me.Label1.Text = "ORDERS CRUD"
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(234, 24)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(472, 26)
        Me.TextBox4.TabIndex = 177
        '
        'customerName
        '
        Me.customerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.customerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerName.Location = New System.Drawing.Point(140, 473)
        Me.customerName.Margin = New System.Windows.Forms.Padding(2)
        Me.customerName.Name = "customerName"
        Me.customerName.Size = New System.Drawing.Size(279, 26)
        Me.customerName.TabIndex = 176
        '
        'orderID
        '
        Me.orderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.orderID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orderID.Location = New System.Drawing.Point(44, 528)
        Me.orderID.Margin = New System.Windows.Forms.Padding(2)
        Me.orderID.Name = "orderID"
        Me.orderID.Size = New System.Drawing.Size(85, 26)
        Me.orderID.TabIndex = 175
        '
        'Button5
        '
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Location = New System.Drawing.Point(750, 24)
        Me.Button5.Margin = New System.Windows.Forms.Padding(2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(146, 30)
        Me.Button5.TabIndex = 174
        Me.Button5.Text = "CLEAR INPUT"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'delete
        '
        Me.delete.BackColor = System.Drawing.SystemColors.WindowText
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.delete.ForeColor = System.Drawing.SystemColors.Window
        Me.delete.Location = New System.Drawing.Point(748, 558)
        Me.delete.Margin = New System.Windows.Forms.Padding(2)
        Me.delete.Name = "delete"
        Me.delete.Size = New System.Drawing.Size(146, 30)
        Me.delete.TabIndex = 172
        Me.delete.Text = "DELETE"
        Me.delete.UseVisualStyleBackColor = False
        '
        'update
        '
        Me.update.BackColor = System.Drawing.SystemColors.WindowText
        Me.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.update.ForeColor = System.Drawing.SystemColors.Window
        Me.update.Location = New System.Drawing.Point(748, 524)
        Me.update.Margin = New System.Windows.Forms.Padding(2)
        Me.update.Name = "update"
        Me.update.Size = New System.Drawing.Size(146, 30)
        Me.update.TabIndex = 171
        Me.update.Text = "UPDATE"
        Me.update.UseVisualStyleBackColor = False
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
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ControlText
        Me.DataGridView1.Location = New System.Drawing.Point(45, 65)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(851, 369)
        Me.DataGridView1.TabIndex = 170
        '
        'dataorders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 611)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.total)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.address)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.productName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.customerID)
        Me.Controls.Add(Me.phone)
        Me.Controls.Add(Me.orderDate)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.customerName)
        Me.Controls.Add(Me.orderID)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.delete)
        Me.Controls.Add(Me.update)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dataorders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manage Orders"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents status As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents total As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents address As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents productName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents customerID As TextBox
    Friend WithEvents phone As MaskedTextBox
    Friend WithEvents orderDate As MaskedTextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents customerName As TextBox
    Friend WithEvents orderID As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents delete As Button
    Friend WithEvents update As Button
    Friend WithEvents DataGridView1 As DataGridView
End Class
