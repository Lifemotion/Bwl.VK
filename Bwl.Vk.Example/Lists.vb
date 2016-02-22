Imports Bwl.Vk

Public Module VkServiceExample


#If DEBUG Then
    'Если режим компиляции Debug, включаем использование эмулятора сервера доступа к ВК
    Private Const useVkEmulator = True
#Else
    'Если режим компиляции другой, пытаемся подключится к серверу доступа к ВК
    Private Const useVkEmulator = false 
#End If

    'ядро сервиса для ответчика вконтакте
    Private _vkService As New VkGateService("ExampleService", useVkEmulator)


    Public Sub Main()
        'подключаем обработчик входящих сообщений
        AddHandler _vkService.Vk.MessageReceived, AddressOf MessageReceivedHandler
        'отображаем форму статуса, с журналом отправленных и полученных сообщений (не обязательно)
        _vkService.ShowStatusForm()
        Application.Run()
    End Sub

    ''' <summary>
    ''' Обработчик присылаемых сервису сообщений
    ''' </summary>
    ''' <param name="receivedText">Текст пришедшего сообщения</param>
    ''' <param name="senderID">ИД отправителя, числа</param>
    ''' <param name="senderName">Имя страницы отправителя, может быть пустым</param>
    ''' <param name="responseText">Поля для размещения ответа от сервиса. Если поместить туда строку, она отправится в ответ написавшему</param>
    Private Sub MessageReceivedHandler(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String)
        'пример простого ответа через responseText, если запрос совпадает со словом "время"
        If receivedText.ToLower = "время" Then responseText = Now.ToString

        'пример более сложного ответа, через функцию SendMessage с созданием отдельного потока
        If receivedText.ToLower = "5 секунд" Then
            responseText = "начало отсчета"
            With New Threading.Thread(Sub()
                                          Threading.Thread.Sleep(5000)
                                          _vkService.Vk.SendMessage(senderID, "5 секунд прошли")
                                      End Sub)
                .Start()
            End With
        End If
    End Sub
End Module
