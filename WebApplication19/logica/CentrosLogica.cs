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
        // Para agregar Centros nuevos al sistema, pensada para administradores
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
        // Procedimiento para obtener todas las filas, los usuarios pueden usarlo libremente
        public static List<clsCentros> ObtenerCentros()
        {
            SqlConnection Conn = new SqlConnection();

            // Se crea una lista para mantener a todos los Centros
            List<clsCentros> listaCentros = new List<clsCentros>();
            // Fuera del try para retornarla si hay error
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    //Stored procedure
                    SqlCommand cmd = new SqlCommand("ConsultarCentro", Conn)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure // Tipo stored procedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // While en vez de if para leer todas las filas
                        while (reader.Read())
                        {
                            clsCentros Centros = new clsCentros
                            {
                                id = reader.GetInt32(0),
                                nombre = reader.GetString(1),
                                descripcion = reader.GetString(2),
                                puntosporkg = reader.GetDecimal(3),
                            };

                            // Pasamos los datos de cada material a la lista
                            listaCentros.Add(Centros);
                        }

                        // Los enviamos para el despliegue
                        return listaCentros;
                    }
                }
            }
            catch (SqlException Ex)
            {
                // Manejo de errores.
                return listaCentros;
            }
            finally
            {
                Conn.Close();
            }
        }
    }
}