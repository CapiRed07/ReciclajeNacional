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
    public partial class Usuarios : SecurePage
    {
        public adminUsuarios()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarioLogueado();
            }
        }
        private void CargarComboProvincia()
        {
            try
            {
                var listaUsuarios = UsuariosLogica.ObtenerUsuarios();

                ddlProvincia.DataSource = listaUsuarios;

                ddlProvincia.DataTextField = "provincia";

                ddlProvincia.DataValueField = "provincia";

                ddlProvincia.DataBind();

                ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de materiales.";
                //lblError.Visible = true;
            }
}
        private void CargarUsuarioLogueado()
        {
            // Se llama a la logica para obtener el usuario logueado, se le envia el ID de SecurePage (UsuarioIdLogueado) y se obtiene el perfil del usuario logueado
            clsUsuarios perfilUsuario = logica.UsuariosLogica.ObtenerUsuarioLogueado(this.UsuarioIdLogueado);

            if(perfilUsuario != null )
            {
                // Se llena el label del inicio con el nombre del usuario logueado
                lblNombreUsuario.Text = perfilUsuario.nombre;

                // El repeater necesita una lista por fuerza, metemos el perfilUsuario en una lista para que funcione
                List<clsUsuarios> listaParaRepeater = new List<clsUsuarios>();
                listaParaRepeater.Add(perfilUsuario);

                // Se enlaza el repeater con la lista que contiene el perfil del usuario logueado
                rptBoxes.DataSource = listaParaRepeater;
                rptBoxes.DataBind();
            }
            else
            {
                // Manejo de errores si no se encuentra el usuario logueado
                lblNombreUsuario.Text = "Error al cargar el usuario logueado.";
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {

        }
    }
}