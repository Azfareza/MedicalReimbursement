Imports System.Data.SqlClient

Module UtiUmum
    Public ConnDB As New SqlConnection(ConfigurationManager.ConnectionStrings("KoneksiDB").ToString)
End Module
