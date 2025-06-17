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

        Public Function InsertPegawai(
            NIP As String, NIK As String, NPWP As String, NamaLengkap As String,
            TempatLahir As String, TanggalLahir As Date, JenisKelamin As String,
            Kebangsaan As String, Seluler As String, Email As String, Agama As String,
            KdDept As String, Password As String, Optional Role As String = "4"
        ) As Boolean

            Dim Comm As New SqlCommand
            Comm.CommandText = "DAFTAR_PEGAWAI_INSERT"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB

            Comm.Parameters.AddWithValue("@NIP", NIP)
            Comm.Parameters.AddWithValue("@NIK", NIK)
            Comm.Parameters.AddWithValue("@NPWP", NPWP)
            Comm.Parameters.AddWithValue("@NamaLengkap", NamaLengkap)
            Comm.Parameters.AddWithValue("@TempatLahir", TempatLahir)
            Comm.Parameters.AddWithValue("@TanggalLahir", TanggalLahir)
            Comm.Parameters.AddWithValue("@JenisKelamin", JenisKelamin)
            Comm.Parameters.AddWithValue("@Kebangsaan", Kebangsaan)
            Comm.Parameters.AddWithValue("@Seluler", Seluler)
            Comm.Parameters.AddWithValue("@Email", Email)
            Comm.Parameters.AddWithValue("@Agama", Agama)
            Comm.Parameters.AddWithValue("@KdDept", KdDept)
            Comm.Parameters.AddWithValue("@Password", Password)
            Comm.Parameters.AddWithValue("@Role", Role)
            'Comm.Parameters.AddWithValue("@Foto", DBNull.Value)

            Try
                Comm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
            Finally
                Comm.Dispose()
            End Try
        End Function

        Public Function InsertAlamatPegawai(
            NIP As String,
            Provinsi As String,
            Kota As String,
            Kecamatan As String,
            Kelurahan As String,
            DetilAlamat As String,
            KodePos As Integer,
            Optional Aktif As Boolean = True
        ) As Boolean

            Dim Comm As New SqlCommand
            Comm.CommandText = "MASTER_ALAMAT_INSERT"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB

            Comm.Parameters.AddWithValue("@NIP", NIP)
            Comm.Parameters.AddWithValue("@Provinsi", Provinsi)
            Comm.Parameters.AddWithValue("@Kota", Kota)
            Comm.Parameters.AddWithValue("@Kecamatan", Kecamatan)
            Comm.Parameters.AddWithValue("@Kelurahan", Kelurahan)
            Comm.Parameters.AddWithValue("@DetilAlamat", DetilAlamat)
            Comm.Parameters.AddWithValue("@KodePos", KodePos)
            Comm.Parameters.AddWithValue("@Aktif", Aktif)

            Try
                Comm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
            Finally
                Comm.Dispose()
            End Try
        End Function


        Public Function InsertMutasiPegawai(
            NIP As String,
            KdDepartemen As String,
            KdJabatan As String,
            TglMulai As DateTime,
            Status As String,
            Catatan As String,
            Optional TglSelesai As DateTime? = Nothing,
            Optional Aktif As Boolean = True
        ) As Boolean

            Dim Comm As New SqlCommand
            Comm.CommandText = "DAFTAR_MUTASI_PEGAWAI_INSERT"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB

            Comm.Parameters.AddWithValue("@NIP", NIP)
            Comm.Parameters.AddWithValue("@KdDepartemen", KdDepartemen)
            Comm.Parameters.AddWithValue("@KdJabatan", KdJabatan)
            Comm.Parameters.AddWithValue("@TglMulai", TglMulai)
            Comm.Parameters.AddWithValue("@Status", Status)
            Comm.Parameters.AddWithValue("@Catatan", Catatan)
            Comm.Parameters.AddWithValue("@Aktif", Aktif)

            If TglSelesai.HasValue Then
                Comm.Parameters.AddWithValue("@TglSelesai", TglSelesai.Value)
            Else
                Comm.Parameters.AddWithValue("@TglSelesai", DBNull.Value)
            End If

            Try
                Comm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Return False
            Finally
                Comm.Dispose()
            End Try
        End Function

    End Class

End Namespace
