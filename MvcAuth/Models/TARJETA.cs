//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcAuth.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TARJETA
    {
        public int ID_TARJETA { get; set; }
        public string TIPO_TARJETA { get; set; }
        public string NUM_TARJETA { get; set; }
        public string ANIO_TARJETA { get; set; }
        public string MES_TARJETA { get; set; }
        public string COD_TARJETA { get; set; }
        public decimal MONTO_TARJETA { get; set; }
        public string ID_USUARIO { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
