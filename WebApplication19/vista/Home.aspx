<%@ Page Language="C#" MasterPageFile="~/Layout/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication19.vista.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Se puede dejar vacio sino se necesita css o javascript unicos -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="home">
            <h1>Sistema de reciclaje nacional</h1>
    </div>
        <div class="top-home">
    <h2>Bienvenido al sistema de Reciclaje</h2>

    <p class="texto-home">
        Seleccione una opción para comenzar.
    </p>

    <div class="cards">

        <a href="~/vista/Centros.aspx" class="menu-card">
            <img src="../Images/centroReciclaje.jpg"
                 class="card-image"
                 alt="Centros" />
            <h3>Centro de Reciclaje</h3>
            <p>Consulte por el centro más cercano</p>
        </a>

        <a href="~/vista/Materiales.aspx" class="menu-card">
            <img src="../Images/materiales.jpg"
                 class="card-image"
                 alt="Materiales" />
            <h3>Material</h3>
            <p>Consulte por el material de reciclaje</p>
        </a>

        <a href="~/vista/Registros.aspx" class="menu-card">
            <img src="../Images/registros.jpg"
                 class="card-image"
                 alt="Registros" />
            <h3>Registro de reciclaje</h3>
            <p>Consulte y gestione sus registros de reciclaje</p>
        </a>

        <a href="~/vista/Recompensas.aspx" class="menu-card">
            <img src="../Images/Rewards.jpg"
                 class="card-image"
                 alt="Recompensas" />
            <h3>Recompensas</h3>
            <p>Consulte nuestras recompensas</p>
        </a>

        <a href="~/vista/Canje.aspx" class="menu-card">
            <img src="../Images/Canje.jpg"
                 class="card-image"
                 alt="Canjeo" />
            <h3>Canje de Recompensas</h3>
            <p>Utilice sus puntos para canjear recompensas</p>
        </a>

        <a href="~/vista/Usuarios.aspx" class="menu-card" runat="server">
            <img src="../Images/usuarios.jpg"
                 class="card-image"
                 alt="Usuarios" />
            <h3>Usuario</h3>
            <p>Gestione su perfil, puntos y configuración</p>
        </a>

    </div>

</div>
</asp:Content>