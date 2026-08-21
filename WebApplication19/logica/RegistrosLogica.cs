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
}