Imports System.Data.SqlClient
Namespace DAFTAR_PEGAWAI
    Public Class Pegawai
        Public Function SelectAllPegawai() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_ALL_PEGAWAI"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataUnProcessedHR")
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
        Public Function SelectPegawaiByNIP(NIP As String) As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_PEGAWAI_BY_NIP"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Comm.Parameters.Add(New SqlParameter("@NIP", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, NIP))
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataUnProcessedHR")
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

        Public Function SelectTanggunganByNik(NIK As String) As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "REQUEST_SELECT_TANGGUNGAN_BY_NIK"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Comm.Parameters.Add(New SqlParameter("@NIK", SqlDbType.Char, 16, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, NIK))
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataUnProcessedHR")
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
