<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Tipo.aspx.cs" Inherits="WebApplication19.vista.Tipo" %>

<!DOCTYPE html>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
</asp:Content>
