using PMM.Core;
using PMM.Core.Data;
using PMM.Service;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Controllers
{
    public class SevaTypeController : BaseController
    {
        private readonly ISevaTypeService sevaTypeService;
        private readonly IWorkContext contextService;
        private readonly ISevaGradeService gardeService;

        public SevaTypeController(ISevaTypeService _sevaTypeService, IWorkContext _contextService,
            ISevaGradeService _gardeService)
        {
            sevaTypeService = _sevaTypeService;
            contextService = _contextService;
            gardeService = _gardeService;
        }

        public ActionResult List()
        {
            var model = new SevaTypeListModel();
            return View(model);
        }

        public ActionResult TypeList()
        {
            var model = new SevaTypeListModel();
            try
            {
                model.SevaTypes = sevaTypeService.GetAll().Select(x => new SevaTypeDetailModel()
                {
                    Id = x.Id,
                    SevaGradeId = x.SevaGradeId,
                    SevaTypeText = x.SevaTypeText,
                    Amount = x.Amount,
                    SevaGradeText = x.SevaGradeText
                }).ToList();
            }
            catch (Exception error)
            {
                Logger.Log("error while get type list due to:" + error);
            }

            return PartialView("_SevaTypeList", model);
        }


        public ActionResult ManageSevaType(int id = 0)
        {
            var model = new SevaTypeDetailModel();
            try
            {
                if (id > 0)
                {
                    var sevaTypes = sevaTypeService.GetSevaTypeById(id);
                    if (sevaTypes != null && sevaTypes.Id > 0)
                    {
                        model.Id = sevaTypes.Id;
                        model.SevaGradeId = sevaTypes.SevaGradeId;
                        model.SevaGradeText = sevaTypes.SevaGradeText;
                        model.SevaTypeText = sevaTypes.SevaTypeText;
                        model.Amount = sevaTypes.Amount;
                    }
                }
                model.SevaGrades = GetSevaGrades(gardeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            }
            catch (Exception error)
            {
                Logger.Log("Error while manage sevatype for id:" + id + ", due to:" + error);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageSevaType(SevaTypeDetailModel model)
        {
            try
            {
                model.SevaGrades = GetSevaGrades(gardeService, CommonHelper.PlaceHolderDrdForSevaGrade);
                if (ModelState.IsValid)
                {
                    SevaType newSevaType = new SevaType();
                    newSevaType.Id = model.Id;
                    newSevaType.SevaGradeId = model.SevaGradeId;
                    newSevaType.SevaTypeText = model.SevaTypeText.ToUpper();
                    newSevaType.Amount = model.Amount;

                    var currentUser = contextService.CurrentUser;
                    newSevaType.IsDeleted = false;
                    newSevaType.CreatedBy = newSevaType.UpdatedBy = currentUser.Id;
                    newSevaType.CreatedDate = newSevaType.UpdatedDate = CommonHelper.DefaultDateTime;
                    sevaTypeService.SaveOrUpdateSevaTypeDetail(newSevaType);
                    return RedirectToRoute("SevaType");
                }
                else
                {
                    string errorMsg = "";
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            errorMsg += error.ErrorMessage;
                        }
                    }
                    Logger.Log("Failure in modelstate validation while Save or update seva type due to" + errorMsg);
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while manage seva type detail due to:" + error);
            }
            return View(model);
        }

        public ActionResult DeleteSevaType(int id = 0)
        {
            try
            {
                var currentUser = contextService.CurrentUser;
                SevaType typeDetail = new SevaType();
                typeDetail.Id = id;
                typeDetail.UpdatedBy = currentUser.Id;
                typeDetail.UpdatedDate = CommonHelper.DefaultDateTime;
                sevaTypeService.DeleteSevaTypeById(typeDetail);
            }
            catch (Exception error)
            {
                Logger.Log("Error while delete seva type for id:" + id + ", due to:" + error);
            }

            return RedirectToRoute("TypeList");
        }
    }
}
