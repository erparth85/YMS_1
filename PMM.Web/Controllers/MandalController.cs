using PMM.Core;
using PMM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMM.Web.Extension;
using PMM.Core.Data;
using PMM.Web.Models.Mandal;

namespace PMM.Web.Controllers
{
    public class MandalController : BaseController
    {
        private readonly IMandalService mandalService;
        private readonly IWorkContext contextService;
        private readonly ICityService cityService;

        public MandalController(IMandalService _mandalService, IWorkContext _contextService,
            ICityService _cityService)
        {
            this.mandalService = _mandalService;
            this.contextService = _contextService;
            this.cityService = _cityService;
        }


        public ActionResult Index()
        {
            var model = new MandalListModel();
            return View(model);
        }

        public ActionResult MandalList()
        {
            var model = new MandalListModel();

            var mandalList = mandalService.GetAll();
            model.Mandals = LoadList(mandalList);
            return PartialView("_MandalList", model);
        }

        public ActionResult ManageMandal(int id = 0)
        {
            var model = new MandalDetailModel();
            if (id > 0)
            {
                model = mandalService.GetMandalById(id).ToModel();
            }
            model.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity,false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMandal(MandalDetailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mandalEntity = model.ToEntity();
                    if (!mandalService.IsMandalNameExist(mandalEntity))
                    {
                        var currentUser = contextService.CurrentUser;
                        model.CreatedBy = model.UpdatedBy = currentUser.Id;
                        model.CreatedDate = model.UpdatedDate = CommonHelper.DefaultDateTime;
                        mandalService.SaveOrUpdateMandal(mandalEntity);
                        return RedirectToRoute("MandalHome");
                    }
                    else
                    {
                        ModelState.AddModelError("error", CommonHelper.ValErrorMsgForDuplicateMandal);
                    }                  
                }
                model.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity,false);
            }
            catch (Exception error)
            {
                Logger.Log("Error while saveorupdating Mandal detail for id: " + model.Id + ", due to:" + error);
            }
            return View(model);
        }

        public ActionResult DeleteMandal(int id)
        {
            var model = new MandalListModel();
            var currentUser = contextService.CurrentUser;
            Mandal MandalDetail = new Mandal();
            MandalDetail.Id = id;
            MandalDetail.IsDeleted = true;
            MandalDetail.UpdatedBy = currentUser.Id;
            MandalDetail.UpdatedDate = CommonHelper.DefaultDateTime;
            mandalService.DeleteMandalDetailById(MandalDetail);

            var MandalList = mandalService.GetAll();
            model.Mandals = LoadList(MandalList);
            return PartialView("_MandalList", model);
        }

        private IList<MandalDetailModel> LoadList(List<Mandal> MandalList)
        {
            return MandalList.Select(x =>
            {
                var mandalModel = new MandalDetailModel();
                mandalModel.MandalName = x.Title;
                mandalModel.CityId = x.CityId;
                mandalModel.Id = x.Id;
                mandalModel.CityTitle = x.CityName;
                return mandalModel;
            }).ToList();
        }

        public JsonResult LoadMandalByCity(int cityId)
        {
            var mandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, cityId,false);
            return Json(new { data = mandalList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadMandalBySelectedCity(string cityId)
        {
            var mandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, cityId, false);
            return Json(new { data = mandalList }, JsonRequestBehavior.AllowGet);
        }
    }
}
