using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication19.modelo;
using WebApplication19.logica;
using BCrypt.Net;
namespace WebApplication19.logica
{
    public class UsuariosLogica
    {
        // Metodo para obtener un usuario por su ID desde la base de datos
        public static clsUsuarios ObtenerUsuarioLogueado(int idUsuarioLogueado)
        {
            clsUsuarios usuario = null; // Inicializamos la variable de usuario como null por si no encuentra un usuario
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado para consultar un usuario por su ID
                    SqlCommand cmd = new SqlCommand("ConsultarUsuarioporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // Agregamos el parámetro del ID del usuario al comando
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", idUsuarioLogueado));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // If en vez de while, ya que esperamos un solo resultado
                        if (reader.Read())
                        {
                            usuario = new clsUsuarios
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                correo = reader.GetString(2),
                                provincia = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                puntos = reader.GetInt32(4),
                                rol = reader.GetString(5)
                            };
                        }
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
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
            return usuario;
        }
        // Metodo para agregar un nuevo usuario a la base de datos
        public static int AgregarUsuario(clsUsuarios NuevoUsuario)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            // Se cifra la contraseña antes de enviarla a la base de datos para el registro
            NuevoUsuario.pwd = BCrypt.Net.BCrypt.HashPassword(NuevoUsuario.pwd);
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure para agregar un usuario
                    SqlCommand cmd = new SqlCommand("AgregarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    // Agregamos los parámetros del usuario al comando
                    cmd.Parameters.Add(new SqlParameter("@Nombre", NuevoUsuario.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Correo", NuevoUsuario.correo));
                    cmd.Parameters.Add(new SqlParameter("@Pwd", NuevoUsuario.pwd));
                    cmd.Parameters.Add(new SqlParameter("@Rol", NuevoUsuario.rol));

                    // Validacion para la provincia, si es null o vacía para usuarios de rol "admin", se pasa como DBNull
                    if (NuevoUsuario.rol == "admin" || string.IsNullOrEmpty(NuevoUsuario.provincia))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Provincia", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Provincia", NuevoUsuario.provincia));
                    }
                    retorno = cmd.ExecuteNonQuery(); //Devuelve el numero de filas afectadas, en este caso debería ser 1 si se insertó correctamente
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                // Manejo de errores
                return -1; // Retorna -1 en caso de error
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }
            return retorno;
        }
        // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsUsuarios> ObtenerUsuarios()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los Usuarios
            List<clsUsuarios> listaUsuarios = new List<clsUsuarios>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarUsuarios", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsUsuarios Usuarios = new clsUsuarios
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                correo = reader.GetString(2),
                                provincia = reader.IsDBNull(3) ? null : reader.GetString(3),
                                puntos = reader.GetInt32(4),
                                rol = reader.GetString(5)
                            };

                            // Pasamos los datos de cada Usuario a la lista
                            listaUsuarios.Add(Usuarios);
                        }

                        // Los enviamos para el despliegue
                        return listaUsuarios;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaUsuarios;
            }
            finally
            {
                Conn.Close();
            }
        }
        // Metodo para borrar Usuarios, pensado para administradores
        public static int BorrarUsuarios(clsUsuarios Eliminado)
        {
            SqlConnection Conn = new SqlConnection();
            int retorno = 0; // Se inicia en 0 en caso de no borrar nada.

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Procedimiento almacenado
                    SqlCommand cmd = new SqlCommand("EliminarUsuario", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Parametro de id para encontrar el match
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", Eliminado.id));

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
        // Para consultar Usuarios especificos, todos pueden acceder
        public static clsUsuarios ConsultaUsuarioporID(clsUsuarios UsuarioConsultado)
        {
            clsUsuarios UsuarioConsulta = null; // Sino encuentra, devuelve en nulo
            SqlConnection Conn = new SqlConnection();

            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarUsuarioporID", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se le pasa ID como parametro para buscar el Usuario a consultar
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioConsultado.id));

                    // Se llama al reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //If, solo buscamos uno en este caso.
                        if (reader.Read())
                        {
                            UsuarioConsulta = new clsUsuarios
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                correo = reader.GetString(2),
                                provincia = reader.GetString(3),
                                puntos = reader.GetInt32(4),
                                rol = reader.GetString(6)
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
            return UsuarioConsulta;
        }
        // Funcion para modificar Usuarios, pensada para administradores.
        public static int ModificarUsuarios(clsUsuarios UsuarioModificar)
        {
            int retorno = 0; // 0 sino modifica nada

            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure
                    SqlCommand cmd = new SqlCommand("ModificarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Variables a modificar como parametros
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioModificar.id));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", UsuarioModificar.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Correo", UsuarioModificar.correo));
                    cmd.Parameters.Add(new SqlParameter("@Pwd", UsuarioModificar.pwd));
                    cmd.Parameters.Add(new SqlParameter("@Rol", UsuarioModificar.rol));

                    // Validacion para la provincia, si es null o vacía para usuarios de rol "admin", se pasa como DBNull
                    if (UsuarioModificar.rol == "admin" || string.IsNullOrEmpty(UsuarioModificar.provincia))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Provincia", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@Provincia", UsuarioModificar.provincia));
                    }
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
        // Metodo para validar el login de un usuario
        public static bool ValidarLogin(clsUsuarios log)
        {
            // Se utiliza un bloque try-catch para manejar posibles excepciones al interactuar con la base de datos
            try
            {
                string storedHash = null;

                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Stored procedure busca por @correo y devuelve el Hash, Id, Nombre y Rol
                    SqlCommand cmd = new SqlCommand("ValidarLogin", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se agrega únicamente el parámetro de correo al comando
                    cmd.Parameters.Add(new SqlParameter("@Correo", log.correo));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asume que la columna 3 contiene el hash de BCrypt guardado
                            storedHash = reader.GetString(3);

                            // Guardamos los datos temporalmente
                            log.id = reader.GetInt32(0);
                            log.nombre = reader.GetString(1);
                            log.rol = reader.GetString(2);
                        }
                        else
                        {
                            // El correo no existe en la base de datos
                            return false;
                        }
                    }
                }

                // Validacion de BCrypt
                // Se compara la contraseña en texto plano con el hash extraído de la base de datos
                bool esValido = BCrypt.Net.BCrypt.Verify(log.pwd, storedHash);

                if (esValido)
                {
                    return true;
                }
                else
                {
                    // Si la contraseña no coincide, limpiamos los datos del objeto por seguridad
                    log.id = 0;
                    log.nombre = null;
                    log.rol = null;
                    return false;
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores de SQL
                throw new Exception("Error en la base de datos al validar el login.", ex);
            }
        }


    }
}