using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace MvcAuth.Models
{
    public class Usuario
    {
        [DisplayName("CODIGO")]
        public string id { get; set; }
        [DisplayName("NOMBRE")]
        public string nombres { get; set; }
        [DisplayName("APELLIDO")]
        public string apellidos { get; set; }
        [DisplayName("EMAIL")]
        public string email { get; set; }
        [DisplayName("USERNAME")]
        public string username { get; set; }
    }
}