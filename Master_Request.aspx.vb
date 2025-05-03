Public Class Master_Request
    Inherits System.Web.UI.Page
    Dim simpan As New DataReq.Data_Dashboard

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "simpan" Then
            If simpan.RequestInsert(Nama.Text, Tanggal.Text, Kategori.SelectedValue, Status.Text) = True Then
            End If
        End If
    End Sub
End Class