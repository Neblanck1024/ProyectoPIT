using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Datos;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
namespace MvcAuth.Controllers
{
    [Authorize]
    public class ContactoController : Controller
    {
        BD_PITEntities bd = new BD_PITEntities();

        
        public new System.Security.Principal.IPrincipal User { get; }
        // GET: Contacto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View(new TB_CONTACTO());
        }

        [HttpPost]
        public ActionResult Crear(TB_CONTACTO reg)
        {
            var userId = User.Identity.GetUserId();
            //string userName= HttpContext.Current.
            reg.COD_DEMA = userId;
            try
            {
                bd.TB_CONTACTO.Add(reg);
                bd.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return View(reg);
        }
    }
}