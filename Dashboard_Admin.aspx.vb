Imports System.Threading.Tasks
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient ' Tambahkan ini untuk SqlCommand, SqlConnection jika belum ada
Imports System.Data ' Tambahkan ini untuk DataTable, DataRowVersion, SqlDbType

Partial Public Class Dashboard_Admin
    Inherits System.Web.UI.Page

    Dim DataPegawai As New DAFTAR_PEGAWAI.Pegawai
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin
            ElseIf userRole = "2" Then ' Role 2: Direksi
                Response.Redirect("Login.aspx")
            ElseIf userRole = "3" Then ' Role 3: HR
                Response.Redirect("Login.aspx")
            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If

            Session("ActiveMenu") = "Dashboard_Admin"

            BindAllPegawai()
            BindRolesToDropdown()

            If Session("SuccessMessage") IsNot Nothing Then
                Dim alertScript As String = $"<script type='text/javascript'>alert('{Session("SuccessMessage").ToString().Replace("'", "\'")}');</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "ShowAlert", alertScript, False)
                Session.Remove("SuccessMessage")
            End If
        End If
    End Sub

    Private Sub BindAllPegawai()
        rptPegawai.Visible = True
        Dim dtPegawai As DataTable = DataPegawai.SelectAllPegawai()
        rptPegawai.DataSource = dtPegawai
        rptPegawai.DataBind()
    End Sub

    Private Sub BindRolesToDropdown()
        Dim dtRoles As New DataTable()
        dtRoles.Columns.Add("RoleID", GetType(String))
        dtRoles.Columns.Add("RoleName", GetType(String))

        dtRoles.Rows.Add("1", "Admin")
        dtRoles.Rows.Add("2", "Direksi")
        dtRoles.Rows.Add("3", "HR")
        dtRoles.Rows.Add("4", "User")

        ddlNewRole.DataSource = dtRoles
        ddlNewRole.DataTextField = "RoleName"
        ddlNewRole.DataValueField = "RoleID"
        ddlNewRole.DataBind()
    End Sub

    Protected Sub rptPegawai_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptPegawai.ItemCommand
        If e.CommandName = "Edit" Then
            Dim nip As String = e.CommandArgument.ToString()

            Dim lblRole As Label = CType(e.Item.FindControl("lblRole"), Label)
            Dim currentRole As String = ""
            If lblRole IsNot Nothing Then
                currentRole = lblRole.Text
            End If

            lblEditNIP.Text = nip

            Try
                ddlNewRole.SelectedValue = currentRole
            Catch ex As Exception
                If ddlNewRole.Items.Count > 0 Then
                    ddlNewRole.SelectedIndex = 0
                End If
            End Try
            pnlEditModal.Visible = True
            upModal.Update()
        End If
    End Sub

    Protected Sub btnSaveEdit_Click(sender As Object, e As EventArgs) Handles btnSaveEdit.Click
        Dim selectedNIP As String = lblEditNIP.Text
        Dim newRole As String = ddlNewRole.SelectedValue

        Dim updateSuccess As Boolean = False
        Try
            updateSuccess = RoleUpdater(selectedNIP, newRole)

            If updateSuccess Then
                Session("SuccessMessage") = $"Role untuk NIP {selectedNIP} berhasil diperbarui menjadi {newRole}!"
            Else
                Session("SuccessMessage") = $"Gagal memperbarui Role untuk NIP {selectedNIP}."
            End If
        Catch ex As Exception
            Session("SuccessMessage") = $"Terjadi kesalahan saat memperbarui Role: {ex.Message}"
        End Try

        pnlEditModal.Visible = False
        BindAllPegawai()
        upModal.Update()
        Response.Redirect(Request.RawUrl)
    End Sub

    Public Function RoleUpdater(NIP As String, Role As String) As Boolean
        Dim cmd As New SqlCommand
        cmd.CommandText = "DTA_ROLE_UPDATE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = ConnDB
        cmd.Parameters.Add(New SqlParameter("@NIP", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, NIP))
        cmd.Parameters.Add(New SqlParameter("@Role", SqlDbType.NChar, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Role))

        Try
            'ConnDB.Open()
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            'ConnDB.Close()
            cmd.Dispose()
            cmd = Nothing
        End Try
    End Function

    Protected Sub btnCloseModal_Click(sender As Object, e As EventArgs) Handles btnCloseModal.Click
        pnlEditModal.Visible = False
        upModal.Update()
    End Sub

End Class