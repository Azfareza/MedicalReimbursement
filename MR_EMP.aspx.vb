Imports System.IO
Imports System.Net

Public Class MR_EMP
    Inherits System.Web.UI.Page
    Dim MrEmployee As New DataReq.Data_Dashboard
    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindLogHistoris()
            txtMedicalDetail.Attributes("placeholder") = "Input Diagnosa Disini"

            'Sesi kalau Berhasil
            If Session("SuccessMessage") IsNot Nothing Then
                Dim alertScript As String = $"<script type='text/javascript'>alert('{Session("SuccessMessage").ToString().Replace("'", "\'")}');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
                Session.Remove("SuccessMessage")
            End If

            btnSubmit.Enabled = False
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
                ' Jika kategori tidak sesuai atau belum dipilih
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

        'VALIDASI KELENGKAPAN FORM
        If ddlReimbursementCategory.SelectedIndex = 0 OrElse
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

            Dim alertMessage As String = $"Category: {category}\nDate: {selectedDate}\nDetail: {medicalDetail}\nCalculated Amount: {result}"
            alertMessage = alertMessage.Replace("'", "\'").Replace(vbCrLf, "\n").Replace(vbLf, "\n")

            Session("SuccessMessage") = alertMessage

            ' Simpan data ke database
            Pengajuan.AddNewRequest(
                Kategori:=category,
                TanggalPengobatan:=Date.Parse(selectedDate),
                TanggalPengajuan:=DateTime.Now,
                DetailPenyakit:=medicalDetail,
                Biaya:=result,
                Status_Terakhir:="On Process"
            )
            sendReqNotif(
                Kategori:=category,
                TanggalPengobatan:=Date.Parse(selectedDate),
                TanggalPengajuan:=DateTime.Now,
                DetailPenyakit:=medicalDetail,
                Biaya:=result,
                Status_Terakhir:="On Process"
            )
            ' Redirect agar form tidak dipost ulang saat reload
            Response.Redirect(Request.RawUrl)
        End If
    End Sub

    ' Fungsi format Rupiah
    Private Function FormatRupiah(value As Integer) As String
        Return "Rp" & value.ToString("N0", New Globalization.CultureInfo("id-ID"))
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

        Dim nomor As String = "6285156909701"
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
        'Dim token As String = "zF8z5jBiZ5MBaXpP6q9N"

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

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Response.Redirect("Dashboard_EMP.aspx")
    End Sub

    Private Sub BindLogHistoris()
        gvLogHistory.Visible = True
        gvLogHistory.DataSource = MrEmployee.SelectAllLogHistorisByNip
        gvLogHistory.DataBind()
    End Sub
End Class