Public Class Dashboaed
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindRequest()
        End If
    End Sub

    Private Sub BindRequest()
        rptRequests.Visible = True
        rptRequests.DataSource = Datadashboard.SelectAllDahsboard
        rptRequests.DataBind()
    End Sub

    Private Sub btnMedicalReimbursement_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursement.Click
        Response.Redirect("MR_HR.aspx")
    End Sub

    Private Sub btnEmployees_Click(sender As Object, e As EventArgs) Handles btnEmployees.Click
        Response.Redirect("Employee_HR.aspx")
    End Sub
End Class