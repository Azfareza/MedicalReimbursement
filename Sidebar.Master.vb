Public Class Sidebar
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Trim(Session("Role")) = "" Then
            Response.Redirect("Login.aspx")
        Else

        End If
        Dim userRole As String = Session("Role").ToString()
        If userRole = "1" Then ' Role 1: Admin
            hrpanel.Visible = False
            Labeladmin.Visible = True
            Labeladmin.Text = "Hallo Admin!"
            'NameLabelAdmin.Visible = True
            'NameLabelAdmin.Text = Session("Name").ToString()
        ElseIf userRole = "2" Then ' Role 2: Direksi
            Dirpanel.Visible = True
            Labeldir.Visible = True
            Labeldir.Text = "DIREKSI"
            ' NameLabelDir.Visible = True
            ' NameLabelDir.Text = Session("Name").ToString()
        ElseIf userRole = "3" Then ' Role 3: HR
            hrpanel.Visible = True
            Labelhr.Visible = True
            Labelhr.Text = "HR"
            ' NameLabelHr.Visible = True
            ' NameLabelHr.Text = Session("Name").ToString()
        ElseIf userRole = "4" Then 'role 4 : pegawai
            emppanel.Visible = True
            Labelemp.Visible = True
            Labelemp.Text = "HR"
            ' NameLabelEmp.Visible = True
            'NameLabelEmp.Text = Session("Name").ToString()
        End If


    End Sub



    Private Sub btnDashboardAdmin_Click(sender As Object, e As EventArgs) Handles btnDashboardAdmin.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnDashboardDir_Click(sender As Object, e As EventArgs) Handles btnDashboardDir.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnDashboardHr_Click(sender As Object, e As EventArgs) Handles btnDashboardHr.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnDashboardEmp_Click(sender As Object, e As EventArgs) Handles btnDashboardEmp.Click
        Response.Redirect("Dashboard_EMP.aspx")
    End Sub

    Private Sub btnMedicalReimbursementDir_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursementDir.Click
        Response.Redirect("MR_HR.aspx")
    End Sub

    Private Sub btnMedicalReimbursementHr_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursementHr.Click
        Response.Redirect("MR_HR.aspx")
    End Sub

    Private Sub btnMedicalReimbursementEmp_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursementEmp.Click
        Response.Redirect("MR_EMP.aspx")
    End Sub

    Private Sub btnEmployeeDir_Click(sender As Object, e As EventArgs) Handles btnEmployeeDir.Click
        Response.Redirect("Employee_HR.aspx")
    End Sub

    Private Sub btnEmployeeHr_Click(sender As Object, e As EventArgs) Handles btnEmployeeHr.Click
        Response.Redirect("Employee_HR.aspx")
    End Sub

    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        Response.Redirect("Login.aspx")
    End Sub
End Class