using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMM.Core;
using PMM.Core.Data;
using PMM.Service;
using PMM.Web.Models;

namespace PMM.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Yuvak/
        private readonly IUserDetailService _userService;
        private readonly IWorkContext _userContext;
        private readonly IAuthenticationService _authenticationService;
        public HomeController(IUserDetailService userService, IWorkContext userContext,
            IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._userContext = userContext;
            this._authenticationService = authenticationService;
        }


        public ActionResult Index()
        {
            var currentUser = _userContext.CurrentUser;
            if (currentUser == null)
                return RedirectToRoute("Login");

            return View("Dashboard");
        }

    }
}
