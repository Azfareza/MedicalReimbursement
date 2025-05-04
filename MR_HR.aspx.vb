Public Class MR_HR
    Inherits System.Web.UI.Page
    Dim DataMedicalReimburse As New DataReq.Data_Dashboard

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

    Private Sub BindgvReqList()
        gvRequestList.Visible = True
        gvRequestList.DataSource = DataMedicalReimburse.SelectAllAktif
        gvRequestList.DataBind()
    End Sub
    Private Sub BindgvHistory()
        gvHistory.Visible = True
        gvHistory.DataSource = DataMedicalReimburse.SelectAllNonAktif
        gvHistory.DataBind()
    End Sub

End Class