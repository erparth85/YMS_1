using FluentValidation.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using PMM.Data;
using System.Web.Optimization;

namespace PMM.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<IocDbContext>(null);
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FluentValidationModelValidatorProvider.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteTable.Routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Yagna", action = "List", id = UrlParameter.Optional }
             );
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Url.AbsoluteUri.Contains("login"))
            {
                if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    HttpContext.Current.Response.Redirect("~/login");
                }
            }
        }

    }
}