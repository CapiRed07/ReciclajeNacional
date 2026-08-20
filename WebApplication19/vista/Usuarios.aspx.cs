using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication19.vista
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarioLogueado();
            }
        }
        private void CargarUsuarioLogueado()
        {
            rptBoxes.DataSource = logica.UsuariosLogica.ObtenerUsuarioLogueado();
            rptBoxes.DataBind();
        }
    }
}