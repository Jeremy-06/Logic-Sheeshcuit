<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class customerprofile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(customerprofile))
        Me.Button2 = New System.Windows.Forms.Button()
        Me.customerIdlbl = New System.Windows.Forms.Label()
        Me.usernamelbl = New System.Windows.Forms.Label()
        Me.addresslbl = New System.Windows.Forms.Label()
        Me.phonelbl = New System.Windows.Forms.Label()
        Me.rolelbl = New System.Windows.Forms.Label()
        Me.lightTheme = New System.Windows.Forms.Button()
        Me.darkTheme = New System.Windows.Forms.Button()
        Me.firstNameTextBox = New System.Windows.Forms.TextBox()
        Me.lastNameTextBox = New System.Windows.Forms.TextBox()
        Me.addressTextBox = New System.Windows.Forms.TextBox()
        Me.phoneTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.editDatalbl = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.editDataIcon = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.editDataIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Location = New System.Drawing.Point(276, 622)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 31)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'customerIdlbl
        '
        Me.customerIdlbl.BackColor = System.Drawing.Color.Transparent
        Me.customerIdlbl.Font = New System.Drawing.Font("Malgun Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.customerIdlbl.ForeColor = System.Drawing.Color.Gray
        Me.customerIdlbl.Location = New System.Drawing.Point(245, 111)
        Me.customerIdlbl.Name = "customerIdlbl"
        Me.customerIdlbl.Size = New System.Drawing.Size(111, 21)
        Me.customerIdlbl.TabIndex = 2
        '
        'usernamelbl
        '
        Me.usernamelbl.BackColor = System.Drawing.Color.Transparent
        Me.usernamelbl.Font = New System.Drawing.Font("Bahnschrift", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usernamelbl.ForeColor = System.Drawing.Color.Gray
        Me.usernamelbl.Location = New System.Drawing.Point(145, 70)
        Me.usernamelbl.Name = "usernamelbl"
        Me.usernamelbl.Size = New System.Drawing.Size(212, 34)
        Me.usernamelbl.TabIndex = 3
        Me.usernamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'addresslbl
        '
        Me.addresslbl.BackColor = System.Drawing.Color.Transparent
        Me.addresslbl.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addresslbl.ForeColor = System.Drawing.Color.White
        Me.addresslbl.Location = New System.Drawing.Point(80, 218)
        Me.addresslbl.Name = "addresslbl"
        Me.addresslbl.Size = New System.Drawing.Size(294, 40)
        Me.addresslbl.TabIndex = 4
        '
        'phonelbl
        '
        Me.phonelbl.BackColor = System.Drawing.Color.Transparent
        Me.phonelbl.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.phonelbl.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.phonelbl.ForeColor = System.Drawing.Color.White
        Me.phonelbl.Location = New System.Drawing.Point(80, 317)
        Me.phonelbl.Name = "phonelbl"
        Me.phonelbl.Size = New System.Drawing.Size(291, 33)
        Me.phonelbl.TabIndex = 5
        '
        'rolelbl
        '
        Me.rolelbl.BackColor = System.Drawing.Color.Transparent
        Me.rolelbl.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rolelbl.ForeColor = System.Drawing.Color.Gray
        Me.rolelbl.Location = New System.Drawing.Point(28, 149)
        Me.rolelbl.Name = "rolelbl"
        Me.rolelbl.Size = New System.Drawing.Size(87, 16)
        Me.rolelbl.TabIndex = 6
        Me.rolelbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lightTheme
        '
        Me.lightTheme.BackColor = System.Drawing.Color.White
        Me.lightTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lightTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lightTheme.Location = New System.Drawing.Point(230, 150)
        Me.lightTheme.Name = "lightTheme"
        Me.lightTheme.Size = New System.Drawing.Size(54, 23)
        Me.lightTheme.TabIndex = 7
        Me.lightTheme.UseVisualStyleBackColor = False
        '
        'darkTheme
        '
        Me.darkTheme.BackColor = System.Drawing.Color.DarkGray
        Me.darkTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.darkTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.darkTheme.Location = New System.Drawing.Point(292, 150)
        Me.darkTheme.Name = "darkTheme"
        Me.darkTheme.Size = New System.Drawing.Size(54, 23)
        Me.darkTheme.TabIndex = 8
        Me.darkTheme.UseVisualStyleBackColor = False
        '
        'firstNameTextBox
        '
        Me.firstNameTextBox.Location = New System.Drawing.Point(137, 474)
        Me.firstNameTextBox.Multiline = True
        Me.firstNameTextBox.Name = "firstNameTextBox"
        Me.firstNameTextBox.Size = New System.Drawing.Size(181, 20)
        Me.firstNameTextBox.TabIndex = 9
        '
        'lastNameTextBox
        '
        Me.lastNameTextBox.Location = New System.Drawing.Point(137, 508)
        Me.lastNameTextBox.Multiline = True
        Me.lastNameTextBox.Name = "lastNameTextBox"
        Me.lastNameTextBox.Size = New System.Drawing.Size(181, 20)
        Me.lastNameTextBox.TabIndex = 10
        '
        'addressTextBox
        '
        Me.addressTextBox.Location = New System.Drawing.Point(137, 542)
        Me.addressTextBox.Multiline = True
        Me.addressTextBox.Name = "addressTextBox"
        Me.addressTextBox.Size = New System.Drawing.Size(181, 20)
        Me.addressTextBox.TabIndex = 11
        '
        'phoneTextBox
        '
        Me.phoneTextBox.Location = New System.Drawing.Point(137, 576)
        Me.phoneTextBox.Multiline = True
        Me.phoneTextBox.Name = "phoneTextBox"
        Me.phoneTextBox.Size = New System.Drawing.Size(181, 20)
        Me.phoneTextBox.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Gray
        Me.Label6.Location = New System.Drawing.Point(155, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 23)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "THEME"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(150, 143)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(206, 37)
        Me.PictureBox1.TabIndex = 14
        Me.PictureBox1.TabStop = False
        '
        'editDatalbl
        '
        Me.editDatalbl.BackColor = System.Drawing.Color.Transparent
        Me.editDatalbl.Cursor = System.Windows.Forms.Cursors.Hand
        Me.editDatalbl.Font = New System.Drawing.Font("Microsoft Himalaya", 14.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.editDatalbl.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.editDatalbl.Location = New System.Drawing.Point(157, 437)
        Me.editDatalbl.Name = "editDatalbl"
        Me.editDatalbl.Size = New System.Drawing.Size(64, 23)
        Me.editDatalbl.TabIndex = 15
        Me.editDatalbl.Text = "Edit Data"
        Me.editDatalbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(44, 475)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 16)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "First Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(44, 509)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Last Name"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(44, 543)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 16)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Address"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(44, 577)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 16)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Phone"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'editDataIcon
        '
        Me.editDataIcon.BackColor = System.Drawing.Color.Transparent
        Me.editDataIcon.BackgroundImage = CType(resources.GetObject("editDataIcon.BackgroundImage"), System.Drawing.Image)
        Me.editDataIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.editDataIcon.Cursor = System.Windows.Forms.Cursors.Hand
        Me.editDataIcon.Location = New System.Drawing.Point(216, 439)
        Me.editDataIcon.Name = "editDataIcon"
        Me.editDataIcon.Size = New System.Drawing.Size(13, 13)
        Me.editDataIcon.TabIndex = 24
        Me.editDataIcon.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(109, 409)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(182, 26)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Account Details"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Location = New System.Drawing.Point(9, 622)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 31)
        Me.Button1.TabIndex = 26
        Me.Button1.UseVisualStyleBackColor = True
        '
        'customerprofile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(384, 661)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.editDataIcon)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.editDatalbl)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.phoneTextBox)
        Me.Controls.Add(Me.addressTextBox)
        Me.Controls.Add(Me.lastNameTextBox)
        Me.Controls.Add(Me.firstNameTextBox)
        Me.Controls.Add(Me.darkTheme)
        Me.Controls.Add(Me.lightTheme)
        Me.Controls.Add(Me.rolelbl)
        Me.Controls.Add(Me.phonelbl)
        Me.Controls.Add(Me.addresslbl)
        Me.Controls.Add(Me.usernamelbl)
        Me.Controls.Add(Me.customerIdlbl)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "customerprofile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.editDataIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As Button
    Friend WithEvents customerIdlbl As Label
    Friend WithEvents usernamelbl As Label
    Friend WithEvents addresslbl As Label
    Friend WithEvents phonelbl As Label
    Friend WithEvents rolelbl As Label
    Friend WithEvents lightTheme As Button
    Friend WithEvents darkTheme As Button
    Friend WithEvents firstNameTextBox As TextBox
    Friend WithEvents lastNameTextBox As TextBox
    Friend WithEvents addressTextBox As TextBox
    Friend WithEvents phoneTextBox As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents editDatalbl As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents editDataIcon As PictureBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Button1 As Button
End Class
