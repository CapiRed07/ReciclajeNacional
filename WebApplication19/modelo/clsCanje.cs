using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication19.modelo
{
    public class clsCanje
    {
        public int id {  get; set; }

        public int fkusuario { get; set; }

        public int fkrecompensa { get; set; }

        public int cantidad { get; set; }
        public DateTime fecha { get; set; }

        public int puntosutilizados { get; set; }
    }
}