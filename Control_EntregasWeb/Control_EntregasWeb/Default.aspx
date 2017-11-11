<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Control_EntregasWeb._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes de Envíos</title>
    <style>
        h1 {color:white;
        }

    </style>
</head>
<body style="background-color:#808080">
    <form id="form1" runat="server">
    <div>
  <h1>Reportes de Logística</h1>
        
        <table>
            <tr>

                <td>
                     <div>
            <asp:Button ID="btnEntregados" runat="server" Text="Reporte de E. Entregados"   Width="225px" />
                    <br/>
             <br/>
            <asp:Button ID="btnProceso" runat="server" Text="Reporte de E. Proceso" Width="225px" />
             <br/>
             <br/>
            <asp:Button ID="btnCancelados" runat="server" Text="Reporte de E. Cancelados" Width="225px" />
            <br/>
             <br/>
            <asp:Button ID="btnReporteseguimiento" runat="server" Text="Reporte de Seguimiento" Width="225px" />
             <br/>
             <br/>
            <asp:Button ID="btnEmpresas" runat="server" Text="Empresas de Envío" Width="225px" />
             <br/>
             <br/>
            <asp:Button ID="btnMensajeros" runat="server" Text="Mensajeros Empleados" Width="225px" />
             <br/>
             <br/>
            <asp:Button ID="btnOrderDetail" runat="server" Text="Orden de  Envío" Width="225px" /><br/>
            <asp:Label ID="lblOrderID" runat="server" Text="Order Id"></asp:Label><asp:TextBox ID="txtOrderId" runat="server" Width="46px"></asp:TextBox>
            <asp:Button ID="btnViewDetail" runat="server" Text="VER" Width="50px" />
                         <br/>
             <br/>
            <asp:Button ID="btnEmpresasEnvios" runat="server" Text="Empresas Envios" Width="225px" />
                            <br/>
             <br/>
            <asp:Button ID="btnEmpleadosEnvios" runat="server" Text="Empleados Envios" Width="225px" />
              <br/>
             <br/>
            <asp:Button ID="btnStatus" runat="server" Text="Estados de la Orden" Width="225px" />
                         <br/>
             <br/>
            <asp:Button ID="btnstatustrack" runat="server" Text="Estados del Tracking" Width="225px" />
        </div>
                </td>
                <td style="vertical-align:top">
                    <asp:Image ID="Image1" runat="server" Height="295px" Width="511px" style="margin-right: 0px; margin-bottom: 0px" ImageUrl="/img/back.jpg" />
                </td>
            </tr>


        </table>
        
        
        
       


    </div>
    </form>
</body>
</html>
