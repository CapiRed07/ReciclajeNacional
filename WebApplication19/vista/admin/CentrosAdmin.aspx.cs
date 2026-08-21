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

    public partial class Centros : SecurePage
    {
        public adminCentros()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCentros();
            }
        }
        private void CargarCentros()
        {
            GridCentros.DataSource = CentrosLogica.ObtenerCentros();
            GridCentros.DataBind();
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarCentros();
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
            clsCentros NuevaConsulta = new clsCentros { id = parsedID };

            // Consulta a la capa de logica
            clsCentros Consulta = CentrosLogica.ConsultaCentroporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridCentros.DataSource = new List<clsCentros> { Consulta };
                GridCentros.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridCentros.DataSource = null;
                GridCentros.DataBind();
                // lbl mensaje "no se encontro"
            }
        }
    }
}