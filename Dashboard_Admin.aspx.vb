Public Class Dashboard_Admin
    Inherits System.Web.UI.Page

    Dim DataPegawai As New DAFTAR_PEGAWAI.Pegawai
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin
            ElseIf userRole = "2" Then ' Role 2: Direksi
                Response.Redirect("Login.aspx")
            ElseIf userRole = "3" Then ' Role 3: HR
                Response.Redirect("Login.aspx")
            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If
            BindAllPegawai()

            'Sesi kalau Berhasil
            If Session("SuccessMessage") IsNot Nothing Then
                Dim alertScript As String = $"<script type='text/javascript'>alert('{Session("SuccessMessage").ToString().Replace("'", "\'")}');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
                Session.Remove("SuccessMessage")
            End If
        End If
    End Sub

    Private Sub BindAllPegawai()
        rptPegawai.Visible = True
        Dim dtPegawai As DataTable = DataPegawai.SelectAllPegawai()
        rptPegawai.DataSource = dtPegawai
        rptPegawai.DataBind()
    End Sub

End Class