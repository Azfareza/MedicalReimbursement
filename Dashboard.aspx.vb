Public Class Dashboard
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindRequest()
        End If
    End Sub

    Public Function GetStatusText(ByVal status As Object) As String
        If status Is Nothing OrElse Convert.IsDBNull(status) Then
            Return "On Progress"
        ElseIf Convert.ToBoolean(status) Then
            Return "Approved"
        Else
            Return "Rejected"
        End If
    End Function

    Public Function GetStatusColor(ByVal status As Object) As String
        Return If(status Is Nothing OrElse Convert.IsDBNull(status), "#878787", If(Convert.ToBoolean(status), "#43AD96", "#EE5C5F"))
    End Function

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