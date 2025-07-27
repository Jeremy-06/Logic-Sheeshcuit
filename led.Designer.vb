<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class led
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
        Me.Back = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.minus_btn = New System.Windows.Forms.Button()
        Me.plus_btn = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.addtocart_btn = New System.Windows.Forms.Button()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Back
        '
        Me.Back.BackColor = System.Drawing.Color.Transparent
        Me.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Back.Font = New System.Drawing.Font("Georgia", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Back.ForeColor = System.Drawing.Color.Black
        Me.Back.Location = New System.Drawing.Point(789, 11)
        Me.Back.Margin = New System.Windows.Forms.Padding(2)
        Me.Back.Name = "Back"
        Me.Back.Size = New System.Drawing.Size(103, 31)
        Me.Back.TabIndex = 88
        Me.Back.Text = "BACK"
        Me.Back.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Image = Global.Logic_Sheeshcuit.My.Resources.Resources.Product_7
        Me.PictureBox1.Location = New System.Drawing.Point(31, 102)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(450, 407)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 101
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift Condensed", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 77)
        Me.Label1.TabIndex = 199
        Me.Label1.Text = "LEDs"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(524, 167)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(337, 126)
        Me.Label5.TabIndex = 209
        Me.Label5.Text = "Standard through-hole LEDs for general use in electronics." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Colors: Blue, Green" &
    ", Red, White, Yellow" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Diameter: 5mm" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Current: ~20mA" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "- Forward Voltage: ~2V " &
    "(depends on color)"
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Gainsboro
        Me.PictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox9.Location = New System.Drawing.Point(528, 298)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(12, 41)
        Me.PictureBox9.TabIndex = 208
        Me.PictureBox9.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Calibri", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(539, 298)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 41)
        Me.Label4.TabIndex = 206
        Me.Label4.Text = "₱25.00"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Gainsboro
        Me.Label3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(655, 369)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 19)
        Me.Label3.TabIndex = 205
        Me.Label3.Text = "QUANTITY"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Bahnschrift SemiBold Condensed", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(518, 102)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(321, 122)
        Me.Label2.TabIndex = 204
        Me.Label2.Text = "LED 5mm (10 pcs)"
        '
        'minus_btn
        '
        Me.minus_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.minus_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.minus_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.minus_btn.Location = New System.Drawing.Point(539, 392)
        Me.minus_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.minus_btn.Name = "minus_btn"
        Me.minus_btn.Size = New System.Drawing.Size(47, 36)
        Me.minus_btn.TabIndex = 203
        Me.minus_btn.Text = "-"
        Me.minus_btn.UseVisualStyleBackColor = True
        '
        'plus_btn
        '
        Me.plus_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.plus_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.plus_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.plus_btn.Location = New System.Drawing.Point(799, 392)
        Me.plus_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.plus_btn.Name = "plus_btn"
        Me.plus_btn.Size = New System.Drawing.Size(47, 36)
        Me.plus_btn.TabIndex = 202
        Me.plus_btn.Text = "+"
        Me.plus_btn.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(590, 393)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(205, 35)
        Me.TextBox1.TabIndex = 201
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'addtocart_btn
        '
        Me.addtocart_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.addtocart_btn.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.addtocart_btn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.addtocart_btn.Location = New System.Drawing.Point(539, 440)
        Me.addtocart_btn.Margin = New System.Windows.Forms.Padding(2)
        Me.addtocart_btn.Name = "addtocart_btn"
        Me.addtocart_btn.Size = New System.Drawing.Size(307, 54)
        Me.addtocart_btn.TabIndex = 200
        Me.addtocart_btn.Text = "ADD TO CART"
        Me.addtocart_btn.UseVisualStyleBackColor = True
        '
        'PictureBox8
        '
        Me.PictureBox8.BackColor = System.Drawing.Color.Gainsboro
        Me.PictureBox8.Location = New System.Drawing.Point(524, 360)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(337, 149)
        Me.PictureBox8.TabIndex = 207
        Me.PictureBox8.TabStop = False
        '
        'led
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(904, 556)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.PictureBox9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.minus_btn)
        Me.Controls.Add(Me.plus_btn)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.addtocart_btn)
        Me.Controls.Add(Me.PictureBox8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Back)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "led"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LEDs"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Back As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents PictureBox9 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents minus_btn As Button
    Friend WithEvents plus_btn As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents addtocart_btn As Button
    Friend WithEvents PictureBox8 As PictureBox
End Class
