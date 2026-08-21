using System;
using System.Collections.Generic;
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
    }
    // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsRecompensas> ObtenerRecompensas()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los recompensas
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
    }
}