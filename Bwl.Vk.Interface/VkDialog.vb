Imports Bwl.Framework
Imports Bwl.Vk
Imports VkNet.Enums.Filters

Public Class VkDialog
    Implements IVk

    Private _logger As Logger
    Private _vkAppId As IntegerSetting
    Private _vkLogin As StringSetting
    Private _vkPassword As PasswordSetting

    Private _vk As New VkNet.VkApi
    Private _connected As Boolean
    Private _vkDelay = 400

    Public Sub New(storage As SettingsStorage, logger As Logger)
        _vkAppId = New IntegerSetting(storage, "VkAppID", 0)
        _vkLogin = New StringSetting(storage, "VkLogin", "+79")
        _vkPassword = New PasswordSetting(storage, "VkPassword")
        _logger = logger
    End Sub

    Public Sub Start()
        Dim thread As New Threading.Thread(AddressOf WorkThreadSub)
        thread.IsBackground = True
        thread.Name = "VkDialog Thread"
        thread.Start()
        _logger.AddMessage("VkDialog started")
    End Sub

    Public Event MessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String) Implements IVk.MessageReceived
    Public Event MessageSent As IVk.MessageSentEventHandler Implements IVk.MessageSent
    Public Property LoginErrorCooldownSecs As Integer

    Public ReadOnly Property Working As Boolean Implements IVk.Working
        Get
            If Not _connected Then Return False
            Return True
        End Get
    End Property

    Public ReadOnly Property Status As String Implements IVk.Status
        Get
            If Not _connected Then Return "Not Connected"
            Return "Connected"
        End Get
    End Property

    Public Sub SendMessage(receiverID As Integer, messageText As String) Implements IVk.SendMessage
        _vk.Messages.Send(receiverID, False, messageText)
    End Sub

    Private Sub WorkThreadSub()
        Do
            Try
                If _connected Then
                    Dim unreadMessages = _vk.Messages.GetDialogs(100, 0, 0, 0, True)
                    For Each msg In unreadMessages
                        _vk.Messages.MarkAsRead(msg.Id)
                        Dim responseText = ""
                        RaiseEvent MessageReceived(msg.Body, msg.UserId, "", responseText)
                        _logger.AddDebug("-> " + msg.Body)
                        If responseText IsNot Nothing AndAlso responseText > "" Then
                            _vk.Messages.Send(msg.UserId, False, responseText)
                            _logger.AddDebug("<- " + responseText)
                        End If
                        Threading.Thread.Sleep(_vkDelay)
                    Next
                Else
                    If _vkLogin.Value.Length > 7 And _vkPassword.Value.Length > 6 Then
                        Dim scope = Settings.All
                        _logger.AddMessage("VkNet connecting...")
                        _vk.Authorize(_vkAppId, _vkLogin, _vkPassword.Pass, scope)
                        _connected = True
                        _logger.AddMessage("VkNet connected ok as " + _vkLogin.Value)
                        LoginErrorCooldownSecs = 1
                        '   _logger.AddMessage(info.FirstName + " " + info.LastName + " " + info.Status)
                    End If
                End If
            Catch ex As Exception
                _logger.AddWarning(ex.Message)
                LoginErrorCooldownSecs *= 2
                If LoginErrorCooldownSecs > 60 * 60 Then LoginErrorCooldownSecs = 60 * 60
                _logger.AddMessage("Cooldown " + LoginErrorCooldownSecs.ToString + " sec")
                Threading.Thread.Sleep(10000)
                _connected = False
            End Try
            Threading.Thread.Sleep(_vkDelay)
        Loop
    End Sub


End Class
