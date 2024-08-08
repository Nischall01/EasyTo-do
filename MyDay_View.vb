﻿Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Imports System.Data.SqlServerCe
Imports System.Runtime.CompilerServices
Imports System.Threading

Public Class MyDay_View
    Private connectionString As String = My.Settings.ConnectionString

    Private dt As New DataTable()

    Private AddReminderButtonPlaceholderText As String = "Add Reminder"
    Private RepeatButtonPlaceholderText As String = "Repeat"
    Private DueDatePlaceHolderText As String = "Add Due Date"
    Private DescriptionPlaceholderText As String = "Add Description..."

    Private UserDefaultTimeFormat As String = My.Settings.TimeFormat

    ' Image cache variables
    Private UncheckedImportantIcon As Image
    Private CheckedImportantIcon As Image
    Private DisabledImportantIcon As Image

    'Private HasReminder As Boolean
    Private CurrentDateTime As DateTime = DateTime.Now
    Private IsTaskPropertiesVisible As Boolean = True
    Private Task As String
    Private Done As Boolean

    Private SelectedTaskIndex As Integer = -1
    Private SelectedTaskItem As TaskItem

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    '---------------------------------------------------------------------------------Initialization----------------------------------------------------------------------------------------'
#Region "Initialization"
    Private Sub InitializeMy_day()
        AddNewTask_TextBox.Focus()
        LoadTasksToMyDay()
        Select Case My.Settings.TaskPropertiesSidebarOnStart
            Case "Expanded"
                IsTaskPropertiesVisible = True
            Case "Collapsed"
                IsTaskPropertiesVisible = False
        End Select
        ShowOrHideTaskProperties()
        DayDate_Label.Text = CurrentDateTime.ToString("dddd, MMMM dd")

        LoadCachedImages()
        DisableTaskProperties(True)
    End Sub

    Private Sub My_Day_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeMy_day()

        'ReminderTimer.Interval = 1000 ' Set interval to 5 seconds
        'ReminderTimer.Start() ' Start the Timers

        'NotifyIcon1.Text = "EasyTo_do"
        'NotifyIcon1.Icon = My.Resources.EasyToDo_Icon
        'NotifyIcon1.Visible = True
    End Sub

    Private Sub LoadCachedImages()
        ' Cache images
        UncheckedImportantIcon = My.Resources._1
        CheckedImportantIcon = My.Resources._2
        DisabledImportantIcon = My.Resources._3
    End Sub

    Private Sub DisableTaskProperties(Disable As Boolean)
        If Disable Then
            TaskTitle_TextBox.Text = Nothing
            Label_TaskEntryDateTime.Text = Nothing
            Button_Important.BackgroundImage = DisabledImportantIcon

            If My.Settings.ColorScheme = "Dark" Then
                TaskTitle_TextBox.BackColor = Color.FromArgb(30, 30, 30)
                TaskDescription_RichTextBox.Hide()
            End If
            TaskTitle_TextBox.Enabled = False
            TaskDescription_RichTextBox.Text = Nothing
            TaskDescription_RichTextBox.Enabled = False

            Label_ADT.Enabled = False
            Label_TaskEntryDateTime.Enabled = False
            Button_Important.Enabled = False

            CustomButton_AddReminder.Enabled = False
            CustomButton_AddReminder.ButtonText = AddReminderButtonPlaceholderText
            CustomButton_Repeat.Enabled = False
            CustomButton_DueDate.Enabled = False

            Button_DeleteTask.Enabled = False


        Else
            If My.Settings.ColorScheme = "Dark" Then
                TaskTitle_TextBox.BackColor = Color.FromArgb(40, 40, 40)
                TaskDescription_RichTextBox.Show()
            End If
            TaskTitle_TextBox.Enabled = True
            TaskDescription_RichTextBox.Enabled = True
            Label_ADT.Enabled = True
            Label_TaskEntryDateTime.Enabled = True
            Button_Important.Enabled = True
            CustomButton_Repeat.Enabled = True
            CustomButton_DueDate.Enabled = True
            CustomButton_AddReminder.Enabled = True
            Button_DeleteTask.Enabled = True
        End If
    End Sub
