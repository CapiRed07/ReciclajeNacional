<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplication19.vista.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Usuario</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Usuario</h1>
        </div>
        <div>
            <asp:Repeater ID="rptBoxes" runat="server" OnItemCommand="rptBoxes_ItemCommand">
    <HeaderTemplate>
        <div class="box-container">
    </HeaderTemplate>
    <ItemTemplate>
        <div class="data-box">
            <h3><%# Eval("Nombre") %></h3>
            <p><strong>Email:</strong> <%# Eval("Correo") %></p>
            <p><strong>Province:</strong> <%# Eval("Provincia") %></p>
            <p class="points-badge"><strong>Points:</strong> <%# Eval("Puntos") %></p>
        </div>
    </ItemTemplate>
        </asp:Repeater>
        </div>
    </form>
</body>
</html>
