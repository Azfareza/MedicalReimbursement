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
    End Class
End Namespace
