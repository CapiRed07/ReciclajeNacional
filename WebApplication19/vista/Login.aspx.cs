using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;
using WebApplication19.modelo;
using System;

namespace WebApplication19.vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No requiere lógica de carga
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text;
            string password = txtPassword.Text;

            // 1. Le pedimos a la lógica que intente autenticar al usuario
            clsUsuarios usuarioAutenticado = logica.UsuariosLogica.SimularAutenticacion(correo, password);

            if (usuarioAutenticado != null)
            {
                // 2. Si las credenciales fueron correctas, llenamos la Sesión
                Session["IsLoggedIn"] = true;
                Session["UserID"] = usuarioAutenticado.id;
                Session["UserRol"] = usuarioAutenticado.rol;

                // 3. Redirigimos a la página de perfil que acabas de construir
                Response.Redirect("~/vista/Usuarios.aspx");
            }
            else
            {
                // Si la lógica devolvió null, mostramos error
                lblError.Text = "Correo o contraseña incorrectos.";
            }
        }
    }
}
