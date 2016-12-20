using System.Web.Mvc;
using PMM.Core;
using PMM.Service;
using PMM.Web.Models;
using PMM.Core.Data;
using System;

namespace PMM.Web.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: Yuvak/


        private readonly IUserDetailService _userService;
        private readonly IWorkContext _userContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICityService cityService;
        private readonly IMandalService mandalService;

        public AdminController(IUserDetailService userService, IWorkContext userContext,
            IAuthenticationService authenticationService, ICityService _cityService, IMandalService _mandalService)
        {
            _userService = userService;
            _userContext = userContext;
            _authenticationService = authenticationService;
            cityService = _cityService;
            mandalService = _mandalService;
        }


        #region login
        public ActionResult Login()
        {
            var currentUser = _userContext.CurrentUser;
            if (currentUser != null)
            {
                return RedirectToRoute("Default");
            }
            return View();
        }

        //[HttpPost]
        public ActionResult UserLogin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var _userDetail = _userService.ValidateUser(model.Username, model.Password);
                if (_userDetail != null && _userDetail.Id > 0)
                {
                    _authenticationService.SignIn(_userDetail, model.IsRemember);

                    return Json(new
                    {
                        status = "success"
                    });
                    // return RedirectUserToHome(_userDetail);
                }
                //else
                //{
                //    ModelState.AddModelError("error", "Invalid Username or Password");
                //}
                return Json(new
                {
                    status = "Invalid Username or Password"
                });
            }
            else
            {
                ModelState.AddModelError("error", "Invalid Username or Password");
            }
            return Json(new
            {
                status = "Invalid Username or Password"
            });
            //return RedirectToAction("Index", "User");   
        }

        public ActionResult Logout()
        {
            _authenticationService.SignOut();
            return RedirectToRoute("Login");
        }
        #endregion

        #region add chosen value
        [HttpPost]
        public ActionResult AddNewInput(string tag, string target)
        {
            try
            {
                var currentUser = _userContext.CurrentUser;

                int returnValue = 0;
                bool isSuccess = false;
                if (target.ToLower().Trim().Contains("cityid"))
                {

                    City cityData = new City();
                    cityData.Title = tag.ToUpper();
                    cityData.UpdatedBy = cityData.CreatedBy = currentUser.Id;
                    cityData.CreatedDate = cityData.UpdatedDate = CommonHelper.DefaultDateTime;
                    if (!cityService.IsCityNameIsAlreadyExist(cityData))
                    {
                        returnValue = cityService.SaveOrUpdateCity(cityData);
                        isSuccess = true;
                    }
                }
                else if (target.ToLower().Trim().Contains("mandalid"))
                {
                    Mandal mandalData = new Mandal();
                    mandalData.CityId = 0;
                    mandalData.Title = tag.ToUpper();
                    mandalData.UpdatedBy = mandalData.CreatedBy = currentUser.Id;
                    mandalData.CreatedDate = mandalData.UpdatedDate = CommonHelper.DefaultDateTime;
                    if (!mandalService.IsMandalNameExist(mandalData))
                    {
                        returnValue = mandalService.SaveOrUpdateMandal(mandalData);
                        isSuccess = true;
                    }
                }

                if (isSuccess)
                {
                    return Json(new
                    {
                        html = string.Format("Added New item {0} to {1}", tag, target),
                        value = returnValue
                    });
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while insert new tag due to:" + error);
            }

            return Json(new
            {
                error = "Unable to add item.",
            });

        }
        #endregion
    }
}
