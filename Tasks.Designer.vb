﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tasks
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tasks))
        Me.SubTableLayoutPanel_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.PictureBox_FormIcon = New System.Windows.Forms.PictureBox()
        Me.MainTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.SubTableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox_AddNewTask = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label_Tasks = New System.Windows.Forms.Label()
        Me.SubTableLayoutPanel_Top.SuspendLayout()
        CType(Me.PictureBox_FormIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainTableLayoutPanel.SuspendLayout()
        Me.SubTableLayoutPanel_Bottom.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SubTableLayoutPanel_Top
        '
        Me.SubTableLayoutPanel_Top.ColumnCount = 2
        Me.SubTableLayoutPanel_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.SubTableLayoutPanel_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.0!))
        Me.SubTableLayoutPanel_Top.Controls.Add(Me.Label_Tasks, 1, 0)
        Me.SubTableLayoutPanel_Top.Controls.Add(Me.PictureBox_FormIcon, 0, 0)
        Me.SubTableLayoutPanel_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubTableLayoutPanel_Top.Location = New System.Drawing.Point(3, 3)
        Me.SubTableLayoutPanel_Top.Name = "SubTableLayoutPanel_Top"
        Me.SubTableLayoutPanel_Top.RowCount = 1
        Me.SubTableLayoutPanel_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SubTableLayoutPanel_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86.0!))
        Me.SubTableLayoutPanel_Top.Size = New System.Drawing.Size(778, 86)
        Me.SubTableLayoutPanel_Top.TabIndex = 0
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
        'MainTableLayoutPanel
        '
        Me.MainTableLayoutPanel.BackColor = System.Drawing.Color.White
        Me.MainTableLayoutPanel.ColumnCount = 1
        Me.MainTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainTableLayoutPanel.Controls.Add(Me.SubTableLayoutPanel_Top, 0, 0)
        Me.MainTableLayoutPanel.Controls.Add(Me.SubTableLayoutPanel_Bottom, 0, 2)
        Me.MainTableLayoutPanel.Controls.Add(Me.DataGridView1, 0, 1)
        Me.MainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainTableLayoutPanel.Name = "MainTableLayoutPanel"
        Me.MainTableLayoutPanel.RowCount = 3
        Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.MainTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.MainTableLayoutPanel.Size = New System.Drawing.Size(784, 461)
        Me.MainTableLayoutPanel.TabIndex = 1
        '
        'SubTableLayoutPanel_Bottom
        '
        Me.SubTableLayoutPanel_Bottom.ColumnCount = 3
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.SubTableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.SubTableLayoutPanel_Bottom.Controls.Add(Me.TextBox_AddNewTask, 1, 0)
        Me.SubTableLayoutPanel_Bottom.Controls.Add(Me.Button1, 0, 0)
        Me.SubTableLayoutPanel_Bottom.Controls.Add(Me.Button2, 2, 0)
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
        Me.TextBox_AddNewTask.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TextBox_AddNewTask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.TextBox_AddNewTask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.TextBox_AddNewTask.Location = New System.Drawing.Point(197, 22)
        Me.TextBox_AddNewTask.Name = "TextBox_AddNewTask"
        Me.TextBox_AddNewTask.Size = New System.Drawing.Size(383, 20)
        Me.TextBox_AddNewTask.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(586, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(3, 95)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(778, 293)
        Me.DataGridView1.TabIndex = 2
        '
        'Label_Tasks
        '
        Me.Label_Tasks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label_Tasks.AutoSize = True
        Me.Label_Tasks.Font = New System.Drawing.Font("Yu Gothic UI", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Tasks.Location = New System.Drawing.Point(104, 24)
        Me.Label_Tasks.Name = "Label_Tasks"
        Me.Label_Tasks.Size = New System.Drawing.Size(81, 37)
        Me.Label_Tasks.TabIndex = 5
        Me.Label_Tasks.Text = "Tasks"
        '
        'Tasks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 461)
        Me.Controls.Add(Me.MainTableLayoutPanel)
        Me.Name = "Tasks"
        Me.Text = "Tasks"
        Me.SubTableLayoutPanel_Top.ResumeLayout(False)
        Me.SubTableLayoutPanel_Top.PerformLayout()
        CType(Me.PictureBox_FormIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainTableLayoutPanel.ResumeLayout(False)
        Me.SubTableLayoutPanel_Bottom.ResumeLayout(False)
        Me.SubTableLayoutPanel_Bottom.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SubTableLayoutPanel_Top As TableLayoutPanel
    Friend WithEvents PictureBox_FormIcon As PictureBox
    Friend WithEvents MainTableLayoutPanel As TableLayoutPanel
    Friend WithEvents SubTableLayoutPanel_Bottom As TableLayoutPanel
    Friend WithEvents TextBox_AddNewTask As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents Label_Tasks As Label
End Class
