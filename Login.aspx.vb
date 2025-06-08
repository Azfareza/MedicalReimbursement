Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim LoginLogic As New LoginLogic.Login

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Session.Clear()
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim nip As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If LoginLogic.ValidateUser(nip, password) Then
            ' Simpan session
            Session("NIP") = nip
            Session("Role") = LoginLogic.GetUserRole(nip)

            ' Redirect sesuai role
            Select Case Session("Role").ToString()
                Case "1"
                    Response.Redirect("Dashboard.aspx")
                Case "2"
                    Response.Redirect("Dashboard.aspx")
                Case "3"
                    Response.Redirect("Dashboard.aspx")
                Case "4"
                    Response.Redirect("Dashboard_EMP.aspx")
                Case Else
                    Response.Redirect("Login.aspx")
            End Select
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Username atau password salah');", True)
        End If
    End Sub
End Class
