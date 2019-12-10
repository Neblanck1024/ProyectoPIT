using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Datos;
using Microsoft.AspNet.Identity;
using MvcAuth.Models;

namespace MvcAuth.Controllers
{
    
    public class MembresiaController : Controller
    {
        BD_PITEntities db = new BD_PITEntities();
        // GET: Membresia
        public ActionResult Index()
        {   
            return View(db.CLIENTEMEMBRESIA.ToList().Where(d =>  d.ID_USU == User.Identity.GetUserId()));
        }

        
        public ActionResult Membresía()
        {
            return View(db.MEMBRESIA.ToList());
        }

        [Authorize]
        public ActionResult Pago(int id)
        {
            PagoViewModel pvm = new PagoViewModel();
            pvm.usuario = db.AspNetUsers.Find(User.Identity.GetUserId());
            pvm.membresia = db.MEMBRESIA.Find(id);
            pvm.tarjeta = new TARJETA();
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Pago(PagoViewModel pvm)
        {
            TARJETA t = db.TARJETA.Where(x => x.NUM_TARJETA.Equals(pvm.tarjeta.NUM_TARJETA) &&
                                            x.ANIO_TARJETA.Equals(pvm.tarjeta.ANIO_TARJETA)
                                            &&
                                            x.MES_TARJETA.Equals(pvm.tarjeta.MES_TARJETA) &&
                                            x.COD_TARJETA.Equals(pvm.tarjeta.COD_TARJETA)).FirstOrDefault();
            MEMBRESIA m = db.MEMBRESIA.Find(pvm.membresia.ID_MEMBRESIA);


            var usuario = User.Identity.GetUserId();
            //Busco el cliente para ver si ya tenia una membresia y si la tiene lo tendre que eliminar porque si estoy comprando es que ya caduco la membresia pasada
            //CLIENTEMEMBRESIA mem = db.CLIENTEMEMBRESIA.Find(pvm.usuario.Id);

            CLIENTEMEMBRESIA cmem = db.CLIENTEMEMBRESIA.Where(cm => cm.ID_USU.Equals(usuario)).FirstOrDefault();

            var fecha = DateTime.Now;
            var diasaume = Convert.ToDouble(m.DIAS_MEM);
            if (cmem == null)
            {
                if (t.ID_TARJETA > 0)
                {
                    // Verificamos el monto}
                    if (t.MONTO_TARJETA > m.PRE_MEN)
                    {
                        // Registramos en membresia-cliente
                        CLIENTEMEMBRESIA cm = new CLIENTEMEMBRESIA()
                        {
                            ID_USU = pvm.usuario.Id,
                            ID_MEMBRESIA = pvm.membresia.ID_MEMBRESIA,
                            FECHA = DateTime.Now,
                            FEC_FIN = fecha.AddDays(diasaume)
                        };
                        db.CLIENTEMEMBRESIA.Add(cm);
                        // Restamos el importe
                        t.MONTO_TARJETA -= m.PRE_MEN;

                        db.SaveChanges();
                    }
                }
            }
            else {

                db.CLIENTEMEMBRESIA.Remove(cmem);
                db.SaveChanges();

                if (t.ID_TARJETA > 0)
                {
                    // Verificamos el monto}
                    if (t.MONTO_TARJETA > m.PRE_MEN)
                    {
                        // Registramos en membresia-cliente
                        CLIENTEMEMBRESIA cm = new CLIENTEMEMBRESIA()
                        {
                            ID_USU = pvm.usuario.Id,
                            ID_MEMBRESIA = pvm.membresia.ID_MEMBRESIA,
                            FECHA = DateTime.Now,
                            FEC_FIN = fecha.AddDays(diasaume)
                        };
                        db.CLIENTEMEMBRESIA.Add(cm);
                        // Restamos el importe
                        t.MONTO_TARJETA -= m.PRE_MEN;

                        db.SaveChanges();
                    }
                }
            }

            return View(pvm);
        }

        public ActionResult DepaPublicados()
        {
            return View(db.TB_DEPARTAMENTO.ToList().Where(d => d.ID == User.Identity.GetUserId()));
        }

        public ActionResult Interesados(int id)
        {
            return View(db.TB_CONTACTO.ToList().Where(d => d.COD_DEP == id));
        }
    }
}