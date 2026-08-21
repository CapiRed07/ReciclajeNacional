using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication19.modelo
{
    public class clsRegistros
    {
        public int id {  get; set; }

        public int fkusuario { get; set; }

        public int fkmaterial { get; set; }

        public int fkcentro { get; set; }

        public int cantidadkg { get; set; }

        public DateTime fecha { get; set; }
        
        public int puntosobtenidos { get; set; }
    }
}