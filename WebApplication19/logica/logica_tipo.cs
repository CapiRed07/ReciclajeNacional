using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication19.modelo;

namespace WebApplication19.logica
{


    public class logica_tipo
    {

        public static List<cls_tipo> tipos = new List<cls_tipo>();

        public static List<cls_tipo> ObtenerTipos()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();

            try
            {

                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_listatipos ", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cls_tipo tipo = new cls_tipo();   // instancia
                            tipo.id = reader.GetInt32(0);
                            tipo.nombre = reader.GetString(1);
                            tipos.Add(tipo);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return tipos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return tipos;
        }

        public static int BorrarTipos(int codigo)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_borrartipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@id", codigo));
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return retorno;
        }

        public static int AgregarTipos(string nombre)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = modelo.DBconn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("dbo.sp_agregartipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@descripcion", nombre));
                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
    }

  
    }