Public Class MR_EMP
    Inherits System.Web.UI.Page
    Dim MrEmployee As New DataReq.Data_Dashboard

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindLogHistoris()
        End If
    End Sub

    Private Sub gvLogHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLogHistory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim status As String = drv("Status").ToString().ToLower()
            Dim lblStatus As Label = CType(e.Row.FindControl("lblStatus"), Label)
            If lblStatus IsNot Nothing Then
                Select Case status
                    Case "on process"
                        lblStatus.CssClass = "text-[#f97316] font-semibold p-3"
                    Case "approved"
                        lblStatus.CssClass = "text-[#4ade80] font-semibold p-3"
                    Case "reject"
                        lblStatus.CssClass = "text-[#f87171] font-semibold p-3"
                    Case Else
                        lblStatus.CssClass = "p-3"
                End Select
                lblStatus.Text = drv("Status").ToString()
            End If
        End If
    End Sub

    'Private Sub btnAddNewRequest_Click(sender As Object, e As EventArgs) Handles btnAddNewRequest.Click

    'End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Response.Redirect("Dashboard_EMP.aspx")
    End Sub

    Private Sub BindLogHistoris()
        gvLogHistory.Visible = True
        gvLogHistory.DataSource = MrEmployee.SelectAllLogHistorisByNip
        gvLogHistory.DataBind()
    End Sub
End Class