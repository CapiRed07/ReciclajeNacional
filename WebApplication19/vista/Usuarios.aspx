<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebApplication19.vista.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/Usuarios.css" rel="stylesheet" type="text/css"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <h1>Usuario</h1>
        </div>
<div>
    
    <asp:Label ID="lblNombreUsuario" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:Repeater ID="rptBoxes" runat="server">
        <HeaderTemplate>
            <!-- Abre el contenedor principal una sola vez -->
            <div class="box-container"> 
        </HeaderTemplate>
        
        <ItemTemplate>
            <!-- Esto se repetirá para cada fila de SQL Server -->
            <div class="data-box">
                <h3><%# Eval("nombre") %></h3>
                <p><strong>Email:</strong> <%# Eval("correo") %></p>
                <p><strong>Province:</strong> <%# Eval("provincia") %></p>
                <p><strong>Rol:</strong> <%# Eval("rol") %></p>
                <p class="points-badge"><strong>Points:</strong> <%# Eval("puntos") %></p>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            </div> 
        </FooterTemplate>
    </asp:Repeater>
</div>

</asp:Content>