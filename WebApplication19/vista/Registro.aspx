<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebApplication19.vista.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrarse</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Registrarse</h1>
        </div>
        <div>
            <asp:Label ID="lnombre" runat="server" Text="Nombre"></asp:Label>
            <br />
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lCorreo" runat="server" Text="Correo"></asp:Label>
            <br />
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lPassword" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:dropdownlist id="ddlProvincia" runat="server">
                <asp:ListItem>San Jose</asp:ListItem>
                <asp:ListItem>Cartago</asp:ListItem>
                <asp:ListItem>Heredia</asp:ListItem>
                <asp:ListItem>Alajuela</asp:ListItem>
                <asp:ListItem>Guanacaste</asp:ListItem>
                <asp:ListItem>Puntarenas</asp:ListItem>
                <asp:ListItem>Limón</asp:ListItem>
            </asp:dropdownlist>
            <br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
            <br />
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" OnClick="btnRegistrar_Click" />
        </div>
    </form>
</body>
</html>
