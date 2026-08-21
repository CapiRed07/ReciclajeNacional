using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class CanjeLogica
    {
         // Todos los metodos estan pensados para cualquier usuario
         // Los usuarios sin permisos solo pueden modificar tablas
         // Para agregar canjes nuevos al sistema
         public static int AgregarCanje(clsCanje NuevoCanje)
         {
             int retorno = 0;
             SqlConnection Conn = new SqlConnection();

             try
             {
                 using (Conn = modelo.DBconn.obtenerConexion())
                 {
                     //Se llama al stored procedure
                     SqlCommand cmd = new SqlCommand("AgregarCanje", Conn)
                     {
                        CommandType = System.Data.CommandType.StoredProcedure
                     };
                     // Se agregan parametros al comando
                     cmd.Parameters.Add(new SqlParameter("@UsuarioID", NuevoCanje.fkusuario));
                     cmd.Parameters.Add(new SqlParameter("@RecompensaID", NuevoCanje.fkrecompensa));
                     cmd.Parameters.Add(new SqlParameter("@Fecha", NuevoCanje.fecha));
                     cmd.Parameters.Add(new SqlParameter("@PuntosUtilizados", NuevoCanje.puntosutilizados));

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
        
        // Procedimiento para obtener todas las filas
        public static List<clsCanje> ObtenerCanje()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los canjes
            List<clsCanje> listaCanje = new List<clsCanje>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarCanje", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsCanje canjes = new clsCanje
                            {
                                id = reader.GetInt32(0),
                                fkusuario = reader.GetInt32(1),
                                fkrecompensa = reader.GetInt32(2),
                                fecha = reader.GetDateTime(3),
                                puntosutilizados = reader.GetInt32(4)
                            };

                            // Pasamos los datos de cada canje a la lista
                            listaCanje.Add(canjes);
                        }

                        // Los enviamos para el despliegue
                        return listaCanje;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaCanje;
            }
            finally
            {
                Conn.Close();
            }
        }
        // Metodo para borrar canjes
        public static int BorrarCanjes(clsCanje Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarCanje", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@CanjeID", Eliminado.id));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", Eliminado.fkusuario));

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
        // Para consultar canjes especificos
        public static clsCanje ConsultaCanjeporID(clsCanje CanjeConsultado)
        {
            clsCanje CanjeConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarCanjeporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar el canje a consultar
                    cmd.Parameters.Add(new SqlParameter("@CanjeID", CanjeConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            CanjeConsulta = new clsCanjes
                            {
                                id = reader.GetInt32(0),
                                fkusuario = reader.GetInt32(1),
                                fkrecompensa = reader.GetInt32(2),
                                fecha = reader.GetDateTime(3),
                                puntosutilizados = reader.GetInt32(4)
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
            return CanjeConsulta;
        }
        // Funcion para modificar canjes
        public static int ModificarCanjes(clsCanje CanjeModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarCanje", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@CanjeID", CanjeModificar.id)); // Este es el identificador, no se cambia, busca
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", CanjeModificar.fkusuario));
                    cmd.Parameters.Add(new SqlParameter("@RecompensaID", CanjeModificar.fkrecompensa));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", CanjeModificar.fecha));
                    cmd.Parameters.Add(new SqlParameter("@PuntosUtilizados", CanjeModificar.puntosutilizados));

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
        public static int CalcularPuntosCanje(int idRecompensa, int cantidadUnidades)
        {
            // Se utiliza el fetcher para traer datos frescos de la base de datos
            List<clsRecompensas> listaRecompensas = RecompensasLogica.ObtenerRecompensas();

            // LINQ para buscar la recompensa especifica
            clsRecompensas recompensaSeleccionada = listaRecompensas.FirstOrDefault(r => r.id == idRecompensa);

            if (recompensaSeleccionada != null)
            {
                // Se multiplica por la cantidad de unidades
                int puntosPorUnidad = Convert.ToInt32(recompensaSeleccionada.puntosnecesarios);

                return puntosPorUnidad * cantidadUnidades;
            }

            // Si por alguna razón no encuentra la recompensa, retorna 0 puntos para evitar inconsistencias
            return 0;
        }
    }
}