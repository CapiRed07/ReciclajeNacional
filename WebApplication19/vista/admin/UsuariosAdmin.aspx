<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="WebApplication19.vista.admin.UsuariosAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/CSS/Usuarios.css" rel="stylesheet" type="text/css"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div>
            <h1>Usuario</h1>
        </div>
<div>
    <asp:GridView ID="GridUsuarios" runat="server"></asp:GridView>
    <br />
    <asp:Label ID="LblID" runat="server" Text="Ingrese un ID"></asp:Label>
    <br />
    <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblNombre" runat="server" Text="Nombre"></asp:Label>
    <br />
    <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblCorreo" runat="server" Text="Correo"></asp:Label>
    <br />
    <asp:TextBox ID="TxtCorreo" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblProvincia" runat="server" Text="Provincia"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlProvincia" runat="server"></asp:DropDownList>
    <br />
    <asp:Label ID="LblRol" runat="server" Text="Rol"></asp:Label>
    <br />
    <asp:TextBox ID="TxtRol" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblPuntos" runat="server" Text="Puntos"></asp:Label>
    <br />
    <asp:TextBox ID="TxtPuntos" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" OnClick="BtnAgregar_Click" />
    <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" OnClick="BtnEliminar_Click" />
    <asp:Button ID="BtnActualizar" runat="server" Text="Actualizar" OnClick="BtnActualizar_Click" />
    <asp:Button ID="BtnConsultar" runat="server" Text="Consultar Por ID" OnClick="BtnConsultar_Click" />
    <asp:Button ID="BtnRefrescar" runat="server" Text="Refrescar" OnClick="BtnRefrescar_Click" />
</div>

</asp:Content>