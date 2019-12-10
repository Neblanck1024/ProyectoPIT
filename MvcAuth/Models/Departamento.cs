using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace MvcAuth.Models
{
    public class Departamento
    {
        
        public int codigo { get; set; }
        
        public string titulo { get; set; }
        
        public int tipo { get; set; }
        
        public int numpiso { get; set; }
        
        public int numhabi { get; set; }
        
        public double precio { get; set; }
        
        public byte[] foto { get; set; }
        
        public int idDistrito { get; set; }
        
        public string idUsuario { get; set; }
        
        public string direccion { get; set; }
        
        public string descripcion { get; set; }
        
        public string servicios { get; set; }
    }
}