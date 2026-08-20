using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                                provincia = reader.IsDbNull(3) ? string.Empty : reader.GetString(3),
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
                Conn.Close();
                Conn.Dispose();
                return new clsUsuarios();
            }
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
                    })
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
    }
}