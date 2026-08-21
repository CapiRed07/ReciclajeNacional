<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication19.vista.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div>
            <asp:Label ID="lCorreo" runat="server" Text="Correo Electrónico"></asp:Label>
            <br />
            <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lPassword" runat="server" Text="Contraseña"></asp:Label>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
            <br />
            <asp:Label ID="LblCredentialsA" runat="server" Text="Credenciales: maria@email.com, 123 (admin)"></asp:Label>
            <br />
            <asp:Label ID="LblCredentialsB" runat="server" Text="Credenciales: juan@email.com, 123 (usuario)"></asp:Label>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
            <br />

        </div>
</asp:Content>
