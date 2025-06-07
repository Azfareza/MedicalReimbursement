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
        Public Function SelectDocument(Kdklaim As Integer) As Dictionary(Of String, Byte())
            Dim result As New Dictionary(Of String, Byte())()
            Using cmd As New SqlCommand("DOKUMEN_KLAIM_SELECT_BY_ID", ConnDB)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@kdklaim", SqlDbType.Int).Value = Kdklaim

                Try
                    ConnDB.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not reader.IsDBNull(reader.GetOrdinal("FileKwitansi")) Then
                                result("kwitansi") = CType(reader("FileKwitansi"), Byte())
                            End If
                            If Not reader.IsDBNull(reader.GetOrdinal("FileResep")) Then
                                result("resep") = CType(reader("FileResep"), Byte())
                            End If
                            If Not reader.IsDBNull(reader.GetOrdinal("FilePendukung")) Then
                                result("pendukung") = CType(reader("FilePendukung"), Byte())
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    ' Tambahkan log error jika perlu
                Finally
                    ConnDB.Close()
                End Try
            End Using
            Return result
        End Function

        Public Function SummaryModal() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_SUMMARY_MODAL_HR"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataModalHR")
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

        Public Function SelectAllAwaiting() As Dictionary(Of String, Object)
            Dim userdetails As New Dictionary(Of String, Object)
            Dim connectionString As String = "data source=LOOSEFORDAYS\SQLEXPRESS; initial catalog=Medical; Persist Security Info=True;User ID=sa;Password=isal;Language=BRITISH ENGLISH;"
            Using con As New SqlConnection(connectionString)
                Using cmd As New SqlCommand("DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_BY_AWAITING", con)
                    cmd.CommandType = CommandType.StoredProcedure
                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            userdetails("NIP") = If(IsDBNull(reader("nip")), String.Empty, reader("nip").ToString())
                            userdetails("NamaLengkap") = If(IsDBNull(reader("namalengkap")), String.Empty, reader("namalengkap").ToString())
                            userdetails("NamaDepartemen") = If(IsDBNull(reader("NamaDepartemen")), String.Empty, reader("NamaDepartemen").ToString())
                            userdetails("Kategori") = If(IsDBNull(reader("Kategori")), String.Empty, reader("Kategori").ToString())
                            userdetails("TanggalPengajuan") = If(IsDBNull(reader("TanggalPengajuan")), String.Empty, reader("TanggalPengajuan").ToString())
                        End If
                    End Using
                End Using
            End Using
            Return userdetails
        End Function
        Public Function SelectAllUnProcessed() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_NOTPROCESSEDYET"
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
        Public Function SelectAllProcessed() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_KLAIM_STATUS_HISTORY"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("DataProcessedHR")
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
        Public Function RetrieveNipPegawai() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_NIP_PEGAWAI"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("RetrieveNip")
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

        Public Function RetrieveDepartemen() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_KODE_DEPARTEMEN"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("RetrieveDept")
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

        Public Function RetrieveKategori() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_KATEGORI_TANGGAL_PENGAJUAN"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Dim DA As SqlDataAdapter = New SqlDataAdapter(Comm)
            Dim DT As DataTable = New DataTable("RetrieveKategori")
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
