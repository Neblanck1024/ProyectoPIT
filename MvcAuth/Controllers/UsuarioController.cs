using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcAuth.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;

namespace MvcAuth.Controllers
{
    public class UsuarioController : Controller
    {
        SqlConnection cn = new SqlConnection("server=.;database=BD_PIT; uid=sa; pwd=sql");

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
     
    }
}