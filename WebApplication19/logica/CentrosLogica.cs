using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class CentrosLogica
    {
        //CRUD y consulta general
        // Para agregar Centros nuevos al sistema, pensada para administradores
        public static int AgregarCentros(clsCentros NuevoCentro)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure
                    SqlCommand cmd = new SqlCommand("AgregarCentro", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Se agregan parametros al comando
                    cmd.Parameters.Add(new SqlParameter("@Nombre", NuevoCentro.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Provincia", NuevoCentro.provincia));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", NuevoCentro.direccion));
                    cmd.Parameters.Add(new SqlParameter("@Horario", NuevoCentro.horario));

                    //Devuelve el numero de filas afectadas, 1 si tuvo exito
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Manejo de errores
                return -2; // Retorna -1 en caso de error
            }
            catch
            {
                // Error general
                return -1;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
            return retorno;
        }
        // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsCentros> ObtenerCentros()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los Centros
            List<clsCentros> listaCentros = new List<clsCentros>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarCentro", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsCentros Centros = new clsCentros
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                provincia = reader.GetString(2),
                                direccion = reader.GetString(3),
                                horario = reader.GetString(4),
                            };

                            // Pasamos los datos de cada Centro a la lista
                            listaCentros.Add(Centros);
                        }

                        // Los enviamos para el despliegue
                        return listaCentros;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaCentros;
            }
            finally
            {
                Conn.Close();
            }
        }
        // Metodo para borrar centros, pensado para administradores
        public static int BorrarCentro(clsCentros Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarCentro", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@CentroID", Eliminado.id));

                    retorno = cmd.ExecuteNonQuery();
                    // Si se logra, se asignan las filas afectadas, cambiando a 1
                    return retorno;
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores
                if (Ex.Number == 547)
                {
                    return -2; // Violacion de llave foranea
                }
                return 0;
            }
            catch (Exception)
            {
                return -1; //Cualquier otro error
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
        // Para consultar centros especificos, todos pueden acceder
        public static clsCentros ConsultaCentroporID(clsCentros CentroConsultado)
        {
            clsCentros CentroConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarCentroporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar el Centro a consultar
                    cmd.Parameters.Add(new SqlParameter("@CentroID", CentroConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            CentroConsulta = new clsCentros
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                provincia = reader.GetString(2),
                                direccion = reader.GetString(3),
                                horario = reader.GetString(4)
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
            return CentroConsulta;
        }
        // Funcion para modificar centros, pensada para administradores.
        public static int ModificarCentros(clsCentros CentroModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarCentro", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@CentroID", CentroModificar.id)); // Este es el identificador, no se cambia, busca
                    cmd.Parameters.Add(new SqlParameter("@Nombre", CentroModificar.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Provincia", CentroModificar.provincia));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", CentroModificar.direccion));
                    cmd.Parameters.Add(new SqlParameter("@Horario", CentroModificar.horario));

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