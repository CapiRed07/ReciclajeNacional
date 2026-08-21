using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication19.modelo
{
    public class clsRecompensas
    {
        public int id {  get; set; }

        public string name { get; set; }

        public string descripcion { get; set; }

        public int puntosnecesarios { get; set; }

        public int disponible { get; set; }
    }
}