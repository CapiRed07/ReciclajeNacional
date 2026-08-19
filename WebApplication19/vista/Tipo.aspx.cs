using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.modelo;

namespace WebApplication19.vista
{
    public partial class Tipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                CargarTipos();
            }
        }

        private void CargarTipos()
        {
            List<cls_tipo> tipos = logica.logica_tipo.ObtenerTipos();

            GridView1.DataSource = tipos;
            GridView1.DataBind();
        }
        protected void bagregar_Click(object sender, EventArgs e)
        {
            if (logica.logica_tipo.AgregarTipos(txtnombre.Text) > 0)
            {
                // Manejar éxito
                CargarTipos();
            }
            else
            {
                // Manejar error
            }
        }

        protected void bborrar_Click(object sender, EventArgs e)
        {
            if (logica.logica_tipo.BorrarTipos(Convert.ToInt32(txtid.Text)) > 0)
            {
                // Manejar éxito
                CargarTipos();
            }
            else
            {
                // Manejar error
            }
        }
    }
}