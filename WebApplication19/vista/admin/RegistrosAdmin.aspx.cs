using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public adminRegistros()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRegistros();
                CargarComboCentros();
                CargarComboMateriales();
            }
        }
        private void CargarRegistros()
        {
            GridRegistros.DataSource = RegistrosLogica.ObtenerRegistros();
            GridRegistros.DataBind();
        }

        private void CargarUsuarios()
        {
            try
            {
                var listaUsuarios = UsuariosLogica.ObtenerUsuarios();

                ddlFKUsuarios.DataSource = listaUsuarios;

                ddlFKUsuarios.DataTextField = "nombre";

                ddlFKUsuarios.DataValueField = "id";

                ddlFKUsuarios.DataBind();

                ddlFKUsuarios.Items.Insert(0, new ListItem("-- Seleccione un usuario --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de Usuarios.";
                //lblError.Visible = true;
            }
        }
        private void CargarComboMateriales()
        {
            try
            {
                //  Invoca al fetcher, trae la lista
                var listaMateriales = MaterialesLogica.ObtenerMateriales();

                // Se le asigna la fuente de datos al control
                ddlFKMaterial.DataSource = listaMateriales;

                // DataTextField nombre de la propiedad que se quiere mostrar al usuario
                ddlFKMaterial.DataTextField = "nombre";

                // DataValueField nombre de la propiedad que guarda el ID (la FK)
                ddlFKMaterial.DataValueField = "id";

                // Se enlazan los datos para que se dibuje el componente
                ddlFKMaterial.DataBind();

                // Agregar una opción neutra al principio de la lista
                ddlFKMaterial.Items.Insert(0, new ListItem("-- Seleccione un Material --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de materiales.";
                //lblError.Visible = true;
            }
        }
        private void CargarComboCentros()
        {
            try
            {
                // Se invoca al fetcher
                var listaCentros = CentrosLogica.ObtenerCentros();

                ddlFKCentros.DataSource = listaCentros;

                // 2. Mapea según las propiedades de la clase clsCentros
                ddlFKCentros.DataTextField = "nombre";
                ddlFKCentros.DataValueField = "id";

                ddlFKCentros.DataBind();

                ddlFKCentros.Items.Insert(0, new ListItem("-- Seleccione un Centro --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de centros.";
                //lblError.Visible = true;
            }
        }
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsRegistros NuevoGuardado = new clsRegistros();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.fkusuario = Convert.ToInt32(ddlFKUsuarios.SelectedValue);
                NuevoGuardado.fkmaterial = Convert.ToInt32(ddlFKMaterial.SelectedValue);
                NuevoGuardado.fkcentro = Convert.ToInt32(ddlFKCentros.SelectedValue);

                // Se convierten los datos numericos de los textboxes
                NuevoGuardado.cantidadkg = Convert.ToInt32(TxtKg.Text);
                NuevoGuardado.fecha = Convert.ToDateTime(TxtFecha.Text);
                // Los puntos obtenidos se deben calcular
                NuevoGuardado.puntosobtenidos = RegistrosLogica.CalcularPuntos(NuevoGuardado.fkmaterial, NuevoGuardado.cantidadkg);

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
            try
            {
                // Se instancia el objeto
                clsRegistros NuevoModificado = new clsRegistros();

                // Se convierten los valores de los ddl a enteros
                NuevoModificado.id = Convert.ToInt32(TxtID.Text);
                NuevoModificado.fkusuario = Convert.ToInt32(ddlFKUsuarios.SelectedValue);
                NuevoModificado.fkmaterial = Convert.ToInt32(ddlFKMaterial.SelectedValue);
                NuevoModificado.fkcentro = Convert.ToInt32(ddlFKCentros.SelectedValue);

                // Se convierten los datos numericos de los textboxes
                NuevoModificado.cantidadkg = Convert.ToInt32(TxtKg.Text);
                NuevoModificado.fecha = Convert.ToDateTime(TxtFecha.Text);
                // Los puntos obtenidos se deben calcular

                // Se envia el registro a la base
                int resultado = RegistrosLogica.ModificarRegistros(NuevoModificado);

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
                clsRegistros NuevoBorrado = new clsRegistros();

                // Se le pasa el id del registro
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);
                NuevoBorrado.fkusuario = Convert.ToInt32(ddlFKUsuarios.SelectedValue);

                // Enviar registro a base
                int resultado = RegistrosLogica.BorrarRegistros(NuevoBorrado);

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