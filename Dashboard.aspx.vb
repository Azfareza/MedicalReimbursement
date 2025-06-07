Imports MedicalReimbursement.dataPengajuanKlaim

Public Class Dashboard
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard
    Dim pengajuan As New DAFTAR_PENGAJUAN_KLAIM


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindRequest()
        End If
    End Sub

    Private Sub BindRequest()
        rptRequests.Visible = True
        rptRequests.DataSource = pengajuan.SelectAllUnProcessed
        rptRequests.DataBind()
    End Sub

    Private Sub btnMedicalReimbursement_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursement.Click
        Response.Redirect("MR_HR.aspx")
    End Sub

    Private Sub btnEmployees_Click(sender As Object, e As EventArgs) Handles btnEmployees.Click
        Response.Redirect("Employee_HR.aspx")
    End Sub
End Class