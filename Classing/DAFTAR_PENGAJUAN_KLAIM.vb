﻿Imports System.Data.SqlClient

Namespace dataPengajuanKlaim
    Public Class DAFTAR_PENGAJUAN_KLAIM

        Public Function AddNewRequest(Kategori As String, TanggalPengobatan As Date, TanggalPengajuan As DateTime, DetailPenyakit As String, Biaya As Integer, Status_Terakhir As String, NIP As String) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "DAFTAR_PENGAJUAN_KLAIM_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB

            cmd.Parameters.Add(New SqlParameter("@Kategori", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Kategori))
            cmd.Parameters.Add(New SqlParameter("@TanggalPengobatan", SqlDbType.Date, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, TanggalPengobatan))
            cmd.Parameters.Add(New SqlParameter("@TanggalPengajuan", SqlDbType.DateTime, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, TanggalPengajuan))
            cmd.Parameters.Add(New SqlParameter("@DetailPenyakit", SqlDbType.Char, 30, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, DetailPenyakit))
            cmd.Parameters.Add(New SqlParameter("@Biaya", SqlDbType.Int, 32, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Biaya))
            cmd.Parameters.Add(New SqlParameter("@StatusTerakhir", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Status_Terakhir))
            cmd.Parameters.Add(New SqlParameter("@NIP", SqlDbType.Char, 10, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, NIP))

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

        Public Function KlaimUpdater(KdKlaim As Int32, StatusTerakhir As String) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "DTA_KLAIM_STATUS_UPDATE"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB
            cmd.Parameters.Add(New SqlParameter("@kdklaim", SqlDbType.Int, 9, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, KdKlaim))
            cmd.Parameters.Add(New SqlParameter("@status", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, StatusTerakhir))
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

        Public Function SaveDocument(kwitansi As Byte(), WaktuUpload As DateTime, resep As Byte(), pendukung As Byte(), KdKlaim As Integer) As Boolean
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

        Public Function InsertHistoryStatus(Status As String, Waktu As DateTime, Catatan As String, Kdklaim As Integer) As Boolean
            Dim cmd As New SqlCommand
            cmd.CommandText = "DAFTAR_HISTORY_STATUS_INSERT"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = ConnDB

            cmd.Parameters.Add(New SqlParameter("@Status", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Status))
            cmd.Parameters.Add(New SqlParameter("@Waktu", SqlDbType.DateTime, 8, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Waktu))

            If String.IsNullOrWhiteSpace(Catatan) Then
                cmd.Parameters.Add(New SqlParameter("@Catatan", SqlDbType.Char, 30)).Value = DBNull.Value
            Else
                cmd.Parameters.Add(New SqlParameter("@Catatan", SqlDbType.Char, 30)).Value = Catatan
            End If

            cmd.Parameters.Add(New SqlParameter("@Kdklaim", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, Kdklaim))

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

        'SELECT DOCUMENT BY ID
        Public Function SelectDocument(Kdklaim As Integer) As Dictionary(Of String, Byte())
            Dim result As New Dictionary(Of String, Byte())()
            Using cmd As New SqlCommand("DOKUMEN_KLAIM_SELECT_BY_ID", ConnDB)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@kdklaim", SqlDbType.Int).Value = Kdklaim

                Try
                    'ConnDB.Open()
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
                    'ConnDB.Close()
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

        'Public Function SelectAllAwaiting() As Dictionary(Of String, Object)
        '    Dim userdetails As New Dictionary(Of String, Object)
        '    Dim connectionString As String = "data source=LOOSEFORDAYS\SQLEXPRESS; initial catalog=Medical; Persist Security Info=True;User ID=sa;Password=isal;Language=BRITISH ENGLISH;"
        '    Using con As New SqlConnection(connectionString)
        '        Using cmd As New SqlCommand("DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_BY_AWAITING", con)
        '            cmd.CommandType = CommandType.StoredProcedure
        '            con.Open()
        '            Using reader As SqlDataReader = cmd.ExecuteReader()
        '                If reader.Read() Then
        '                    userdetails("NIP") = If(IsDBNull(reader("nip")), String.Empty, reader("nip").ToString())
        '                    userdetails("NamaLengkap") = If(IsDBNull(reader("namalengkap")), String.Empty, reader("namalengkap").ToString())
        '                    userdetails("NamaDepartemen") = If(IsDBNull(reader("NamaDepartemen")), String.Empty, reader("NamaDepartemen").ToString())
        '                    userdetails("Kategori") = If(IsDBNull(reader("Kategori")), String.Empty, reader("Kategori").ToString())
        '                    userdetails("TanggalPengajuan") = If(IsDBNull(reader("TanggalPengajuan")), String.Empty, reader("TanggalPengajuan").ToString())
        '                End If
        '            End Using
        '        End Using
        '    End Using
        '    Return userdetails
        'End Function

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

        Public Function SelectAllOnProcess() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_ONPROCESS"
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

        Public Function SelectAllReject() As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_DAFTAR_PENGAJUAN_KLAIM_SELECT_REJECTED"
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

        Public Function SelectAllLogHistorisByNip(NIP As String) As DataTable
            Dim Comm As New SqlCommand
            Comm.CommandText = "DTA_KLAIM_STATUS_HISTORY_BY_NIP"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB
            Comm.Parameters.Add("@NIP", SqlDbType.Char).Value = NIP
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

        Public Function GetStatusSummary() As DataTable
            Dim Comm As New SqlCommand("SELECT Status_Terakhir FROM DAFTAR_PENGAJUAN_KLAIM", ConnDB)
            Comm.CommandType = CommandType.Text
            Dim DA As New SqlDataAdapter(Comm)
            Dim DT As New DataTable("GetStatusSummary")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
            End Try
        End Function
        Public Function GetKategoriSummary() As DataTable
            Dim Comm As New SqlCommand("SELECT Kategori FROM DAFTAR_PENGAJUAN_KLAIM", ConnDB)
            Comm.CommandType = CommandType.Text
            Dim DA As New SqlDataAdapter(Comm)
            Dim DT As New DataTable("GetStatusSummary")
            Try
                DA.Fill(DT)
                Return DT
            Catch ex As Exception
                Return DT
            Finally
                DA.Dispose()
            End Try
        End Function
        Public Function GetDetailPenyakit(kdklaim As String) As DataTable
            Dim query As String = "SELECT DetailPenyakit FROM DAFTAR_PENGAJUAN_KLAIM WHERE KdKlaim = @kdklaim"
            Dim cmd As New SqlCommand(query, ConnDB)
            cmd.Parameters.AddWithValue("@kdklaim", kdklaim)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                'ConnDB.Open()
                da.Fill(dt)
            Catch ex As Exception
                ' Optional log
            Finally
                'ConnDB.Close()
            End Try

            Return dt
        End Function
        Public Function GetBiaya(kdklaim As String) As DataTable
            Dim query As String = "SELECT Biaya FROM DAFTAR_PENGAJUAN_KLAIM WHERE KdKlaim = @kdklaim"
            Dim cmd As New SqlCommand(query, ConnDB)
            cmd.Parameters.AddWithValue("@kdklaim", kdklaim)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                'ConnDB.Open()
                da.Fill(dt)
            Catch ex As Exception
                ' Optional log
            Finally
                'ConnDB.Close()
            End Try

            Return dt
        End Function
        Public Function GetAlasanReject(kdklaim As String) As DataTable
            Dim query As String = "SELECT 
        hs.Catatan as Catatan
    FROM DAFTAR_PENGAJUAN_KLAIM dp
    OUTER APPLY (
        SELECT TOP 1 Catatan
        FROM DAFTAR_HISTORI_STATUS
        WHERE KdKlaim = dp.KdKlaim
        ORDER BY KdHistatus DESC
    ) hs
    WHERE dp.KdKlaim = @KdKlaim
      AND dp.Status_Terakhir = 'Rejected'
"
            Dim cmd As New SqlCommand(query, ConnDB)
            cmd.Parameters.AddWithValue("@kdklaim", kdklaim)

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            Try
                'ConnDB.Open()
                da.Fill(dt)
            Catch ex As Exception
                ' Optional log
            Finally
                'ConnDB.Close()
            End Try

            Return dt
        End Function

    End Class
End Namespace
