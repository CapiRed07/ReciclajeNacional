using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;

namespace WebApplication19.vista.admin
{
    public partial class WebForm1Admin : WebForm1
    {
        public void adminHome()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}