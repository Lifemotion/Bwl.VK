Public Class ListOperations

    Private _dataPath As String

    Public Sub New(dataPath As String)
        _dataPath = dataPath
    End Sub

    Public Function GetLists(id As Long) As String()
        Dim files = IO.Directory.GetFiles(_dataPath, "list_" + id.ToString + "_*.txt")
        Return files
    End Function

    Public Function GetListsNames(id As Long) As String()
        Dim list As New List(Of String)
        For Each file In GetLists(id)
            Dim name = IO.Path.GetFileNameWithoutExtension(file)
            Dim listName = name.Replace("list_" + id.ToString + "_", "")
            list.Add(listName)
        Next
        Return list.ToArray
    End Function

    Public Function GetListPath(id As Long, name As String) As String
        Dim result = IO.Path.Combine(_dataPath, "list_" + id.ToString + "_" + name.Replace(" ", "-") + ".txt")
        Return result
    End Function

    Public Function GetList(id As Long, name As String) As String
        Dim path = GetListPath(id, name)
        If IO.File.Exists(path) Then Return path
        Return ""
    End Function

    Public Sub WriteList(id As Long, name As String, lines As IEnumerable(Of String))
        Dim path = GetListPath(id, name)
        IO.File.WriteAllLines(path, lines, System.Text.Encoding.UTF8)
    End Sub

    Public Function ReadList(id As Long, name As String) As List(Of String)
        Dim path = GetListPath(id, name)
        Dim lines = IO.File.ReadAllLines(path, System.Text.Encoding.UTF8)
        Return New List(Of String)(lines)
    End Function

    Public Sub SendList(ByRef responseText As String, listName As String, lines As IEnumerable(Of String))
        responseText = """" + listName + """:" + vbCrLf
        For i = 1 To lines.Count
            responseText += i.ToString + ". " + lines(i - 1) + vbCrLf
        Next
    End Sub
End Class
