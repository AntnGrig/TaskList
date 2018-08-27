using System.Web;
using System.Web.Optimization;

namespace BroadridgeTask
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularjs").Include(
                      "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ag-grid").Include(
                        "~/Scripts/ag-grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/app-angular.js").Include(
                      "~/Scripts/BroadridgeScripts/app-angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                      "~/Scripts/BroadridgeScripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/formjs").Include(
                      "~/Scripts/BroadridgeScripts/form.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/BroadridgeCSS/main.css"));
        }
    }
}
