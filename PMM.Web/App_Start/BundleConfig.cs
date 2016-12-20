using System.Web;
using System.Web.Optimization;

namespace PMM.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/template/").Include(
                          "~/js/jquery-2.1.1.min.js",
                         "~/js/jquery-ui-1.9.2.min.js",
                        "~/bootstrap/dist/js/bootstrap.min.js",
                       "~/js/jquery.datetimepicker.js",
                      "~/plugin/progressbar/bootstrap-progressbar.min.js",
                      "~/js/custom.js",
                     "~/validator/jquery.validate.min.js",
                    "~/js/common.js",
                    "~/js/jquery.min.js",
                    "~/plugin/chosen/chosen.jquery.js",
                    "~/js/bootstrap-multiselect.min.js",
                    "~/plugin/datepicker/js/bootstrap-datepicker.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/template/").Include(
                      "~/bootstrap/dist/css/bootstrap.min.css",
                      "~/fonts/css/font-awesome.min.css",
                      "~/css/animate.min.css",
                      "~/plugin/datepicker/jquery-ui.min.css",
                      "~/css/custom.css",
                      "~/css/main.css",
                      "~/plugin/chosen/chosen.css",
                      "~/plugin/chosen/select.css"));
        }
    }
}
