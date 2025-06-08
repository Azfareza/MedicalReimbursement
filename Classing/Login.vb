Imports System.Data.SqlClient

Namespace LoginLogic
    Public Class Login
        Public Shared Function ValidateUser(nip As String, password As String) As Boolean
            Dim query As String = "SELECT COUNT(*) FROM DAFTAR_PEGAWAI WHERE NIP = @NIP AND Password = @Password"
            Dim cmd As New SqlCommand(query, ConnDB)
            cmd.Parameters.AddWithValue("@NIP", nip)
            cmd.Parameters.AddWithValue("@Password", password)

            Try
                ConnDB.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            Catch ex As Exception
                ' Log error atau tampilkan pesan ke sistem
                Return False
            Finally
                ConnDB.Close()
                cmd.Dispose()
            End Try
        End Function

        Public Shared Function GetUserRole(nip As String) As String
            Dim query As String = "SELECT Role FROM DAFTAR_PEGAWAI WHERE NIP = @NIP"
            Dim cmd As New SqlCommand(query, ConnDB)
            cmd.Parameters.AddWithValue("@NIP", nip)
            Try
                ConnDB.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    Return result.ToString().Trim()
                End If
            Catch ex As Exception
                Return ""
            Finally
                ConnDB.Close()
                cmd.Dispose()
            End Try

            Return ""
        End Function
    End Class
End Namespace
