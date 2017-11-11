Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Xml
Imports System.Data
Imports System.Web.UI

Public Class clsFunciones
    Dim objConexion As New clsConexion
    Public Sub sCargaMenu(ByRef pmMenu As Menu, ByVal psCriterio As String)
        'Procedimiento que carga un menu por rol de usuario
        Dim vdMenu As Data.DataSet

        pmMenu.Items.Clear()

        'No llenar menú
        If psCriterio.Length <> 0 Then
            'Carga de menú de opciones por rol de usuario
            vdMenu = objConexion.fEjecutaQueryDS(psCriterio)

            For viItem As Integer = 0 To vdMenu.Tables(0).Rows.Count - 1
                Dim mniOpcion As New MenuItem

                With mniOpcion
                    .ImageUrl = "../images/arr2.gif"
                    '.SeparatorImageUrl = "../images/px.gif"
                    .Text = "&nbsp;&nbsp;" & vdMenu.Tables(0).Rows(viItem).Item(0)
                    '.Text = "<li>" & vdrMenu.Item(0) & "</li>"
                    .NavigateUrl = vdMenu.Tables(0).Rows(viItem).Item(1)
                End With

                pmMenu.Items.Add(mniOpcion)
            Next
        End If

        'Agregar la opción salir
        Dim mniOpcionSalir As New MenuItem

        With mniOpcionSalir
            .ImageUrl = "../images/arr2.gif"
            '.SeparatorImageUrl = "../images/px.gif"
            .Text = "&nbsp;&nbsp;Salir"
            .NavigateUrl = "../salir.aspx"
        End With

        pmMenu.Items.Add(mniOpcionSalir)

        objConexion.fCierraConexion()
        'objConexion = Nothing
    End Sub
    Public Function fsMensajeAccesoRestringido() As String
        'Funcion que arma el mensaje de acceso restringido
        Dim vsMensaje As String

        vsMensaje = "<table width=""50%"" border=""0"" cellpadding=""0"" cellspacing=""0"" align=""center"">" & _
                    "<tr>" & _
                    "<td align=""right"" style=""text-align: right""><img src=""../images/atencion.gif"" alt="""" />" & _
                    "</td>" & _
                    "<td class=""style6"" > " & _
                    "<div style=""text-align: center""><strong><font color=""red"">ACCESO RESTRINGIDO</font> </strong></div>" & _
                    "</td>" & _
                    "</tr>" & _
                    "<tr background=""../gfo/images_gfo/bg.gif"" class=""style6"" >" & _
                    "<td valign=""top"" colspan=""2"">" & _
                    "<div style=""margin-left:5px; text-align: center ""><b>No tiene acceso a esta opción o su sesión ha expirado.</b></div>" & _
                    "</td>" & _
                    "</tr>" & _
                    "<tr>" & _
                    "<td colspan=""2"" class=""style6"">" & _
                    "<div style=""margin-left:5px; text-align: center"" class=""yellow2""><b>Si aún no tiene acceso al sistema, solicítelo <a href=""mailto:intranetgfo@occidente.com"">[aquí]</a>.</b></div>" & _
                    "</td>" & _
                    "</tr>" & _
                    "<tr><td colspan=""2"" class=""style6"" align=""center"" style=""text-align: center""><div style=""margin-left:5px; text-align: center"" class=""yellow2""><b>Si desea regresar al inicio, presione <a href=""../Default.aspx"">[aquí]</a></b></div></td></tr>" & _
                    "</table>"

        Return vsMensaje
    End Function
    Public Function fsMensajeEnConstruccion() As String
        'Funcion que arma el mensaje de acceso restringido
        Dim vsMensaje As String

        vsMensaje = "<table border=""0"" align=""center"" cellpadding=""0"" cellspacing=""0"" width=""100%"">" & _
               "<tr>" & _
                      "<td height=""7"" style=""background-position:top; background-repeat:no-repeat;""></td>" & _
                    "</tr>" & _
                    "<tr>" & _
                      "<td height=""7"" background=""../images/datatop.jpg"" style=""background-position:top; background-repeat:no-repeat;""></td>" & _
                    "</tr>" & _
                    "<tr>" & _
                      "<td valign=""top"" background=""../images/datacenter.jpg"" style=""background-position:center; background-repeat:repeat-y;"">" & _
                        "<table width=""30%"" align=""center"" border=""0"">" & _
                            "<tr>" & _
                                "<td align=""right"" style=""text-align: right""><img src=""../images/notaatencion.gif"" alt="""" /></td>" & _
                                "<td>" & _
                                    "<div style=""text-align: center""><strong><font color=""red"">OPCIÓN EN CONSTRUCCIÓN</font> </strong></div>" & _
                                "</td>" & _
                            "</tr>" & _
                            "<tr background=""../images/bg_sm.gif"">" & _
                                "<td valign=""top"" colspan=""2"">" & _
                                    "<div style=""margin-left:5px; text-align: center "" ><b>Esta opción se encuentra en construcción, reintente su ingreso más adelante.</b></div>" & _
                                "</td>" & _
                            "</tr>" & _
                            "<tr>" & _
                                "<td colspan=""2"">" & _
                                    "<div style=""margin-left:5px; text-align: center"" class=""yellow2""><b>Si aún no tiene acceso al Portal BASIS, solicítelo <a href=""mailto:basis@dhl.com"">[aquí]</a>.</b></div>" & _
                                "</td>" & _
                            "</tr>" & _
                        "</table>" & _
                      "</td>" & _
                    "</tr>" & _
                    "<tr>" & _
                      "<td height=""7"" background=""../images/databottom.jpg"" style="" background-position:top; background-repeat:no-repeat;""></td>" & _
                    "</tr>" & _
                  "</table>"

        Return vsMensaje
    End Function
    Public Sub sBuscaDatoCombo(ByVal psDato As String, ByRef pcCombo As WebControls.DropDownList)
        'Procedimiento que busca y selecciona el psDato en pcCombo, valores por referencia
        Dim vlConta As Long
        For vlConta = 0 To pcCombo.Items.Count - 1
            If pcCombo.Items(vlConta).Value = psDato Then
                pcCombo.SelectedValue = pcCombo.Items(vlConta).Value
                Exit Sub
            End If
        Next
    End Sub
    Public Function fsEsquemaCorreo(ByVal psAsunto As String, ByVal psTitulo As String, ByVal psContenido As String) As String
        'Función que arma esquema HTML para formato de correo
        Dim dtsEsquema As Data.DataSet

        Try
            dtsEsquema = objConexion.fEjecutaQueryDS("sp_int_home 10,null,null,null,'" & psAsunto & "','" & psTitulo & "','" & psContenido & "'")

            Return dtsEsquema.Tables(0).Rows(0).Item(0)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function sDescargarArchivo(ByVal strRequest As String, ByVal pPagina As Page) As String
        Try
            'Dim strRequest As String = e.CommandArgument 'Request.QueryString("file") '-- if something was passed to the file querystring

            If strRequest <> "" Then 'get absolute path of the file

                'strRequest = "../polsprods/" & strRequest
                strRequest = strRequest

                Dim path As String = pPagina.Server.MapPath(strRequest) 'get file object as FileInfo
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path) '-- if the file exists on the server
                If file.Exists Then 'set appropriate headers
                    pPagina.Response.Clear()
                    pPagina.Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                    pPagina.Response.AddHeader("Content-Length", file.Length.ToString())
                    pPagina.Response.ContentType = "application/octet-stream"
                    pPagina.Response.WriteFile(file.FullName)
                    pPagina.Response.End() 'if file does not exist
                    Return ("")
                Else
                    'Response.Write("This file does not exist.")
                    Return ("El archivo no existe")
                End If 'nothing in the URL as HTTP GET
            Else
                'pPagina.Write("Please provide a file to download.")
                Return ("Please provide a file to download.")
            End If
        Catch ex As Exception
            Return ("Error al descargar archivo: " & ex.Message)
        End Try
    End Function
    Public Sub sBitacoraOpcion(ByVal psUsuario As String, ByVal psOpcion As String)
        'Graba la opción a la que ingresó el usuario

        If psUsuario Is Nothing Then
            Exit Sub
        Else
            If psUsuario.Length = 0 Then Exit Sub
        End If

        objConexion.fEjecutaQueryDS("sp_int_home 11,'" & psUsuario & "'," & psOpcion)
    End Sub
    Public Function Msg(ByVal str As String, ByVal supportVB As Boolean, ByVal style As MsgBoxStyle) As String

        If supportVB Then

            str = str.Replace("""", "'")
            Return "<script language=""vbscript"" type=""text/vbscript"" >MsgBox """ & str & """," & CInt(style).ToString & ", ""Message Box""</script>"
        Else
            str = str.Replace("'", """")
            Return "<script>window.alert('" & str & "')</script>"
        End If
    End Function

    Public Function Msg(ByVal str As String) As String
        Return Msg(str, False, MsgBoxStyle.Exclamation)
    End Function
End Class
