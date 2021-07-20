using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMWebApp.Models
{
    public class MdlProspecto
    {
        public string Nombre { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Colonia { get; set; }
        public int Codigo_Postal { get; set; }
        public long Telefono { get; set; }
        public string Rfc { get; set; }
        public string Estatus { get; set; }
        public string Comentarios { get; set; }
        public string Archivos {get;set;}
        public  string NombreArchivo { get; set; }







    }
}