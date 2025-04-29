Public Class Employee_HR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnAddEmployee_Click(sender As Object, e As EventArgs) Handles btnAddEmployee.Click
        ViewState("ShowModal") = True
    End Sub

    Private Sub btnCloseModal_Click(sender As Object, e As EventArgs) Handles btnCloseModal.Click
        ViewState("ShowModal") = False
    End Sub

    Private Sub Employee_HR_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If ViewState("ShowModal") IsNot Nothing AndAlso CBool(ViewState("ShowModal")) Then
            pnlModal.CssClass = pnlModal.CssClass.Replace("hidden", "").Trim()
        Else
            If Not pnlModal.CssClass.Contains("hidden") Then
                pnlModal.CssClass += " hidden"
            End If
        End If
    End Sub

End Class