Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page
    Dim LoginLogic As New LoginLogic.Login

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session.Clear()
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim nip As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If LoginLogic.ValidateUser(nip, password) Then
            ' Simpan session
            Session("NIP") = nip
            Session("Role") = LoginLogic.GetUserRole(nip)
            Session("Seluler") = LoginLogic.GetUserCell(nip)

            Dim userDetails As Dictionary(Of String, Object) = LoginLogic.GetUserDetail(nip)
            If userDetails IsNot Nothing Then
                If userDetails.ContainsKey("NIP") Then
                    Session("NIP") = userDetails("NIP").ToString()
                End If
                If userDetails.ContainsKey("Name") Then
                    Session("Name") = userDetails("Name").ToString()
                End If
                If userDetails.ContainsKey("Dept") Then
                    Session("Dept") = userDetails("Dept").ToString()
                End If
                If userDetails.ContainsKey("Jabatan") Then
                    Session("Jabatan") = userDetails("Jabatan").ToString()
                End If
                If userDetails.ContainsKey("Status") Then
                    Session("Status") = userDetails("Status").ToString()
                End If
                If userDetails.ContainsKey("Seluler") Then
                    Session("Seluler") = userDetails("Seluler").ToString()
                End If
            End If

            ' Redirect sesuai role
            Select Case Session("Role").ToString()
                Case "1"
                    Response.Redirect("Dashboard_Admin.aspx")
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
