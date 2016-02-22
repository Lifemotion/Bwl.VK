Imports Bwl.Framework
Imports Bwl.Network.ClientServerMessaging

Public Class VkGateServer
    Private _vk As IVk
    Private _server As New NetServer
    Private _logger As Logger

    Public Sub New(vk As IVk, logger As Logger)
        _vk = vk
        _logger = logger
        AddHandler _vk.MessageReceived, AddressOf VkMessageReceived
        AddHandler _server.ReceivedMessage, AddressOf NetMessageReceived
        AddHandler _server.ClientConnected, AddressOf NetClientConnected
    End Sub

    Private Sub NetClientConnected(client As ConnectedClient)
        _logger.AddMessage("Connected: " + client.ID.ToString + " " + client.IPAddress)
    End Sub

    Public Property ServerPort As Integer = BwlVkShared.Port

    Public Sub Start()
        _server.StartServer(ServerPort, False)
        If _server.IsWorking Then _logger.AddMessage("Server started at port " + ServerPort.ToString)
    End Sub

    Private Sub NetMessageReceived(message As NetMessage, client As ConnectedClient)
        Try
            Select Case message.Part(0)
                Case "message-send"
                    Dim id = message.PartDouble(1)
                    Dim msg = message.Part(2)
                    Try
                        _vk.SendMessage(id, msg)
                        client.SendMessage(New NetMessage("S", "message-send", "ok"))
                        _logger.AddMessage("Send " + id.ToString + " " + msg)

                    Catch ex As Exception
                        client.SendMessage(New NetMessage("S", "message-send", "error", ex.Message))
                    End Try
            End Select
        Catch ex As Exception
            _logger.AddWarning(ex.Message)
        End Try
    End Sub

    Private Sub VkMessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String)
        Try
            If receivedText IsNot Nothing AndAlso receivedText.Length > 0 Then
                _logger.AddMessage("Received " + senderID.ToString + " " + receivedText)
                For Each client In _server.Clients.ToArray
                    Try
                        client.SendMessage(New NetMessage("S", "message-received", senderID.ToString, receivedText))
                    Catch ex As Exception
                        _logger.AddWarning(ex.Message)
                    End Try
                Next
            End If
        Catch ex As Exception
            _logger.AddWarning(ex.Message)
        End Try
    End Sub
End Class
