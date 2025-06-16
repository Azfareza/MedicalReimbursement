Imports MedicalReimbursement.dataPengajuanKlaim

Public Class Dashboard_DIR
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard
    Dim pengajuan As New DAFTAR_PENGAJUAN_KLAIM


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin

            ElseIf userRole = "2" Then ' Role 2: Direksi

            ElseIf userRole = "3" Then ' Role 3: HR

            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If
            BindRequest()
        End If

    End Sub

    Private Sub BindRequest()
        rptRequests.Visible = True
        rptRequests.DataSource = pengajuan.SelectAllOnProcess
        rptRequests.DataBind()
    End Sub
End Class