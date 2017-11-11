Imports System.Data.SqlClient
'Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Web

'Clase utilizada para llamar a la prm_comand_sql ejecutando querys por Id
Public Class SysUtil

    Public Function GetDataTableById(paramId As Integer, parameters As String) As DataTable
        Dim vobjConexion As New clsConexion
        Dim dt As New DataTable
        Try
            Dim sQuery = GetParameterById(paramId)
            Dim dr As SqlDataReader = vobjConexion.fEjecutaQueryRDParam(sQuery, parameters)
            dt.Load(dr)
            Return dt
        Catch ex As Exception
            dt.Clear()
            Return dt
        End Try
    End Function

    Public Function GetCatalogById(paramId As Integer, parameters As String) As List(Of Catalog.Catalog)
        Dim vobjConexion As New clsConexion
        Dim currentcatalog = New List(Of Catalog.Catalog)()
        Dim dt As New DataTable
        Try
            Dim sQuery = GetParameterById(paramId)
            Dim dr As SqlDataReader = vobjConexion.fEjecutaQueryRDParam(sQuery, parameters)
            dt.Load(dr)
            currentcatalog = CommonAssembler.GetCatalogs(dt)
            dt.Clear()
            dt = Nothing
            Return currentcatalog
        Catch ex As Exception
            dt.Clear()
            dt = Nothing
            Return CommonAssembler.SetErrorToCatalog("Error en procedimiento: " + ex.Message)
        End Try
    End Function

    Public Function GetParameterById(ByVal pId As Integer) As String
        Dim ds As DataSet = New DataSet
        Dim vobjConexion As New clsConexion
        Dim stringReturn As String = ""
        Dim vsParametros(0) As String
        vsParametros(0) = "@p_id|1|" + pId.ToString()
        Try
            ds = vobjConexion.fdEjecutaSPDatos("sp_selectcommand", vsParametros)
            If ds Is Nothing Then
            Else
                For Each dr As DataRow In ds.Tables(0).Rows
                    stringReturn = dr.ItemArray(1)
                Next
                Return stringReturn
                Exit Function
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function convertelementDictionaryByString(ByVal element As KeyValuePair(Of String, Object)) As String()
        Dim stringelement = element.ToString()
        stringelement = stringelement.Replace("[", "")
        stringelement = stringelement.Replace("]", "")
        Dim part = stringelement.Split(",")
        Return part
    End Function

    Public Function isStringDBnull(ByVal value As Object) As String
        If Not IsDBNull(value) Then
            Return value.ToString()
        Else
            Return ""
        End If
    End Function

    Public Function isUrlExtern(ByVal sUrl As String) As Boolean
        Dim leftUrl = Microsoft.VisualBasic.Left(sUrl, 4)
        If leftUrl = "http" Then
            Return True
        End If
        Return False
    End Function

    Public Function isValidDate(ByVal StringDate As String) As Boolean
        Dim bln As Boolean = True
        Dim r As New Regex("^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$")
        If Not r.IsMatch(StringDate) Then
            bln = False
        End If
        Return bln
    End Function

    'Public Function GetCatalogByIdOfMysql(paramId As Integer, parameters As String) As List(Of Catalog.Catalog)
    '    Dim vobjConexion As New clsConexion
    '    Dim currentcatalog = New List(Of Catalog.Catalog)()
    '    Dim dt As New DataTable
    '    Dim conexion As New clsConexionMysql
    '    Try
    '        Dim sQuery = GetParameterById(paramId) 'Conecta a sql server solo para traer la consulta
    '        Dim dr As MySqlDataReader = conexion.fEjecutaQueryRDParam(sQuery, parameters)
    '        dt.Load(dr)
    '        currentcatalog = CommonAssembler.GetCatalogs(dt)
    '        dt.Clear()
    '        dt = Nothing
    '        Return currentcatalog
    '    Catch ex As Exception
    '        dt.Clear()
    '        dt = Nothing
    '        Return CommonAssembler.SetErrorToCatalog("Error en procedimiento: " + ex.Message)
    '    End Try
    'End Function
    Public Function GetGaleryNumbersAndNames() As List(Of Catalog.Catalog)
        Dim ListCatalog As New List(Of Catalog.Catalog)
        Dim di As DirectoryInfo = New DirectoryInfo(HttpContext.Current.Server.MapPath("~") & "info_galeria\")
        Dim index As Integer = 1
        'Dim catalog As New Catalog.Catalog
        Dim dt As New DataTable()
        dt.Columns.Add("Id")
        dt.Columns.Add("Description")
        dt.Columns.Add("date_create")
        For Each fi In di.GetDirectories()
            Dim dat As DateTime = Directory.GetCreationTime(HttpContext.Current.Server.MapPath("~") & "info_galeria\" & fi.Name & "\")
            If DateTime.Now.Subtract(dat).TotalDays < 365 Then 'Valida si la carpeta tiene menos de un año
                Dim row As DataRow = dt.NewRow()
                row("Id") = index
                row("Description") = fi.Name
                row("date_create") = dat
                dt.Rows.Add(row)
                index = index + 1
            End If
        Next

        ListCatalog = CommonAssembler.GetCatalogs(dt)

        Return ListCatalog

    End Function



    Public Function GetGaleryImagesbyFolder(ByVal GaleryId As String) As List(Of Catalog.Catalog)
        Dim exists As Boolean
        Dim ListCatalog As New List(Of Catalog.Catalog)
        exists = Directory.Exists(HttpContext.Current.Server.MapPath("~") & "info_galeria\" & GaleryId & "\")
        If exists Then
            Dim di As DirectoryInfo = New DirectoryInfo(HttpContext.Current.Server.MapPath("~") & "info_galeria\" & GaleryId & "\")
            Dim index As Integer = 1
            Dim catalog As New Catalog.Catalog
            For Each fi In di.GetFiles()
                catalog = New Catalog.Catalog
                catalog.Id = index
                catalog.Description = fi.Name
                index = index + 1
                ListCatalog.Add(catalog)
            Next
        End If

        Return ListCatalog
    End Function

    Public Function sEnviaCorreoNoticia() As List(Of Catalog.Catalog)
        Dim correo As New System.Net.Mail.MailMessage
        Dim vsContenido, vsNombre, vsCorreo As String
        Try
            '---------------- Envío de contraseña por correo ----------------
            Dim vsEMail As String, vbEnviar As Boolean = False
            Dim smtp As New System.Net.Mail.SmtpClient

            vsContenido = ""
            vsNombre = ""
            vsEMail = ""


            vsNombre = "Novedades Confío"
            vsEMail = "seguros@occidente.com.gt"

            Dim header As String
            Dim footer As String
            Dim body As String
            header = GetParameterById(6)
            footer = GetParameterById(7)
            body = GetParameterById(8)
            Dim htmlMail As String = ""
            Dim cataloglist As New List(Of Catalog.Catalog)
            Dim dt As New DataTable

            dt = GetDataTableById(9, "")
            cataloglist = CommonAssembler.GetCatalogs(dt)

            Dim desc As String = ""
            For Each row As DataRow In dt.Rows
                desc = row("not_descripcion").ToString()
                htmlMail = body.Replace("[##not_noticia##]", row("Id"))
                htmlMail = htmlMail.Replace("[##not_titulo##]", row("Description"))
                htmlMail = htmlMail.Replace("[##not_imagen##]", row("not_imagen"))
                htmlMail = htmlMail.Replace("[##not_descripcion##]", Left(desc, 50) + "...")

                vsContenido = vsContenido + htmlMail
            Next

            vsContenido = header + vsContenido + footer

            vsCorreo = ConfigurationManager.AppSettings("cnCorreoConfio")
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Host = ConfigurationManager.AppSettings("cnServidorCorreo")
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("cnUsuarioServicioTI"), ConfigurationManager.AppSettings("cnClaveServicioTI"))
            smtp.Timeout = 100000

            correo.From = New System.Net.Mail.MailAddress("seguros@occidente.com.gt", "Noticias Confío")  'Correo Remitente
            correo.To.Add(New System.Net.Mail.MailAddress(vsCorreo, vsNombre))
            correo.Subject = "Ultimas Novedades Confío"
            correo.IsBodyHtml = True
            correo.Priority = System.Net.Mail.MailPriority.Normal
            correo.Body = vsContenido

            smtp.Send(correo)

            Return cataloglist

        Catch ex As Exception
            Return CommonAssembler.SetErrorToCatalog("Error en procedimiento: " + ex.Message)
        End Try
    End Function

End Class
