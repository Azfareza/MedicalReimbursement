Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Data
Imports System.Net
Imports MedicalReimbursement.LoginLogic.Login

Public Class MR_HR
    Inherits System.Web.UI.Page
    Dim DataMedicalReimburse As New DataReq.Data_Dashboard
    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM
    Dim KlaimUpdating As New KLAIM_UPDATE
    Dim LoginLogic As New LoginLogic.Login


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                If Trim(Session("Seluler")) = "" Then
                    Response.Redirect("Login.aspx")
                Else

                End If
            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin
                Response.Redirect("Login.aspx")
            ElseIf userRole = "2" Then ' Role 2: Direksi

            ElseIf userRole = "3" Then ' Role 3: HR

            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If
            BindgvReqList()
            BindgvHistory()
        End If
    End Sub
    Private Sub BindgvReqList()
        gvRequestList.Visible = True
        gvRequestList.DataSource = Pengajuan.SelectAllUnProcessed()
        gvRequestList.DataBind()
    End Sub
    Private Sub BindgvHistory()
        gvHistory.Visible = True
        gvHistory.DataSource = Pengajuan.SelectAllProcessed()
        gvHistory.DataBind()
    End Sub

    Protected Sub gvHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim status As Object = DataBinder.Eval(e.Row.DataItem, "status_terakhir")
            Dim statusText As String = Convert.ToString(status).Trim().ToLower()
            Dim targetCell As TableCell = e.Row.Cells(6)

            Select Case statusText
                Case "approved"
                    targetCell.Text = "Approved"
                    targetCell.ForeColor = Drawing.Color.Green
                    targetCell.Font.Bold = True
                Case "rejected"
                    targetCell.Text = "Rejected"
                    targetCell.ForeColor = Drawing.Color.Red
                    targetCell.Font.Bold = True
                Case Else
                    targetCell.Text = statusText
                    targetCell.ForeColor = Drawing.Color.Black
            End Select
            e.Row.Cells(6).Text = statusText
        End If
    End Sub


    Private Sub gvRequestList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRequestList.SelectedIndexChanged
        ViewState("ShowModal") = True
        'pnlModal.Visible = True
        Dim PilihText As GridViewRow = gvRequestList.SelectedRow

        Dim kodeklaim As String = gvRequestList.DataKeys(PilihText.RowIndex).Value.ToString()
        txtClaim.Text = kodeklaim
        txtNIPModal.Text = Trim(PilihText.Cells(1).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(2).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(3).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(4).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(5).Text)
        txtbiayaModal.Text = Trim(PilihText.Cells(6).Text)


        txtClaim.Enabled = False
        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtbiayaModal.Enabled = False

        Dim kdklaim = txtClaim.Text
        Dim dokumen = Pengajuan.SelectDocument(kdklaim)

        If dokumen IsNot Nothing Then
            If dokumen.ContainsKey("kwitansi") AndAlso dokumen("kwitansi") IsNot Nothing Then
                Dim base64String = Convert.ToBase64String(dokumen("kwitansi"))
                imgKwitansi.ImageUrl = "data:image/jpeg;base64," & base64String
            Else
                imgKwitansi.ImageUrl = ""
            End If
            If dokumen.ContainsKey("resep") AndAlso dokumen("resep") IsNot Nothing Then
                Dim base64String = Convert.ToBase64String(dokumen("resep"))
                imgResep.ImageUrl = "data:image/jpeg;base64," & base64String
            Else
                imgResep.ImageUrl = ""
            End If
            If dokumen.ContainsKey("pendukung") AndAlso dokumen("pendukung") IsNot Nothing Then
                Dim base64String = Convert.ToBase64String(dokumen("pendukung"))
                imgPendukung.ImageUrl = "data:image/jpeg;base64," & base64String
            Else
                imgPendukung.ImageUrl = ""
            End If

        End If


    End Sub


    Private Sub gvHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvHistory.SelectedIndexChanged
        ViewState("ShowModal") = True

        Dim PilihText As GridViewRow = gvHistory.SelectedRow



        txtClaim.Text = Trim(PilihText.Cells(0).Text)
        txtNIPModal.Text = Trim(PilihText.Cells(1).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(2).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(3).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(4).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(5).Text)
        txtbiayaModal.Text = Trim(PilihText.Cells(6).Text)


        txtClaim.Enabled = False
        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtbiayaModal.Enabled = False


        Dim kdklaim = txtClaim.Text
        Dim dokumen = Pengajuan.SelectDocument(kdklaim)

        If dokumen IsNot Nothing Then
            If dokumen.ContainsKey("kwitansi") Then
                Dim base64String = Convert.ToBase64String(dokumen("kwitansi"))
                Debug.WriteLine("KWITANSI BASE64: " & base64String.Substring(0, Math.Min(base64String.Length, 100)))
                imgKwitansi.ImageUrl = "data:image/jpeg;base64," & base64String
            End If
            If dokumen.ContainsKey("resep") Then
                Dim base64String = Convert.ToBase64String(dokumen("resep"))
                Debug.WriteLine("RESEP BASE64: " & base64String.Substring(0, Math.Min(base64String.Length, 100)))
                imgResep.ImageUrl = "data:image/jpeg;base64," & base64String
            End If
            If dokumen.ContainsKey("pendukung") Then
                Dim base64String = Convert.ToBase64String(dokumen("pendukung"))
                Debug.WriteLine("PENDUKUNG BASE64: " & base64String.Substring(0, Math.Min(base64String.Length, 100)))
                imgPendukung.ImageUrl = "data:image/jpeg;base64," & base64String
            End If

        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "removeReviewOption", "document.getElementById('reviewOption')?.remove();", True)
    End Sub

    Private Sub MR_HR_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If ViewState("ShowModal") IsNot Nothing AndAlso CBool(ViewState("ShowModal")) Then
            pnlModal.CssClass = pnlModal.CssClass.Replace("hidden", "").Trim()
        Else
            If Not pnlModal.CssClass.Contains("hidden") Then
                pnlModal.CssClass += " hidden"
            End If
        End If
    End Sub


    Private Sub btnCloseModal_Click(sender As Object, e As EventArgs) Handles btnCloseModal.Click
        ViewState("ShowModal") = False
    End Sub

    Private Sub btnSetuju_Click(sender As Object, e As EventArgs) Handles btnSetuju.Click
        Dim kdklaim As Integer = CInt(txtClaim.Text)
        If KlaimUpdating.KlaimUpdater(txtClaim.Text, "Approved") = True Then
            Pengajuan.InsertHistoryStatus("Approve", DateTime.Now, Nothing, kdklaim)
            ViewState("ShowModal") = False
            BindgvReqList()
            BindgvHistory()
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btntidaksetuju_Click(sender As Object, e As EventArgs) Handles btntidaksetuju.Click
        Dim kdklaim As Integer = CInt(txtClaim.Text)
        Dim catatan As String = Request.Form("rejectNote")
        If KlaimUpdating.KlaimUpdater(txtClaim.Text, "Rejected") = True Then
            Dim category As String = lblKategoriModal.Text
            sendRejectNotif(
                Kategori:=category,
                Note:=catatan
)
            Pengajuan.InsertHistoryStatus("Reject", DateTime.Now, catatan, kdklaim)
            pnlModal.Visible = False
            BindgvReqList()
            BindgvHistory()
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub

    Protected Sub sendRejectNotif(Kategori As String, Note As String)

        Dim nip As String = txtNIPModal.Text.Trim()

        Dim nomor As String = LoginLogic.GetUserCell(nip)
        Dim pesanLengkap As String =
            $"*PENGAJUAN ANDA DI TOLAK!*" & vbCrLf &
            $"Kategori: {Kategori}" & vbCrLf &
            $"Note: {Note}" & vbCrLf

        'Fill the TOKEN!
        Dim token As String = "zF8z5jBiZ5MBaXpP6q9N"

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

