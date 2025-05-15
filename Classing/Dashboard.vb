Imports System.Data.SqlClient

Namespace DataReq
    Public Class Data_Dashboard
        Public Function SelectAllPegawai() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_DATA_PEGAWAI"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataPegawai")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
                DA = Nothing
            End Try
        End Function


        Public Function RequestInsert(nama As String, tanggal As String, kategori As String, status As Boolean) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "REQUEST_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB
            cmd.Parameters.Add(New SqlParameter("@nama", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, nama))
            cmd.Parameters.Add(New SqlParameter("@tanggal", SqlDbType.Char, 20, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, tanggal))
            cmd.Parameters.Add(New SqlParameter("@kategori", SqlDbType.Char, 11, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, kategori))
            cmd.Parameters.Add(New SqlParameter("@status", SqlDbType.Char, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, status))
            Try
                ConnDB.Open()
                cmd.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
            Finally
                ConnDB.Close()
                cmd.Dispose()
                cmd = Nothing
            End Try
        End Function

        Public Function SelectAllAktif() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_ALL_AKTIF"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataReqAktif")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
                DA = Nothing
            End Try
        End Function
        Public Function SelectAllDahsboard() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_FOR_DASHBOARD"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataReqDashboard")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
                DA = Nothing
            End Try
        End Function
        Public Function SelectAllNonAktif() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_ALL_NonAktif"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataReqNonAktif")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
                DA = Nothing
            End Try
        End Function

        Public Function SelectAllLogHistorisByNip() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "SELECT_ALL_LOG_HISTORIS_PEGAWAI_BY_NIP"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataLogHistorisByNip")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
                DA = Nothing
            End Try
        End Function

    End Class
End Namespace
