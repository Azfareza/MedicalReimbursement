Imports System.Data.SqlClient

Namespace dataPengajuanKlaim
    Public Class DAFTAR_PENGAJUAN_KLAIM

        Public Function AddNewRequest(Kategori As String, TanggalPengobatan As Date, TanggalPengajuan As DateTime, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "DAFTAR_PENGAJUAN_KLAIM_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB

            cmd.Parameters.Add(New SqlParameter("@Kategori", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Kategori))
            cmd.Parameters.Add(New SqlParameter("@TanggalPengobatan", SqlDbType.Date, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, TanggalPengobatan))
            cmd.Parameters.Add(New SqlParameter("@TanggalPengajuan", SqlDbType.DateTime, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, TanggalPengajuan))
            cmd.Parameters.Add(New SqlParameter("@DetailPenyakit", SqlDbType.Char, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, DetailPenyakit))
            cmd.Parameters.Add(New SqlParameter("@Biaya", SqlDbType.Int, 32, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Biaya))
            cmd.Parameters.Add(New SqlParameter("@StatusTerakhir", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Status_Terakhir))
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

    End Class
End Namespace
