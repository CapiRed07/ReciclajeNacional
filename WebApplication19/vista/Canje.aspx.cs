using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;

namespace WebApplication19.vista
{
    public partial class Canje : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargarCanjes()
        {
            GridCanje.DataSource = CanjeLogica.ObtenerCanje();
            GridCanje.DataBind();
        }
        private void CargarComboCanjes()
        {
            try
            {
                //  Invoca al fetcher, trae la lista
                var listaRecompensas = RecompensasLogica.ObtenerRecompensas();

                // Se le asigna la fuente de datos al control
                ddlFKRecompensas.DataSource = listaRecompensas;

                // DataTextField nombre de la propiedad que se quiere mostrar al usuario
                ddlFKRecompensas.DataTextField = "nombre";

                // DataValueField nombre de la propiedad que guarda el ID (la FK)
                ddlFKRecompensas.DataValueField = "id";

                // Se enlazan los datos para que se dibuje el componente
                ddlFKRecompensas.DataBind();

                // Agregar una opción neutra al principio de la lista
                ddlFKRecompensas.Items.Insert(0, new ListItem("-- Seleccione un Material --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de materiales.";
                //lblError.Visible = true;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
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