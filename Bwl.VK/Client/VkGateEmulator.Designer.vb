<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VkGateEmulator
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbSender = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbSenderName = New System.Windows.Forms.TextBox()
        Me.tbSenderID = New System.Windows.Forms.TextBox()
        Me.tbMessage = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbDialog = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lbSender
        '
        Me.lbSender.FormattingEnabled = True
        Me.lbSender.Items.AddRange(New Object() {"Василий Уткин/4364352446", "Максим Зайцев/5245636401", "Big Cat/6445445454", "(настраиваемое)"})
        Me.lbSender.Location = New System.Drawing.Point(12, 72)
        Me.lbSender.Name = "lbSender"
        Me.lbSender.Size = New System.Drawing.Size(181, 69)
        Me.lbSender.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(611, 30)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Средство отладки сервиса для VkGate. Выберите пользователя, от имени которого вы " &
    """пишете"" сервису, пишите сообщения, смотрите ответы."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Настраиваемые имя и ID:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Выбор отправителя:"
        '
        'tbSenderName
        '
        Me.tbSenderName.Location = New System.Drawing.Point(12, 165)
        Me.tbSenderName.Name = "tbSenderName"
        Me.tbSenderName.Size = New System.Drawing.Size(78, 20)
        Me.tbSenderName.TabIndex = 4
        Me.tbSenderName.Text = "Тест Тестов"
        '
        'tbSenderID
        '
        Me.tbSenderID.Location = New System.Drawing.Point(93, 165)
        Me.tbSenderID.Name = "tbSenderID"
        Me.tbSenderID.Size = New System.Drawing.Size(100, 20)
        Me.tbSenderID.TabIndex = 5
        Me.tbSenderID.Text = "745452412"
        '
        'tbMessage
        '
        Me.tbMessage.Location = New System.Drawing.Point(209, 368)
        Me.tbMessage.Multiline = True
        Me.tbMessage.Name = "tbMessage"
        Me.tbMessage.Size = New System.Drawing.Size(414, 50)
        Me.tbMessage.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(206, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Диалог:"
        '
        'lbDialog
        '
        Me.lbDialog.FormattingEnabled = True
        Me.lbDialog.Location = New System.Drawing.Point(209, 72)
        Me.lbDialog.Name = "lbDialog"
        Me.lbDialog.Size = New System.Drawing.Size(414, 290)
        Me.lbDialog.TabIndex = 9
        '
        'VkGateEmulator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 427)
        Me.Controls.Add(Me.lbDialog)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbMessage)
        Me.Controls.Add(Me.tbSenderID)
        Me.Controls.Add(Me.tbSenderName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbSender)
        Me.Name = "VkGateEmulator"
        Me.Text = "VkTestForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbSender As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbSenderName As TextBox
    Friend WithEvents tbSenderID As TextBox
    Friend WithEvents tbMessage As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lbDialog As ListBox
End Class
