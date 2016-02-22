Imports Bwl.Framework

Public Module Lists
    Private _vkService As New VkGateService("Bwl Lists", False)
    Private _lists As New ListOperations(_vkService.AppBase.DataFolder)

    Public Sub Main()
        AddHandler _vkService.Vk.MessageReceived, AddressOf _vk_MessageReceived
        _vkService.CreateStatusForm.Show()
        Application.Run()
    End Sub

    Private Sub _vk_MessageReceived(receivedText As String, senderID As Long, senderName As String, ByRef responseText As String)
        If receivedText.ToLower = "время" Then responseText = Now.ToString

        Dim srcParts = receivedText.Split(" ")

        Dim parts As New List(Of String)(srcParts)
        Do While parts.Count < 5
            parts.Add("")
        Loop

        Select Case parts(0).ToLower
            Case "создать", "соз"
                Select Case (parts(1)).ToLower
                    Case "список", "спи"
                        Dim name = parts(2)
                        If _lists.GetList(senderID, name) > "" Then
                            responseText = "Такой список уже существует"
                        Else
                            _lists.WriteList(senderID, name, {})
                            responseText = "Создан список """ + name + """"
                        End If
                End Select
            Case "показать", "все", "?", "мои", "вывести", "отобразить", "пок"
                Select Case (parts(1)).ToLower
                    Case "списки", "спи"
                        Dim names = _lists.GetListsNames(senderID)
                        If names.Count = 0 Then
                            responseText = "У вас нет ни одного списка"
                        Else
                            For i = 1 To names.Length
                                responseText += i.ToString + ". " + names(i - 1) + vbCrLf
                            Next
                        End If
                End Select
            Case "список", "спи"

                Dim listName = ""
                Dim lists = _lists.GetListsNames(senderID)
                For Each list In lists
                    If list.ToLower = parts(1).ToLower Then listName = list
                Next

                If listName = "" Then
                    For Each list In lists
                        If list.ToLower.Substring(0, 3) = parts(1).ToLower Then listName = list
                    Next
                End If

                If listName = "" Then
                    responseText = "Такого списка у вас нет"
                Else
                    Select Case parts(2).ToLower
                        Case "", "показать", "?", "пок"
                            Dim lines = _lists.ReadList(senderID, listName)
                            If lines.Count = 0 Then
                                responseText = "Cписок """ + listName + """ пустой"
                            Else
                                _lists.SendList(responseText, listName, lines)
                            End If
                        Case "добавить", "+", "доб"
                            Dim txt = ""
                            For i = 3 To srcParts.Length - 1
                                txt += srcParts(i) + " "
                            Next
                            txt = txt.Replace("  ", " ").Replace("  ", " ")
                            Dim lines = _lists.ReadList(senderID, listName)
                            Dim newlines = txt.Split({vbCr, vbLf, ";"}, StringSplitOptions.RemoveEmptyEntries)
                            For Each newline In newlines
                                lines.Add(newline.Trim)
                            Next
                            _lists.WriteList(senderID, listName, lines)
                            _lists.SendList(responseText, listName, lines)
                        Case "есть", "метка", "ест", "мет", "*"
                            Dim index = CInt(Val(parts(3)))
                            If index > 0 Then
                                Dim lines = _lists.ReadList(senderID, listName)
                                If lines.Count >= index Then
                                    If lines(index - 1).StartsWith("(*) ") = False Then
                                        lines(index - 1) = "(*) " + lines(index - 1)
                                        _lists.WriteList(senderID, listName, lines)
                                        _lists.SendList(responseText, listName, lines)
                                    End If
                                End If
                            End If

                        Case "нет", "-", "очи"
                            Dim index = CInt(Val(parts(3)))
                            If index > 0 Then
                                Dim lines = _lists.ReadList(senderID, listName)
                                If lines.Count >= index Then
                                    If lines(index - 1).StartsWith("(*) ") Then
                                        lines(index - 1) = lines(index - 1).Substring(4)
                                        _lists.WriteList(senderID, listName, lines)
                                        _lists.SendList(responseText, listName, lines)
                                    End If
                                End If
                            End If
                        Case "удалить", "уда"
                            Dim txt = ""
                            For i = 3 To parts.Count - 1
                                txt += parts(i) + " "
                            Next

                            Dim newlines = txt.Split({vbCr, vbLf, ";", " ", ","}, StringSplitOptions.RemoveEmptyEntries)
                            Dim lines = _lists.ReadList(senderID, listName)

                            Dim deleted = 0
                            For Each newline In newlines
                                Dim index = CInt(Val(newline))
                                If index > 0 And lines.Count >= index - deleted Then lines.RemoveAt(index - 1 - deleted) : deleted += 1
                            Next

                            _lists.WriteList(senderID, listName, lines)
                            _lists.SendList(responseText, listName, lines)
                    End Select
                End If
        End Select
    End Sub
End Module
