<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="CanjeAdmin.aspx.cs" Inherits="WebApplication19.vista.admin.CanjeAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:GridView ID="GridCanje" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="LblFKUsuarios" runat="server" Text="Usuarios"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFKUsuarios" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="LblFKRecompensa" runat="server" Text="Recompensas"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFKRecompensas" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="LblID" runat="server" Text="ID"></asp:Label>
        <br />
        <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LblCant" runat="server" Text="Cantidad"></asp:Label>
        <br />
        <asp:TextBox ID="TxtCant" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LblFecha" runat="server" Text="Fecha"></asp:Label>
        <br />
        <asp:TextBox ID="TxtFecha" runat="server" TextMode="date"></asp:TextBox>
        <br />
        <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" />
        <asp:Button ID="BtnBorrar" runat="server" Text="Borrar" OnClick="BtnBorrar_Click" />
        <asp:Button ID="BtnActualizar" runat="server" Text="Actualizar" OnClick="BtnActualizar_Click" />
        <asp:Button ID="BtnConsultar" runat="server" Text="Consultar por ID" OnClick="BtnConsultar_Click" />
        <asp:Button ID="BtnRefrescar" runat="server" Text="Refrescar" OnClick="BtnRefrescar_Click" />
    </div>
</asp:Content>