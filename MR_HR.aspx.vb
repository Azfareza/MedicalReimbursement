Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Data

Public Class MR_HR
    Inherits System.Web.UI.Page
    Dim DataMedicalReimburse As New DataReq.Data_Dashboard
    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM
    Dim KlaimUpdating As New KLAIM_UPDATE

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindgvReqList()
            BindgvHistory()
        End If
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnEmployees_Click(sender As Object, e As EventArgs) Handles btnEmployees.Click
        Response.Redirect("Employee_HR.aspx")
    End Sub

    Private Sub BindNipPegawai()
        Dim dt As DataTable = Pengajuan.RetrieveNipPegawai()

        For Each row As DataRow In dt.Rows
            Dim list As New ListItem()
            list.Text = row("nip").ToString()
        Next

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
        pnlModal.Visible = True
        Dim PilihText As GridViewRow = gvRequestList.SelectedRow

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
                imgKwitansi.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("kwitansi"))
            End If
            If dokumen.ContainsKey("resep") Then
                imgResep.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("resep"))
            End If
            If dokumen.ContainsKey("pendukung") Then
                imgPendukung.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("pendukung"))
            End If
        End If


    End Sub


    Private Sub gvHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvHistory.SelectedIndexChanged
        ViewState("ShowModal") = True

        Dim PilihText As GridViewRow = gvHistory.SelectedRow

        'DUMMY MANGGIL GAMBAR JPEG!

        'Dim dokumen = Pengajuan.SelectDocument(KdDokumen:=7)

        'If dokumen IsNot Nothing Then
        '    If dokumen.ContainsKey("kwitansi") Then
        '        imgKwitansi.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("kwitansi"))
        '    End If
        '    If dokumen.ContainsKey("resep") Then
        '        imgResep.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("resep"))
        '    End If
        '    If dokumen.ContainsKey("pendukung") Then
        '        imgPendukung.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(dokumen("pendukung"))
        '    End If
        'End If

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
        If KlaimUpdating.KlaimUpdater(txtClaim.Text, "Approved") = True Then
            pnlModal.Visible = False
            BindgvReqList()
            BindgvHistory()
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub btntidaksetuju_Click(sender As Object, e As EventArgs) Handles btntidaksetuju.Click
        If KlaimUpdating.KlaimUpdater(txtClaim.Text, "Rejected") = True Then
            pnlModal.Visible = False
            BindgvReqList()
            BindgvHistory()
        Else
            MsgBox("ALERT", MsgBoxStyle.Critical)
        End If
    End Sub
End Class

