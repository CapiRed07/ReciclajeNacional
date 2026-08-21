using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class RegistrosLogica
    {
        // Para agregar registros nuevos al sistema, pensada para administradores
        public static int AgregarRegistros(clsRegistros NuevoRegistro)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure
                    SqlCommand cmd = new SqlCommand("AgregarRegistroReciclaje", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Se agregan parametros al comando
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", NuevoRegistro.fkusuario));
                    cmd.Parameters.Add(new SqlParameter("@MaterialID", NuevoRegistro.fkmaterial));
                    cmd.Parameters.Add(new SqlParameter("@CentroID", NuevoRegistro.fkcentro));
                    cmd.Parameters.Add(new SqlParameter("@CantidadKg", NuevoRegistro.cantidadkg));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", NuevoRegistro.fecha));
                    cmd.Parameters.Add(new SqlParameter("@PuntosObtenidos", NuevoRegistro.puntosobtenidos));

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
        public static List<clsRegistros> ObtenerRegistros()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los registros
            List<clsRegistros> listaRegistros = new List<clsRegistros>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarRegistroReciclaje", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsRegistros registros = new clsRegistros
                            {
                                id = reader.GetInt32(0),
                                fkusuario = reader.GetInt32(1),
                                fkmaterial = reader.GetInt32(2),
                                fkcentro = reader.GetInt32(3),
                                cantidadkg = reader.GetInt32(4),
                                fecha = reader.GetDateTime(5),
                                puntosobtenidos = reader.GetInt32(6)
                            };

                            // Pasamos los datos de cada registro a la lista
                            listaRegistros.Add(registros);
                        }

                        // Los enviamos para el despliegue
                        return listaRegistros;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaRegistros;
            }
            finally
            {
                Conn.Close();
            }
        }
        // Metodo para borrar registros, pensado para administradores
        public static int BorrarRegistros(clsRegistros Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarRegistroReciclaje", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@RegistroID", Eliminado.id));

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
                if (Conn != null)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
    }