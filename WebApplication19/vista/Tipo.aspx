<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tipo.aspx.cs" Inherits="WebApplication19.vista.Tipo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label1> 
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <br />
            ID: </label1>
            <br />
            <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
            <label2> 
            <br />
            Nombre</label2>
            <br />
            <asp:TextBox ID="txtnombre" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="bagregar" runat="server" Text="Agregar" OnClick="bagregar_Click" />
            <asp:Button ID="bborrar" runat="server" Text="Borrar" OnClick="bborrar_Click" />


        </div>
    </form>
</body>
</html>
