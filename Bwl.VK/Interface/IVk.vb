Public Interface IVk
    Sub SendMessage(receiverID As Integer, messageText As String)
    Event MessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String)
    Event MessageSent(receiverID As Long, messageText As String)
    ReadOnly Property Working As Boolean
    ReadOnly Property Status As String
End Interface

Public Class BwlVkShared
    Public Const Port = 3650
End Class