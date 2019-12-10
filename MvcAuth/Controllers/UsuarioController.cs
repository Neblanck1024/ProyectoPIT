using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;
using MvcAuth.Datos;
using Microsoft.AspNet.Identity;

namespace MvcAuth.Controllers
{
    public class UsuarioController : Controller
    {
        SqlConnection cn = new SqlConnection("server=.;database=BD_PIT; uid=sa; pwd=sql");

        BD_PITEntities db = new BD_PITEntities();

        //3. Definir la lista de vendedores
        List<Usuario> Usuarios(string id)
        {
            List<Usuario> tUsu = new List<Usuario>();
            SqlCommand cmd = new SqlCommand("SP_LISTARUSUARIOXCOD", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@COD", id);

            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Usuario objU = new Usuario
                {
                    id = dr[0].ToString(),
                    nombres = dr[1].ToString(),
                    apellidos = dr[2].ToString(),
                    email = dr[3].ToString(),
                    username = dr[4].ToString(),
                };
                tUsu.Add(objU);
            }
            cn.Close();
            return tUsu;
        }

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Actualizar(string id)
        {
            id = User.Identity.GetUserId();
            AspNetUsers usu = db.AspNetUsers.Find(id);
            return View(usu);
        }

        [HttpPost]
        public ActionResult Actualizar([Bind(Include = "Id,Nombres,Apellidos,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Direccion,Genero,FechaNacimiento,Dni,EstadoCivil")] AspNetUsers usu)
        {
            db.Entry(usu).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Manage");
        }

    }
}