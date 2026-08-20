using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication19.logica
{
    public class MasterLogica
    {
        // Paginas ocultas
        private static readonly HashSet<string> PaginasOcultas = new HashSet<string>
        {
            "login.aspx",
            "registro.aspx",
            "unauthorized.aspx"
        };
        public static bool NavOculta(string paginaActual)
        {
            if(PaginasOcultas.Any(p => paginaActual.Contains(p)))
            {
                return false;
            }
            return true;

        }
    }
}