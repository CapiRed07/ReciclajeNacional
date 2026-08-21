using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class RegistrosLogica
    {
        // Todos los metodos estan pensados para cualquier usuario
        // Los usuarios sin permisos solo pueden modificar tablas
        // Para agregar registros nuevos al sistema
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
    // Procedimiento para obtener todas las filas
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
        // Metodo para borrar registros
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
        // Para consultar registros especificos
        public static clsRegistros ConsultaRegistroporID(clsRegistros RegistroConsultado)
        {
            clsRegistros RegistroConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarRegistroReciclajeporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar el registro a consultar
                    cmd.Parameters.Add(new SqlParameter("@RegistroID", RegistroConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            RegistroConsulta = new clsRegistros
                            {
                                id = reader.GetInt32(0),
                                fkusuario = reader.GetInt32(1),
                                fkmaterial = reader.GetInt32(2),
                                fkcentro = reader.GetInt32(3),
                                cantidadkg = reader.GetInt32(4),
                                fecha = reader.GetDateTime(5),
                                puntosobtenidos = reader.GetInt32(6)
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
            return RegistroConsulta;
        }
        // Funcion para modificar registros
        public static int ModificarRegistros(clsRegistros RegistroModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarRegistroReciclaje", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@RegistroID", RegistroModificar.id)); // Este es el identificador, no se cambia, busca
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", RegistroModificar.fkusuario));
                    cmd.Parameters.Add(new SqlParameter("@MaterialID", RegistroModificar.fkmaterial));
                    cmd.Parameters.Add(new SqlParameter("@CentroID", RegistroModificar.fkcentro));
                    cmd.Parameters.Add(new SqlParameter("@CantidadKg", RegistroModificar.cantidadkg));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", RegistroModificar.fecha));
                    cmd.Parameters.Add(new SqlParameter("@PuntosObtenidos", RegistroModificar.puntosobtenidos));

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