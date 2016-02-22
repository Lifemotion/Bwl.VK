<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VkGateServiceForm
    Inherits Bwl.Framework.FormBase

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
        Me.components = New System.ComponentModel.Container()
        Me.tbStatus = New System.Windows.Forms.TextBox()
        Me.cbWorking = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'logWriter
        '
        Me.logWriter.Location = New System.Drawing.Point(1, 57)
        Me.logWriter.Size = New System.Drawing.Size(734, 307)
        '
        'tbStatus
        '
        Me.tbStatus.Location = New System.Drawing.Point(99, 30)
        Me.tbStatus.Name = "tbStatus"
        Me.tbStatus.Size = New System.Drawing.Size(100, 20)
        Me.tbStatus.TabIndex = 2
        '
        'cbWorking
        '
        Me.cbWorking.AutoSize = True
        Me.cbWorking.Location = New System.Drawing.Point(12, 30)
        Me.cbWorking.Name = "cbWorking"
        Me.cbWorking.Size = New System.Drawing.Size(66, 17)
        Me.cbWorking.TabIndex = 3
        Me.cbWorking.Text = "Working"
        Me.cbWorking.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'VkGateServiceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(737, 365)
        Me.Controls.Add(Me.cbWorking)
        Me.Controls.Add(Me.tbStatus)
        Me.Name = "VkGateServiceForm"
        Me.Text = "Service: "
        Me.Controls.SetChildIndex(Me.logWriter, 0)
        Me.Controls.SetChildIndex(Me.tbStatus, 0)
        Me.Controls.SetChildIndex(Me.cbWorking, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbStatus As TextBox
    Friend WithEvents cbWorking As CheckBox
    Friend WithEvents Timer1 As Timer
End Class
