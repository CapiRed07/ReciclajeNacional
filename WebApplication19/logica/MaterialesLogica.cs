using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{
    public class MaterialesLogica
    {
        public static int AgregarMateriales(clsMateriales NuevoMaterial)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();

            try
            {
                using(Conn = modelo.DBconn.obtenerConexion())
                {
                    //Se llama al stored procedure
                    SqlCommand cmd = new SqlCommand("AgregarMaterial", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    // Se agregan parametros al comando
                    cmd.Parameters.Add(new SqlParameter("@Nombre", NuevoMaterial.nombre));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", NuevoMaterial.descripcion));
                    cmd.Parameters.Add(new SqlParameter("@PuntosporKG", NuevoMaterial.puntosporkg));

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