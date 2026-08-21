<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="RecompensasAdmin.aspx.cs" Inherits="WebApplication19.vista.admin.RecompensasAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:GridView ID="GridRecompensas" runat="server"></asp:GridView>
        <br />
        <asp:Label ID="LblMensaje" runat="server" Visible="false" Text=""></asp:Label>
        <br />
    <asp:Label ID="LblID" runat="server" Text="Ingrese un ID"></asp:Label>
    <br />
    <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblNombre" runat="server" Text="Nombre"></asp:Label>
    <br />
    <asp:TextBox ID="TxtNombre" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblDescripcion" runat="server" Text="Descripcion"></asp:Label>
    <br />
    <asp:TextBox ID="TxtDescripcion" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LblCant" runat="server" Text="Cantidad Disponible"></asp:Label>
    <br />
    <asp:TextBox ID="TxtCant" runat="server"></asp:TextBox>
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
