Imports System.IO
Imports System.Net
Imports System.Text

Partial Class SendNotif

    Inherits System.Web.UI.Page

    Protected Sub btnKirim_Click(sender As Object, e As EventArgs)
        Dim nomor As String = txtNomor.Text.Trim()

        Dim pesan As String = txtPesan.Text.Trim()
        Dim waktuSekarang As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        Dim pesanLengkap As String = pesan & vbCrLf & "*Dikirim pada tanggal: " & waktuSekarang & "*"

        'Fill the TOKEN!
        Dim token As String = ""

        Dim boundary As String = "------------------------" & DateTime.Now.Ticks.ToString("x")
        Dim request As HttpWebRequest = CType(WebRequest.Create("https://api.fonnte.com/send"), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "multipart/form-data; boundary=" & boundary
        request.Headers.Add("Authorization", token)

        Dim postData As New StringBuilder()
        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""target""")
        postData.AppendLine()
        postData.AppendLine(nomor)

        postData.AppendLine("--" & boundary)
        postData.AppendLine("Content-Disposition: form-data; name=""message""")
        postData.AppendLine()
        postData.AppendLine(pesanLengkap)

        postData.AppendLine("--" & boundary & "--")

        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData.ToString())
        request.ContentLength = byteArray.Length

        Try
            Using dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
            End Using

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseText As String = reader.ReadToEnd()
                lblStatus.Text = "Pesan dikirim!"
            End Using
        Catch ex As WebException
            Using reader As New StreamReader(ex.Response.GetResponseStream())
                Dim errorText As String = reader.ReadToEnd()
                lblStatus.Text = "Gagal mengirim pesan!"
            End Using
        End Try
    End Sub
End Class
