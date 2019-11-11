using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Datos;

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
                    nombre = dr[1].ToString()
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

            var lista = from p in storeDB.TB_DEPARTAMENTO
                        where p.NOM_DEP.StartsWith(nom) && p.TIP_DEP == idT && p.DISTRITO == idD
                        select p;
            return View(lista.ToList());
        }

        public ActionResult Details(int? id = null)
        {
            return View(storeDB.TB_DEPARTAMENTO.Where(d => d.COD_DEP == id).FirstOrDefault());
        }
    }
}