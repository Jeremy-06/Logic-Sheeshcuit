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
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dataorders))
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.customerName = New System.Windows.Forms.TextBox()
        Me.orderID = New System.Windows.Forms.TextBox()
        Me.delete = New System.Windows.Forms.Button()
        Me.update = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'status
        '
        Me.status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.status.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.status.FormattingEnabled = True
        Me.status.Location = New System.Drawing.Point(748, 488)
        Me.status.Name = "status"
        Me.status.Size = New System.Drawing.Size(146, 28)
        Me.status.TabIndex = 194
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(427, 532)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 13)
        Me.Label10.TabIndex = 193
        Me.Label10.Text = "TOTAL AMOUNT"
        '
        'total
        '
        Me.total.BackColor = System.Drawing.SystemColors.Window
        Me.total.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.total.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.total.Location = New System.Drawing.Point(427, 548)
        Me.total.Margin = New System.Windows.Forms.Padding(2)
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        Me.total.Size = New System.Drawing.Size(146, 26)
        Me.total.TabIndex = 192
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(427, 474)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 191
        Me.Label9.Text = "ADDRESS"
        '
        'address
        '
        Me.address.BackColor = System.Drawing.SystemColors.Window
        Me.address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.address.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.address.Location = New System.Drawing.Point(427, 490)
        Me.address.Margin = New System.Windows.Forms.Padding(2)
        Me.address.Name = "address"
        Me.address.ReadOnly = True
        Me.address.Size = New System.Drawing.Size(146, 26)
        Me.address.TabIndex = 190
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(141, 532)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 13)
        Me.Label8.TabIndex = 189
        Me.Label8.Text = "PRODUCT NAME"
        '
        'productName
        '
        Me.productName.BackColor = System.Drawing.SystemColors.Window
        Me.productName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.productName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.productName.Location = New System.Drawing.Point(144, 548)
        Me.productName.Margin = New System.Windows.Forms.Padding(2)
        Me.productName.Name = "productName"
        Me.productName.ReadOnly = True
        Me.productName.Size = New System.Drawing.Size(279, 26)
        Me.productName.TabIndex = 188
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(41, 474)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 187
        Me.Label7.Text = "CUSTOMER ID"
        '
        'customerID
        '
        Me.customerID.BackColor = System.Drawing.SystemColors.Window
        Me.customerID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.customerID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerID.Location = New System.Drawing.Point(44, 490)
        Me.customerID.Margin = New System.Windows.Forms.Padding(2)
        Me.customerID.Name = "customerID"
        Me.customerID.ReadOnly = True
        Me.customerID.Size = New System.Drawing.Size(90, 26)
        Me.customerID.TabIndex = 186
        '
        'phone
        '
        Me.phone.BackColor = System.Drawing.SystemColors.Window
        Me.phone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.phone.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.phone.Location = New System.Drawing.Point(578, 490)
        Me.phone.Mask = "+63 000-000-0000"
        Me.phone.Name = "phone"
        Me.phone.ReadOnly = True
        Me.phone.Size = New System.Drawing.Size(147, 26)
        Me.phone.TabIndex = 185
        '
        'orderDate
        '
        Me.orderDate.BackColor = System.Drawing.SystemColors.Window
        Me.orderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.orderDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orderDate.Location = New System.Drawing.Point(578, 548)
        Me.orderDate.Mask = "00/00/0000"
        Me.orderDate.Name = "orderDate"
        Me.orderDate.ReadOnly = True
        Me.orderDate.Size = New System.Drawing.Size(147, 26)
        Me.orderDate.TabIndex = 184
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(575, 474)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 13)
        Me.Label6.TabIndex = 183
        Me.Label6.Text = "CUSTOMER PHONE"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(141, 474)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 13)
        Me.Label5.TabIndex = 182
        Me.Label5.Text = "CUSTOMER NAME"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(745, 473)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 181
        Me.Label4.Text = "ORDER STATUS"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(575, 532)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 180
        Me.Label3.Text = "ORDER DATE"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 532)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 179
        Me.Label2.Text = "ORDER ID"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("NSimSun", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(39, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 27)
        Me.Label1.TabIndex = 178
        Me.Label1.Text = "ORDERS CRUD"
        '
        'customerName
        '
        Me.customerName.BackColor = System.Drawing.SystemColors.Window
        Me.customerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.customerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerName.Location = New System.Drawing.Point(144, 490)
        Me.customerName.Margin = New System.Windows.Forms.Padding(2)
        Me.customerName.Name = "customerName"
        Me.customerName.ReadOnly = True
        Me.customerName.Size = New System.Drawing.Size(279, 26)
        Me.customerName.TabIndex = 176
        '
        'orderID
        '
        Me.orderID.BackColor = System.Drawing.SystemColors.Window
        Me.orderID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.orderID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.orderID.Location = New System.Drawing.Point(44, 548)
        Me.orderID.Margin = New System.Windows.Forms.Padding(2)
        Me.orderID.Name = "orderID"
        Me.orderID.ReadOnly = True
        Me.orderID.Size = New System.Drawing.Size(90, 26)
        Me.orderID.TabIndex = 175
        '
        'delete
        '
        Me.delete.BackColor = System.Drawing.SystemColors.Window
        Me.delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.delete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.delete.ForeColor = System.Drawing.SystemColors.WindowText
        Me.delete.Location = New System.Drawing.Point(749, 557)
        Me.delete.Margin = New System.Windows.Forms.Padding(2)
        Me.delete.Name = "delete"
        Me.delete.Size = New System.Drawing.Size(146, 30)
        Me.delete.TabIndex = 172
        Me.delete.Text = "DELETE"
        Me.delete.UseVisualStyleBackColor = False
        '
        'update
        '
        Me.update.BackColor = System.Drawing.SystemColors.Window
        Me.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.update.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.update.ForeColor = System.Drawing.SystemColors.WindowText
        Me.update.Location = New System.Drawing.Point(749, 523)
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
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Window
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ControlText
        Me.DataGridView1.Location = New System.Drawing.Point(45, 80)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(850, 350)
        Me.DataGridView1.TabIndex = 170
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox2.Location = New System.Drawing.Point(864, 38)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(30, 30)
        Me.PictureBox2.TabIndex = 200
        Me.PictureBox2.TabStop = False
        '
        'Button4
        '
        Me.Button4.BackgroundImage = Global.Logic_Sheeshcuit.My.Resources.Resources.Search_Icon
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Location = New System.Drawing.Point(827, 36)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(35, 35)
        Me.Button4.TabIndex = 198
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox6
        '
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox6.Location = New System.Drawing.Point(401, 39)
        Me.TextBox6.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(424, 29)
        Me.TextBox6.TabIndex = 199
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Product Name", "Customer Name", "Order ID", "Status", "Month", "Year"})
        Me.ComboBox1.Location = New System.Drawing.Point(250, 39)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(146, 28)
        Me.ComboBox1.TabIndex = 201
        '
        'dataorders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(932, 611)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.ComboBox1)
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
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.customerName)
        Me.Controls.Add(Me.orderID)
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
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents customerName As TextBox
    Friend WithEvents orderID As TextBox
    Friend WithEvents delete As Button
    Friend WithEvents update As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
End Class
