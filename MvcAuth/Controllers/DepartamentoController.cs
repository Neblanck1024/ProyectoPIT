using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MvcAuth.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data;
using System.Web.Helpers;
using MvcAuth.Datos;

namespace MvcAuth.Controllers
{
    public class DepartamentoController : Controller
    {
        //2. Definir la cadena de conexion
        SqlConnection cn = new SqlConnection("server=.;database=BD_PIT; uid=sa; pwd=sql");
        BD_PITEntities storeDB = new BD_PITEntities();

        //3. Definir la lista de vendedores
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
        // GET: Departamento
        public ActionResult Index()
        {
            return View(Departamentos());
        }

        public ActionResult Crear()
        {
            ViewBag.tipo = new SelectList(listadoTipos(), "codigo", "nombre");
            ViewBag.distrito = new SelectList(listadoDistritos(), "codigo", "nombre");
            return View(new Departamento());
        }

        [HttpPost]
        public ActionResult Crear(Departamento objD)
        {
            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

            objD.foto = image.GetBytes();
            objD.idUsuario = "f758a8fc-0798-4c91-8f44-d5bee1d4c0a8";

            if (!ModelState.IsValid)
            {
                return View(objD);
            }


            ViewBag.mensaje = "";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_InsertaDep", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOM_DEP", objD.nombre);
                cmd.Parameters.AddWithValue("@TIP_DEP", objD.tipo);
                cmd.Parameters.AddWithValue("@NRO_PISO", objD.numpiso);
                cmd.Parameters.AddWithValue("@NUM_HABI", objD.numhabi);
                cmd.Parameters.AddWithValue("@PRECIO", objD.precio);
                cmd.Parameters.AddWithValue("@FOTO", objD.foto);
                cmd.Parameters.AddWithValue("@DISTRITO", objD.idDistrito);
                cmd.Parameters.AddWithValue("@ID", objD.idUsuario);
                int x = cmd.ExecuteNonQuery();
                ViewBag.mensaje = x.ToString() + " Proveedor registrado correctamente..!!";
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;

            }
            cn.Close();
            return RedirectToAction("Index", "Home");
        }

    }
}