using System.Web;
using System.Web.Optimization;

namespace MvcAuth
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/vendor/jquery/jquery.min.js",
                      "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                      "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                      "~/Content/vendor/magnific-popup/jquery.magnific-popup.min.js",
                      "~/Content/js/creative.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/vendor/fontawesome-free/css/all.min.css",
                        "~/Content/vendor/fontawesome-free/css/fontawesome.min.css",
                        "~/Content/vendor/magnific-popup/magnific-popup.css",
                        "~/Content/css/creative.min.css"));
        }
    }
}
