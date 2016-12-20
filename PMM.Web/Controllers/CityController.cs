using PMM.Service;
using PMM.Web.Models;
using System.Web.Mvc;
using PMM.Web.Extension;
using PMM.Core.Data;
using PMM.Core;
using System;
using System.Linq;

namespace PMM.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService cityService;
        private readonly IWorkContext contextService;

        public CityController(ICityService _cityService, IWorkContext _contextService)
        {
            this.cityService = _cityService;
            this.contextService = _contextService;
        }

        public ActionResult Index()
        {
            var model = new CityListModel();
            return View(model);
        }

        public ActionResult CityList()
        {
            var model = new CityListModel();

            model.Cities = cityService.GetAll().Select(x =>
             {
                 var cityDetail = new CityModel();
                 cityDetail.Title = x.Title;
                 cityDetail.Id = x.Id;
                 return cityDetail;
             }).ToList();
            return PartialView("_CityList", model);
        }

        public ActionResult ManageCity(int id = 0)
        {
            var model = new CityModel();
            if (id > 0)
            {
                model = cityService.GetCityById(id).ToModel();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageCity(CityModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cityEntity = model.ToEntity();
                    if (!cityService.IsCityNameIsAlreadyExist(cityEntity))
                    {
                        var currentUser = contextService.CurrentUser;
                        model.CreatedBy = model.UpdatedBy = currentUser.Id;
                        model.CreatedDate = model.UpdatedDate = CommonHelper.DefaultDateTime;
                        cityService.SaveOrUpdateCity(cityEntity);
                        return RedirectToRoute("CityHome");
                    }
                    else
                    {
                        ModelState.AddModelError("error", CommonHelper.ValErrorMsgForDuplicateCity);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while saveorupdating city detail for id: " + model.Id + ", due to:" + error);
            }
            return View(model);
        }

        public ActionResult DeleteCity(int id)
        {
            var model = new CityListModel();
            var currentUser = contextService.CurrentUser;
            City cityDetail = new City();
            cityDetail.Id = id;
            cityDetail.UpdatedBy = currentUser.Id;
            cityDetail.UpdatedDate = CommonHelper.DefaultDateTime;
            cityService.DeleteCity(cityDetail);

            model.Cities = cityService.GetAll().Select(x =>
            {
                var cityModel = new CityModel();
                cityModel.Title = x.Title;
                cityModel.Id = x.Id;
                return cityModel;
            }).ToList();
            return PartialView("_CityList", model);
        }

    }
}
