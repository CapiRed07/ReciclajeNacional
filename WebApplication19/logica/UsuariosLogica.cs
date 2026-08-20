using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using WebApplication19.modelo;
using WebApplication19.logica;
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
                    cmd.Parameters.Add(new SqlParameter("@idusuario", idUsuarioLogueado));

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
        public static int AgregarUsuario(clsUsuarios NuevoUsuario)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
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
                    cmd.Parameters.Add(new SqlParameter("@nombre", NuevoUsuario.nombre));
                    cmd.Parameters.Add(new SqlParameter("@correo", NuevoUsuario.correo));
                    cmd.Parameters.Add(new SqlParameter("@pwd", NuevoUsuario.pwd));
                    cmd.Parameters.Add(new SqlParameter("@rol", NuevoUsuario.rol));

                    // Validacion para la provincia, si es null o vacía para usuarios de rol "admin", se pasa como DBNull
                    if (NuevoUsuario.rol == "admin" || string.IsNullOrEmpty(NuevoUsuario.provincia))
                    {
                        cmd.Parameters.Add(new SqlParameter("@provincia", DBNull.Value));
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@provincia", NuevoUsuario.provincia));
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
        // Metodo para validar el login de un usuario
        public static bool ValidarLogin(clsUsuarios log)
        {
            // Se cifra la contraseña antes de enviarla a la base de datos para la validación
            // Utilizando modelo SHA-256 para cifrar la contraseña
            log.pwd = SecurityHelper.ConvertirSHA256(log.pwd);

            // Se utiliza un bloque try-catch para manejar posibles excepciones al interactuar con la base de datos
            try
            {
                using (SqlConnection Conn = modelo.DBconn.obtenerConexion())
                {
                    // Se llama al stored procedure para validar el login
                    SqlCommand cmd = new SqlCommand("ValidarLogin", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Se agregan los parámetros de correo y contraseña al comando
                    cmd.Parameters.Add(new SqlParameter("@correo", log.correo));
                    cmd.Parameters.Add(new SqlParameter("@pwd", log.pwd));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            log.id = reader.GetInt32(0);
                            log.nombre = reader.GetString(1);
                            log.rol = reader.GetString(2);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
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