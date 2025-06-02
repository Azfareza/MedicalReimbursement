Imports System.Data.SqlClient
Imports System.Drawing.Drawing2D

Public Class MR_HR
    Inherits System.Web.UI.Page
    Dim DataMedicalReimburse As New DataReq.Data_Dashboard
    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM

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
        gvRequestList.DataSource = Pengajuan.SelectAllAwaiting()
        gvRequestList.DataBind()
    End Sub
    Private Sub BindgvHistory()
        gvHistory.Visible = True
        gvHistory.DataSource = Pengajuan.SelectAllProcessed()
        gvHistory.DataBind()
    End Sub

    Protected Sub gvHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim status As Object = DataBinder.Eval(e.Row.DataItem, "status_approval")
            Dim statusText As String = ""
            Dim targetCell As TableCell = e.Row.Cells(5)

            If Not IsDBNull(status) AndAlso Convert.ToBoolean(status) = True Then
                statusText = "Approved"
                targetCell.ForeColor = System.Drawing.Color.Green
                targetCell.Font.Bold = True
            Else
                statusText = "Rejected"
                targetCell.ForeColor = System.Drawing.Color.Red
                targetCell.Font.Bold = True
            End If
            e.Row.Cells(5).Text = statusText
        End If
    End Sub


    Private Sub gvRequestList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRequestList.SelectedIndexChanged
        ViewState("ShowModal") = True
        Dim PilihText As GridViewRow = gvRequestList.SelectedRow
        txtNIPModal.Text = Trim(PilihText.Cells(0).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(1).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(2).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(3).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(4).Text)

        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
    End Sub


    Private Sub gvHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvHistory.SelectedIndexChanged
        ViewState("ShowModal") = True

        Dim PilihText As GridViewRow = gvHistory.SelectedRow

        'DUMMY MANGGIL GAMBAR JPEG!

        Dim dokumen = Pengajuan.SelectDocument(KdDokumen:=7)

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

        txtNIPModal.Text = Trim(PilihText.Cells(0).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(1).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(2).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(3).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(4).Text)

        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
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

    'Private Function selectallwaiting(nip As String) As Dictionary(Of String, Object)
    '    Dim connectionString As String = "data source=LOOSEFORDAYS\SQLEXPRESS; initial catalog=Medical; Persist Security Info=True;User ID=sa;Password=isal;Language=BRITISH ENGLISH;"
    '    Dim userDetails As New Dictionary(Of String, Object)
    '    Using con As New SqlConnection(connectionString)
    '        Dim cmd As New SqlCommand("DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_BY_AWAITING", con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("@NIP", nip)
    '        con.Open()

    '        Using reader As SqlDataReader = cmd.ExecuteReader()
    '            If reader.Read() Then
    '                userDetails("NIP") = If(IsDBNull(reader("nip")), String.Empty, reader("nip").ToString())
    '                userDetails("NamaLengkap") = If(IsDBNull(reader("namalengkap")), String.Empty, reader("namalengkap").ToString())
    '                userDetails("NamaDepartemen") = If(IsDBNull(reader("NamaDepartemen")), String.Empty, reader("NamaDepartemen").ToString())
    '                userDetails("Kategori") = If(IsDBNull(reader("Kategori")), String.Empty, reader("Kategori").ToString())
    '                userDetails("TanggalPengajuan") = If(IsDBNull(reader("TanggalPengajuan")), String.Empty, reader("TanggalPengajuan").ToString())
    '            End If
    '        End Using
    '    End Using

    '    Return userDetails
    'End Function

    'Private Function GetNIP(nip As String) As String
    '    Dim connectionString As String = "data source=LOOSEFORDAYS\SQLEXPRESS; initial catalog=Medical; Persist Security Info=True;User ID=sa;Password=isal;Language=BRITISH ENGLISH;"
    '    Using con As New SqlConnection(connectionString)
    '        Dim query As String = "SELECT NIP FROM DAFTAR_PEGAWAI WHERE NIP = @nip"
    '        Using cmd As New SqlCommand(query, con)
    '            cmd.Parameters.AddWithValue("@nip", nip)
    '            con.Open()
    '            Dim result As Object = cmd.ExecuteScalar()
    '            If result IsNot Nothing Then
    '                Return result.ToString()
    '            End If
    '        End Using
    '    End Using
    '    Return Nothing
    'End Function

End Class

