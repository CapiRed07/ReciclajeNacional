using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class RecompensasLogica
    {
        //CRUD y consulta general
        // Para agregar Recompensas nuevos al sistema, pensada para administradores
        public static int AgregarRecompensas(clsRecompensas NuevaRecompensa)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure
                    SqlCommand cmd = new SqlCommand("AgregarRecompensa", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Se agregan parametros al comando
                    cmd.Parameters.Add(new SqlParameter("@Nombre", NuevaRecompensa.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", NuevaRecompensa.descripcion));
                    cmd.Parameters.Add(new SqlParameter("@Puntos", NuevaRecompensa.puntosnecesarios));
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", NuevaRecompensa.disponible));

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
        public static List<clsRecompensas> ObtenerRecompensas()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos las recompensas
            List<clsRecompensas> listaRecompensas = new List<clsRecompensas>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarRecompensa", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsRecompensas recompensas = new clsRecompensas
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosnecesarios = reader.GetInt32(3),
                                disponible = reader.GetInt32(4)
                            };

                            // Pasamos los datos de cada recompensa a la lista
                            listaRecompensas.Add(recompensas);
                        }

                        // Los enviamos para el despliegue
                        return listaRecompensas;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaRecompensas;
            }
            finally
            {
                Conn.Close();
            }
        }
        // Metodo para borrar recompensas, pensado para administradores
        public static int BorrarRecompensas(clsRecompensas Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarRecompensa", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@RecompensaID", Eliminado.id));

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
                if (Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
        // Para consultar recompensas especificos, todos pueden acceder
        public static clsRecompensas ConsultaRecompensaporID(clsRecompensas RecompensaConsultado)
        {
            clsRecompensas RecompensaConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarRecompensaporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar la recompensa a consultar
                    cmd.Parameters.Add(new SqlParameter("@RecompensaID", RecompensaConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            RecompensaConsulta = new clsRecompensas
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosnecesarios = reader.GetInt32(3),
                                disponible = reader.GetInt32(4)
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
                if (Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
            return RecompensaConsulta;
        }
        // Funcion para modificar recompensas, pensada para administradores.
        public static int ModificarRecompensas(clsRecompensas RecompensaModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarRecompensa", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@RecompensaID", RecompensaModificar.id)); // Este es el identificador, no se cambia, busca
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", RecompensaModificar.descripcion));
                    cmd.Parameters.Add(new SqlParameter("@Puntos", RecompensaModificar.puntosnecesarios));
                    cmd.Parameters.Add(new SqlParameter("@Cantidad", RecompensaModificar.disponible));

                    // Conexion abierta y ejecucion
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