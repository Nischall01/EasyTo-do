﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Repeated_View
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Repeated_View))
        Me.MainTlp = New System.Windows.Forms.TableLayoutPanel()
        Me.Repeated_CheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.SubTableLayoutPanel_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Repeated_Label = New System.Windows.Forms.Label()
        Me.PictureBox_FormIcon = New System.Windows.Forms.PictureBox()
        Me.SubTableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox_AddNewTask = New System.Windows.Forms.TextBox()
        Me.MainTlp.SuspendLayout()
        Me.SubTableLayoutPanel_Top.SuspendLayout()
        CType(Me.PictureBox_FormIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SubTableLayoutPanel_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTlp
        '
        Me.MainTlp.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.MainTlp.ColumnCount = 1
        Me.MainTlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainTlp.Controls.Add(Me.Repeated_CheckedListBox, 0, 1)
        Me.MainTlp.Controls.Add(Me.SubTableLayoutPanel_Top, 0, 0)
        Me.MainTlp.Controls.Add(Me.SubTableLayoutPanel_Bottom, 0, 2)
        Me.MainTlp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTlp.Location = New System.Drawing.Point(0, 0)
        Me.MainTlp.Name = "MainTlp"
        Me.MainTlp.RowCount = 3
        Me.MainTlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.MainTlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.MainTlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.MainTlp.Size = New System.Drawing.Size(784, 461)
        Me.MainTlp.TabIndex = 0
        '
        'Repeated_CheckedListBox
        '
        Me.Repeated_CheckedListBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.Repeated_CheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Repeated_CheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Repeated_CheckedListBox.Font = New System.Drawing.Font("Microsoft PhagsPa", 12.0!)
        Me.Repeated_CheckedListBox.ForeColor = System.Drawing.Color.White
        Me.Repeated_CheckedListBox.FormattingEnabled = True
        Me.Repeated_CheckedListBox.Location = New System.Drawing.Point(6, 92)
        Me.Repeated_CheckedListBox.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Repeated_CheckedListBox.Name = "Repeated_CheckedListBox"
        Me.Repeated_CheckedListBox.Size = New System.Drawing.Size(772, 299)
        Me.Repeated_CheckedListBox.TabIndex = 3
        '
        'SubTableLayoutPanel_Top
        '
        Me.SubTableLayoutPanel_Top.ColumnCount = 2
        Me.SubTableLayoutPanel_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.SubTableLayoutPanel_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0!))
        Me.SubTableLayoutPanel_Top.Controls.Add(Me.Repeated_Label, 1, 0)
        Me.SubTableLayoutPanel_Top.Controls.Add(Me.PictureBox_FormIcon, 0, 0)
        Me.SubTableLayoutPanel_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubTableLayoutPanel_Top.Location = New System.Drawing.Point(3, 3)
        Me.SubTableLayoutPanel_Top.Name = "SubTableLayoutPanel_Top"
        Me.SubTableLayoutPanel_Top.RowCount = 1
        Me.SubTableLayoutPanel_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SubTableLayoutPanel_Top.Size = New System.Drawing.Size(778, 86)
        Me.SubTableLayoutPanel_Top.TabIndex = 0
        '
        'Repeated_Label
        '
        Me.Repeated_Label.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Repeated_Label.AutoSize = True
        Me.Repeated_Label.BackColor = System.Drawing.Color.Transparent
        Me.Repeated_Label.Font = New System.Drawing.Font("Yu Gothic UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Repeated_Label.ForeColor = System.Drawing.Color.White
        Me.Repeated_Label.Location = New System.Drawing.Point(104, 24)
        Me.Repeated_Label.Name = "Repeated_Label"
        Me.Repeated_Label.Size = New System.Drawing.Size(132, 37)
        Me.Repeated_Label.TabIndex = 5
        Me.Repeated_Label.Text = "Repeated"
        '
        'PictureBox_FormIcon
        '
        Me.PictureBox_FormIcon.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.PictureBox_FormIcon.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox_FormIcon.Enabled = False
        Me.PictureBox_FormIcon.Image = CType(resources.GetObject("PictureBox_FormIcon.Image"), System.Drawing.Image)
        Me.PictureBox_FormIcon.Location = New System.Drawing.Point(61, 23)
        Me.PictureBox_FormIcon.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox_FormIcon.Name = "PictureBox_FormIcon"
        Me.PictureBox_FormIcon.Size = New System.Drawing.Size(40, 40)
        Me.PictureBox_FormIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox_FormIcon.TabIndex = 2
        Me.PictureBox_FormIcon.TabStop = False
        '
        'SubTableLayoutPanel_Bottom
        '
        Me.SubTableLayoutPanel_Bottom.ColumnCount = 3
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SubTableLayoutPanel_Bottom.Controls.Add(Me.TextBox_AddNewTask, 1, 0)
        Me.SubTableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubTableLayoutPanel_Bottom.Location = New System.Drawing.Point(3, 394)
        Me.SubTableLayoutPanel_Bottom.Name = "SubTableLayoutPanel_Bottom"
        Me.SubTableLayoutPanel_Bottom.RowCount = 1
        Me.SubTableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SubTableLayoutPanel_Bottom.Size = New System.Drawing.Size(778, 64)
        Me.SubTableLayoutPanel_Bottom.TabIndex = 1
        '
        'TextBox_AddNewTask
        '
        Me.TextBox_AddNewTask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TextBox_AddNewTask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.TextBox_AddNewTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.TextBox_AddNewTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox_AddNewTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_AddNewTask.ForeColor = System.Drawing.Color.White
        Me.TextBox_AddNewTask.Location = New System.Drawing.Point(197, 3)
        Me.TextBox_AddNewTask.Name = "TextBox_AddNewTask"
        Me.TextBox_AddNewTask.Size = New System.Drawing.Size(383, 20)
        Me.TextBox_AddNewTask.TabIndex = 4
        '
        'Repeated_View
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.MainTlp)
        Me.Name = "Repeated_View"
        Me.Text = "Daily"
        Me.MainTlp.ResumeLayout(False)
        Me.SubTableLayoutPanel_Top.ResumeLayout(False)
        Me.SubTableLayoutPanel_Top.PerformLayout()
        CType(Me.PictureBox_FormIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SubTableLayoutPanel_Bottom.ResumeLayout(False)
        Me.SubTableLayoutPanel_Bottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainTlp As TableLayoutPanel
    Friend WithEvents SubTableLayoutPanel_Top As TableLayoutPanel
    Friend WithEvents SubTableLayoutPanel_Bottom As TableLayoutPanel
    Friend WithEvents PictureBox_FormIcon As PictureBox
    Friend WithEvents Repeated_CheckedListBox As CheckedListBox
    Friend WithEvents Repeated_Label As Label
    Friend WithEvents TextBox_AddNewTask As TextBox
End Class