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
    public partial class RecompensasAdmin : Recompensas 
    {
        public void adminRecompensas()
        {
            RolRequerido = "admin";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRecompensas();
            }
        }

        private void CargarRecompensas()
        {
            GridRecompensas.DataSource = RecompensasLogica.ObtenerRecompensas();
            GridRecompensas.DataBind();
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
            clsRecompensas NuevaConsulta = new clsRecompensas { id = parsedID };

            // Consulta a la capa de logica
            clsRecompensas Consulta = RecompensasLogica.ConsultaRecompensaporID(NuevaConsulta);

            // Validacion de datos y asignacion al GridView
            if (Consulta != null)
            {
                // Lista para que gridview lo pueda usar
                GridRecompensas.DataSource = new List<clsRecompensas> { Consulta };
                GridRecompensas.DataBind();
            }
            else
            {
                // Limpiar el grid sino hay resultados
                GridRecompensas.DataSource = null;
                GridRecompensas.DataBind();
                // lbl mensaje "no se encontro"
            }
        }

        protected void BtnRefrescar_Click(object sender, EventArgs e)
        {
            CargarRecompensas();
            TxtID.Text = "";
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se instancia el objeto
                clsRecompensas NuevoGuardado = new clsRecompensas();

                // Asignar valores
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.descripcion = Convert.ToString(TxtDescripcion.Text);
                NuevoGuardado.disponible = Convert.ToInt32(TxtCant.Text);
                NuevoGuardado.puntosnecesarios = Convert.ToInt32(TxtPuntos.Text);

                // Se envia el registro a la base
                int resultado = RecompensasLogica.AgregarRecompensas(NuevoGuardado);

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
                clsRecompensas NuevoBorrado = new clsRecompensas();

                // Se le pasa el id del registro
                NuevoBorrado.id = Convert.ToInt32(TxtID.Text);

                // Enviar registro a base
                int resultado = RecompensasLogica.BorrarRecompensas(NuevoBorrado);

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
                clsRecompensas NuevoGuardado = new clsRecompensas();

                // Se convierten los valores de los ddl a enteros
                NuevoGuardado.id = Convert.ToInt32(TxtID.Text);
                NuevoGuardado.nombre = Convert.ToString(TxtNombre.Text);
                NuevoGuardado.descripcion = Convert.ToString(TxtDescripcion.Text);
                NuevoGuardado.disponible = Convert.ToInt32(TxtCant.Text);
                NuevoGuardado.puntosnecesarios = Convert.ToInt32(TxtPuntos.Text);

                // Se envia el registro a la base
                int resultado = RecompensasLogica.ModificarRecompensas(NuevoGuardado);

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