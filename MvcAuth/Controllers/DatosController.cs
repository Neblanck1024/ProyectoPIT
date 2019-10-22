using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Models;
using MvcAuth.Datos;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Configuration;
namespace MvcAuth.Controllers
{
    public class DatosController : Controller
    {

        BD_PITEntities db = new BD_PITEntities();
        //ExternalLoginConfirmationViewModel usu = new ExternalLoginConfirmationViewModel();

        //List<Usuario> listadoUsuario()
        //{
        //    List<Usuario> aUsuario = new List<Usuario>();
        //    SqlCommand cmd = new SqlCommand("SP_LISTAUSUARIO", cn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cn.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        Usuario objU = new Usuario()
        //        {
        //            id = dr[0].ToString(),
        //            nombres = dr[1].ToString(),
        //            apellidos = dr[2].ToString(),
        //            email = dr[3].ToString(),
        //            username = dr[4].ToString(),
        //        };
        //        aUsuario.Add(objU);
        //    }
        //    cn.Close();
        //    return aUsuario;
        //}
        // GET: Datos
        public ActionResult Index()
        {
            return View();
        }

    }
}