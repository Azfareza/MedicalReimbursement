Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class MR_EMP
    Inherits System.Web.UI.Page
    Dim MrEmployee As New DataReq.Data_Dashboard
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
                Response.Redirect("Login.aspx")
            ElseIf userRole = "4" Then ' Role 4: USER

            End If
            BindLogHistoris()
            txtMedicalDetail.Attributes("placeholder") = "Input Diagnosa Disini"

            'Sesi kalau Berhasil
            If Session("SuccessMessage") IsNot Nothing Then
                Dim alertScript As String = $"<script type='text/javascript'>alert('{Session("SuccessMessage").ToString().Replace("'", "\'")}');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
                Session.Remove("SuccessMessage")
            End If

            btnSubmit.Enabled = False
            fileKwitansi.Enabled = False
            fileResep.Enabled = False
            filePendukung.Enabled = False
            btnSubmit.Style("display") = "none"

        End If
    End Sub

    Private Function HitungReimbursement(ByRef hasil As Integer) As Boolean
        Dim rawInput As String = txtTotalCost.Text.Replace(".", "").Replace(",", "").Replace("Rp", "").Trim()

        Dim totalCost As Integer
        If Not Integer.TryParse(rawInput, totalCost) Then
            hasil = 0
            Return False
        End If

        Dim kategori As String = ddlReimbursementCategory.SelectedItem.Text.Trim()

        Select Case kategori
            Case "Kacamata"
                hasil = Math.Min(totalCost * 0.85, 600000)

            Case "Persalinan"
                hasil = Math.Min(totalCost, 15000000)

            Case "Rawat Jalan"
                hasil = totalCost

            Case Else
                hasil = 0
                Return False
        End Select

        Return True
    End Function

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim result As Integer
        If HitungReimbursement(result) Then
            Dim rawInput As String = txtTotalCost.Text.Replace(".", "").Replace(",", "").Replace("Rp", "").Trim()

            ' Validasi input tidak boleh negatif
            If Not Integer.TryParse(rawInput, Nothing) OrElse Convert.ToInt32(rawInput) <> 0 Then
                btnSubmit.Enabled = True
                fileKwitansi.Enabled = True
                fileResep.Enabled = True
                filePendukung.Enabled = True
                btnSubmit.Style("display") = "inline-block"
                Dim totalCost As Integer = Convert.ToInt32(rawInput)
                Dim formattedTotal As String = FormatRupiah(totalCost)
                Dim formattedResult As String = FormatRupiah(result)
                Dim kategori As String = ddlReimbursementCategory.SelectedItem.Text.Trim()

                Select Case kategori
                    Case "Kacamata"
                        lblCalculation.Text = $"{formattedTotal} x 0.85 (maks. Rp600.000) = {formattedResult}"

                    Case "Rawat Jalan"
                        lblCalculation.Text = $"Total klaim = {formattedResult}"

                    Case "Persalinan"
                        lblCalculation.Text = $"{formattedTotal} (maks.Rp15.000.000) → {formattedResult}"

                    Case Else
                        lblCalculation.Text = ""
                End Select
            Else
                lblCalculation.Text = ""
            End If
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim category As String = ddlReimbursementCategory.SelectedItem.Text
        Dim selectedDate As Date = Date.Parse(txtDate.Text)
        Dim medicalDetail As String = txtMedicalDetail.Text.Trim()
        Dim rawInput As String = txtTotalCost.Text.Replace(".", "").Replace(",", "").Replace("Rp", "").Trim()
        Dim totalCost As Integer

        If Not Integer.TryParse(rawInput, totalCost) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "InvalidCost", "<script>alert('Total biaya tidak valid.');</script>", False)
            Return
        End If

        Dim result As Integer
        If Not HitungReimbursement(result) Then Return

        ' EDIT MODE (UPDATE)
        If ViewState("EditID") IsNot Nothing Then
            Dim editId As Integer = Convert.ToInt32(ViewState("EditID"))
            Dim updated As Boolean = UpdateRequest(editId, category, selectedDate, medicalDetail, result, "Awaiting")

            ' File upload (opsional)
            Dim kwitansi As Byte() = Nothing
            Dim resep As Byte() = Nothing
            Dim pendukung As Byte() = Nothing

            If fileKwitansi.HasFile Then
                kwitansi = New Byte(fileKwitansi.PostedFile.ContentLength - 1) {}
                fileKwitansi.PostedFile.InputStream.Read(kwitansi, 0, fileKwitansi.PostedFile.ContentLength)
            End If
            If fileResep.HasFile Then
                resep = New Byte(fileResep.PostedFile.ContentLength - 1) {}
                fileResep.PostedFile.InputStream.Read(resep, 0, fileResep.PostedFile.ContentLength)
            End If
            If filePendukung.HasFile Then
                pendukung = New Byte(filePendukung.PostedFile.ContentLength - 1) {}
                filePendukung.PostedFile.InputStream.Read(pendukung, 0, filePendukung.PostedFile.ContentLength)
            End If

            UpdateDocument(kwitansi, resep, pendukung, editId)
            Session("SuccessMessage") = "Pengajuan berhasil diperbarui."
            Response.Redirect(Request.RawUrl)
            Return
        End If

        ' INSERT MODE (default)
        If ddlReimbursementCategory.SelectedIndex = 0 OrElse
        Not fileKwitansi.HasFile OrElse
        Not fileResep.HasFile OrElse
        Not filePendukung.HasFile OrElse
        String.IsNullOrWhiteSpace(txtDate.Text) OrElse
        String.IsNullOrWhiteSpace(txtMedicalDetail.Text) OrElse
        String.IsNullOrWhiteSpace(txtTotalCost.Text) Then

            ClientScript.RegisterStartupScript(Me.GetType(), "InputValidation", "<script>alert('Harap isi semua data sebelum mengirim.');</script>", False)
            Return
        End If

        Dim isSuccess As Boolean = Pengajuan.AddNewRequest(category, selectedDate, DateTime.Now, medicalDetail, result, "Awaiting", Session("NIP"))

        If isSuccess Then
            Dim KdKlaim As Integer = GetLastInsertedKlaimId()
            Dim nip As String = Session("NIP").ToString()
            Dim kwitansi As Byte() = New Byte(fileKwitansi.PostedFile.ContentLength - 1) {}
            fileKwitansi.PostedFile.InputStream.Read(kwitansi, 0, fileKwitansi.PostedFile.ContentLength)

            Dim resep As Byte() = New Byte(fileResep.PostedFile.ContentLength - 1) {}
            fileResep.PostedFile.InputStream.Read(resep, 0, fileResep.PostedFile.ContentLength)

            Dim pendukung As Byte() = New Byte(filePendukung.PostedFile.ContentLength - 1) {}
            filePendukung.PostedFile.InputStream.Read(pendukung, 0, filePendukung.PostedFile.ContentLength)

            Try
                Pengajuan.SaveDocument(kwitansi, DateTime.Now, resep, pendukung, KdKlaim)
            Catch ex As Exception
                ClientScript.RegisterStartupScript(Me.GetType(), "SaveError", $"<script>alert('Gagal menyimpan dokumen: {ex.Message}');</script>", False)
                Return
            End Try

            sendReqNotif(nip, category, selectedDate, DateTime.Now, medicalDetail, result, "Awaiting")
            Session("SuccessMessage") = "Pengajuan berhasil disimpan."
            Response.Redirect(Request.RawUrl)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "SaveError", "<script>alert('Gagal menyimpan pengajuan.');</script>", False)
        End If
    End Sub

    Private Function FormatRupiah(value As Integer) As String
        Return "Rp" & value.ToString("N0", New Globalization.CultureInfo("id-ID"))
    End Function

    Public Function GetLastInsertedKlaimId() As Integer
        Dim lastId As Integer = 0
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT TOP 1 KdKlaim FROM DAFTAR_PENGAJUAN_KLAIM ORDER BY TanggalPengajuan DESC"
        cmd.CommandType = CommandType.Text
        cmd.Connection = ConnDB

        Try
            'ConnDB.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lastId = Convert.ToInt32(reader("KdKlaim"))
            End If
            reader.Close()
        Catch ex As Exception
            ' Tangani kesalahan jika diperlukan
        Finally
            'ConnDB.Close()
            cmd.Dispose()
        End Try

        Return lastId
    End Function

    Protected Sub rptLogHistory_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptLogHistory.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblStatusTerakhir As Label = CType(e.Item.FindControl("lblStatusTerakhir"), Label)
            Dim lnkViewDetails As LinkButton = CType(e.Item.FindControl("lnkViewDetails"), LinkButton)
            If lblStatusTerakhir IsNot Nothing AndAlso lnkViewDetails IsNot Nothing Then
                Dim statusKlaim As String = lblStatusTerakhir.Text.Trim()
                lnkViewDetails.Visible = statusKlaim.Equals("Rejected", StringComparison.OrdinalIgnoreCase)
            End If
        End If
    End Sub

    Protected Sub rptLogHistory_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName = "Update" Then
            Dim kdKlaim As Integer = Convert.ToInt32(e.CommandArgument)
            ViewState("EditID") = kdKlaim
            LoadDataToForm(kdKlaim)
        End If
    End Sub

    Protected Sub lnkViewDetails_Click(source As Object, e As RepeaterCommandEventArgs) Handles rptLogHistory.ItemCommand
        If e.CommandName = "Update" Then
            Dim kdKlaim As Integer = Convert.ToInt32(e.CommandArgument)
            ViewState("EditID") = kdKlaim
            LoadDataToForm(kdKlaim)
            ' Optional: feedback visual
            lblCalculation.Text &= "<br/><span class='text-xs text-blue-600'>🔄 Mode edit aktif untuk KdKlaim #" & kdKlaim & "</span>"
            btnSubmit.Text = "Update Pengajuan"
        End If
    End Sub
    Private Sub LoadDataToForm(kdKlaim As Integer)
        Dim cmd As New SqlCommand("DAFTAR_PENGAJUAN_KLAIM_DETAIL", ConnDB)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@KdKlaim", kdKlaim)

        Using reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                ddlReimbursementCategory.SelectedValue = reader("Kategori").ToString().Trim()
                txtDate.Text = Convert.ToDateTime(reader("TanggalPengobatan")).ToString("yyyy-MM-dd")
                txtMedicalDetail.Text = reader("DetailPenyakit").ToString().Trim()
                txtTotalCost.Text = reader("Biaya").ToString()

                lblCalculation.Text = String.Empty
                If Not IsDBNull(reader("FileKwitansi")) Then
                    lblCalculation.Text &= "<br/><span class='text-xs text-green-600'>📄 Kwitansi sebelumnya tersedia.</span>"
                End If
                If Not IsDBNull(reader("FileResep")) Then
                    lblCalculation.Text &= "<br/><span class='text-xs text-green-600'>📄 Resep sebelumnya tersedia.</span>"
                End If
                If Not IsDBNull(reader("FilePendukung")) Then
                    lblCalculation.Text &= "<br/><span class='text-xs text-green-600'>📄 Dokumen pendukung sebelumnya tersedia.</span>"
                End If

                btnSubmit.Enabled = True
                btnSubmit.Style("display") = "inline-block"
                fileKwitansi.Enabled = True
                fileResep.Enabled = True
                filePendukung.Enabled = True
            End If
        End Using
    End Sub

    Public Function UpdateRequest(KdKlaim As Integer, Kategori As String, TanggalPengobatan As Date, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String) As Boolean
        Dim cmd As New SqlCommand
        cmd.CommandText = "DAFTAR_PENGAJUAN_KLAIM_UPDATE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = ConnDB

        cmd.Parameters.Add(New SqlParameter("@KdKlaim", SqlDbType.Int)).Value = KdKlaim
        cmd.Parameters.Add(New SqlParameter("@Kategori", SqlDbType.Char, 12)).Value = Kategori
        cmd.Parameters.Add(New SqlParameter("@TanggalPengobatan", SqlDbType.Date)).Value = TanggalPengobatan
        cmd.Parameters.Add(New SqlParameter("@DetailPenyakit", SqlDbType.Char, 30)).Value = DetailPenyakit
        cmd.Parameters.Add(New SqlParameter("@Biaya", SqlDbType.Int)).Value = Biaya
        cmd.Parameters.Add(New SqlParameter("@StatusTerakhir", SqlDbType.Char, 12)).Value = Status_Terakhir

        Try
            'ConnDB.Open()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            'ConnDB.Close()
            cmd.Dispose()
        End Try
    End Function
    Public Function UpdateDocument(kwitansi As Byte(), resep As Byte(), pendukung As Byte(), KdKlaim As Integer) As Boolean
        Dim cmd As New SqlCommand
        cmd.CommandText = "DAFTAR_DOKUMEN_KLAIM_UPDATE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = ConnDB

        cmd.Parameters.Add("@FileKwitansi", SqlDbType.VarBinary).Value = If(kwitansi IsNot Nothing, kwitansi, DBNull.Value)
        cmd.Parameters.Add("@FileResep", SqlDbType.VarBinary).Value = If(resep IsNot Nothing, resep, DBNull.Value)
        cmd.Parameters.Add("@FilePendukung", SqlDbType.VarBinary).Value = If(pendukung IsNot Nothing, pendukung, DBNull.Value)
        cmd.Parameters.Add("@KdKlaim", SqlDbType.Int).Value = KdKlaim

        Try
            'ConnDB.Open()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            'ConnDB.Close()
            cmd.Dispose()
        End Try
    End Function

    Protected Sub sendReqNotif(nip As String, Kategori As String, TanggalPengobatan As Date, TanggalPengajuan As DateTime, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String)

        Dim nomor As String = LoginLogic.GetUserCell(nip)
        Dim waktuSekarang As String = TanggalPengajuan
        Dim pesanLengkap As String =
            $"*PENGAJUAN BERHASIL DIAJUKAN!*" & vbCrLf &
            $"NIP: {nip}" & vbCrLf &
            $"Kategori: {Kategori}" & vbCrLf &
            $"Tanggal Pengobatan: {TanggalPengobatan:dd/MM/yyyy}" & vbCrLf &
            $"Detail Penyakit: {DetailPenyakit}" & vbCrLf &
            $"Total Biaya: Rp{Biaya:N0}" & vbCrLf &
            $"Status: {Status_Terakhir}" & vbCrLf &
            $"Diajukan pada: {TanggalPengajuan:dd/MM/yyyy HH:mm:ss}"

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

    Private Sub BindLogHistoris()
        rptLogHistory.Visible = True
        Dim dtLogHistory As DataTable = Pengajuan.SelectAllLogHistorisByNip(Session("NIP"))
        rptLogHistory.DataSource = dtLogHistory
        rptLogHistory.DataBind()
    End Sub
End Class