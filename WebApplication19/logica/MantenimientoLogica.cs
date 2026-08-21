using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication19.logica
{
    public class MantenimientoLogica
    {
        // Método genérico para ejecutar los SPs de pruebas
        public static bool EjecutarProcedimiento(string nombreSP)
        {
            using (SqlConnection conn = modelo.DBconn.obtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand(nombreSP, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException)
                    {
                        return false;
                    }
                }
            }
        }
    }
}