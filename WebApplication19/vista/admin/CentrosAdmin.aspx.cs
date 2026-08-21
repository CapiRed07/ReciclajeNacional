using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;
using WebApplication19.modelo;

namespace WebApplication19.vista.admin
{

    public partial class CentrosAdmin : Centros
    {
        public void adminCentros()
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
                LblMensaje.Text = "Ingrese un ID valido (numeros enteros)";
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
                LblMensaje.Text = "No se encontro el centro. Esta seguro de que ese ID existe?";
                LblMensaje.Visible = false;
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
                NuevoGuardado.direccion = Convert.ToString(TxtDireccion.Text);
                NuevoGuardado.horario = Convert.ToString(TxtHorario.Text);

                // Se envia el registro a la base
                int resultado = CentrosLogica.AgregarCentros(NuevoGuardado);

                if (resultado > 0)
                {
                    LblMensaje.Text = "Centro borrado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    LblMensaje.Text = "No se pudo borrar al centro.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarCentros();
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
                int resultado = CentrosLogica.BorrarCentro(NuevoBorrado);

                if (resultado > 0)
                {
                    LblMensaje.Text = "Centro borrado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    if (resultado == -2)
                    {
                        LblMensaje.Text = "No puede borrar al centro, ya que tiene asignado algo, borre sus asignaciones primero";
                        LblMensaje.Visible = true;
                    }
                    LblMensaje.Text = "No se pudo borrar al centro.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarCentros();
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
                NuevoGuardado.direccion = Convert.ToString(TxtDireccion.Text);
                NuevoGuardado.horario = Convert.ToString(TxtHorario.Text);

                // Se envia el registro a la base
                int resultado = CentrosLogica.ModificarCentros(NuevoGuardado);

                if (resultado > 0)
                {
                    LblMensaje.Text = "Centro borrado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    LblMensaje.Text = "No se pudo borrar al centro.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarCentros();
        }
    }
}