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
    public partial class Usuarios : SecurePage
    {
        public adminUsuarios()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarioLogueado();
            }
        }

        private void CargarUsuarios()
        {
            GridUsuarios.DataSource = UsuariosLogica.ObtenerUsuarios();
            GridUsuarios.DataBind();
        }
        private void CargarComboProvincia()
        {
            try
            {
                var listaUsuarios = UsuariosLogica.ObtenerUsuarios();

                ddlProvincia.DataSource = listaUsuarios;

                ddlProvincia.DataTextField = "provincia";

                ddlProvincia.DataValueField = "provincia";

                ddlProvincia.DataBind();

                ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));
            }
            catch (Exception ex)
            {
                //lblError.Text = "Ocurrió un error al cargar el catálogo de Usuarios.";
                //lblError.Visible = true;
            }
}
        private void CargarUsuarioLogueado()
        {
            // Se llama a la logica para obtener el usuario logueado, se le envia el ID de SecurePage (UsuarioIdLogueado) y se obtiene el perfil del usuario logueado
            clsUsuarios perfilUsuario = logica.UsuariosLogica.ObtenerUsuarioLogueado(this.UsuarioIdLogueado);

            if(perfilUsuario != null )
            {
                // Se llena el label del inicio con el nombre del usuario logueado
                lblNombreUsuario.Text = perfilUsuario.nombre;

                // El repeater necesita una lista por fuerza, metemos el perfilUsuario en una lista para que funcione
                List<clsUsuarios> listaParaRepeater = new List<clsUsuarios>();
                listaParaRepeater.Add(perfilUsuario);

                // Se enlaza el repeater con la lista que contiene el perfil del usuario logueado
                rptBoxes.DataSource = listaParaRepeater;
                rptBoxes.DataBind();
            }
            else
            {
                // Manejo de errores si no se encuentra el usuario logueado
                lblNombreUsuario.Text = "Error al cargar el usuario logueado.";
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsUsuarios NuevoGuardado = new clsUsuarios();

                // Asignar valores
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.correo = Convert.ToString(TxtCorreo.Text);
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedValue);
                NuevoGuardado.puntos = Convert.ToInt32(TxtPuntos.Text);
                NuevoGuardado.rol = Convert.ToString(TxtRol.Text);

                // Se envia el registro a la base
                int resultado = UsuariosLogica.AgregarUsuarios(NuevoGuardado);

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
                clsUsuarios NuevoBorrado = new clsUsuarios();

                // Se le pasa el id del registro
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);

                // Enviar registro a base
                int resultado = UsuariosLogica.BorrarUsuarios(NuevoBorrado);

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
                clsUsuarios NuevoGuardado = new clsUsuarios();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.id = Convert.ToInt32(TxtID.Text);
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.correo = Convert.ToString(TxtCorreo.Text);
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedValue);
                NuevoGuardado.puntos = Convert.ToInt32(TxtPuntos.Text);
                NuevoGuardado.rol = Convert.ToString(TxtRol.Text);

                // Se envia el registro a la base
                int resultado = UsuariosLogica.ModificarUsuarios(NuevoGuardado);

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
            clsUsuarios NuevaConsulta = new clsUsuarios { id = parsedID };

            // Consulta a la capa de logica
            clsUsuarios Consulta = UsuariosLogica.ConsultaUsuarioporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridUsuarios.DataSource = new List<clsUsuarios> { Consulta };
                GridUsuarios.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridUsuarios.DataSource = null;
                GridUsuarios.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
            TxtID = "";
            TxtCorreo = "";
            TxtNombre = "";
            TxtPuntos = "";
            TxtRol = "";
        }
    }
}