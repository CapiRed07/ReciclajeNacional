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
    public partial class Registros : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRegistros();
            }
        }
        private void CargarRegistros()
        {
            GridRegistros.DataSource = RegistrosLogica.ObtenerRegistros();
            GridRegistros.DataBind();
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsRegistros NuevoGuardado = new clsRegistros();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.fkusuario = UsuarioIdLogueado;
                NuevoGuardado.fkmaterial = Convert.ToInt32(ddlFKMaterial.SelectedValue);
                NuevoGuardado.fkcentro = Convert.ToInt32(ddlFKCentros.SelectedValue);

                // Se convierten los datos numericos de los textboxes
                NuevoGuardado.cantidadkg = Convert.ToInt32(TxtKg.Text);
                NuevoGuardado.fecha = TxtFecha.Text;
                // Los puntos obtenidos se deben calcular

                // Se envia el registro a la base
                int resultado = RegistrosLogica.AgregarRegistros(NuevoGuardado);

                if (resultado > 0)
                {
                    // mensajes de exito
                }
                else
                {
                    // mensaje de error
                }
            }
            catch (Exception ex)
            {
                // Manejo de error, formato de los datos
            }
        }
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

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
            clsRegistros NuevaConsulta = new clsRegistros { id = parsedID };

            // Consulta a la capa de logica
            clsRegistros Consulta = RegistrosLogica.ConsultaRegistroporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridRegistros.DataSource = new List<clsRegistros> { Consulta };
                GridRegistros.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridRegistros.DataSource = null;
                GridRegistros.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarRegistros();
            TxtID.Text = "";
        }
    }
}