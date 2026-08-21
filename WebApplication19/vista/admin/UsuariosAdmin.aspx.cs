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
    public partial class UsuariosAdmin : Usuarios
    {
        public void adminUsuarios()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            GridUsuarios.DataSource = UsuariosLogica.ObtenerUsuarios();
            GridUsuarios.DataBind();
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
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedItem.Text);
                NuevoGuardado.pwd = Convert.ToString(Txtpwd.Text);
                NuevoGuardado.puntos = Convert.ToInt32(TxtPuntos.Text);
                NuevoGuardado.rol = Convert.ToString(TxtRol.Text);

                // Se envia el registro a la base
                int resultado = UsuariosLogica.AgregarUsuario(NuevoGuardado);

                if (resultado > 0)
                {
                    LblMensaje.Text = "Usuario guardado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    LblMensaje.Text = "No se pudo guardar al usuario.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarUsuarios();
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
                    LblMensaje.Text = "Usuario borrado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    if (resultado == -2)
                    {
                        LblMensaje.Text = "No puede borrar al usuario, ya que tiene asignado algo, borre sus asignaciones primero";
                        LblMensaje.Visible = true;
                    }
                    LblMensaje.Text = "No se pudo borrar al usuario.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarUsuarios();
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
                NuevoGuardado.pwd = Convert.ToString(Txtpwd.Text);
                NuevoGuardado.provincia = Convert.ToString(ddlProvincia.SelectedItem.Text);
                NuevoGuardado.puntos = Convert.ToInt32(TxtPuntos.Text);
                NuevoGuardado.rol = Convert.ToString(TxtRol.Text);

                // Se envia el registro a la base
                int resultado = UsuariosLogica.ModificarUsuarios(NuevoGuardado);

                if (resultado > 0)
                {
                    LblMensaje.Text = "Usuario modificado con exito.";
                    LblMensaje.Visible = true;
                }
                else
                {
                    LblMensaje.Text = "No se pudo modificar al usuario.";
                    LblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Asegúrese de dar los datos de forma correcta (los ID son enteros, no use caracteres)";
                LblMensaje.Visible = true;
            }
            CargarUsuarios();
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtID.Text, out int parsedID))
            {
                // Manejo de errores
                LblMensaje.Text = "Error, recuerde que los ID son números enteros";
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
                LblMensaje.Text = "El usuario no se encontró. Está seguro que existe ese ID?";
                LblMensaje.Visible = true;
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
    }
}