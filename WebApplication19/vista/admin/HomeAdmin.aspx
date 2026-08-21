<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="HomeAdmin.aspx.cs" Inherits="WebApplication19.vista.admin.WebForm1Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="home">
            <h1>Sistema de reciclaje nacional</h1>
    </div>
        <div class="top-home">
    <h2>Bienvenido al sistema de Reciclaje</h2>
            <h3>Usted es un admin</h3>

    <p class="texto-home">
        Seleccione una opción para comenzar.
    </p>

    <div class="cards">

        <a href="~/vista/admin/CentrosAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/centroReciclaje.jpg"
                 class="card-image"
                 alt="Centros" />
            <h3>Centro de Reciclaje</h3>
            <p>Gestione los centros</p>
        </a>

        <a href="~/vista/admin/MaterialesAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/materiales.jpg"
                 class="card-image"
                 alt="Materiales" />
            <h3>Material</h3>
            <p>Gestione el material de reciclaje</p>
        </a>

        <a href="~/vista/admin/RegistrosAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/registros.jpg"
                 class="card-image"
                 alt="Registros" />
            <h3>Registro de reciclaje</h3>
            <p>Gestione los registros de reciclaje</p>
        </a>

        <a href="~/vista/admin/RecompensasAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/Rewards.jpg"
                 class="card-image"
                 alt="Recompensas" />
            <h3>Recompensas</h3>
            <p>Gestione las recompensas</p>
        </a>

        <a href="~/vista/admin/CanjeAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/Canje.jpg"
                 class="card-image"
                 alt="Canjeo" />
            <h3>Canje de Recompensas</h3>
            <p>Gestione el sistema de canjeo</p>
        </a>

        <a href="~/vista/admin/UsuariosAdmin.aspx" class="menu-card" runat="server">
            <img src="~/Images/usuarios.jpg"
                 class="card-image"
                 alt="Usuarios" />
            <h3>Usuario</h3>
            <p>Gestione perfiles, puntos y configuración</p>
        </a>

    </div>

</div>
    <div>

        <asp:Button ID="BtnInsert" runat="server" Text="Datos de prueba" OnClick="BtnInsert_Click" />
        <asp:Button ID="BtnClean" runat="server" Text="Limpiar todos los datos" OnClick="BtnClean_Click" />
        <asp:Button ID="BtnLogout" runat="server" Text="Cerrar Sesion" OnClick="BtnLogout_Click" />
        <br />
        <asp:Label ID="LblMensaje" runat="server" visible="false" Text=""></asp:Label>
    </div>
</asp:Content>