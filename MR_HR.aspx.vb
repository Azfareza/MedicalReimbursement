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
    Dim LoginLogic As New LoginLogic.Login

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin
                Response.Redirect("Login.aspx")
            ElseIf userRole = "2" Then ' Role 2: Direksi
                Response.Redirect("Login.aspx")
            ElseIf userRole = "3" Then ' Role 3: HR

            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If
            BindgvReqList()
            BindgvHistory()
            BindgvRejectList()
        End If
    End Sub
    Private Sub BindgvReqList()
        gvRequestList.Visible = True
        gvRequestList.DataSource = Pengajuan.SelectAllUnProcessed()
        gvRequestList.DataBind()
    End Sub
    Private Sub BindgvHistory()
        gvHistory.Visible = True
        gvHistory.DataSource = Pengajuan.SelectAllOnProcess()
        gvHistory.DataBind()
    End Sub
    Private Sub BindgvRejectList()
        gvRejectList.Visible = True
        gvRejectList.DataSource = Pengajuan.SelectAllReject()
        gvRejectList.DataBind()
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
                Case "on process"
                    targetCell.Text = "On Process"
                    targetCell.ForeColor = Drawing.Color.Orange
                    targetCell.Font.Bold = True
                Case Else
                    targetCell.Text = statusText
                    targetCell.ForeColor = Drawing.Color.Black
            End Select
            e.Row.Cells(6).Text = statusText
        End If
    End Sub

    Protected Sub gvRejectList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
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
                Case "on process"
                    targetCell.Text = "On Process"
                    targetCell.ForeColor = Drawing.Color.Orange
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

        Dim dtDetil As DataTable = Pengajuan.GetDetailPenyakit(kodeklaim)

        If dtDetil IsNot Nothing AndAlso dtDetil.Rows.Count > 0 Then
            Dim drDetil As DataRow = dtDetil.Rows(0)

            If drDetil.Table.Columns.Contains("DetailPenyakit") AndAlso Not IsDBNull(drDetil("DetailPenyakit")) Then
                txtDetilPenyakitModal.Text = drDetil("DetailPenyakit").ToString().Trim()
            Else
                txtDetilPenyakitModal.Text = "-"
            End If
        Else
            txtDetilPenyakitModal.Text = "-"
        End If
        txtDetilPenyakitModal.Enabled = False


        ' ⬇ Ambil dokumen opsional
        Dim dokumen = Pengajuan.SelectDocument(kodeklaim)
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


        txtClaim.Enabled = False
        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtbiayaModal.Visible = False
        lblBiayaModal.Visible = False


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

    Private Sub gvRejectList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRejectList.SelectedIndexChanged
        ViewState("ShowModal") = True

        Dim PilihText As GridViewRow = gvRejectList.SelectedRow

        txtClaim.Text = Trim(PilihText.Cells(0).Text)
        txtNIPModal.Text = Trim(PilihText.Cells(1).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(2).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(3).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(4).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(5).Text)
        'txtbiayaModal.Text = Trim(PilihText.Cells(6).Text)


        txtClaim.Enabled = False
        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtbiayaModal.Visible = False
        lblBiayaModal.Visible = False


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
        If Pengajuan.KlaimUpdater(txtClaim.Text, "On Process") = True Then
            Dim nip As String = txtNIPModal.Text
            Dim nama As String = txtNamaModal.Text
            Dim category As String = txtKategoriModal.Text
            Dim tanggal As String = txtTanggalModal.Text
            Dim biaya As String = txtbiayaModal.Text
            sendApproveNotif(
                nip:=nip,
                Nama:=nama,
                Kategori:=category,
                Tanggal:=tanggal,
                Biaya:=biaya
            )
            Pengajuan.InsertHistoryStatus("On Process", DateTime.Now, Nothing, kdklaim)
            ViewState("ShowModal") = False
            BindgvReqList()
            BindgvHistory()
            BindgvRejectList()
            gvRequestList.SelectedIndex = -1
            gvHistory.SelectedIndex = -1
            gvRejectList.SelectedIndex = -1
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btntidaksetuju_Click(sender As Object, e As EventArgs) Handles btntidaksetuju.Click
        Dim kdklaim As Integer = CInt(txtClaim.Text)
        Dim catatan As String = Request.Form("rejectNote")
        If Pengajuan.KlaimUpdater(txtClaim.Text, "Rejected") = True Then
            Dim nip As String = txtNIPModal.Text
            Dim nama As String = txtNamaModal.Text
            Dim category As String = txtKategoriModal.Text
            Dim tanggal As String = txtTanggalModal.Text
            Dim biaya As String = txtbiayaModal.Text
            sendRejectNotif(
                nip:=nip,
                Nama:=nama,
                Kategori:=category,
                Note:=catatan,
                Tanggal:=tanggal,
                Biaya:=biaya
            )
            Pengajuan.InsertHistoryStatus("Reject", DateTime.Now, catatan, kdklaim)
            ViewState("ShowModal") = False
            BindgvReqList()
            BindgvHistory()
            BindgvRejectList()
            gvRequestList.SelectedIndex = -1
            gvHistory.SelectedIndex = -1
            gvRejectList.SelectedIndex = -1
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub

    Protected Sub sendRejectNotif(nip As String, Nama As String, Kategori As String, Note As String, Tanggal As Date, Biaya As String)

        Dim nomor As String = LoginLogic.GetUserCell(nip)
        Dim pesanLengkap As String =
            $"*PENGAJUAN ANDA DI TOLAK OLEH HR!*" & vbCrLf &
            $"NIP: {nip}" & vbCrLf &
            $"Nama: {Nama}" & vbCrLf &
            $"Kategori: {Kategori}" & vbCrLf &
            $"Tanggal Pengobatan: {Tanggal:dd/MM/yyyy}" & vbCrLf &
            $"Biaya: {Biaya}" & vbCrLf & vbCrLf &
            $"*Alasan Penolakan:*" & vbCrLf &
            $"{Note}" & vbCrLf

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
            End Using
        Catch ex As WebException
            Using reader As New StreamReader(ex.Response.GetResponseStream())
                Dim errorText As String = reader.ReadToEnd()
            End Using
        End Try
    End Sub

    Protected Sub sendApproveNotif(nip As String, Nama As String, Kategori As String, Tanggal As Date, Biaya As String)

        Dim nomor As String = LoginLogic.GetUserCell(nip)
        Dim pesanLengkap As String =
            $"*PENGAJUAN ANDA TELAH DISETUJUI OLEH HR!*" & vbCrLf &
            $"NIP: {nip}" & vbCrLf &
            $"Nama: {Nama}" & vbCrLf &
            $"Kategori: {Kategori}" & vbCrLf &
            $"Tanggal Pengobatan: {Tanggal:dd/MM/yyyy}" & vbCrLf &
            $"Biaya: {Biaya}" & vbCrLf & vbCrLf

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
            End Using
        Catch ex As WebException
            Using reader As New StreamReader(ex.Response.GetResponseStream())
                Dim errorText As String = reader.ReadToEnd()
            End Using
        End Try
    End Sub

End Class

