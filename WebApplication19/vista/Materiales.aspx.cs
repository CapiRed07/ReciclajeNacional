using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;

namespace WebApplication19.vista
{
    public partial class Materiales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarMateriales();
        }
        private void CargarMateriales()
        {
            GridMateriales.DataSource = MaterialesLogica.ObtenerMateriales();
            GridMateriales.DataBind();
        }
    }
}