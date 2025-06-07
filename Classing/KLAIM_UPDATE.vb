Imports System.Data
Imports System.Data.SqlClient
Public Class KLAIM_UPDATE

    Public Function KlaimUpdater(KdKlaim As Int32, StatusTerakhir As String) As Boolean
        Dim cmd As New SqlCommand
        cmd.CommandText = "DTA_KLAIM_STATUS_UPDATE"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = ConnDB
        cmd.Parameters.Add(New SqlParameter("@kdklaim", SqlDbType.Int, 9, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, KdKlaim))
        cmd.Parameters.Add(New SqlParameter("@status", SqlDbType.Char, 12, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Original, StatusTerakhir))
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
