Imports System.Data.SqlClient
Imports System.Data
Imports MedicalReimbursement.DAFTAR_PEGAWAI

Public Class Employee_HR
    Inherits System.Web.UI.Page

    Dim DataPegawai As New DAFTAR_PEGAWAI.Pegawai

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else
                Dim userRole As String = Session("Role").ToString()
                Select Case userRole
                    Case "1", "4"
                        Response.Redirect("Login.aspx")
                    Case "2", "3"
                End Select
            End If
            BindAllPegawai()
        End If

        If hdnShowModal.Value = "true" Then
            pnlModal.CssClass = pnlModal.CssClass.Replace("hidden", "").Trim()
        Else
            pnlModal.CssClass = pnlModal.CssClass + " hidden"
            pnlModal.CssClass = pnlModal.CssClass.Replace("  ", " ").Trim()
        End If
    End Sub

    Private Sub BindAllPegawai()
        rptPegawai.Visible = True
        Dim dtPegawai As DataTable = DataPegawai.SelectAllPegawai()
        rptPegawai.DataSource = dtPegawai
        rptPegawai.DataBind()
    End Sub

    Protected Sub rptPegawai_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName = "View" Then
            Dim nip As String = e.CommandArgument.ToString()
            LoadModalPegawaiDetail(nip)
            hdnShowModal.Value = "true"
            upModal.Update()
        End If
    End Sub

    Private Sub LoadModalPegawaiDetail(nip As String)
        Dim dt As DataTable = DataPegawai.SelectPegawaiByNIP(nip)

        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)

            lblNIP.Text = row("NIP").ToString()
            lblNamaLengkap.Text = row("NamaLengkap").ToString()
            lblTempatLahir.Text = row("TempatLahir").ToString()
            lblTanggalLahir.Text = Convert.ToDateTime(row("TanggalLahir")).ToString("dd MMMM yyyy")
            lblJenisKelamin.Text = row("JenisKelamin").ToString()
            lblKebangsaan.Text = row("Kebangsaan").ToString()
            lblAgama.Text = row("Agama").ToString()
            lblNIK.Text = row("NIK").ToString()
            lblNPWP.Text = row("NPWP").ToString()
            lblSeluler.Text = row("Seluler").ToString()
            lblEmail.Text = row("Email").ToString()
            lblDepartemen.Text = row("NamaDepartemen").ToString()
            lblJabatan.Text = row("NamaJabatan").ToString()
            lblAlamat.Text = row("DetilAlamat").ToString()
            lblKecamatan.Text = row("Kecamatan").ToString()
            lblKelurahan.Text = row("Kelurahan").ToString()
            lblKota.Text = row("Kota").ToString()
            lblProvinsi.Text = row("Provinsi").ToString()

            Dim NIK = row("NIK").ToString()

            Dim dtTanggungan As DataTable = DataPegawai.SelectTanggunganByNik(NIK)
            rptTanggungan.DataSource = dtTanggungan
            rptTanggungan.DataBind()

        End If
    End Sub

    Protected Sub btnCloseModal_Click(sender As Object, e As EventArgs) Handles btnCloseModal.Click
        hdnShowModal.Value = "false"
        pnlModal.CssClass &= " hidden"
        upModal.Update()
    End Sub

End Class