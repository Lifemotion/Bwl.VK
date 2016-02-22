Imports Bwl.VK

Public Class VkGateEmulator
    Implements IVk

    Public ReadOnly Property Status As String Implements IVk.Status
        Get
            Return "Emulator Ok"
        End Get
    End Property

    Public ReadOnly Property Working As Boolean Implements IVk.Working
        Get
            Return True
        End Get
    End Property

    Public Event MessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String) Implements IVk.MessageReceived
    Public Event MessageSent(receiverID As Long, messageText As String) Implements IVk.MessageSent

    Public Sub SendMessage(receiverID As Integer, messageText As String) Implements IVk.SendMessage
        AddToDailog("Сервис (0) для " + "(" + receiverID.ToString + ")", messageText)
        RaiseEvent MessageSent(receiverID, messageText)
    End Sub

    Private Sub VkGateEmulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tbMessage_TextChanged(sender As Object, e As EventArgs) Handles tbMessage.TextChanged

    End Sub

    Public Sub AddToDailog(from As String, fromtext As String)
        If InvokeRequired Then
            Me.Invoke(Sub() AddToDailog(from, fromtext))
        Else
            lbDialog.Items.Add(from + ":")
            Dim lines = fromtext.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            For Each line In lines
                lbDialog.Items.Add(line)
            Next
            lbDialog.SelectedIndex = lbDialog.Items.Count - 1
        End If

    End Sub

    Private Sub tbMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles tbMessage.KeyDown
        If e.KeyCode = Keys.Enter Then
            If e.Control = True Or e.Shift = True Then
                Dim msg = tbMessage.Text.Trim
                Dim senderId = CLng(Val(tbSenderID.Text))
                Dim senderName = tbSenderName.Text.Trim
                Dim parts = lbSender.Text.Split("/")
                If parts.Length = 2 Then
                    senderId = CLng(Val(parts(1)))
                    senderName = parts(0).Trim
                End If
                AddToDailog(senderName + " (" + senderId.ToString + ")", msg)
                Dim response As String = ""
                RaiseEvent MessageReceived(msg, senderId, senderName, response)
                If response IsNot Nothing AndAlso response > "" Then
                    AddToDailog("Сервис (0) для " + senderName + " (" + senderId.ToString + ")", response)
                    RaiseEvent MessageSent(senderId, response)
                End If
                e.SuppressKeyPress = True
                If e.Control Then tbMessage.Text = ""
            End If
        End If
    End Sub
End Class