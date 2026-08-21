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

        private void CargarComboProvincia()
        {
            try
            {
                var listaCentros = CentrosLogica.ObtenerCentros();

                ddlProvincia.DataSource = listaCentros;

                ddlProvincia.DataTextField = "provincia";

                ddlProvincia.DataValueField = "provincia";

                ddlProvincia.DataBind();

                ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de Centros.";
                //lblError.Visible = true;
            }
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

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsCentros NuevoGuardado = new clsCentros();

                // Asignar valores
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedValue);
                NuevoGuardado.direccion = Convert.ToInt32(TxtDireccion.Text);
                NuevoGuardado.horario = Convert.ToString(TxtHorario.Text);

                // Se envia el registro a la base
                int resultado = CentrosLogica.AgregarCentros(NuevoGuardado);

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

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Instancia del objeto
                clsCentros NuevoBorrado = new clsCentros();

                // Se le pasa el id del registro
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);

                // Enviar registro a base
                int resultado = CentrosLogica.BorrarCentros(NuevoBorrado);

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
            try
            {
                // Se instancia el objeto
                clsCentros NuevoGuardado = new clsCentros();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.id = Convert.ToInt32(TxtID.Text);
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedValue);
                NuevoGuardado.direccion = Convert.ToInt32(TxtDireccion.Text);
                NuevoGuardado.horario = Convert.ToString(TxtHorario.Text);

                // Se envia el registro a la base
                int resultado = CentrosLogica.ModificarCentros(NuevoGuardado);

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
    }
}