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
            <dropdownlist id="ddlProvincia" runat="server"></dropdownlist>
            <br />
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" OnClick="btnRegistrar_Click" />
        </div>
    </form>
</body>
</html>
