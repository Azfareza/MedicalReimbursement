Imports System.Data.SqlClient
Imports System.IO

Public Class PreviewDokumen
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim kdKlaimStr As String = Request.QueryString("kdklaim")
        Dim tipe As String = Request.QueryString("tipe")?.ToLower()

        If String.IsNullOrEmpty(kdKlaimStr) OrElse String.IsNullOrEmpty(tipe) Then
            Response.Write("<h3>Parameter tidak lengkap</h3>")
            Return
        End If

        Dim kdKlaim As Integer
        If Not Integer.TryParse(kdKlaimStr, kdKlaim) Then
            Response.Write("<h3>KdKlaim tidak valid</h3>")
            Return
        End If

        Dim cmd As New SqlCommand("DAFTAR_PENGAJUAN_KLAIM_DETAIL", ConnDB)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@KdKlaim", kdKlaim)

        Try
            Using reader As SqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Dim fileBytes As Byte() = Nothing
                    Dim contentType As String = "application/octet-stream"

                    Select Case tipe
                        Case "kwitansi"
                            If Not IsDBNull(reader("FileKwitansi")) Then fileBytes = CType(reader("FileKwitansi"), Byte())
                        Case "resep"
                            If Not IsDBNull(reader("FileResep")) Then fileBytes = CType(reader("FileResep"), Byte())
                        Case "pendukung"
                            If Not IsDBNull(reader("FilePendukung")) Then fileBytes = CType(reader("FilePendukung"), Byte())
                    End Select

                    If fileBytes Is Nothing Then
                        Response.Write("<h3>Dokumen tidak ditemukan.</h3>")
                        Return
                    End If

                    ' Tebak jenis file berdasarkan header
                    contentType = GetContentTypeFromBytes(fileBytes)
                    Response.Clear()
                    Response.ContentType = contentType
                    Response.OutputStream.Write(fileBytes, 0, fileBytes.Length)
                    Response.Flush()
                    Response.End()
                Else
                    Response.Write("<h3>Data tidak ditemukan</h3>")
                End If
            End Using
        Catch ex As Exception
            Response.Write("<h3>Error: " & ex.Message & "</h3>")
        Finally
            cmd.Dispose()
        End Try
    End Sub

    Private Function GetContentTypeFromBytes(fileBytes As Byte()) As String
        ' Minimal magic byte detection (signature based)
        If fileBytes.Length > 4 Then
            If fileBytes(0) = &H25 AndAlso fileBytes(1) = &H50 Then Return "application/pdf" ' PDF
            If fileBytes(0) = &HFF AndAlso fileBytes(1) = &HD8 Then Return "image/jpeg"
            If fileBytes(0) = &H89 AndAlso fileBytes(1) = &H50 Then Return "image/png"
        End If
        Return "application/octet-stream"
    End Function

End Class
