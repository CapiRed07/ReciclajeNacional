using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;
using WebApplication19.modelo;
namespace WebApplication19.vista
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Clear();
                Session.Abandon();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Se limpia mensaje anterior
            lblError.Visible = false;

            // Crear una instancia de clsUsuarios con los datos ingresados por el usuario
            clsUsuarios nuevoLogin = new clsUsuarios();
            nuevoLogin.correo = txtCorreo.Text.Trim();
            nuevoLogin.pwd = txtPassword.Text;

            try
            {
                // Manejo de campos vacios
                if (string.IsNullOrWhiteSpace(nuevoLogin.correo) || string.IsNullOrWhiteSpace(nuevoLogin.pwd))
                {
                    lblError.Text = "Por favor, complete todos los campos.";
                    lblError.Visible = true;
                    return;
                }
                // Se le pasa el objeto nuevoLogin a la función ValidarLogin para verificar si el login es correcto
                if (UsuariosLogica.ValidarLogin(nuevoLogin))
                {
                    // Se guardan los datos de la sesion
                    Session["UsuarioID"] = nuevoLogin.id;
                    Session["UsuarioNombre"] = nuevoLogin.nombre;
                    Session["UsuarioRol"] = nuevoLogin.rol;
                    Session["UsuarioCorreo"] = nuevoLogin.correo;
                    Session["EstaLogueado"] = true;
                    if (nuevoLogin.rol != null && nuevoLogin.rol.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {

                        Response.Redirect("~/vista/admin/HomeAdmin.aspx");
                    }
                    else
                    {
                        // Login exitoso
                        Response.Redirect("Home.aspx");
                    }
                }
                else
                {
                    // Login fallido
                    lblError.Text = "Correo o contraseña incorrectos.";
                    lblError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                lblError.Text = "Ocurrió un error durante el inicio de sesión. Por favor, inténtelo de nuevo más tarde.";
                lblError.Visible = true;
            }
        }
    }
}