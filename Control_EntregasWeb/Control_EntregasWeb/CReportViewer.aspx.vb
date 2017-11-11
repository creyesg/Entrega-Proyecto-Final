Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions
Imports System.IO

Public Class CReportViewer
    Inherits System.Web.UI.Page
    Public Shared conex As New clsConexionOracle
    Public Shared utils As New SysUtil
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim crystalReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            'If setVariables() Then
            '    BindReport(crystalReport)
            '    crystalReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Presupuesto_No." + parameters)
            '    Response.[End]()
            'End If
            'setVariables()
            'Genera_PDF()http://localhost:50239/CReportViewer.aspx.vb

        End If
    End Sub



    Public Function bindReportEntregados() As List(Of Orders)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listOrders As New List(Of Orders)
        Ds = conex.fEjecutaQueryDS("select * from view_paq_entregados where fecha_orden > to_date(add_months(sysdate,-2))")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim order As New Orders
            order.idOrder = utils.isStringDBnull(rw("ORDER_ID"))
            order.NombreCliente = utils.isStringDBnull(rw("NOMBRE_FACTURA"))
            order.Direction = utils.isStringDBnull(rw("DIRECCION"))
            order.fecOrder = utils.isStringDBnull(rw("fecha_orden"))
            order.fecSend = utils.isStringDBnull(rw("fecha_envio"))
            order.fecEntrega = utils.isStringDBnull(rw("Fecha_entrega"))
            listOrders.Add(order)
        Next

        Return listOrders

    End Function

    Public Function bindReportCancelados() As List(Of Orders)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listOrders As New List(Of Orders)
        Ds = conex.fEjecutaQueryDS("select * from view_paq_cancelados where fecha_orden > to_date(add_months(sysdate,-2))")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim order As New Orders
            order.idOrder = utils.isStringDBnull(rw("ORDER_ID"))
            order.NombreCliente = utils.isStringDBnull(rw("NOMBRE_FACTURA"))
            order.Direction = utils.isStringDBnull(rw("DIRECCION"))
            order.fecOrder = utils.isStringDBnull(rw("fecha_orden"))
            order.fecSend = utils.isStringDBnull(rw("fecha_envio"))
            order.fecEntrega = utils.isStringDBnull(rw("Fecha_entrega"))
            listOrders.Add(order)
        Next

        Return listOrders

    End Function
    Public Function bindReportProceso() As List(Of Orders)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listOrders As New List(Of Orders)
        Ds = conex.fEjecutaQueryDS("select * from view_paq_proc where fecha_orden > to_date(add_months(sysdate,-2))")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim order As New Orders
            order.idOrder = utils.isStringDBnull(rw("ORDER_ID"))
            order.NombreCliente = utils.isStringDBnull(rw("NOMBRE_FACTURA"))
            order.Direction = utils.isStringDBnull(rw("DIRECCION"))
            order.fecOrder = utils.isStringDBnull(rw("fecha_orden"))
            order.fecSend = utils.isStringDBnull(rw("fecha_envio"))
            order.fecEntrega = utils.isStringDBnull(rw("Fecha_entrega"))
            listOrders.Add(order)
        Next

        Return listOrders

    End Function

    Public Function bindReporSeguimiento() As List(Of Tracking)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listTracking As New List(Of Tracking)
        Ds = conex.fEjecutaQueryDS("select * from view_seguimiento_tracking where fec_track > to_date(add_months(sysdate,-2))")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim track As New Tracking
            track.idOrder = utils.isStringDBnull(rw("ORDER_ID"))
            track.fecTrack = utils.isStringDBnull(rw("FEC_TRACK"))
            track.EMISOR_ALIAS = utils.isStringDBnull(rw("emisor"))
            track.RESEPTOR_ALIAS = utils.isStringDBnull(rw("receptor"))
            track.OBSERVACIONES = utils.isStringDBnull(rw("TXT_OBSERVACIONES"))
            track.ID_STATUS = utils.isStringDBnull(rw("ID_STATUS"))
            track.coordenadas = utils.isStringDBnull(rw("coordenadas"))
            listTracking.Add(track)
        Next

        Return listTracking

    End Function



    Public Function bindReportEmpresas() As List(Of Empresas)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listEmpresas As New List(Of Empresas)
        Ds = conex.fEjecutaQueryDS("select ID_EMPRESA, NOMBRE_EMPRESA, TARIFA_COBRO, TRACK_ALIAS from EMPRESAS_ENVIO where id_empresa >0")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim emp As New Empresas
            emp.idEmpresa = utils.isStringDBnull(rw("ID_EMPRESA"))
            emp.NombreEmpresa = utils.isStringDBnull(rw("NOMBRE_EMPRESA"))
            emp.Tarifa = utils.isStringDBnull(rw("TARIFA_COBRO"))
            emp.TrackAlias = utils.isStringDBnull(rw("TRACK_ALIAS"))
            listEmpresas.Add(emp)
        Next
        Return listEmpresas
    End Function



    Public Function bindReportEmpleados() As List(Of Empleados)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listEmpleados As New List(Of Empleados)
        Ds = conex.fEjecutaQueryDS("select COD_EMPLEADO, TRACK_ALIAS from ENVIOS_INTERNOS where cod_empleado > 0")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim emp As New Empleados
            emp.COD_EMPLEADO = utils.isStringDBnull(rw("COD_EMPLEADO"))
            emp.TrackAlias = utils.isStringDBnull(rw("TRACK_ALIAS"))
            listEmpleados.Add(emp)
        Next
        Return listEmpleados
    End Function

    Public Function bindDetail(ByVal orderId As String) As List(Of Details)
        Dim dtDetails As New DataTable
        Dim Ds As New DataSet

        Dim listDetail As New List(Of Details)
        Dim ssql As String = " select ord.order_id, ord.nombre_factura, ord.nit, ord.direccion, ord.fec_order, ord.peso_total,"
        ssql = ssql + " ord.tamanio_total, det.det_id, det.cod_producto, det.peso, det.tamaño from ORDERS ord"
        ssql = ssql + " left join orders_det det on det.ORDER_ID = ord.ORDER_ID where ord.ORDER_ID = " + orderId
        Ds = conex.fEjecutaQueryDS(ssql)
        dtDetails = Ds.Tables(0)
        For Each rw As DataRow In dtDetails.Rows
            Dim order As New Details
            order.idOrder = utils.isStringDBnull(rw("order_id"))
            order.NombreCliente = utils.isStringDBnull(rw("nombre_factura"))
            order.NIT = utils.isStringDBnull(rw("nit"))
            order.Direction = utils.isStringDBnull(rw("DIRECCION"))
            order.fecOrder = utils.isStringDBnull(rw("fec_order"))
            order.PESO_TOTAL = utils.isStringDBnull(rw("peso_total"))
            order.TAMANIO_T = utils.isStringDBnull(rw("tamanio_total"))
            order.det_id = utils.isStringDBnull(rw("det_id"))
            order.cod_producto = utils.isStringDBnull(rw("cod_producto"))
            order.peso = utils.isStringDBnull(rw("peso"))
            order.tamanio = utils.isStringDBnull(rw("tamaño"))
            listDetail.Add(order)
        Next
        Return listDetail
    End Function

    
    Public Function bindReportEnviosEmp() As List(Of Envios)
        Dim dtEnvios As New DataTable
        Dim Ds As New DataSet

        Dim listEnvios As New List(Of Envios)
        Ds = conex.fEjecutaQueryDS("SELECT env.order_id, env.envio_id, env.id_empresa || ' ' || nombre_empresa as id_empresa  FROM ENVIOS env INNER JOIN empresas_envio emp on emp.id_empresa = env.id_empresa")
        dtEnvios = Ds.Tables(0)
        For Each rw As DataRow In dtEnvios.Rows
            Dim emp As New Envios
            emp.idOrder = utils.isStringDBnull(rw("order_id"))
            emp.ENVIO_ID = utils.isStringDBnull(rw("envio_id"))
            emp.ID_EMPRESA = utils.isStringDBnull(rw("id_empresa"))
            listEnvios.Add(emp)
        Next
        Return listEnvios
    End Function

    Public Function bindReportEnviosEmple() As List(Of Envios)
        Dim dtEnvios As New DataTable
        Dim Ds As New DataSet

        Dim listEnvios As New List(Of Envios)
        Ds = conex.fEjecutaQueryDS("SELECT env.order_id, env.envio_id, env.cod_emp_interno || ' ' || track_alias as id_empresa FROM ENVIOS env INNER JOIN ENVIOS_INTERNOS emp on emp.cod_empleado = env.cod_emp_interno where sn_interno = -1")
        dtEnvios = Ds.Tables(0)
        For Each rw As DataRow In dtEnvios.Rows
            Dim emp As New Envios
            emp.idOrder = utils.isStringDBnull(rw("order_id"))
            emp.ENVIO_ID = utils.isStringDBnull(rw("envio_id"))
            emp.ID_EMPRESA = utils.isStringDBnull(rw("id_empresa"))
            listEnvios.Add(emp)
        Next
        Return listEnvios
    End Function


    Public Function bindReportEstadosOrder() As List(Of Orders)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listOrders As New List(Of Orders)
        Ds = conex.fEjecutaQueryDS("select id_status || ' - ' || description_status as estado from STATUS_ORDER")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim order As New Orders
            order.estado = utils.isStringDBnull(rw("estado"))
            listOrders.Add(order)
        Next

        Return listOrders

    End Function


    Public Function bindReportEstadosTrack() As List(Of Orders)
        Dim dtEntregados As New DataTable
        Dim Ds As New DataSet

        Dim listOrders As New List(Of Orders)
        Ds = conex.fEjecutaQueryDS("select id_status || ' - ' || description_status as estado from status_track")
        dtEntregados = Ds.Tables(0)
        For Each rw As DataRow In dtEntregados.Rows
            Dim order As New Orders
            order.estado = utils.isStringDBnull(rw("estado"))
            listOrders.Add(order)
        Next

        Return listOrders

    End Function








    Public Function Genera_PDF(ByVal namereport As String, ByVal idParameter As String, ByVal parameters As String, ByVal variables As String) As String
        Dim printObjects = New List(Of Orders)()
        Dim printSeguimiento = New List(Of Tracking)()
        Dim printEmpresas = New List(Of Empresas)()
        Dim printEmpleados = New List(Of Empleados)()
        Dim printDetails = New List(Of Details)()
        Dim printEnvios = New List(Of Envios)()
        Dim pdf_name As String
        pdf_name = "Reporte"



        Try
            Dim Reporte As New CrystalReports.Engine.ReportDocument

            Select Case idParameter
                Case "133"
                    ' Dim v_variables = "p_id_presupuesto.varchar"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printObjects = bindReportEntregados()
                    Reporte.SetDataSource(printObjects)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor

                Case "134"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printObjects = bindReportProceso()
                    Reporte.SetDataSource(printObjects)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor

                Case "135"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printSeguimiento = bindReporSeguimiento()
                    Reporte.SetDataSource(printSeguimiento)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "136"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printObjects = bindReportCancelados()
                    Reporte.SetDataSource(printObjects)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "137"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printEmpresas = bindReportEmpresas()
                    Reporte.SetDataSource(printEmpresas)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "138"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printEmpleados = bindReportEmpleados()
                    Reporte.SetDataSource(printEmpleados)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "139"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printDetails = bindDetail(variables)
                    Reporte.SetDataSource(printDetails)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor

                Case "140"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printEnvios = bindReportEnviosEmp()
                    Reporte.SetDataSource(printEnvios)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "141"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printEnvios = bindReportEnviosEmple()
                    Reporte.SetDataSource(printEnvios)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "142"
                    ' Dim v_variables = "p_id_presupuesto.varchar"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printObjects = bindReportEstadosOrder()
                    Reporte.SetDataSource(printObjects)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
                Case "143"
                    ' Dim v_variables = "p_id_presupuesto.varchar"
                    pdf_name = "Prods_" + parameters
                    Dim urlreport = "~/Reportes/" + namereport + ".rpt"
                    Reporte.Load(Server.MapPath(urlreport))
                    Reporte.Refresh()
                    printObjects = bindReportEstadosTrack()
                    Reporte.SetDataSource(printObjects)
                    Dim crDiskFileDestinationOptions As New CrystalDecisions.Shared.DiskFileDestinationOptions
                    Dim crExportOptions As New CrystalDecisions.Shared.ExportOptions()
                    crDiskFileDestinationOptions.DiskFileName = Server.MapPath("\Reportes\" + pdf_name + ".pdf")
                    crExportOptions = Reporte.ExportOptions
                    With crExportOptions
                        .DestinationOptions = crDiskFileDestinationOptions
                        .ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                        .ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                    End With
                    Reporte.Export()  'Graba el PDF en el sistema de archivos del servidor
            End Select

            Dim url As String = GetUrlRoot(pdf_name)
            Return url
        Catch ex As Exception
            Dim serror = "Error con el rpt, favor contactarnos para brindarle un mejor servicio. " & ex.Message
            Return serror
        End Try

    End Function


    Public Function GetUrlRoot(ByVal reportName As String)
        Return "/Reportes/" & reportName & ".pdf"
    End Function

    Private Sub sError(ByVal psMensaje As String)
        'panError.Visible = True
        Dim script = "showAlertModal('" + psMensaje + "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", script, True)
    End Sub

    Protected Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Response.Redirect("~/Default.aspx")
    End Sub
End Class