using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication19.modelo
{
    public class clsUsuarios
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public string correo { get; set; }

        public string provincia { get; set; }

        public int puntos { get; set; }

        public string pwd { get; set; }

        public string rol { get; set; }
    }
}