using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Datos;
using Microsoft.AspNet.Identity;

namespace MvcAuth.Controllers
{ 
    [RequireHttps]
    public class HomeController : Controller
    {
        SqlConnection cn = new SqlConnection("server=.;database=BD_PIT; uid=sa; pwd=sql");
        BD_PITEntities storeDB = new BD_PITEntities();

        List<Departamento> Departamentos()
        {
            List<Departamento> tDep = new List<Departamento>();
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("SP_LISTARDEPA", cn);
            SqlDataReader dr = da.SelectCommand.ExecuteReader();

            while (dr.Read())
            {
                Departamento objD = new Departamento
                {
                    codigo = int.Parse(dr[0].ToString()),
                    titulo = dr[1].ToString()
                };
                tDep.Add(objD);
            }
            cn.Close();
            return tDep;
        }


        List<TipoDepartamento> listadoTipos()
        {
            List<TipoDepartamento> aTipos = new List<TipoDepartamento>();
            SqlCommand cmd = new SqlCommand("SP_LISTADOTIPO", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoDepartamento objTP = new TipoDepartamento()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                };
                aTipos.Add(objTP);
            }
            cn.Close();
            return aTipos;
        }

        List<Distrito> listadoDistritos()
        {
            List<Distrito> aDistritos = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("SP_LISTADODISTRITOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Distrito objD = new Distrito()
                {
                    codigo = int.Parse(dr[0].ToString()),
                    nombre = dr[1].ToString(),
                };
                aDistritos.Add(objD);
            }
            cn.Close();
            return aDistritos;
        }

        public ActionResult Index()
        {
            ViewBag.tipo = new SelectList(storeDB.TB_TIPODEPARTAMENTO.ToList(), "COD_TIPDEP", "NOM_TIPDEP");
            ViewBag.distrito = new SelectList(storeDB.TB_DISTRITO.ToList(), "COD_DIS", "NOM_DIS");
            return View();
        }

        public ActionResult convertirImagen(int codigoDepa)
        {
            var imagen = storeDB.TB_DEPARTAMENTO.Where(x => x.COD_DEP == codigoDepa).FirstOrDefault();

            return File(imagen.FOTO, "image/jpeg");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListadoDepartamentos(int? idT = null,int? idD=null,string nom = null)
        {
            ViewBag.tipo = new SelectList(storeDB.TB_TIPODEPARTAMENTO.ToList(), "COD_TIPDEP", "NOM_TIPDEP",idT);
            ViewBag.distrito = new SelectList(storeDB.TB_DISTRITO.ToList(), "COD_DIS", "NOM_DIS",idD);
            ViewBag.nomdep = nom;

            //var lista = from p in storeDB.TB_DEPARTAMENTO
            //            where p.NOM_DEP.StartsWith(nom) &&
            //            p.TIP_DEP == idT ||
            //            p.DISTRITO == idD
            //            select p;
            
            if (ViewBag.tipo != null && ViewBag.distrito == null && ViewBag.nomdep == null)
            {
                var lista = storeDB.TB_DEPARTAMENTO.Where(x => x.TIP_DEP == idT);
                return View(lista.ToList());
            }
            else if (ViewBag.tipo == null && ViewBag.distrito != null && ViewBag.nomdep == null)
            {
                var lista = storeDB.TB_DEPARTAMENTO.Where(x => x.TIP_DEP == idT);
                return View(lista.ToList());
            }
            else
            {
                var lista = storeDB.TB_DEPARTAMENTO.Where(x => x.NOM_DEP.StartsWith(nom) || x.TIP_DEP == idT || x.DISTRITO == idD);
                return View(lista.ToList());
            }
        }

        public ActionResult Details(int? id = null)
        {
            var vm = new ContactDepViewModel()
            {
                departamento = storeDB.TB_DEPARTAMENTO.ToList().Where(d => d.COD_DEP == id).FirstOrDefault(),
                contacto = storeDB.TB_CONTACTO.ToList().Where(d => d.COD_DEP == id).FirstOrDefault()
            };

            return View(vm);
        }

        
        public ActionResult Contacto()
        {
            return View(new ContactDepViewModel().contacto);
        }


        [HttpPost]
        public ActionResult Contacto(ContactDepViewModel reg)
        {
            try
            {
                storeDB.TB_CONTACTO.Add(reg.contacto);
                storeDB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Details", "Home", new { id= reg.contacto.COD_DEP });
        }

        //public ActionResult Contacto(int? id = null)
        //{
        //    var vm = new ContactDepViewModel()
        //    {
        //        contacto = storeDB.TB_CONTACTO.ToList().Where(d => d.COD_DEP == id).FirstOrDefault()

        //    };

        //    return View(vm);
        //}

        //public ActionResult Contacto(TB_CONTACTO cont)
        //{
        //    var userId = User.Identity.GetUserId();

        //    if (ModelState.IsValid)
        //    {
        //        TB_CONTACTO obb = new TB_CONTACTO();
        //        obb.COD_DEMA = userId;
        //        obb.COD_OFER = cont.COD_OFER;
        //        obb.NOM_CON = cont.NOM_CON;
        //        obb.EMAIL_CON = cont.EMAIL_CON;
        //        obb.TEL_CON = cont.TEL_CON;
        //        obb.TEXT_CON = cont.TEXT_CON;
        //        obb.ESTADO = cont.ESTADO;
        //        obb.COD_DEP = cont.COD_DEP;
        //        storeDB.TB_CONTACTO.Add(obb);
        //        storeDB.SaveChanges();
        //    }
        //    return Json("success", JsonRequestBehavior.AllowGet);
        //}
    }
}