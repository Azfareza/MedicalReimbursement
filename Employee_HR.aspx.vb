Imports System.Data.SqlClient
Imports System.Data
Imports MedicalReimbursement.DAFTAR_PEGAWAI

Public Class Employee_HR
    Inherits System.Web.UI.Page

    Dim DataPegawai As New DAFTAR_PEGAWAI.Pegawai
    Dim DataTanggungan As New DAFTAR_TANGGUNGAN.MASTER_TANGGUNGAN

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack AndAlso Not IsCallback Then
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
            hdnShowAddModal.Value = "false"
            hdnShowModal.Value = "false"

            Session("ActiveMenu") = "Employee_HR"

            BindAllPegawai()
        End If

        If hdnShowAddModal.Value = "true" Then
            pnlModalAdd.CssClass = pnlModalAdd.CssClass.Replace("hidden", "").Trim()
        Else
            pnlModalAdd.CssClass &= " hidden"
        End If

        If hdnShowModal.Value = "true" Then
            pnlModal.CssClass = pnlModal.CssClass.Replace("hidden", "").Trim()
        Else
            pnlModal.CssClass = pnlModal.CssClass + " hidden"
            pnlModal.CssClass = pnlModal.CssClass.Replace("  ", " ").Trim()
        End If
    End Sub

    Protected Sub btnTambahPegawai_Click(sender As Object, e As EventArgs) Handles btnTambahPegawai.Click
        pnlModalAdd.CssClass = pnlModalAdd.CssClass.Replace("hidden", "").Trim()
        hdnShowAddModal.Value = "true"
        upModalAdd.Update()
    End Sub

    Protected Sub btnBatalTambah_Click(sender As Object, e As EventArgs) Handles btnBatalTambah.Click
        pnlModalAdd.CssClass &= " hidden"
        hdnShowAddModal.Value = "false"
        upModalAdd.Update()
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

    Protected Sub btnSimpanPegawai_Click(sender As Object, e As EventArgs) Handles btnSimpanPegawai.Click
        Try
            Dim nip As String = txtNIP.Text.Trim()
            Dim namaLengkap As String = txtNamaLengkap.Text.Trim()
            Dim tempatLahir As String = txtTempatLahir.Text.Trim()
            Dim tanggalLahir As Date = Convert.ToDateTime(txtTanggalLahir.Text)
            Dim jenisKelamin As String = ddlJenisKelamin.SelectedValue
            Dim kebangsaan As String = txtKebangsaan.Text.Trim()
            Dim agama As String = txtAgama.Text.Trim()
            Dim nik As String = txtNIK.Text.Trim()
            Dim npwp As String = txtNPWP.Text.Trim()
            Dim seluler As String = txtSeluler.Text.Trim()
            Dim email As String = txtEmail.Text.Trim()
            Dim password As String = txtPassword.Text.Trim()
            Dim role As Integer = 4

            Dim provinsi As String = txtProvinsi.Text.Trim()
            Dim kota As String = txtKota.Text.Trim()
            Dim kecamatan As String = txtKecamatan.Text.Trim()
            Dim kelurahan As String = txtKelurahan.Text.Trim()
            Dim detilAlamat As String = txtDetilAlamat.Text.Trim()
            Dim kodePos As Integer = If(IsNumeric(txtKodePos.Text), CInt(txtKodePos.Text), 0)
            Dim aktifAlamat As Boolean = True

            Dim kdDept As String = txtKdDept.Text.Trim()
            Dim kdJabatan As String = txtKdJabatan.Text.Trim()
            Dim tglMulai As Date = Convert.ToDateTime(txtTanggalMulai.Text)
            Dim statusMutasi As String = DropDownList1.SelectedValue
            Dim catatan As String = txtCatatan.Text.Trim()
            Dim aktifMutasi As Boolean = True

            Dim insertedPegawai = DataPegawai.InsertPegawai(nip, nik, npwp, namaLengkap, tempatLahir, tanggalLahir, jenisKelamin, kebangsaan, seluler, email, agama, kdDept, password, role)
            Dim insertedAlamat = DataPegawai.InsertAlamatPegawai(nip, provinsi, kota, kecamatan, kelurahan, detilAlamat, kodePos, aktifAlamat)
            Dim insertedMutasi = DataPegawai.InsertMutasiPegawai(nip, kdDept, kdJabatan, tglMulai, statusMutasi, catatan, Nothing, aktifMutasi)

            If insertedPegawai AndAlso insertedAlamat AndAlso insertedMutasi Then
                Session("SuccessMessage") = "Data berhasil ditambahkan"
                hdnShowAddModal.Value = "false"
                pnlModalAdd.CssClass &= " hidden"
                BindAllPegawai()
                upPegawaiTable.Update()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "HideModalAdd", "$('#" & pnlModalAdd.ClientID & "').addClass('hidden'); $('#" & hdnShowAddModal.ClientID & "').val('false');", True)
                Response.Redirect(Request.RawUrl)
            Else
                Dim alertScript As String = "<script type='text/javascript'>alert('GAGAL!');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
                upModalAdd.Update()
            End If

        Catch ex As Exception
            Dim alertScript As String = "<script type='text/javascript'>alert('GAGAL: " & ex.Message & "');</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
            upModalAdd.Update()
        End Try
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
            Dim dtTanggungan As DataTable = DataTanggungan.SelectTanggunganByNik(NIK)
            rptTanggungan.DataSource = dtTanggungan
            rptTanggungan.DataBind()
        End If
    End Sub

    Protected Sub btnShowFormTanggungan_Click(sender As Object, e As EventArgs)
        pnlFormTanggungan.CssClass = Replace(pnlFormTanggungan.CssClass, "hidden", "").Trim()
    End Sub

    Protected Sub btnCancelTanggungan_Click(sender As Object, e As EventArgs)
        pnlFormTanggungan.CssClass &= " hidden"
    End Sub

    Protected Sub btnSubmitTanggungan_Click(sender As Object, e As EventArgs)
        Dim result As Boolean = DataTanggungan.InsertTanggungan(
        lblNIK.Text.Trim(),
        txtNpwpTgn.Text.Trim(),
        txtNamaLengkapTgn.Text.Trim(),
        txtTempatLahirTgn.Text.Trim(),
        Convert.ToDateTime(txtTanggalLahirTgn.Text),
        txtJenisKelaminTgn.Text.Trim(),
        txtPekerjaanTgn.Text.Trim(),
        txtHubunganTgn.Text.Trim()
    )

        If result Then
            DataTanggungan.SelectTanggunganByNik(lblNIK.Text)
            txtNpwpTgn.Text = ""
            txtNamaLengkapTgn.Text = ""
            txtTempatLahirTgn.Text = ""
            txtTanggalLahirTgn.Text = ""
            txtJenisKelaminTgn.Text = ""
            txtPekerjaanTgn.Text = ""
            txtHubunganTgn.Text = ""
            pnlFormTanggungan.CssClass &= " hidden"
        Else
        End If
    End Sub



    Protected Sub btnCloseModal_Click(sender As Object, e As EventArgs) Handles btnCloseModal.Click
        hdnShowModal.Value = "false"
        pnlModal.CssClass &= " hidden"
        upModal.Update()
    End Sub

End Class