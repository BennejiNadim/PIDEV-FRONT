using System.Web;
using System.Web.Optimization;

namespace PIDEV_FRONTEND
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            #region Template Design
            bundles.Add(new ScriptBundle("~/template/js").Include(
                        "~/Scripts/js/jquery.min.js",
                        "~/ Scripts / js / popper.min.js",
                        "~/Scripts/js/bootstrap.min.js",
                        "~/Scripts/js/rangeslider.js",
                        "~/Scripts/js/select2.min.js",
                        "~/Scripts/js/jquery.magnific-popup.min.js",
                        "~/Scripts/js/slick.js",
                        "~/Scripts/js/slider-bg.js",
                        "~/Scripts/js/lightbox.js",
                        "~/Scripts/js/imagesloaded.js",
                        "~/Scripts/js/custom.js"));
            bundles.Add(new StyleBundle("~/template/css").Include(
                      "~/Content/css/styles.css",
                      "~/Content/css/colors.css"));

            #endregion
        }
    }
}
