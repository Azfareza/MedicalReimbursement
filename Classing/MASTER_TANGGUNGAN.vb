
Imports System.Data.SqlClient
Namespace DAFTAR_TANGGUNGAN
    Public Class MASTER_TANGGUNGAN
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

        Public Function InsertTanggungan(
            NIK As String,
            NPWP As String,
            NamaLengkap As String,
            TempatLahir As String,
            TanggalLahir As Date,
            JenisKelamin As String,
            Pekerjaan As String,
            Hubungan As String
        ) As Boolean

            Dim Comm As New SqlCommand
            Comm.CommandText = "MASTER_TANGGUNGAN_INSERT"
            Comm.CommandType = CommandType.StoredProcedure
            Comm.Connection = ConnDB

            Comm.Parameters.AddWithValue("@NIK", NIK)
            Comm.Parameters.AddWithValue("@NPWP", NPWP)
            Comm.Parameters.AddWithValue("@NamaLengkap", NamaLengkap)
            Comm.Parameters.AddWithValue("@TempatLahir", TempatLahir)
            Comm.Parameters.AddWithValue("@TanggalLahir", TanggalLahir)
            Comm.Parameters.AddWithValue("@JenisKelamin", JenisKelamin)
            Comm.Parameters.AddWithValue("@Pekerjaan", Pekerjaan)
            Comm.Parameters.AddWithValue("@Hubungan", Hubungan)

            Try
                Comm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                ' Optional: Tambahkan log error jika perlu
                Return False
            Finally
                Comm.Dispose()
            End Try

        End Function


    End Class
End Namespace
