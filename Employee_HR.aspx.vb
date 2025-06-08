Public Class Employee_HR
    Inherits System.Web.UI.Page
    Dim DataEmployee As New DataReq.Data_Dashboard
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin
                Response.Redirect("Login.aspx")
            ElseIf userRole = "2" Then ' Role 2: Direksi

            ElseIf userRole = "3" Then ' Role 3: HR

            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If
            BindEmployee()
        End If
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

    Private Sub BindEmployee()
        gvEmployees.Visible = True
        gvEmployees.DataSource = DataEmployee.SelectAllPegawai
        gvEmployees.DataBind()
    End Sub

End Class