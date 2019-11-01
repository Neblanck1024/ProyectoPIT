using MvcAuth.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAuth.Controllers
{ 
    [RequireHttps]
    public class HomeController : Controller
    {
        SqlConnection cn = new SqlConnection("server=.;database=BD_PIT; uid=sa; pwd=sql");

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

        public ActionResult Index()
        {
            ViewBag.tipo = new SelectList(listadoTipos(), "codigo", "nombre");
            return View();
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
    }
}