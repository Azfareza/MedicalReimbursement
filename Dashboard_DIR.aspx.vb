Imports MedicalReimbursement.dataPengajuanKlaim

Public Class Dashboard_DIR
    Inherits System.Web.UI.Page
    Dim Datadashboard As New DataReq.Data_Dashboard
    Dim pengajuan As New DAFTAR_PENGAJUAN_KLAIM


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(Session("Role")) = "" Then
                Response.Redirect("Login.aspx")
            Else

            End If
            Dim userRole As String = Session("Role").ToString()
            If userRole = "1" Then ' Role 1: Admin

            ElseIf userRole = "2" Then ' Role 2: Direksi

            ElseIf userRole = "3" Then ' Role 3: HR

            ElseIf userRole = "4" Then ' Role 4: USER
                Response.Redirect("Login.aspx")
            End If

            Session("ActiveMenu") = "Dashboard_DIR"

            BindRequest()
            Dim statusData = GetStatusChartData()
            Dim kategoriData = GetKategoriChartData()

            Dim script As String = $"
    renderCharts({statusData}, {kategoriData});
"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "renderCharts", script, True)
        End If

    End Sub

    Private Sub BindRequest()
        rptRequests.Visible = True
        rptRequests.DataSource = pengajuan.SelectAllOnProcess
        rptRequests.DataBind()
    End Sub

    Private Function GetStatusChartData() As String
        Dim dt As DataTable = pengajuan.GetStatusSummary()
        Dim processed = dt.Select("status_terakhir = 'Approved'").Length
        Dim unprocessed = dt.Select("status_terakhir = 'On Process'").Length
        Return $"[{processed},{unprocessed}]"
    End Function

    Private Function GetKategoriChartData() As String
        Dim dt As DataTable = pengajuan.GetKategoriSummary()
        Dim rawatJalan = dt.Select("kategori = 'Rawat Jalan'").Length
        Dim kacamata = dt.Select("kategori = 'Kacamata'").Length
        Dim persalinan = dt.Select("kategori = 'Persalinan'").Length
        Return $"[{rawatJalan},{kacamata},{persalinan}]"
    End Function
End Class