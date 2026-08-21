<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Recompensas.aspx.cs" Inherits="WebApplication19.vista.Recompensas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:GridView ID="GridRecompensas" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="LblID" runat="server" Text="Ingrese un ID"></asp:Label>
        <br />
        <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="BtnConsultar" runat="server" Text="Consultar por ID" OnClick="BtnConsultar_Click" />
        <asp:Button ID="BtnRefrescar" runat="server" Text="Refrescar" OnClick="BtnRefrescar_Click" />
    </div>
</asp:Content>
