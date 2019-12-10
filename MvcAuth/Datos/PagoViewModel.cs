using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAuth.Datos
{
    public class PagoViewModel
    {
        public AspNetUsers usuario { get; set; }
        public TARJETA tarjeta { get; set; }
        public MEMBRESIA membresia { get; set; }
    }
}