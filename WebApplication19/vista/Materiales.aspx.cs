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
            TxtID.Text = "";
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtID.Text, out int parsedID))
            {
                // Manejo de errores
                // Agregar lbl
                return;
            }

            // Inicializacion limpia con objeto id
            clsMateriales NuevaConsulta = new clsMateriales { id = parsedID};
            
            // Consulta a la capa de logica
            clsMateriales Consulta = MaterialesLogica.ConsultaMaterialporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if(Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridMateriales.DataSource = new List<clsMateriales> { Consulta };
                GridMateriales.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridMateriales.DataSource = null;
                GridMateriales.DataBind();
                // lbl mensaje "no se encontro"
            }
        }
    }
}