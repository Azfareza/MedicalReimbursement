Public Class Dashboard_EMP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnMedicalReimbursement_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursement.Click
        Response.Redirect("MR_EMP.aspx")
    End Sub
End Class