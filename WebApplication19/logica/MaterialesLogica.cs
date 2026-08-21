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
        // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsMateriales> ObtenerMateriales()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los materiales
            List<clsMateriales> listaMateriales = new List<clsMateriales>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarMaterial", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while(reader.Read())
                        {
                            clsMateriales materiales = new clsMateriales
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosporkg = reader.GetDecimal(3),
                            };

                            // Pasamos los datos de cada material a la lista
                            listaMateriales.Add(materiales);
                        }

                        // Los enviamos para el despliegue
                        return listaMateriales;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaMateriales;
            }
            finally
            {
                Conn.Close();
            }  
        }
    }
}