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
    public partial class Recompensas : System.Web.UI.Page
    {
        public adminRecompensas()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRecompensas();
            }
        }

        private void CargarRecompensas()
        {
            GridRecompensas.DataSource = RecompensasLogica.ObtenerRecompensas();
            GridRecompensas.DataBind();
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
            clsRecompensas NuevaConsulta = new clsRecompensas { id = parsedID };

            // Consulta a la capa de logica
            clsRecompensas Consulta = RecompensasLogica.ConsultaRecompensaporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridRecompensas.DataSource = new List<clsRecompensas> { Consulta };
                GridRecompensas.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridRecompensas.DataSource = null;
                GridRecompensas.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarRecompensas();
            TxtID.Text = "";
        }
    }
}