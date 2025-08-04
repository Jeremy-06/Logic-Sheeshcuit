<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class adminprofile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(adminprofile))
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.editDatalbl = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PhoneTextBox = New System.Windows.Forms.TextBox()
        Me.EmailTextBox = New System.Windows.Forms.TextBox()
        Me.fullNameTextBox = New System.Windows.Forms.TextBox()
        Me.darkTheme = New System.Windows.Forms.Button()
        Me.lightTheme = New System.Windows.Forms.Button()
        Me.rolelbl = New System.Windows.Forms.Label()
        Me.phonelbl = New System.Windows.Forms.Label()
        Me.emaillbl = New System.Windows.Forms.Label()
        Me.usernamelbl = New System.Windows.Forms.Label()
        Me.adminIdlbl = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.editDataIcon = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.editDataIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(109, 409)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(182, 26)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "Account Details"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(44, 563)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 16)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "Phone"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(44, 522)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 16)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Email"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(44, 481)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 16)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Full Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.editDatalbl.TabIndex = 41
        Me.editDatalbl.Text = "Edit Data"
        Me.editDatalbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Font = New System.Drawing.Font("Bahnschrift", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Gray
        Me.Label6.Location = New System.Drawing.Point(156, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 23)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "THEME"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PhoneTextBox
        '
        Me.PhoneTextBox.Location = New System.Drawing.Point(137, 562)
        Me.PhoneTextBox.Multiline = True
        Me.PhoneTextBox.Name = "PhoneTextBox"
        Me.PhoneTextBox.Size = New System.Drawing.Size(181, 20)
        Me.PhoneTextBox.TabIndex = 38
        '
        'EmailTextBox
        '
        Me.EmailTextBox.Location = New System.Drawing.Point(137, 521)
        Me.EmailTextBox.Multiline = True
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.Size = New System.Drawing.Size(181, 20)
        Me.EmailTextBox.TabIndex = 37
        '
        'fullNameTextBox
        '
        Me.fullNameTextBox.Location = New System.Drawing.Point(137, 480)
        Me.fullNameTextBox.Multiline = True
        Me.fullNameTextBox.Name = "fullNameTextBox"
        Me.fullNameTextBox.Size = New System.Drawing.Size(181, 20)
        Me.fullNameTextBox.TabIndex = 35
        '
        'darkTheme
        '
        Me.darkTheme.BackColor = System.Drawing.Color.DarkGray
        Me.darkTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.darkTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.darkTheme.Location = New System.Drawing.Point(293, 149)
        Me.darkTheme.Name = "darkTheme"
        Me.darkTheme.Size = New System.Drawing.Size(54, 23)
        Me.darkTheme.TabIndex = 34
        Me.darkTheme.UseVisualStyleBackColor = False
        '
        'lightTheme
        '
        Me.lightTheme.BackColor = System.Drawing.Color.White
        Me.lightTheme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.lightTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lightTheme.Location = New System.Drawing.Point(231, 149)
        Me.lightTheme.Name = "lightTheme"
        Me.lightTheme.Size = New System.Drawing.Size(54, 23)
        Me.lightTheme.TabIndex = 33
        Me.lightTheme.UseVisualStyleBackColor = False
        '
        'rolelbl
        '
        Me.rolelbl.BackColor = System.Drawing.Color.Transparent
        Me.rolelbl.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rolelbl.ForeColor = System.Drawing.Color.Gray
        Me.rolelbl.Location = New System.Drawing.Point(29, 148)
        Me.rolelbl.Name = "rolelbl"
        Me.rolelbl.Size = New System.Drawing.Size(87, 16)
        Me.rolelbl.TabIndex = 32
        Me.rolelbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'phonelbl
        '
        Me.phonelbl.BackColor = System.Drawing.Color.Transparent
        Me.phonelbl.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.phonelbl.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.phonelbl.ForeColor = System.Drawing.Color.White
        Me.phonelbl.Location = New System.Drawing.Point(81, 316)
        Me.phonelbl.Name = "phonelbl"
        Me.phonelbl.Size = New System.Drawing.Size(291, 33)
        Me.phonelbl.TabIndex = 31
        '
        'emaillbl
        '
        Me.emaillbl.BackColor = System.Drawing.Color.Transparent
        Me.emaillbl.Font = New System.Drawing.Font("Microsoft Himalaya", 27.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.emaillbl.ForeColor = System.Drawing.Color.White
        Me.emaillbl.Location = New System.Drawing.Point(78, 245)
        Me.emaillbl.Name = "emaillbl"
        Me.emaillbl.Size = New System.Drawing.Size(294, 40)
        Me.emaillbl.TabIndex = 30
        '
        'usernamelbl
        '
        Me.usernamelbl.BackColor = System.Drawing.Color.Transparent
        Me.usernamelbl.Font = New System.Drawing.Font("Bahnschrift", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usernamelbl.ForeColor = System.Drawing.Color.Gray
        Me.usernamelbl.Location = New System.Drawing.Point(146, 69)
        Me.usernamelbl.Name = "usernamelbl"
        Me.usernamelbl.Size = New System.Drawing.Size(212, 34)
        Me.usernamelbl.TabIndex = 29
        Me.usernamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'adminIdlbl
        '
        Me.adminIdlbl.BackColor = System.Drawing.Color.Transparent
        Me.adminIdlbl.Font = New System.Drawing.Font("Malgun Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adminIdlbl.ForeColor = System.Drawing.Color.Gray
        Me.adminIdlbl.Location = New System.Drawing.Point(246, 110)
        Me.adminIdlbl.Name = "adminIdlbl"
        Me.adminIdlbl.Size = New System.Drawing.Size(111, 21)
        Me.adminIdlbl.TabIndex = 28
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Location = New System.Drawing.Point(10, 621)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 31)
        Me.Button1.TabIndex = 52
        Me.Button1.UseVisualStyleBackColor = True
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
        Me.editDataIcon.TabIndex = 50
        Me.editDataIcon.TabStop = False
        '
        'Button2
        '
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Location = New System.Drawing.Point(277, 621)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 31)
        Me.Button2.TabIndex = 27
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(151, 142)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(206, 37)
        Me.PictureBox1.TabIndex = 40
        Me.PictureBox1.TabStop = False
        '
        'adminprofile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Logic_Sheeshcuit.My.Resources.Resources.Admin_Light_Theme
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(384, 661)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.editDataIcon)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.editDatalbl)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.PhoneTextBox)
        Me.Controls.Add(Me.EmailTextBox)
        Me.Controls.Add(Me.fullNameTextBox)
        Me.Controls.Add(Me.darkTheme)
        Me.Controls.Add(Me.lightTheme)
        Me.Controls.Add(Me.rolelbl)
        Me.Controls.Add(Me.phonelbl)
        Me.Controls.Add(Me.emaillbl)
        Me.Controls.Add(Me.usernamelbl)
        Me.Controls.Add(Me.adminIdlbl)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PictureBox1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "adminprofile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "adminprofile"
        CType(Me.editDataIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents editDataIcon As PictureBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents editDatalbl As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents PhoneTextBox As TextBox
    Friend WithEvents EmailTextBox As TextBox
    Friend WithEvents fullNameTextBox As TextBox
    Friend WithEvents darkTheme As Button
    Friend WithEvents lightTheme As Button
    Friend WithEvents rolelbl As Label
    Friend WithEvents phonelbl As Label
    Friend WithEvents emaillbl As Label
    Friend WithEvents usernamelbl As Label
    Friend WithEvents adminIdlbl As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox1 As PictureBox
End Class
