Public Class Sidebar
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Redirect jika tidak ada sesi login
        If Session("Role") Is Nothing OrElse Trim(Session("Role").ToString()) = "" Then
            Response.Redirect("Login.aspx")
            Return
        End If

        Dim userRole As String = Session("Role").ToString()

        ' Reset semua panel dan label ke default
        Adminpanel.Visible = False
        Dirpanel.Visible = False
        hrpanel.Visible = False
        emppanel.Visible = False

        Labeladmin.Visible = False

        FullNameAdmin.Visible = False
        FullNameDIR.Visible = False
        FullNameHR.Visible = False
        FullNameEMP.Visible = False

        ' Ambil nama user dari session
        Dim namaLengkap As String = If(Session("Name") IsNot Nothing, Session("Name").ToString(), "User")

        ' Tampilkan sesuai role
        Select Case userRole
            Case "1" ' Admin
                Adminpanel.Visible = True
                Labeladmin.Visible = True
                Labeladmin.Text = "Admin!"
                FullNameAdmin.Visible = True
                FullNameAdmin.Text = namaLengkap

            Case "2" ' Direksi
                Dirpanel.Visible = True
                FullNameDIR.Visible = True
                FullNameDIR.Text = namaLengkap

            Case "3" ' HR
                hrpanel.Visible = True
                FullNameHR.Visible = True
                FullNameHR.Text = namaLengkap

            Case "4" ' Pegawai
                emppanel.Visible = True
                FullNameEMP.Visible = True
                FullNameEMP.Text = namaLengkap

            Case Else
                Response.Redirect("Login.aspx")
        End Select
    End Sub

    Private Sub btnDashboardAdmin_Click(sender As Object, e As EventArgs) Handles btnDashboardAdmin.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnDashboardDir_Click(sender As Object, e As EventArgs) Handles btnDashboardDir.Click
        Response.Redirect("Dashboard_DIR.aspx")
    End Sub

    Private Sub btnDashboardHr_Click(sender As Object, e As EventArgs) Handles btnDashboardHr.Click
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Sub btnDashboardEmp_Click(sender As Object, e As EventArgs) Handles btnDashboardEmp.Click
        Response.Redirect("Dashboard_EMP.aspx")
    End Sub

    Private Sub btnMedicalReimbursementDir_Click(sender As Object, e As EventArgs) Handles btnMedicalReimbursementDir.Click
        Response.Redirect("MR_DIREKSI.aspx")
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