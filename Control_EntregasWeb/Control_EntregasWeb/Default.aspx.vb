Public Class _Default
    Inherits System.Web.UI.Page
    Dim rep As New CReportViewer
    Public Shared isView As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblOrderID.Visible = False
        btnViewDetail.Visible = False
        txtOrderId.Visible = False
    End Sub

    Protected Sub btnReporteseguimiento_Click(sender As Object, e As EventArgs) Handles btnReporteseguimiento.Click
        Dim result As String
        result = rep.Genera_PDF("segTracking", "135", "segTracking", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnProceso_Click(sender As Object, e As EventArgs) Handles btnProceso.Click
        Dim result As String
        result = rep.Genera_PDF("paqProceso", "134", "paqProceso", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnEntregados_Click(sender As Object, e As EventArgs) Handles btnEntregados.Click
        Dim result As String
        result = rep.Genera_PDF("paqEntregados", "133", "paqEntregados", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnCancelados_Click(sender As Object, e As EventArgs) Handles btnCancelados.Click
        Dim result As String
        result = rep.Genera_PDF("paqCancelados", "136", "paqCancelados", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnEmpresas_Click(sender As Object, e As EventArgs) Handles btnEmpresas.Click
        Dim result As String
        result = rep.Genera_PDF("empresasEnvio", "137", "empresasEnvio", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnMensajeros_Click(sender As Object, e As EventArgs) Handles btnMensajeros.Click
        Dim result As String
        result = rep.Genera_PDF("empleadosInternos", "138", "empleadosInternos", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnOrderDetail_Click(sender As Object, e As EventArgs) Handles btnOrderDetail.Click
        If Not isView Then
            lblOrderID.Visible = True
            btnViewDetail.Visible = True
            txtOrderId.Visible = True
            isView = True
        Else
            lblOrderID.Visible = False
            btnViewDetail.Visible = False
            txtOrderId.Visible = False
            isView = False
        End If
    End Sub

    Protected Sub btnViewDetail_Click(sender As Object, e As EventArgs) Handles btnViewDetail.Click
        Dim txt As String = txtOrderId.Text
        If txt = "" Then
            sError("Ingrese Order Id")
        ElseIf Not IsNumeric(txt) Then
            sError("Ingrese Valor Numerico")
        Else
            Dim result As String
            result = rep.Genera_PDF("detalleOrden", "139", "detalleOrden", Val(txt).ToString())
            Session("ss_urlCrystal") = result
            Response.Redirect("~/CReportViewer.aspx")
        End If
    End Sub

    Protected Sub btnEmpresasEnvios_Click(sender As Object, e As EventArgs) Handles btnEmpresasEnvios.Click
        Dim result As String
        result = rep.Genera_PDF("empresasOdenes", "140", "empresasOdenes", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub


    Private Sub sError(ByVal psMensaje As String)
        'panError.Visible = True
        Dim script = "alert('" + psMensaje + "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", script, True)
    End Sub


    Protected Sub btnEmpleadosEnvios_Click(sender As Object, e As EventArgs) Handles btnEmpleadosEnvios.Click
        Dim result As String
        result = rep.Genera_PDF("empleadosOdenes", "141", "empleadosOdenes", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnStatus_Click(sender As Object, e As EventArgs) Handles btnStatus.Click
        Dim result As String
        result = rep.Genera_PDF("estadosOrden", "142", "estadosOrden", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub

    Protected Sub btnstatustrack_Click(sender As Object, e As EventArgs) Handles btnstatustrack.Click
        Dim result As String
        result = rep.Genera_PDF("estadosTrack", "143", "estadosTrack", "")
        Session("ss_urlCrystal") = result
        Response.Redirect("~/CReportViewer.aspx")
    End Sub
End Class