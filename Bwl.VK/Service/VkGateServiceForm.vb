Imports Bwl.Framework

Public Class VkGateServiceForm
    Inherits FormBase

    Protected _vk As IVk

    Public Sub New(vk As IVk, appBase As AppBase)
        _vk = vk
        _storage = appBase.RootStorage
        _logger = appBase.RootLogger
        InitializeComponent()
        AddHandler _vk.MessageReceived, AddressOf MessageReceivedHandler
        AddHandler _vk.MessageSent, AddressOf MessageSentHandler
    End Sub

    Private Sub MessageSentHandler(receiverID As Long, messageText As String)
        _logger.AddMessage("Sent to " + receiverID.ToString + ": " + messageText)

    End Sub

    Private Sub MessageReceivedHandler(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String)
        _logger.AddMessage("Received from " + senderName.ToString + " (" + senderID.ToString + "): " + receivedText)
    End Sub

    Private Sub AppForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text += " " + Application.ProductVersion.ToString
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        cbWorking.Checked = _vk.Working
        tbStatus.Text = _vk.Status
    End Sub
End Class
