using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication19.logica;
namespace WebApplication19.Layout
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public void Page_Load(object sender, EventArgs e)
        {
            // Se obtiene pagina actual
            string paginaActual = Request.Url.AbsolutePath.ToLower();
            // Devuelve false en paginas sin navegacion o no logueadas.
            globalHeader.Visible = MasterLogica.NavOculta(paginaActual);
        }
    }
}