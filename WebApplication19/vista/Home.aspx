<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication19.vista.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reciclaje Nacional</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="home">
            <h1>Sistema de reciclaje nacional</h1>
    </div>
        <div class="top-home">
    <h2>Bienvenido al sistema de Reciclaje</h2>

    <p class="texto-home">
        Seleccione una opción para comenzar.
    </p>

    <div class="cards">

        <a href="" class="menu-card">
            <img src="<%=%>"
                 class="card-image"
                 alt="Centros" />
            <h3>Centro de Reciclaje</h3>
            <p>Consulte por el centro más cercano</p>
        </a>

        <a href="" class="menu-card">
            <img src="<%=%>"
                 class="card-image"
                 alt="Materiales" />
            <h3>Material</h3>
            <p>Consulte por el material de reciclaje</p>
        </a>

        <a href="" class="menu-card">
            <img src="<%=%>"
                 class="card-image"
                 alt="Registros" />
            <h3>Registro de reciclaje</h3>
            <p>Consulte y gestione sus registros de reciclaje</p>
        </a>

        <a href="" class="menu-card">
            <img src="<%=%>"
                 class="card-image"
                 alt="Recompensas" />
            <h3>Recompensas</h3>
            <p>Consulte nuestras recompensas</p>
        </a>

        <a href="" class="menu-card">
            <img src="<%=%>"
                 class="card-image"
                 alt="Canjeo" />
            <h3>Canje de Recompensas</h3>
            <p>Utilice sus puntos para canjear recompensas</p>
        </a>

        <a href="" class="menu-card">
            <img src="<%= %>"
                 class="card-image"
                 alt="Usuarios" />
            <h3>Usuario</h3>
            <p>Gestione su perfil, puntos y configuración</p>
        </a>

    </div>

</div>
    </form>
</body>
</html>
