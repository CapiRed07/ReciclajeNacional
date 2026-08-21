using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class MaterialesLogica
    {
        // Para agregar materiales nuevos al sistema, pensada para administradores
        public static int AgregarMateriales(clsMateriales NuevoMaterial)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure
                    SqlCommand cmd = new SqlCommand("AgregarMaterial", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Se agregan parametros al comando
                    cmd.Parameters.Add(new SqlParameter("@Nombre", NuevoMaterial.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", NuevoMaterial.descripcion));
                    cmd.Parameters.Add(new SqlParameter("@PuntosporKG", NuevoMaterial.puntosporkg));

                    //Devuelve el numero de filas afectadas, 1 si tuvo exito
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores
                return -1; //En caso de error
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
            return retorno;
        }
        // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsMateriales> ObtenerMateriales()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los materiales
            List<clsMateriales> listaMateriales = new List<clsMateriales>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarMaterial", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsMateriales materiales = new clsMateriales
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosporkg = reader.GetDecimal(3),
                            };

                            // Pasamos los datos de cada material a la lista
                            listaMateriales.Add(materiales);
                        }

                        // Los enviamos para el despliegue
                        return listaMateriales;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaMateriales;
            }
            finally
            {
                Conn.Close();
            }
        }

        // Metodo para borrar materiales, pensado para administradores
        public static int BorrarMateriales(clsMateriales Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarMaterial", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@MaterialID", Eliminado.id));

                    Conn.Open();
                    retorno = cmd.ExecuteNonQuery();
                    // Si se logra, se asignan las filas afectadas, cambiando a 1
                    return retorno;
                }
            }
            catch (Exception Ex)
            {
                // Manejo de errores
                return 0;
            }
            finally
            {
                if( Conn != null )
                Conn.Close();
                Conn.Dispose();
            }
        }
        // Para consultar materiales especificos, todos pueden acceder
        public static clsMateriales ConsultaMaterialporID(clsMateriales MaterialConsultado)
        {
            clsMateriales MaterialConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarMaterialporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar el material a consultar
                    cmd.Parameters.Add(new SqlParameter("@MaterialID", MaterialConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            MaterialConsulta = new clsMateriales
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosporkg = reader.GetDecimal(3)
                            };
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                // Manejo de errores
                return null;
            }
            finally
            {
                if(Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
            return MaterialConsulta;
        }
        // Funcion para modificar materiales, pensada para administradores.
        public static int ModificarMateriales(clsMateriales MaterialModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarMaterial", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@MaterialID", MaterialModificar.id)); // Este es el identificador, no se cambia, busca
                    cmd.Parameters.Add(new SqlParameter("@Nombre", MaterialModificar.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", MaterialModificar.descripcion));
                    cmd.Parameters.Add(new SqlParameter("@PuntosporKG", MaterialModificar.puntosporkg));

                    // Conexion abierta y ejecucion
                    Conn.Open();
                    retorno = cmd.ExecuteNonQuery();

                    return retorno; //Si modifica, se cambia a 1
                }
            }
            catch (Exception Ex)
            {
                // Manejo de errores
                retorno = 0;
            }
            return retorno;
        }
    }
}