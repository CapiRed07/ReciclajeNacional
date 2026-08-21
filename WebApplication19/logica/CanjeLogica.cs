using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class CanjeLogica
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
        }
}