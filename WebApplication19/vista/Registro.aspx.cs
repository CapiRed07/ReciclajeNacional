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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Instancia, se le pasan los valores de los campos del formulario al objeto
            clsUsuarios nuevoRegistro = new clsUsuarios();
            nuevoRegistro.nombre = txtNombre.Text;
            nuevoRegistro.correo = txtCorreo.Text;
            nuevoRegistro.provincia = ddlProvincia.SelectedItem.Text;
            nuevoRegistro.pwd = txtPassword.Text;

            // Llamada al método para registrar el usuario
            try
            {
                if(string.IsNullOrWhiteSpace(nuevoRegistro.nombre) || string.IsNullOrWhiteSpace(nuevoRegistro.correo) || string.IsNullOrWhiteSpace(nuevoRegistro.pwd))
                {
                    lblError.Text = "Por favor, complete todos los campos obligatorios.";
                    lblError.Visible = true;
                    return;
                }
                UsuariosLogica.AgregarUsuario(nuevoRegistro);
                lblError.Text = "Usuario registrado correctamente.";
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Visible = true;

            }
            catch (Exception ex)
            {
                lblError.Text = "Error al registrar el usuario: " + ex.Message;
                lblError.Visible = true;
            }
        }
    }
}