using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication19.logica;
using WebApplication19.modelo;
namespace WebApplication19.vista.admin
{
    public partial class MaterialesAdmin : Materiales 
    {
        public void adminMateriales()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarMateriales();
            }
        }
        private void CargarMateriales()
        {
            GridMateriales.DataSource = MaterialesLogica.ObtenerMateriales();
            GridMateriales.DataBind();
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarMateriales();
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
            clsMateriales NuevaConsulta = new clsMateriales { id = parsedID};
            
            // Consulta a la capa de logica
            clsMateriales Consulta = MaterialesLogica.ConsultaMaterialporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if(Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridMateriales.DataSource = new List<clsMateriales> { Consulta };
                GridMateriales.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridMateriales.DataSource = null;
                GridMateriales.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsMateriales NuevoGuardado = new clsMateriales();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.descripcion = Convert.ToString(TxtDescripcion.Text);

                // Se convierten los datos numericos de los textboxes
                NuevoGuardado.puntosporkg = Convert.ToInt32(TxtPuntos.Text);

                // Se envia el registro a la base
                int resultado = MaterialesLogica.AgregarMateriales(NuevoGuardado);

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
            CargarMateriales();
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Instancia del objeto
                clsMateriales NuevoBorrado = new clsMateriales();

                // Se le pasa el id del registro
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);

                // Enviar registro a base
                int resultado = MaterialesLogica.BorrarMateriales(NuevoBorrado);

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
            CargarMateriales();
        }

        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsMateriales NuevoGuardado = new clsMateriales();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.id = Convert.ToInt32(TxtID.Text);
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.descripcion = Convert.ToString(TxtDescripcion.Text);

                // Se convierten los datos numericos de los textboxes
                NuevoGuardado.puntosporkg = Convert.ToInt32(TxtPuntos.Text);

                // Se envia el registro a la base
                int resultado = MaterialesLogica.ModificarMateriales(NuevoGuardado);

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
            CargarMateriales();
        }
    }
}