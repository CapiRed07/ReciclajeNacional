<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication19.vista.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Iniciar Sesión (Simulado)</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 50px auto; width: 300px; text-align: center;">
            <h2>♻️ Sistema de Reciclaje</h2>
            <p>
                <asp:TextBox ID="txtCorreo" runat="server" Placeholder="Correo electrónico" Width="100%" />
            </p>
            <p>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Contraseña" Width="100%" />
            </p>
            <p>
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" Width="100%" />
            </p>
            <p>
                <asp:Label ID="lblError" runat="server" ForeColor="Red" />
            </p>
            <hr />
            <small style="color: gray">Prueba con:<br />user@recicla.com / 123<br />admin@recicla.com / 123</small>
        </div>
    </form>
</body>
</html>
