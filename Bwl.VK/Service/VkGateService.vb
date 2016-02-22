Imports Bwl.Framework

Public Class VkGateService
    Public ReadOnly Property Vk As IVk
    Public ReadOnly Property Name As String
    Public ReadOnly Property AppBase As AppBase

    Public ReadOnly Property Logger As Logger
        Get
            Return _AppBase.RootLogger
        End Get
    End Property

    Public ReadOnly Property Storage As SettingsStorage
        Get
            Return _AppBase.RootStorage
        End Get
    End Property

    Public Sub New(serviceName As String, useEmulator As Boolean)
        _appBase = New AppBase(True)
        _name = serviceName
        Application.EnableVisualStyles()

        If useEmulator Then
            _Vk = New VkGateEmulator
            Dim form = DirectCast(_Vk, VkGateEmulator)
            form.Text += " " + Name
            form.Show()
        Else
            _Vk = New VkGateClient(Storage, Logger)
        End If
    End Sub

    Dim form As VkGateServiceForm

    Public Function CreateStatusForm() As VkGateServiceForm
        form = New VkGateServiceForm(Vk, AppBase)
        form.Text += " " + Name + " " + Vk.GetType.Name.ToString
        Return form
    End Function

    Public Sub ShowStatusForm()
        CreateStatusForm.Show()
    End Sub


End Class
