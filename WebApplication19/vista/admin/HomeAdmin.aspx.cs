using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public static bool LimpiarDatos()
        {
            return MantenimientoLogica.EjecutarProcedimiento("dbo.sp_LimpiarDatosPrueba");
        }

        public static bool InyectarDatos()
        {
            return MantenimientoLogica.EjecutarProcedimiento("dbo.sp_InyectarDatosPrueba");
        }

        protected void BtnClean_Click(object sender, EventArgs e)
        {
            if (LimpiarDatos())
            {
                LblMensaje.Text = "Tablas limpiadas con éxito.";
                LblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblMensaje.Text = "Hubo un error al limpiar los datos.";
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            if (InyectarDatos())
            {
                LblMensaje.Text = "Datos de simulación inyectados correctamente.";
                LblMensaje.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LblMensaje.Text = "Error al inyectar. Verifica que existan usuarios en la base de datos.";
                LblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Session.Abandon();

            Response.Redirect("~/vista/Login.aspx", true);
        }
    }
}