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
    [Authorize]
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
                    titulo = dr[1].ToString()
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
            return View(new DepaMemViewModel().departamento);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Users = "Usuario1, Usuario2")]
        public ActionResult Crear(DepaMemViewModel objD)
        {
            var userId = User.Identity.GetUserId();
            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

            var cant = storeDB.TB_DEPARTAMENTO.Where(r => r.ID == userId).Count();

            var usuario = storeDB.CLIENTEMEMBRESIA.Where(r => r.ID_USU == userId).FirstOrDefault();

            DateTime fecha1 = Convert.ToDateTime(usuario.FECHA);
            DateTime fecha2 = DateTime.Today;

            int dias = ((TimeSpan)(fecha2 - fecha1)).Days;

            if (cant <= 1) { 
                objD.departamento.foto = image.GetBytes();
                objD.departamento.idUsuario = userId;

                //if (!ModelState.IsValid)
                //{
                //    return View(objD);
                //}


                ViewBag.mensaje = "";
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertaDep", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NOM_DEP", objD.departamento.titulo);
                    cmd.Parameters.AddWithValue("@TIP_DEP", objD.departamento.tipo);
                    cmd.Parameters.AddWithValue("@NRO_PISO", objD.departamento.numpiso);
                    cmd.Parameters.AddWithValue("@NUM_HABI", objD.departamento.numhabi);
                    cmd.Parameters.AddWithValue("@PRECIO", objD.departamento.precio);
                    cmd.Parameters.AddWithValue("@FOTO", objD.departamento.foto);
                    cmd.Parameters.AddWithValue("@DISTRITO", objD.departamento.idDistrito);
                    cmd.Parameters.AddWithValue("@ID", objD.departamento.idUsuario);
                    cmd.Parameters.AddWithValue("@DIR", objD.departamento.direccion);
                    cmd.Parameters.AddWithValue("@DES", objD.departamento.descripcion);
                    cmd.Parameters.AddWithValue("@SERVICIO", objD.departamento.servicios);
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

            

           

             else if (usuario.ID_USU != null && usuario.MEMBRESIA.DIAS_MEM>=dias)
            {
                objD.departamento.foto = image.GetBytes();
                objD.departamento.idUsuario = userId;

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
                    cmd.Parameters.AddWithValue("@NOM_DEP", objD.departamento.titulo);
                    cmd.Parameters.AddWithValue("@TIP_DEP", objD.departamento.tipo);
                    cmd.Parameters.AddWithValue("@NRO_PISO", objD.departamento.numpiso);
                    cmd.Parameters.AddWithValue("@NUM_HABI", objD.departamento.numhabi);
                    cmd.Parameters.AddWithValue("@PRECIO", objD.departamento.precio);
                    cmd.Parameters.AddWithValue("@FOTO", objD.departamento.foto);
                    cmd.Parameters.AddWithValue("@DISTRITO", objD.departamento.idDistrito);
                    cmd.Parameters.AddWithValue("@ID", objD.departamento.idUsuario);
                    cmd.Parameters.AddWithValue("@DIR", objD.departamento.direccion);
                    cmd.Parameters.AddWithValue("@DES", objD.departamento.descripcion);
                    cmd.Parameters.AddWithValue("@SERVICIO", objD.departamento.servicios);
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
            else
            {
                return RedirectToAction("Aviso", "Departamento");
            }

        }

        public ActionResult Aviso()
        {
            return View();
        }

    }
}