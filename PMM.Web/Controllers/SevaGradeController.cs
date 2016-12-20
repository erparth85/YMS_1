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
    public class SevaGradeController : Controller
    {
        private readonly ISevaGradeService sevaGradeService;
        private readonly IWorkContext contextService;

        public SevaGradeController(ISevaGradeService _sevaGradeService, IWorkContext _contextService)
        {
            this.sevaGradeService = _sevaGradeService;
            contextService = _contextService;
        }

        public ActionResult Index()
        {
            var model = new SevaGradeListModel();
            return View(model);
        }

        public ActionResult GradeList()
        {
            var model = new SevaGradeListModel();
            try
            {
                model.SevaGrades = sevaGradeService.GetAll().Select(x => new SevaGradeDetailModel()
                {
                    Id = x.Id,
                    Grade = x.Grade.ToString()
                }).ToList();
            }
            catch (Exception error)
            {
                Logger.Log("Error while loading seva grade list due to:" + error);
            }
            return PartialView("_SevaGradeList", model);
        }
        public ActionResult ManageSevaGrade(int id = 0)
        {

            var model = new SevaGradeDetailModel();
            try
            {
                if (id > 0)
                {
                    var sevaGradeDetail = sevaGradeService.GetSevaGradeById(id);
                    if (sevaGradeDetail != null)
                    {
                        model.Id = sevaGradeDetail.Id;
                        model.Grade = sevaGradeDetail.Grade.ToString();
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while get seva grade detail for id:" + id + " ,due to:" + error);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageSevaGrade(SevaGradeDetailModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var currentUser = contextService.CurrentUser;

                    SevaGrade newSeva = new SevaGrade();
                    newSeva.Id = model.Id;
                    newSeva.Grade = model.Grade.ToUpper();
                    newSeva.CreatedBy = newSeva.UpdatedBy = currentUser.Id;
                    newSeva.CreatedDate = newSeva.UpdatedDate = CommonHelper.DefaultDateTime;
                    sevaGradeService.SaveOrUpdateSevaGrade(newSeva);
                    return RedirectToAction("Index");
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
                    Logger.Log("Failure in modelstate validation while Save or update seva grade due to" + errorMsg);
                }
          
            }
            catch (Exception error)
            {
                Logger.Log("Error while manage seva grade detail for id:" + model.Id + ", due to:" + error);
            }
            return View(model);
        }

        public ActionResult DeleteSevaGrade(int id = 0)
        {
            var currentUser = contextService.CurrentUser;
            SevaGrade gradeDetail = new SevaGrade();
            gradeDetail.Id = id;
            gradeDetail.UpdatedBy = currentUser.Id;
            gradeDetail.UpdatedDate = CommonHelper.DefaultDateTime;
            sevaGradeService.DeleteSevaGradeDetailById(gradeDetail);
            return RedirectToRoute("GradeList");
        }
    }
}