#End Region

    '---------------------------------------------------------------------------------Data Handling---------------------------------------------------------------------------------------------'
#Region "Data Handling"
    Private Sub AddNewTaskToMyDay(NewTask As String)
        Dim CurrentDateTime As DateTime = DateTime.Now

        ' Insert the new task
        Dim queryInsertTask As String = "INSERT INTO Tasks (Task, EntryDateTime, Section) VALUES (@Task, @EntryDateTime, @Section)"

        Using connection As New SqlCeConnection(connectionString)
            Using command As New SqlCeCommand(queryInsertTask, connection)
                command.Parameters.AddWithValue("@Task", NewTask)
                command.Parameters.AddWithValue("@EntryDateTime", CurrentDateTime)
                command.Parameters.AddWithValue("@Section", "MyDay")

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        ' Optionally notify success
                        ' MessageBox.Show("Task added successfully.")
                    Else
                        MessageBox.Show("No rows were affected. The task might not have been added.")
                    End If
                Catch ex As SqlCeException
                    MessageBox.Show("SQL CE Error: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Unexpected Error: " & ex.Message)
                End Try
            End Using
        End Using

        ' Reload the data to reflect changes
        Views.RefreshTasks()
    End Sub

    Private Sub HardResetTableTasks()
        Dim dropTableQuery As String = "DROP TABLE Tasks"
        Dim createTableQuery As String = "
                                            CREATE TABLE Tasks (
                                            TaskID int IDENTITY(1,1) NOT NULL,
                                            Task NVARCHAR (256) NOT NULL,Description NVARCHAR(4000) NULL,
                                            IsDone bit NOT NULL DEFAULT 0,
                                            IsImportant bit NOT NULL DEFAULT 0,
                                            DueDate datetime NULL,Section NVARCHAR (256) NOT NULL,
                                            EntryDateTime datetime NOT NULL,
                                            IsRepeated bit NOT NULL DEFAULT 0,
                                            RepeatedDays NVARCHAR (256) NULL);
                                            ALTER TABLE [Tasks] 
                                            ADD CONSTRAINT  [Tasks_PK] PRIMARY KEY ([TaskID]);
                                         "
        Using connection As New SqlCeConnection(connectionString)
            Try
                connection.Open()

                ' Begin a transaction
                Using transaction = connection.BeginTransaction()
                    ' Drop the table if it exists
                    Using dropCommand As New SqlCeCommand(dropTableQuery, connection, transaction)
                        dropCommand.ExecuteNonQuery()
                    End Using

                    ' Recreate the table
                    Using createCommand As New SqlCeCommand(createTableQuery, connection, transaction)
                        createCommand.ExecuteNonQuery()
                    End Using

                    ' Commit the transaction
                    transaction.Commit()
                End Using

            Catch ex As SqlCeException
                ' Detailed SQL CE exception
                MessageBox.Show("SQL CE Error: " & ex.Message)
            Catch ex As Exception
                ' General exception
                MessageBox.Show("Unexpected Error: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using

        ' Reload the data to reflect changes
        LoadTasksToMyDay()
        DisableTaskProperties(True)
    End Sub

    Private Sub UpdateTaskDescription(taskIndex As Integer, newDescription As String)
        Dim query As String = "UPDATE My_Day SET Task_Description = @NewDescription WHERE Task_Index = @TaskIndex"

        Using connection As New SqlCeConnection(connectionString)
            Using command As New SqlCeCommand(query, connection)
                command.Parameters.AddWithValue("@NewDescription", newDescription)
                command.Parameters.AddWithValue("@TaskIndex", taskIndex)

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        'MessageBox.Show("Task description updated successfully.")
                    Else
                        MessageBox.Show("No task found with the specified index.")
                    End If
                Catch ex As SqlCeException
                    MessageBox.Show("SQL CE Error: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Unexpected Error: " & ex.Message)
                End Try
            End Using
        End Using
        LoadTasksToMyDay()

        If MyDay_CheckedListBox.Items.Count > 0 Then
            MyDay_CheckedListBox.SelectedIndex = taskIndex
            MyDay_CheckedListBox.Focus()
        End If
    End Sub
#End Region

    '-----------------------------------------------------------------------------Task Handling------------------------------------------------------------------------------'
#Region "Task Handling"
    Private Sub ShowOrHideTaskProperties(Optional H As String = "Show")
        If IsTaskPropertiesVisible And H = "Show" Then
            MainTlp.ColumnStyles(0).SizeType = SizeType.Percent
            MainTlp.ColumnStyles(0).Width = 75
            MainTlp.ColumnStyles(1).SizeType = SizeType.Percent
            MainTlp.ColumnStyles(1).Width = 25%
            IsTaskPropertiesVisible = False
        Else
            MainTlp.ColumnStyles(0).SizeType = SizeType.Percent
            MainTlp.ColumnStyles(0).Width = 100%
            MainTlp.ColumnStyles(1).SizeType = SizeType.Percent
            MainTlp.ColumnStyles(1).Width = 0%
            IsTaskPropertiesVisible = True
        End If
    End Sub

    Private Sub EnterTaskTo_My_Day_ChecklistBox()
        Dim NewMy_DayTask As String = AddNewTask_TextBox.Text
        If NewMy_DayTask Is String.Empty Then
            Exit Sub
        End If
        MyDay_CheckedListBox.Items.Add(NewMy_DayTask)
        AddNewTaskToMyDay(NewMy_DayTask)

        AddNewTask_TextBox.Clear()
        AddNewTask_TextBox.Focus()
    End Sub

    Private Sub DoneCheckChanged(itemIndex As Integer, isChecked As Boolean)
        Dim done As Integer = If(isChecked, 1, 0)

        Try
            ' Update the database with the new 'Done' value
            Dim query As String = "UPDATE My_Day SET Done = @Done WHERE Task_Index = @Task_Index"

            Using connection As New SqlCeConnection(connectionString)
                Using command As New SqlCeCommand(query, connection)
                    ' Use specific type for parameters
                    command.Parameters.Add("@Task_Index", SqlDbType.Int).Value = itemIndex
                    command.Parameters.Add("@Done", SqlDbType.Int).Value = done

                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As SqlCeException
            MessageBox.Show("SQL CE Error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ImportantCheckChanged(SelectedTaskID As Integer, isChecked As Boolean)
        'MsgBox("Task ID: " & TaskID)
        'MsgBox("IsChecked: " & isChecked)
        Dim IsImportant As Integer = If(isChecked, 1, 0)
        Try
            ' Update the database with the new 'Done' value
            Dim query As String = "UPDATE Tasks SET IsImportant = @IsImportant WHERE TaskID = @TaskID"

            Using connection As New SqlCeConnection(connectionString)
                Using command As New SqlCeCommand(query, connection)
                    command.Parameters.AddWithValue("@TaskID", SelectedTaskID)
                    command.Parameters.AddWithValue("@IsImportant", IsImportant)

                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error updating task status: " & ex.Message)
        End Try

        LoadTasksToMyDay()
        MainWindow.ImportantInstance.LoadTasksToImportant()

        ' Retain Focus after DataTable Reload
        If MyDay_CheckedListBox.Items.Count > 0 Then
            MyDay_CheckedListBox.SelectedIndex = SelectedTaskIndex
            MyDay_CheckedListBox.Focus()
        End If
    End Sub

    Private Function IsTaskImportant() As Boolean
        If SelectedTaskItem.ID < 0 Then
            Return False
        End If

        ' Find the task in the DataTable
        For Each row As DataRow In dt.Rows
            If row("TaskID") = SelectedTaskItem.ID Then
                ' Check if the task is marked as important
                If Convert.ToInt16(row("IsImportant")) = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
        ' If no matching task is found
        Return False
    End Function

    Private Function GetTaskEntryDateTime() As String
        Dim TaskEntryDateTime As String = String.Empty

        For Each row As DataRow In dt.Rows
            If row("TaskID") = SelectedTaskItem.ID Then
                If UserDefaultTimeFormat = "12" Then
                    TaskEntryDateTime = Convert.ToDateTime(row("EntryDateTime")).ToString("yyyy-MM-dd  |  hh:mm tt")
                Else
                    TaskEntryDateTime = Convert.ToDateTime(row("EntryDateTime")).ToString("yyyy-MM-dd  |  HH:mm")
                End If
                Exit For
            End If
        Next
        Return TaskEntryDateTime
    End Function

    Private Function GetTaskDescription() As String
        Dim TaskDescription As String = String.Empty

        For Each row As DataRow In dt.Rows
            If row("TaskID") = SelectedTaskItem.ID Then
                TaskDescription = row("Description").ToString
                Exit For
            End If
        Next
        Return TaskDescription
    End Function

    Private Function GetReminder() As String
        Dim TaskReminder As String = String.Empty

        For Each row As DataRow In dt.Rows
            If row("TaskID") = SelectedTaskItem.ID Then
                If IsDBNull(row("ReminderDateTime")) Then
                    Return String.Empty
                Else
                    Dim reminderDateTime As DateTime = Convert.ToDateTime(row("Reminder_DateTime"))
                    If UserDefaultTimeFormat = "12" Then
                        TaskReminder = reminderDateTime.ToString("hh:mm tt")
                    Else
                        TaskReminder = reminderDateTime.ToString("HH:mm")
                    End If
                End If
                Exit For
            End If
        Next
        Return TaskReminder
    End Function

    Private Function GetDueDate() As String
        Dim TaskDueDate As String = String.Empty

        For Each row As DataRow In dt.Rows
            If row("TaskID") = SelectedTaskItem.ID Then
                If IsDBNull(row("DueDate")) Then
                    Return String.Empty
                Else
                    Dim reminderDueDate As DateTime = Convert.ToDateTime(row("DueDate"))
                    If UserDefaultTimeFormat = "12" Then
                        TaskDueDate = reminderDueDate
                    Else
                        TaskDueDate = reminderDueDate
                    End If
                End If
                Exit For
            End If
        Next
        Return TaskDueDate
    End Function
#End Region

    '-----------------------------------------------------------------Event Handlers---------------------------------------------------'
#Region "Event Handlers"
    Private Sub Button_CloseTaskProperties_Click(sender As Object, e As EventArgs) Handles Button_CloseTaskProperties.Click
        ShowOrHideTaskProperties()
    End Sub

    Private Sub TextBox_AddNewTask_KeyDown(sender As Object, e As KeyEventArgs) Handles AddNewTask_TextBox.KeyDown
        If e.KeyValue = Keys.Enter Then
            EnterTaskTo_My_Day_ChecklistBox()

            '   IncrementCheckedListBoxHeight() ' Increment

        End If
    End Sub

    Private Sub CheckedListBox_MyDay_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        Dim itemIndex As Integer
        itemIndex = e.Index
        DoneCheckChanged(itemIndex, e.NewValue = CheckState.Checked)
    End Sub

    Private Sub CheckedListBox_MyDay_MouseDown(sender As Object, e As MouseEventArgs) Handles MyDay_CheckedListBox.MouseDown
        If e.Button = MouseButtons.Right Then
            ShowOrHideTaskProperties()
        End If
    End Sub

    Private Sub MyDay_CheckedListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MyDay_CheckedListBox.SelectedIndexChanged
        SelectedTaskIndex = MyDay_CheckedListBox.SelectedIndex
        SelectedTaskItem = MyDay_CheckedListBox.SelectedItem

        If MyDay_CheckedListBox.SelectedIndex = -1 Then
            DisableTaskProperties(True)
            TaskTitle_TextBox.Clear()
        Else
            DisableTaskProperties(False)
            TaskTitle_TextBox.Text = MyDay_CheckedListBox.SelectedItem.ToString()
            Label_TaskEntryDateTime.Text = GetTaskEntryDateTime()

            If GetTaskDescription() <> String.Empty Then
                If My.Settings.ColorScheme = "Dark" Then
                    TaskDescription_RichTextBox.ForeColor = Color.Pink
                ElseIf My.Settings.ColorScheme = "Light" Then
                    TaskDescription_RichTextBox.ForeColor = Color.Black
                End If
                TaskDescription_RichTextBox.Text = GetTaskDescription()
            Else
                TaskDescription_RichTextBox.ForeColor = Color.Gray
                TaskDescription_RichTextBox.Text = DescriptionPlaceholderText
            End If

            If IsTaskImportant() Then
                Button_Important.BackgroundImage = CheckedImportantIcon
            Else
                Button_Important.BackgroundImage = UncheckedImportantIcon
            End If

            If GetReminder() <> String.Empty Then
                CustomButton_AddReminder.ButtonText = GetReminder()
            Else
                CustomButton_AddReminder.ButtonText = AddReminderButtonPlaceholderText
            End If

            If GetDueDate() <> String.Empty Then
                CustomButton_DueDate.ButtonText = GetDueDate()
            Else
                CustomButton_DueDate.ButtonText = DueDatePlaceHolderText
            End If
        End If
    End Sub

    Private Sub Button_Important_Click(sender As Object, e As EventArgs) Handles Button_Important.Click
        If IsTaskImportant() Then
            ImportantCheckChanged(SelectedTaskItem.ID, CheckState.Unchecked)
        Else
            ImportantCheckChanged(SelectedTaskItem.ID, CheckState.Checked)
        End If
    End Sub

    Private Sub Button_Important_MouseEnter(sender As Object, e As EventArgs) Handles Button_Important.MouseEnter
        If IsTaskImportant() Then
            Exit Sub
        End If
        Button_Important.BackgroundImage = CheckedImportantIcon
    End Sub

    Private Sub Button_Important1_MouseLeave(sender As Object, e As EventArgs) Handles Button_Important.MouseLeave
        If IsTaskImportant() Then
            Exit Sub
        End If
        Button_Important.BackgroundImage = UncheckedImportantIcon
    End Sub

    Private Sub Button_DeleteTask_Click(sender As Object, e As EventArgs) Handles Button_DeleteTask.Click
        DeleteTask()
    End Sub

    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles TaskDescription_RichTextBox.Enter
        If My.Settings.ColorScheme = "Dark" Then
            TaskDescription_RichTextBox.ForeColor = Color.White
        ElseIf My.Settings.ColorScheme = "Light" Then
            TaskDescription_RichTextBox.ForeColor = Color.FromArgb(69, 69, 69)
        End If
        If TaskDescription_RichTextBox.Text = DescriptionPlaceholderText Then
            TaskDescription_RichTextBox.Text = String.Empty
        End If
    End Sub

    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TaskDescription_RichTextBox.KeyDown
        ' Check if Enter key is pressed
        If e.KeyCode = Keys.Enter Then
            ' Check if Shift key is also pressed
            If e.Shift Then
                ' Allow default behavior (new line)
            Else
                ' Prevent the default behavior
                e.SuppressKeyPress = True
                UpdateTaskDescription(MyDay_CheckedListBox.SelectedIndex, TaskDescription_RichTextBox.Text)
            End If
        End If
    End Sub

    Private Sub TextBox_AddNewTask_Enter(sender As Object, e As EventArgs) Handles AddNewTask_TextBox.Enter
        LoseListItemFocus()
        DisableTaskProperties(True)
    End Sub

    Private Sub SubTlpTaskView_SubTlpTop_Click(sender As Object, e As EventArgs) Handles SubTlpTaskView_SubTlpTop.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
        DisableTaskProperties(True)
    End Sub

    Private Sub SubTlpTaskView_SubTlpBottom_Click(sender As Object, e As EventArgs) Handles SubTlpTaskView_SubTlpBottom.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
        DisableTaskProperties(True)
    End Sub

    Private Sub MyDay_Label_Click(sender As Object, e As EventArgs) Handles MyDay_Label.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
        DisableTaskProperties(True)
    End Sub

    Private Sub CustomButton_AddReminder_Click(sender As Object, e As MouseEventArgs) Handles CustomButton_AddReminder.Click
        If e.Button = MouseButtons.Left Then
            Dim AddReminder_time_Instance = New Reminder_Dialog With {
                .Reminder_SelectedTaskIndex = MyDay_CheckedListBox.SelectedIndex, .NeedsDatePicker = False
            }
            AddReminder_time_Instance.ShowDialog()
            AddReminder_time_Instance.BringToFront()
            LoadTasksToMyDay()
            If MyDay_CheckedListBox.Items.Count > 0 Then
                MyDay_CheckedListBox.SelectedIndex = AddReminder_time_Instance.Reminder_SelectedTaskIndex
                MyDay_CheckedListBox.Focus()
            End If
            AddReminder_time_Instance.Dispose()
        ElseIf e.Button = MouseButtons.Right Then
            ShowContextMenuCentered(ContextMenuStrip1, CustomButton_AddReminder)
        End If
    End Sub

    Private Sub ShowContextMenuCentered(contextMenu As ContextMenuStrip, control As Control)
        ' Calculate the center position of the control on the screen
        Dim buttonCenterScreenPosition As Point = control.PointToScreen(New Point(control.Width / 2, control.Height / 2))

        ' Calculate the location to show the ContextMenuStrip centered over the control
        Dim contextMenuPosition As New Point(buttonCenterScreenPosition.X - (contextMenu.Width / 2), buttonCenterScreenPosition.Y - (contextMenu.Height / 2))

        ' Show the ContextMenuStrip at the calculated position
        contextMenu.Show(contextMenuPosition)
    End Sub

    Private Sub RemoveReminder()
        Dim query As String = "UPDATE My_Day SET Reminder_DateTime = NULL WHERE Task_Index = @TaskIndex"

        Using connection As New SqlCeConnection(connectionString)
            Using command As New SqlCeCommand(query, connection)
                command.Parameters.AddWithValue("@TaskIndex", MyDay_CheckedListBox.SelectedIndex)

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()
                    If rowsAffected > 0 Then
                        'MessageBox.Show("Reminder removed successfully.")
                    Else
                        MessageBox.Show("No task found with the specified index.")
                    End If
                Catch ex As SqlCeException
                    MessageBox.Show("SQL CE Error: " & ex.Message)
                Catch ex As Exception
                    MessageBox.Show("Unexpected Error: " & ex.Message)
                End Try
            End Using
        End Using
        LoadTasksToMyDay()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Dim ToDeleteReminderTaskIndex As Integer = MyDay_CheckedListBox.SelectedIndex
        RemoveReminder()
        If MyDay_CheckedListBox.Items.Count > 0 Then
            MyDay_CheckedListBox.SelectedIndex = ToDeleteReminderTaskIndex
            MyDay_CheckedListBox.Focus()
        End If
    End Sub

    Private Sub ReminderTimer_Tick(sender As Object, e As EventArgs) Handles ReminderTimer.Tick
        CheckReminders()
        If UserDefaultTimeFormat = "12" Then
            Time_Label.Text = DateTime.Now.ToString("hh:mm tt")
        Else
            Time_Label.Text = DateTime.Now.ToString("HH:MM")
        End If
    End Sub
    Private Sub CheckReminders()
        Dim currentTime As DateTime = DateTime.Now

        For Each row As DataRow In dt.Rows
            ' Check if the Reminder_DateTime column is not null
            If row("Reminder_DateTime") IsNot DBNull.Value Then
                ' Directly cast to DateTime
                Dim reminderTime As DateTime = row("Reminder_DateTime")

                ' Convert both current time and reminder time to string in the same format
                Dim currentTimeString As String = currentTime.ToString("yyyy-MM-dd HH:mm:ss")
                Dim reminderTimeString As String = reminderTime.ToString("yyyy-MM-dd HH:mm:ss")

                ' Compare the formatted date and time strings
                If reminderTimeString = currentTimeString Then
                    ' Display reminder
                    If row("Task_Description") IsNot DBNull.Value And row("Important") = True Then
                        ShowNotification(row("Task"), True, row("Task_Description"))
                    ElseIf row("Task_Description") IsNot DBNull.Value And row("Important") = False Then
                        ShowNotification(row("Task"), False, row("Task_Description"))
                    ElseIf row("Task_Description") Is DBNull.Value And row("Important") = True Then
                        ShowNotification(row("Task"), True)
                    ElseIf row("Task_Description") Is DBNull.Value And row("Important") = False Then
                        ShowNotification(row("Task"), False)
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub ShowNotification(title As String, IsImportant As Boolean, Optional message As String = " ")
        NotifyIcon1.BalloonTipTitle = title
        If IsImportant Then
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.Warning
            NotifyIcon1.BalloonTipText = message
        Else
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.None
            NotifyIcon1.BalloonTipText = message
        End If
        NotifyIcon1.ShowBalloonTip(3000) ' 3000 milliseconds = 3 seconds
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon1.BalloonTipClicked
        If MainWindow IsNot Nothing Then
            MainWindow.Activate()
            MainWindow.WindowState = FormWindowState.Normal
            MainWindow.TopMost = True
            System.Threading.Thread.Sleep(500)
            MainWindow.TopMost = False
            SetForegroundWindow(MainWindow.Handle)
        End If
    End Sub

    ' Dispose of the NotifyIcon when the form is closed
    Private Sub My_Day_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        NotifyIcon1.Dispose()
    End Sub

    Private Sub Label_DayDate_Click(sender As Object, e As EventArgs) Handles DayDate_Label.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
    End Sub

    Private Sub TableLayoutPanel1_Click(sender As Object, e As EventArgs) Handles TableLayoutPanel1.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
    End Sub

    Private Sub Time_Label_Click(sender As Object, e As EventArgs) Handles Time_Label.Click
        ShowOrHideTaskProperties("Hide")
        Me.ActiveControl = Nothing
        LoseListItemFocus()
    End Sub
#End Region

    Private Sub RichTextBox1_EnabledChanged(sender As Object, e As EventArgs)
        If Not TaskDescription_RichTextBox.Enabled Then
            TaskDescription_RichTextBox.BackColor = Color.FromArgb(40, 40, 40) ' Set your desired background color
        End If
    End Sub

    Private Sub LoseListItemFocus()
        MyDay_CheckedListBox.SelectedIndex = -1
    End Sub

    Private Sub My_Day_KeyDown(sender As Object, e As KeyEventArgs) Handles MyDay_CheckedListBox.KeyDown
        If e.KeyValue = Keys.Delete Then
            If MyDay_CheckedListBox.SelectedIndex <> -1 Then
                Button_DeleteTask_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub CustomButton_DueDate_Click(sender As Object, e As MouseEventArgs) Handles CustomButton_DueDate.Click
        If e.Button = MouseButtons.Left Then
            Dim DueDateInstance As New DueDate_Dialog With {
                .DueDate_SelectedTaskIndex = MyDay_CheckedListBox.SelectedIndex
            }
            DueDateInstance.ShowDialog()
            DueDateInstance.BringToFront()
            LoadTasksToMyDay()
            If MyDay_CheckedListBox.Items.Count > 0 Then
                MyDay_CheckedListBox.SelectedIndex = DueDateInstance.DueDate_SelectedTaskIndex
                MyDay_CheckedListBox.Focus()
            End If
            DueDateInstance.Dispose()
        ElseIf e.Button = MouseButtons.Right Then

        End If
    End Sub

#Region "Data Handling (My Day Table)"
    ' Load tasks onto the Checked list Box.
    Public Sub LoadTasksToMyDay()
        dt.Clear()
        '  Dim query As String = "SELECT TaskID, Task, IsDone FROM Tasks WHERE DueDate = @Today ORDER BY TaskID;"
        Dim query As String = "SELECT * FROM Tasks WHERE Section = 'MyDay' ORDER BY EntryDateTime;"


        Using connection As New SqlCeConnection(connectionString)
            Using command As New SqlCeCommand(query, connection)
                Using adapter As New SqlCeDataAdapter(command)
                    connection.Open()
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        MyDay_CheckedListBox.Items.Clear()
        For Each row As DataRow In dt.Rows
            Dim item As New TaskItem(row("Task"), row("TaskID"), row("IsDone") <> 0)
            MyDay_CheckedListBox.Items.Add(item, item.IsDone)
        Next
    End Sub


    Private Sub DeleteTask()
        Dim query As String = "DELETE FROM Tasks WHERE TaskID = @TaskID"

        Using connection As New SqlCeConnection(connectionString)
            Using command As New SqlCeCommand(query, connection)
                command.Parameters.AddWithValue("@TaskID", SelectedTaskItem.ID)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using

        Views.RefreshTasks()

        If MyDay_CheckedListBox.Items.Count <> 0 Then
            MyDay_CheckedListBox.SelectedIndex = SelectedTaskIndex - 1
        Else
            MyDay_CheckedListBox_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub
#End Region
End Class