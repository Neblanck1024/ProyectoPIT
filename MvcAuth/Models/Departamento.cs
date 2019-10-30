using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace MvcAuth.Models
{
    public class Departamento
    {
        [DisplayName("CODIGO")]
        public int codigo { get; set; }
        [DisplayName("DESCRIPCION")]
        public string nombre { get; set; }
        [DisplayName("TIPO DE DEPARTAMENTO")]
        public int tipo { get; set; }
        [DisplayName("NUMERO DE PISO")]
        public int numpiso { get; set; }
        [DisplayName("NUMERO DE HABITACIONES")]
        public int numhabi { get; set; }
        [DisplayName("PRECIO")]
        public double precio { get; set; }
        [DisplayName("FOTO")]
        public byte[] foto { get; set; }
        [DisplayName("CODIGO DE DISTRITO")]
        public int idDistrito { get; set; }
        [DisplayName("CODIGO DE USUARIO")]
        public string idUsuario { get; set; }
    }
}