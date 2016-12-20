using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMM.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("Login", "login", new { controller = "Admin", action = "Login" });
            routes.MapRoute("UserLogin", "user/login", new { controller = "Admin", action = "UserLogin" });
            routes.MapRoute("Logout", "logout", new { controller = "Admin", action = "Logout" });

            routes.MapRoute("Dashboard", "dashboard", new { controller = "Home", action = "Index" });

            #region city
            routes.MapRoute("CityHome", "city", new { controller = "City", action = "Index" });
            routes.MapRoute("CityList", "city/list", new { controller = "City", action = "CityList" });
            routes.MapRoute("ManageCity", "manage/city/{id}", new { controller = "City", action = "ManageCity", id = UrlParameter.Optional });
            routes.MapRoute("DeleteCity", "delete/city/{id}", new { controller = "City", action = "DeleteCity", id = UrlParameter.Optional });

            #endregion         

            #region Mandal
            routes.MapRoute("MandalHome", "mandal", new { controller = "Mandal", action = "Index" });
            routes.MapRoute("MandalList", "mandal/list", new { controller = "Mandal", action = "MandalList" });
            routes.MapRoute("ManageMandal", "manage/mandal/{id}", new { controller = "Mandal", action = "ManageMandal", id = UrlParameter.Optional });
            routes.MapRoute("DeleteMandal", "delete/mandal/{id}", new { controller = "Mandal", action = "DeleteMandal", id = UrlParameter.Optional });


            
            #endregion

            #region manage yagna 


            routes.MapRoute("Yagna", "yagna", new { controller = "Yagna", action = "List" });

            routes.MapRoute("ManageYagnaSeva", "yagna/seva/{id}", new { controller = "Yagna", action = "ManageYagnaSeva", id = UrlParameter.Optional });
            routes.MapRoute("SearchYagnaSeva", "filter/seva", new { controller = "Yagna", action = "SearchYagnaSeva" });
            routes.MapRoute("DeleteSeva", "delete/seva/{id}", new { controller = "Yagna", action = "DeleteSeva", id = UrlParameter.Optional });

            routes.MapRoute("EMI", "detail/{id}", new { controller = "Yagna", action = "EMI", id = UrlParameter.Optional });
            routes.MapRoute("ManageEMI", "seva/emi/{pid}/{aId}", new { controller = "Yagna", action = "ManageEMI" });
            routes.MapRoute("AddEMI", "add/emi/{aid}", new { controller = "Yagna", action = "AddEMI" });
            routes.MapRoute("DeleteEMI", "delete/emi/{id}/{aId}", new { controller = "Yagna", action = "DeleteEMI" });

            #endregion

            routes.MapRoute("SearchKaryakarDetailByMobile", "filter/mobile", new { controller = "Yagna", action = "SearchKaryakarDetailByMobile" });
            routes.MapRoute("GetReferralDetailByMobile", "referral/mobile", new { controller = "Yagna", action = "SearcReferralDetailhByMobile" });

            #region seva Grade
            routes.MapRoute("SevaGradeHome", "sevagrade", new { controller = "SevaGrade", action = "Index" });
            routes.MapRoute("GradeList", "grade/list", new { controller = "SevaGrade", action = "GradeList" });

            routes.MapRoute("ManageSevaGrade", "manage/sevagrade/{id}", new { controller = "SevaGrade", action = "ManageSevaGrade", id = UrlParameter.Optional });
            routes.MapRoute("DeleteSevaGrade", "delete/sevagrade/{id}", new { controller = "SevaGrade", action = "DeleteSevaGrade", id = UrlParameter.Optional });


            #endregion

            #region seva type
            routes.MapRoute("SevaType", "sevatype", new { controller = "SevaType", action = "List" });
            routes.MapRoute("TypeList", "type/list", new { controller = "SevaType", action = "TypeList" });

            routes.MapRoute("ManageSevaType", "manage/sevatype/{id}", new { controller = "SevaType", action = "ManageSevaType", id = UrlParameter.Optional });
            routes.MapRoute("DeleteSevaType", "delete/sevatype/{id}", new { controller = "SevaType", action = "DeleteSevaType", id = UrlParameter.Optional });
            #endregion

            #region chosen input
            routes.MapRoute("AddNewInput", "addnewinput", new { controller = "Admin", action = "AddNewInput" });
            #endregion

            routes.MapRoute("RegistrationSummary", "registrationsummary", new { controller = "Yagna", action = "RegistrationSummary" });

            #region reports
            routes.MapRoute("YajmanList", "report/yajmanlist", new { controller = "Report", action = "YajmansList" });
            routes.MapRoute("FilterYajmanReport", "filter/report/yajmanlist", new { controller = "Report", action = "FilterYajmanReport" });

            routes.MapRoute("ReferralSevaList", "report/referral", new { controller = "Report", action = "ReferralSevaList" });
            routes.MapRoute("FilterReferralSevaReport", "filter/report/referralseva", new { controller = "Report", action = "FilterReferralSevaReport" });

            routes.MapRoute("CityWiseSummary", "city/summary", new { controller = "Report", action = "CityWiseSummary" });
            routes.MapRoute("FilterCityWiseSummary", "filter/citysummary", new { controller = "Report", action = "FilterCityWiseSummary" });

            routes.MapRoute("MandalWiseSummary", "mandal/summary", new { controller = "Report", action = "MandalWiseSummary" });
            routes.MapRoute("FilterMandalWiseSummary", "filter/mandalsummary", new { controller = "Report", action = "FilterMandalWiseSummary" });

            routes.MapRoute("Summary", "report/summary", new { controller = "Report", action = "Summary" });
            routes.MapRoute("FilterSummary", "filter/summary", new { controller = "Report", action = "FilterSummary" });

            #endregion

            #region referral detail
            routes.MapRoute("Refferal", "referral/list", new { controller = "Yagna", action = "ReferralList" });
            routes.MapRoute("ManageReferral", "manage/referral/{id}", new { controller = "Yagna", action = "ManageReferral", id = UrlParameter.Optional });
            routes.MapRoute("FilterReferralSeva", "filter/referralseva", new { controller = "Yagna", action = "FilterReferralSeva" });
            routes.MapRoute("DeleteReferralSeva", "delete/referralseva/{id}", new { controller = "Yagna", action = "DeleteReferralSeva", id = UrlParameter.Optional });
            #endregion
        }
    }
}