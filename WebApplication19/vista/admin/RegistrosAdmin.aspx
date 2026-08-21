<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="RegistrosAdmin.aspx.cs" Inherits="WebApplication19.vista.admin.RegistrosAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:GridView ID="GridRegistros" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="LblMensaje" runat="server" Visible="false" Text=""></asp:Label>
        <br />
        <asp:Label ID="LblFKUsuarios" runat="server" Text="Usuarios"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFKUsuarios" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="LblFKMaterial" runat="server" Text="Materiales"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFKMaterial" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="LblCentros" runat="server" Text="Centros"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlFKCentros" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="LblID" runat="server" Text="Ingrese un ID"></asp:Label>
        <br />
        <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LblKg" runat="server" Text="Cantidad de Kilogramos"></asp:Label>
        <br />
        <asp:TextBox ID="TxtKg" runat="server"></asp:TextBox>
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