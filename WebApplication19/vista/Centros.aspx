<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Centros.aspx.cs" Inherits="WebApplication19.vista.Centros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:GridView ID="GridCentros" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="LblID" runat="server" Text="Ingrese un ID"></asp:Label>
        <br />
        <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar por ID" />
        <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" />
    </div>
</asp:Content>

