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
        txtLevelModal.Text = Trim(PilihText.Cells(5).Text)

        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtLevelModal.Enabled = False
    End Sub


    Private Sub gvHistory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvHistory.SelectedIndexChanged
        ViewState("ShowModal") = True

        Dim PilihText As GridViewRow = gvHistory.SelectedRow

        txtNIPModal.Text = Trim(PilihText.Cells(0).Text)
        txtNamaModal.Text = Trim(PilihText.Cells(1).Text)
        txtDepartemenModal.Text = Trim(PilihText.Cells(2).Text)
        txtKategoriModal.Text = Trim(PilihText.Cells(3).Text)
        txtTanggalModal.Text = Trim(PilihText.Cells(4).Text)
        txtLevelModal.Text = Trim(PilihText.Cells(5).Text)

        txtNIPModal.Enabled = False
        txtNamaModal.Enabled = False
        txtDepartemenModal.Enabled = False
        txtKategoriModal.Enabled = False
        txtTanggalModal.Enabled = False
        txtLevelModal.Enabled = False
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
End Class