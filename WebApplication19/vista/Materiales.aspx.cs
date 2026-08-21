using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;
using WebApplication19.modelo;
namespace WebApplication19.vista
{
    public partial class Materiales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarMateriales();
            }
        }
        private void CargarMateriales()
        {
            GridMateriales.DataSource = MaterialesLogica.ObtenerMateriales();
            GridMateriales.DataBind();
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarMateriales();
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            int parsedID; // Variable temporal para convertir a integer
            clsMateriales NuevaConsulta = new clsMateriales();
            if (int.TryParse(TxtID.Text, out parsedID))
            {
                // Si logra assignar, se va al objeto
                NuevaConsulta.id = parsedID;
                clsMateriales Consulta = MaterialesLogica.ConsultaMaterialporID(NuevaConsulta);
                GridMateriales.DataSource = Consulta;
                GridMateriales.DataBind();
            }
            else
            {
                // Manejo de errores
                // Escribir Logica
                return;
            }
        }
    }
}