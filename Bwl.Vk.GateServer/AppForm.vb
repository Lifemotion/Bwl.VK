Imports Bwl.Framework
Imports Bwl.Vk.Interface

Public Class AppForm
    Inherits FormAppBase
    Dim vkdailog As New VkDialog(_storage, _logger)
    Dim vkserver As New VkGateServer(vkdailog, _logger)

    Private Sub AppForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        vkdailog.Start()
        vkserver.Start()
        Text += " " + Application.ProductVersion.ToString
    End Sub
End Class
