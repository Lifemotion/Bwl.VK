Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging
Imports Bwl.VK

Public Class VkGateClient
    Implements IVk

    Private _client As New NetClient
    Private _logger As Logger

    Public Event MessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String) Implements IVk.MessageReceived
    Public Event MessageSent(receiverID As Long, messageText As String) Implements IVk.MessageSent
    Public ReadOnly Property ServerHost As StringSetting
    Public ReadOnly Property ServerPort As IntegerSetting

    Public ReadOnly Property AdminId As Integer = 274544625

    Public Property Autoconnect As Boolean = True

    Public Property StartedAt As DateTime = Now

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _logger = logger

        _ServerHost = storage.CreateStringSetting("ServerHost", "localhost")
        _ServerPort = storage.CreateIntegerSetting("ServerPort", BwlVkShared.Port)

        _client = New NetClient
        _logger = logger
        AddHandler _client.ReceivedMessage, AddressOf NetMessageReceived

        Dim thread As New Threading.Thread(AddressOf WorkThread)
        thread.IsBackground = True
        thread.Name = MyClass.GetType.ToString
        thread.Start()
    End Sub

    Private Sub WorkThread()
        Do
            Threading.Thread.Sleep(3000)
            Try
                If Not working Then Start()
            Catch ex As Exception
                _logger.AddMessage(ex.Message)
            End Try
        Loop
    End Sub

    Private Sub NetMessageReceived(message As NetMessage)
        Try
            Select Case message.Part(0)
                Case "message-received"
                    Dim thread As New Threading.Thread(Sub()
                                                           Dim senderid = message.PartDouble(1)
                                                           Dim msg = message.Part(2)
                                                           Dim senderName = message.Part(3)
                                                           Dim response = ""
                                                           RaiseEvent MessageReceived(msg, senderid, senderName, response)
                                                           If response > "" Then SendMessage(senderid, response)

                                                           '   If senderid = AdminId Then
                                                           If msg = "##клиент" Then

                                                               SendMessage(senderid, Application.ProductName + " " + Application.ProductVersion.ToString + " started " + StartedAt.ToString)
                                                           End If
                                                       End Sub)
                    thread.IsBackground = True
                    thread.Start()
                    ' End If
            End Select
        Catch ex As Exception
            _logger.AddWarning(ex.Message)
        End Try
    End Sub

    Public Sub SendMessage(receiverID As Integer, messageText As String) Implements IVk.SendMessage
        _client.SendMessage(New NetMessage("S", "message-send", receiverID.ToString, messageText.ToString))
        RaiseEvent MessageSent(receiverID, messageText)
    End Sub

    Public ReadOnly Property Working As Boolean Implements IVk.Working
        Get
            If _client.IsConnected Then Return True
            Return False
        End Get
    End Property

    Public ReadOnly Property Status As String Implements IVk.Status
        Get
            If _client.IsConnected Then Return "Connected"
            Return "Disconnected"
        End Get
    End Property

    Public Sub Start()
        _client.Disconnect()
        _client.Connect(ServerHost.Value, ServerPort.Value)
        If _client.IsConnected Then _logger.AddMessage("Connected to " + ServerHost.Value.ToString + ":" + ServerPort.Value.ToString)
    End Sub
End Class
