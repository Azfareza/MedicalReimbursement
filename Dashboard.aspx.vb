Public Class Dashboaed
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Bindgv()
        End If
    End Sub

    Private Sub Bindgv()
        rptRequests.Visible = True
        rptRequests.DataSource = Datadashboard.SelectAllRequest
        rptRequests.DataBind()
    End Sub
End Class