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
    public partial class Canje : SecurePage
    {
        public adminCanje()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarCanjes();
                CargarComboRecompensas();
            }
        }

        private void CargarCanjes()
        {
            GridCanje.DataSource = CanjeLogica.ObtenerCanje();
            GridCanje.DataBind();
        }
        private void CargarComboRecompensas()
        {
            try
            {
                //  Invoca al fetcher, trae la lista
                var listaRecompensas = RecompensasLogica.ObtenerRecompensas();

                // Se le asigna la fuente de datos al control
                ddlFKRecompensas.DataSource = listaRecompensas;

                // DataTextField nombre de la propiedad que se quiere mostrar al usuario
                ddlFKRecompensas.DataTextField = "nombre";

                // DataValueField nombre de la propiedad que guarda el ID (la FK)
                ddlFKRecompensas.DataValueField = "id";

                // Se enlazan los datos para que se dibuje el componente
                ddlFKRecompensas.DataBind();

                // Agregar una opción neutra al principio de la lista
                ddlFKRecompensas.Items.Insert(0, new ListItem("-- Seleccione un Material --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de materiales.";
                //lblError.Visible = true;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsCanje NuevoGuardado = new clsCanje();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.fkusuario = UsuarioIdLogueado;
                NuevoGuardado.fkrecompensa = Convert.ToInt32(ddlFKRecompensas.SelectedValue);

                // Se convierten los datos numericos de los textboxes
                NuevoGuardado.fecha = Convert.ToDateTime(TxtFecha.Text);
                NuevoGuardado.cantidad = Convert.ToInt32(TxtCant.Text);
                // Los puntos obtenidos se deben calcular
                NuevoGuardado.puntosutilizados = CanjeLogica.CalcularPuntosCanje(NuevoGuardado.fkrecompensa, NuevoGuardado.cantidad);

                // Se envia el canje a la base
                int resultado = CanjeLogica.AgregarCanje(NuevoGuardado);

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
                clsCanje NuevoModificado = new clsCanje();

                // Se convierten los valores de los ddl a enteros
                NuevoModificado.id = Convert.ToInt32(TxtID.Text);
                NuevoModificado.fkusuario = UsuarioIdLogueado;
                NuevoModificado.fkrecompensa = Convert.ToInt32(ddlFKRecompensas.SelectedValue);

                // Se convierten los datos numericos de los textboxes
                NuevoModificado.fecha = Convert.ToDateTime(TxtFecha.Text);
                // Los puntos obtenidos se deben calcular

                // Se envia el canje a la base
                int resultado = CanjeLogica.ModificarCanjes(NuevoModificado);

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

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Instancia del objeto
                clsCanje NuevoBorrado = new clsCanje();

                // Se le pasa el id del canje
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);
                NuevoBorrado.fkusuario = UsuarioIdLogueado;

                // Enviar canje a base
                int resultado = CanjeLogica.BorrarCanjes(NuevoBorrado);

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

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtID.Text, out int parsedID))
            {
                // Manejo de errores
                // Agregar lbl
                return;
            }

            // Inicializacion limpia con objeto id
            clsCanje NuevaConsulta = new clsCanje { id = parsedID };

            // Consulta a la capa de logica
            clsCanje Consulta = CanjeLogica.ConsultaCanjeporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridCanje.DataSource = new List<clsCanje> { Consulta };
                GridCanje.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridCanje.DataSource = null;
                GridCanje.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarCanjes();
            TxtID.Text = "";
        }
    }
}