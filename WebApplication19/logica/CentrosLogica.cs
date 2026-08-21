using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class CentrosLogica
    {
        //CRUD y consulta general
        // Para agregar materiales nuevos al sistema, pensada para administradores
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