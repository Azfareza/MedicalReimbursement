Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        ' Contoh validasi login (ganti sesuai database/logic kamu)
        If username = "admin" AndAlso password = "1234" Then
            Response.Redirect("home.aspx") ' redirect setelah login sukses
        Else
            ' Menampilkan alert jika login gagal
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Username atau password salah');", True)
        End If
    End Sub

End Class