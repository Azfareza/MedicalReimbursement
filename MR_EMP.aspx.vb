Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class MR_EMP
    Inherits System.Web.UI.Page
    Dim MrEmployee As New DataReq.Data_Dashboard
    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM

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


    ' Button kalkulasi
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

    'Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

    '    Dim kwitansiFileName As String = Path.GetFileName(fileKwitansi.FileName)
    '    Dim resepFileName As String = Path.GetFileName(fileResep.FileName)
    '    Dim pendukungFileName As String = Path.GetFileName(filePendukung.FileName)

    '    Dim kwitansiPath As String = Server.MapPath("~/Uploads/Kwitansi/" & kwitansiFileName)
    '    Dim resepPath As String = Server.MapPath("~/Uploads/Resep/" & resepFileName)
    '    Dim pendukungPath As String = Server.MapPath("~/Uploads/Pendukung/" & pendukungFileName)

    '    Directory.CreateDirectory(Path.GetDirectoryName(kwitansiPath))
    '    Directory.CreateDirectory(Path.GetDirectoryName(resepPath))
    '    Directory.CreateDirectory(Path.GetDirectoryName(pendukungPath))

    '    fileKwitansi.SaveAs(kwitansiPath)
    '    fileResep.SaveAs(resepPath)
    '    filePendukung.SaveAs(pendukungPath)

    '    Dim kwitansi As Byte() = File.ReadAllBytes(kwitansiPath)
    '    Dim resep As Byte() = File.ReadAllBytes(resepPath)
    '    Dim pendukung As Byte() = File.ReadAllBytes(pendukungPath)

    '    Pengajuan.SaveDocument(kwitansi, DateTime.Now, resep, pendukung)


    '    If ddlReimbursementCategory.SelectedIndex = 0 OrElse
    '        String.IsNullOrWhiteSpace(fileKwitansi.HasFiles) OrElse
    '        String.IsNullOrWhiteSpace(fileResep.HasFiles) OrElse
    '        String.IsNullOrWhiteSpace(filePendukung.HasFiles) OrElse
    '        String.IsNullOrWhiteSpace(txtDate.Text) OrElse
    '        String.IsNullOrWhiteSpace(txtMedicalDetail.Text) OrElse
    '        String.IsNullOrWhiteSpace(txtTotalCost.Text) Then

    '        Dim warningScript As String = "<script>alert('Harap isi semua data sebelum mengirim.');</script>"
    '        ClientScript.RegisterStartupScript(Me.GetType(), "InputValidation", warningScript, False)
    '        Return
    '    End If

    '    Dim rawInput As String = txtTotalCost.Text.Replace(".", "").Replace(",", "").Replace("Rp", "").Trim()
    '    Dim totalCost As Integer
    '    If Not Integer.TryParse(rawInput, totalCost) Then
    '        Dim errorScript As String = "<script>alert('Total biaya tidak valid.');</script>"
    '        ClientScript.RegisterStartupScript(Me.GetType(), "InvalidCost", errorScript, False)
    '        Return
    '    End If

    '    Dim result As Integer
    '    If HitungReimbursement(result) Then
    '        Dim category As String = ddlReimbursementCategory.SelectedItem.Text
    '        Dim selectedDate As String = txtDate.Text
    '        Dim medicalDetail As String = txtMedicalDetail.Text

    '        Dim alertMessage As String = $"Category: {category}\nDate: {selectedDate}\nDetail: {medicalDetail}\nCalculated Amount: {result}"
    '        alertMessage = alertMessage.Replace("'", "\'").Replace(vbCrLf, "\n").Replace(vbLf, "\n")

    '        Session("SuccessMessage") = alertMessage

    '        Pengajuan.AddNewRequest(
    '            Kategori:=category,
    '            TanggalPengobatan:=Date.Parse(selectedDate),
    '            TanggalPengajuan:=DateTime.Now,
    '            DetailPenyakit:=medicalDetail,
    '            Biaya:=result,
    '            Status_Terakhir:="On Process",
    '            NIP:="0987654321"
    '        )

    '        Response.Redirect(Request.RawUrl)
    '    End If
    'End Sub

    ' Fungsi format Rupiah

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        ' VALIDASI KELENGKAPAN FORM
        If ddlReimbursementCategory.SelectedIndex = 0 OrElse
        Not fileKwitansi.HasFile OrElse
        Not fileResep.HasFile OrElse
        Not filePendukung.HasFile OrElse
        String.IsNullOrWhiteSpace(txtDate.Text) OrElse
        String.IsNullOrWhiteSpace(txtMedicalDetail.Text) OrElse
        String.IsNullOrWhiteSpace(txtTotalCost.Text) Then

            Dim warningScript As String = "<script>alert('Harap isi semua data sebelum mengirim.');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "InputValidation", warningScript, False)
            Return
        End If

        Dim rawInput As String = txtTotalCost.Text.Replace(".", "").Replace(",", "").Replace("Rp", "").Trim()
        Dim totalCost As Integer
        If Not Integer.TryParse(rawInput, totalCost) Then
            Dim errorScript As String = "<script>alert('Total biaya tidak valid.');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "InvalidCost", errorScript, False)
            Return
        End If

        Dim result As Integer
        If HitungReimbursement(result) Then
            Dim category As String = ddlReimbursementCategory.SelectedItem.Text
            Dim selectedDate As String = txtDate.Text
            Dim medicalDetail As String = txtMedicalDetail.Text

            ' Simpan data ke database dan ambil KdKlaim
            Dim isSuccess As Boolean = Pengajuan.AddNewRequest(
                Kategori:=category,
                TanggalPengobatan:=Date.Parse(selectedDate),
                TanggalPengajuan:=DateTime.Now,
                DetailPenyakit:=medicalDetail,
                Biaya:=result,
                Status_Terakhir:="Awaiting",
                NIP:="1234567890"
            )

            sendReqNotif(
                Kategori:=category,
                TanggalPengobatan:=Date.Parse(selectedDate),
                TanggalPengajuan:=DateTime.Now,
                DetailPenyakit:=medicalDetail,
                Biaya:=result,
                Status_Terakhir:="Awaiting"
            )

            If isSuccess Then
                ' Ambil KdKlaim terakhir yang disimpan
                Dim KdKlaim As Integer = GetLastInsertedKlaimId() ' Implementasikan fungsi ini untuk mendapatkan ID terakhir

                ' Baca isi file sebagai Byte array
                Dim kwitansi As Byte() = New Byte(fileKwitansi.PostedFile.ContentLength - 1) {}
                fileKwitansi.PostedFile.InputStream.Read(kwitansi, 0, fileKwitansi.PostedFile.ContentLength)

                Dim resep As Byte() = New Byte(fileResep.PostedFile.ContentLength - 1) {}
                fileResep.PostedFile.InputStream.Read(resep, 0, fileResep.PostedFile.ContentLength)

                Dim pendukung As Byte() = New Byte(filePendukung.PostedFile.ContentLength - 1) {}
                filePendukung.PostedFile.InputStream.Read(pendukung, 0, filePendukung.PostedFile.ContentLength)


                ' Simpan dokumen ke database
                Try
                    Pengajuan.SaveDocument(kwitansi, DateTime.Now, resep, pendukung, KdKlaim)
                Catch ex As Exception
                    Dim errorScript As String = $"<script>alert('Gagal menyimpan dokumen: {ex.Message}');</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "SaveError", errorScript, False)
                    Return
                End Try

                ' Set pesan sukses
                Session("SuccessMessage") = "Pengajuan berhasil disimpan."
                Response.Redirect(Request.RawUrl)
            Else
                Dim errorScript As String = "<script>alert('Gagal menyimpan pengajuan.');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "SaveError", errorScript, False)
                Return
            End If
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
            ConnDB.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                lastId = Convert.ToInt32(reader("KdKlaim"))
            End If
            reader.Close()
        Catch ex As Exception
            ' Tangani kesalahan jika diperlukan
        Finally
            ConnDB.Close()
            cmd.Dispose()
        End Try

        Return lastId
    End Function

    Private Sub gvLogHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLogHistory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim status As String = drv("Status").ToString().ToLower()
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            If lblStatus IsNot Nothing Then
                Select Case status
                    Case "on process"
                        lblStatus.CssClass = "text-[#f97316] font-semibold p-3"
                    Case "approved"
                        lblStatus.CssClass = "text-[#4ade80] font-semibold p-3"
                    Case "reject"
                        lblStatus.CssClass = "text-[#f87171] font-semibold p-3"
                    Case Else
                        lblStatus.CssClass = "p-3"
                End Select
                lblStatus.Text = drv("Status").ToString()
            End If
        End If
    End Sub

    'Private Sub btnAddNewRequest_Click(sender As Object, e As EventArgs) Handles btnAddNewRequest.Click

    'End Sub

    Protected Sub sendReqNotif(Kategori As String, TanggalPengobatan As Date, TanggalPengajuan As DateTime, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String)

        Dim nomor As String = "6281806038088"
        Dim waktuSekarang As String = TanggalPengajuan
        Dim pesanLengkap As String =
            $"[PENGAJUAN BARU REIMBURSEMENT]" & vbCrLf &
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
                lblStatus.Text = "Pesan dikirim!"
            End Using
        Catch ex As WebException
            Using reader As New StreamReader(ex.Response.GetResponseStream())
                Dim errorText As String = reader.ReadToEnd()
                lblStatus.Text = "Gagal mengirim pesan!"
            End Using
        End Try
    End Sub

    Private Sub BindLogHistoris()
        gvLogHistory.Visible = True
        gvLogHistory.DataSource = MrEmployee.SelectAllLogHistorisByNip
        gvLogHistory.DataBind()
    End Sub
End Class