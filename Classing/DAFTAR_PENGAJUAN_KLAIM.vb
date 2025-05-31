Imports System.Data.SqlClient

Namespace dataPengajuanKlaim
    Public Class DAFTAR_PENGAJUAN_KLAIM

        Public Function AddNewRequest(Kategori As String, TanggalPengobatan As Date, TanggalPengajuan As DateTime, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String, NIP As String) As Boolean
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
            cmd.Parameters.Add(New SqlParameter("@NIP", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, NIP))

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
        Public Function SaveDocument(kwitansi As Byte(), WaktuUpload As DateTime, resep As Byte(), pendukung As Byte(), KdKlaim As integer) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "DAFTAR_DOKUMEN_KLAIM_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB

            cmd.Parameters.Add("@FileKwitansi", SqlDbType.VarBinary).Value = kwitansi
            cmd.Parameters.Add("@WaktuUpload", SqlDbType.DateTime).Value = WaktuUpload
            cmd.Parameters.Add("@FileResep", SqlDbType.VarBinary).Value = resep
            cmd.Parameters.Add("@FilePendukung", SqlDbType.VarBinary).Value = pendukung
            cmd.Parameters.Add("@KdKlaim", SqlDbType.Int).Value = KdKlaim

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

        'SELECT DOCUMENT BY ID
        Public Function SelectDocument(KdDokumen As Integer) As Dictionary(Of String, Byte())
            Dim result As New Dictionary(Of String, Byte())()
            Dim cmd As New SqlCommand
            cmd.CommandText = "REQUEST_SELECT_DOKUMEN_KLAIM"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB

            cmd.Parameters.Add("@KdDokumen", SqlDbType.Int).Value = KdDokumen

            Try
                ConnDB.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    If Not reader.IsDBNull(0) Then
                        result("kwitansi") = CType(reader("FileKwitansi"), Byte())
                    End If
                    If Not reader.IsDBNull(1) Then
                        result("resep") = CType(reader("FileResep"), Byte())
                    End If
                    If Not reader.IsDBNull(2) Then
                        result("pendukung") = CType(reader("FilePendukung"), Byte())
                    End If
                End If
                reader.Close()
            Catch ex As Exception
            Finally
                ConnDB.Close()
                cmd.Dispose()
                cmd = Nothing
            End Try

            Return result
        End Function


    End Class
End Namespace
