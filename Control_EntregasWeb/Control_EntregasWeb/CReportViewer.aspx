<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CReportViewer.aspx.vb" Inherits="Control_EntregasWeb.CReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Visor Reportes</title>
</head>
<body>

    <div class="container">

    <!-- BEGIN SERVICE BOX -->

    <div class="row mainTitleSearch">

    <form id="frmCrystal" runat="server">
         <br />
                 <asp:Button ID="btnReturn" runat="server" Text="Regresar" Width="164px" />
                 <br />
    <iframe id ="iframeCrystal" style="width:100%; height:1500px" src="<%= Session("ss_urlCrystal")%>"></iframe>
    </form>

        </div>   

    </div>
</body>
</html>