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
        Labeldir.Visible = False
        Labelhr.Visible = False
        Labelemp.Visible = False

        FullNameAdmin.Visible = False
        FullNameDIR.Visible = False
        FullNameHR.Visible = False
        FullNameEMP.Visible = False

        ' Ambil nama user dari session
        Dim namaLengkap As String = If(Session("Name") IsNot Nothing, Session("Name").ToString(), "User")

        ' Highlight active menu berdasarkan Session
        Dim activeMenu As String = If(Session("ActiveMenu"), "").ToString()
        HighlightActiveMenu(activeMenu)

        Select Case activeMenu
            Case "Dashboard_Admin"
                btnDashboardAdmin.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "Dashboard_DIR"
                btnDashboardDir.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "Dashboard"
                btnDashboardHr.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "Dashboard_EMP"
                btnDashboardEmp.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "MR_DIREKSI"
                btnMedicalReimbursementDir.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "MR_HR"
                btnMedicalReimbursementHr.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "MR_EMP"
                btnMedicalReimbursementEmp.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            Case "Employee_HR"
                btnEmployeeDir.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
                btnEmployeeHr.CssClass = btnDashboardHr.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
        End Select

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
                Labeldir.Visible = True
                Labeldir.Text = "DIREKSI"
                FullNameDIR.Visible = True
                FullNameDIR.Text = namaLengkap

            Case "3" ' HR
                hrpanel.Visible = True
                Labelhr.Visible = True
                Labelhr.Text = "HR"
                FullNameHR.Visible = True
                FullNameHR.Text = namaLengkap

            Case "4" ' Pegawai
                emppanel.Visible = True
                Labelemp.Visible = True
                Labelemp.Text = "Employee"
                FullNameEMP.Visible = True
                FullNameEMP.Text = namaLengkap

            Case Else
                Response.Redirect("Login.aspx")
        End Select
    End Sub

    Private _activeMenu As String

    Public Property ActiveMenu As String
        Get
            Return _activeMenu
        End Get
        Set(value As String)
            _activeMenu = value
        End Set
    End Property

    Private Sub HighlightActiveMenu(ByVal activeMenu As String)
        Dim allButtons As New Dictionary(Of String, Button) From {
        {"DashboardAdmin", btnDashboardAdmin},
        {"DashboardDir", btnDashboardDir},
        {"Dashboard", btnDashboardHr},
        {"DashboardEmp", btnDashboardEmp},
        {"MedicalReimbursementDir", btnMedicalReimbursementDir},
        {"MedicalReimbursementHr", btnMedicalReimbursementHr},
        {"MedicalReimbursementEmp", btnMedicalReimbursementEmp},
        {"EmployeeDir", btnEmployeeDir},
        {"EmployeeHr", btnEmployeeHr}
    }

        ' Reset semua tombol
        For Each btn In allButtons.Values
            btn.CssClass = btn.CssClass.Replace("bg-[#FFAA0A]", "bg-[#145445]").Replace("ring-2 ring-white", "").Trim()
        Next

        ' Tambahkan highlight ke tombol yang aktif
        If allButtons.ContainsKey(activeMenu) Then
            Dim targetBtn = allButtons(activeMenu)
            targetBtn.CssClass = targetBtn.CssClass.Replace("bg-[#145445]", "bg-[#FFAA0A]").Trim()
            If Not targetBtn.CssClass.Contains("ring-2") Then
                targetBtn.CssClass &= " ring-2 ring-white"
            End If
        End If
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