﻿Public Class Dashboard_EMP
    Inherits System.Web.UI.Page

    Dim Pengajuan As New dataPengajuanKlaim.DAFTAR_PENGAJUAN_KLAIM

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        NIPLabel.Text = Session("NIP").ToString()
        FullNameLabel.Text = Session("Name").ToString()
        DepartementLabel.Text = Session("Dept").ToString()
        JabatanLabel.Text = Session("Jabatan").ToString()
        StatusLabel.Text = Session("Status").ToString
        selulerlabel.Text = Session("Seluler").ToString
    End Sub

    Private Sub BindLogHistoris()
        rptRequests.Visible = True
        Dim dtLogHistory As DataTable = Pengajuan.SelectAllLogHistorisByNip(Session("NIP"))
        rptRequests.DataSource = dtLogHistory
        rptRequests.DataBind()
    End Sub

End Class