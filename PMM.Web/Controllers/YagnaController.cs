using PMM.Core;
using PMM.Service;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMM.Web.Extension;
using PMM.Core.Data;
using System.Data;

namespace PMM.Web.Controllers
{
    public class YagnaController : BaseController
    {
        private readonly IYagnaSevaDetailService yagnaService;
        private readonly ICityService cityService;
        private readonly IMandalService mandalService;
        private readonly IWorkContext contextService;
        private readonly IUserDetailService userDetailService;
        private readonly ISevaGradeService isevaGradeService;

        public YagnaController(IYagnaSevaDetailService _yagnaService, ICityService _cityService, IMandalService _mandalService,
            IWorkContext _contextService, IUserDetailService _userDetailService, ISevaGradeService sevaGradeService)
        {
            this.yagnaService = _yagnaService;
            this.cityService = _cityService;
            this.mandalService = _mandalService;
            this.contextService = _contextService;
            this.userDetailService = _userDetailService;
            isevaGradeService = sevaGradeService;
        }

        public ActionResult List()
        {
            var model = new YagnaSevaListModel();
            var filterModel = new FilterDataModel();
            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, filterModel.CityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;
            return View(model);
        }

        public ActionResult SearchYagnaSeva(YagnaSevaListModel yagnaData)
        {
            YagnaSevaListModel model = new YagnaSevaListModel();
            try
            {
                if (yagnaData.PageNumber <= 0) yagnaData.PageNumber = 1;
                DataSet dsYagnaList = new DataSet();
                if (yagnaData.Filter != null)
                {
                    var filter = yagnaData.Filter;
                    dsYagnaList = yagnaService.GetAll(filter.Name == null ? "" : filter.Name, filter.Mobile == null ? "" : filter.Mobile, filter.CityId, filter.MandalId, filter.SevaGradeId, filter.PRN == null ? "" : filter.PRN, filter.BookNo, filter.ReceiptNo, yagnaData.PageNumber);
                }
                else
                {
                    dsYagnaList = yagnaService.GetAll("", "", 0, 0, 0, "", "", "", yagnaData.PageNumber);

                }
                LoadList(model, dsYagnaList);
                if (dsYagnaList != null && dsYagnaList.Tables.Count > 0)
                {
                    if (dsYagnaList.Tables[1] != null && dsYagnaList.Tables[1].Rows.Count > 0)
                    {
                        //foreach (DataRow yagnaRow in dsYagnaList.Tables[1].Rows)
                        //{
                        //    model.TotalAmount = Convert.ToDecimal(yagnaRow["TotalAmount"].ToString());
                        //}
                    }
                    if (dsYagnaList.Tables[2] != null && dsYagnaList.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow yagnaRow in dsYagnaList.Tables[2].Rows)
                        {
                            int totalPages = ((Convert.ToInt32(yagnaRow["TotalRecord"].ToString()) - 1) / CommonHelper.PageSize);
                            model.TotalPages = 0;
                            if (totalPages > 0)
                            {
                                model.TotalPages = totalPages + 1;
                            }
                            model.PageNumber = yagnaData.PageNumber;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while search yagna seva detail, Due to:" + error);
            }
            return PartialView("_YagnaList", model);
        }

        private void LoadList(YagnaSevaListModel model, DataSet yagnaSevaList)
        {
            if (yagnaSevaList != null && yagnaSevaList.Tables.Count > 0)
            {
                if (yagnaSevaList.Tables[0] != null && yagnaSevaList.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow yagnaRow in yagnaSevaList.Tables[0].Rows)
                    {
                        var sevaModel = new YagnaSevaModel();
                        var yajmanModel = new BasicDetailModel();
                        var userModel = new BasicDetailModel();
                        var accountModel = new AccountDetailModel();
                        var transModel = new TransactionDetailModel();

                        /// Yagna Seva Detail
                        sevaModel.Id = Convert.ToInt32(yagnaRow["Id"].ToString());
                        sevaModel.SankalpAmount = yagnaRow["SankalpAmount"].ToString();
                        sevaModel.SevaGradeTitle = yagnaRow["Grade"].ToString();

                        sevaModel.Remarks = yagnaRow["Remarks"].ToString();
                        sevaModel.PRN = yagnaRow["PRN"].ToString();

                        ///Yajman User Detail
                        yajmanModel.FirstName = yagnaRow["YajmanFirstName"].ToString();
                        yajmanModel.MiddleName = yagnaRow["YajmanMiddleName"].ToString();
                        yajmanModel.LastName = yagnaRow["YajmanLastName"].ToString();
                        yajmanModel.Mobile = yagnaRow["YajmanMobile"].ToString();
                        yajmanModel.CityName = yagnaRow["YajmanCity"].ToString();
                        yajmanModel.MandalName = yagnaRow["YajmanMandal"].ToString();

                        /// Seva Bring By user Detail
                        userModel.FirstName = yagnaRow["FirstName"].ToString();
                        userModel.MiddleName = yagnaRow["MiddleName"].ToString();
                        userModel.LastName = yagnaRow["LastName"].ToString();
                        userModel.Mobile = yagnaRow["Mobile"].ToString();
                        userModel.CityName = yagnaRow["CityName"].ToString();
                        userModel.MandalName = yagnaRow["MandalName"].ToString();

                        /// Account detail
                        accountModel.DueAmount = yagnaRow["DueAmount"].ToString();


                        sevaModel.Yajmans = yajmanModel;
                        sevaModel.Karyakar = userModel;
                        sevaModel.AccountDetail = accountModel;
                        sevaModel.TransactionDetail = transModel;
                        model.YagnaSevaList.Add(sevaModel);
                    }
                }

            }
        }

        public ActionResult ManageYagnaSeva(int id = 0)
        {
            var model = new YagnaSevaModel();
            try
            {
                var karyakarDetailModel = new BasicDetailModel();
                var yajmanDetailmodel = new BasicDetailModel();

                var accountDetailModel = new AccountDetailModel();
                var transactionDetailModel = new TransactionDetailModel();
                var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();

                model.AvailableForYagna = true;

                DataSet ds = new DataSet();
                if (id > 0)
                {
                    var sevaDetail = yagnaService.GetSevaDetailById(id);
                    if (sevaDetail != null && sevaDetail.Tables.Count > 0)
                    {
                        model = sevaDetail.Tables[0].YagnaEntityToModel();
                        yajmanDetailmodel = sevaDetail.Tables[1].ToModel();
                        karyakarDetailModel = sevaDetail.Tables[2].ToModel();
                        accountDetailModel = sevaDetail.Tables[3].AccountDetailToModel();
                        transactionDetailModel = sevaDetail.Tables[4].TransactionToModel();

                        if (sevaDetail.Tables[5] != null && sevaDetail.Tables[5].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[5].Rows)
                            {
                                svikrutiPatrakDetail.FormNo = dRow["FormNo"].ToString();
                            }
                        }

                        if (sevaDetail.Tables[6] != null && sevaDetail.Tables[6].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[6].Rows)
                            {
                                accountDetailModel.PaidAmount = dRow["PaidAmount"].ToString();
                                accountDetailModel.DueAmount = dRow["DueAmount"].ToString();
                            }
                        }
                    }
                    model.Id = id;
                }
                else
                {
                    accountDetailModel.DateOfReceipt = DateTime.Now.ToString("MM/dd/yyyy");
                }
                DataSet dsList = yagnaService.GetYagnaFormValue(yajmanDetailmodel.CityId, karyakarDetailModel.CityId);
                if (dsList != null)
                {
                    BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailmodel, model, accountDetailModel, transactionDetailModel, null);
                }

                if (model.SevaGrades.Count > 0)
                {
                    var lastSevaGraded = model.SevaGrades.OrderByDescending(t => t.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(lastSevaGraded.Value))
                    {
                        model.SevaGradeId = Convert.ToInt32(lastSevaGraded.Value);
                    }
                }


                model.Yajmans = yajmanDetailmodel;
                model.Karyakar = karyakarDetailModel;
                model.AccountDetail = accountDetailModel;
                model.TransactionDetail = transactionDetailModel;
                model.SvikrutiPatrakDetail = svikrutiPatrakDetail;

                //List<ErrorSummaryModel> errorSummaryList = new List<ErrorSummaryModel>();
                model.ErrorSummary = new List<ErrorSummaryModel>();
            }
            catch (Exception error)
            {
                Logger.Log("error while manage yagna seva for id:" + id + ", due to:" + error);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageYagnaSeva(YagnaSevaModel model)
        {
            var karyakarDetailModel = model.Karyakar;
            var yajmanDetailModel = model.Yajmans;
            var accountDetailModel = model.AccountDetail;
            var transactionDetailModel = model.TransactionDetail;
            var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();
            List<ErrorSummaryModel> errorList = new List<ErrorSummaryModel>();


            DataSet dsList = new DataSet();

            try
            {
                #region Disable Validation for Karyakar address + pincode

                ModelState.Remove("Karyakar.Address");
                ModelState.Remove("Karyakar.PinCode");
                #endregion

                if (ModelState.IsValid)
                {
                    var currentUser = contextService.CurrentUser;

                    YagnaDetail newYagna = new YagnaDetail();
                    model.CreatedBy = model.UpdatedBy = currentUser.Id;
                    newYagna = model.YagnaModelToEntity();

                    newYagna.KaryakarDetail = model.Karyakar.ToEntity();
                    newYagna.YajmanDetail = model.Yajmans.ToEntity();

                    newYagna.Account = model.AccountDetail.ToEntity();
                    newYagna.Transaction = model.TransactionDetail.ToEntity();

                    newYagna.SvikrutiPatraDetail = model.SvikrutiPatrakDetail.ToEntity();


                    dsList = yagnaService.CheckRegistrationDetail(newYagna);
                    if (!model.IsContinue)
                    {
                        if (dsList.Tables.Count > 0)
                        {
                            if (dsList.Tables[0] != null && dsList.Tables[0].Rows.Count > 0)
                            {
                                errorList = dsList.Tables[0].ToModel(model.Id);
                                if (errorList.Count > 0)
                                {
                                    model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateYagnaDetail;
                                    model.ErrorSummary = errorList;
                                    model.ErrorType = RegistrationErrorTypes.FormNo;
                                }
                            }
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[2] != null && dsList.Tables[2].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[2].ToModel(model.Id);
                                    if (errorList.Count > 0)
                                    {
                                        model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateTransactionDetail;
                                        model.ErrorSummary = errorList;
                                        model.ErrorType = RegistrationErrorTypes.TransactionNumber;
                                    }
                                }
                            }
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[1] != null && dsList.Tables[1].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[1].ToModel(model.Id);
                                    model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateAccountNo;
                                    model.ErrorSummary = errorList;
                                    model.ErrorType = RegistrationErrorTypes.Account;
                                }
                            }
                        }
                    }
                    if (errorList.Count == 0)
                    {
                        int prn = yagnaService.SaveOrUpdateYagnaDetail(newYagna);

                        var successModel = new RegistrationSummaryModel();
                        successModel = newYagna.ToModel(prn);
                        if (model.Id == 0)
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullRegistrationPopUpHeader;
                        }
                        else
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullUpdatePopUpHeader;
                        }

                        return RedirectToRoute("RegistrationSummary", successModel);
                    }

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
                    Logger.Log("Failure in modelstate validation while Save or update yagna seva due to" + errorMsg);
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while save or update yagna seva detail for yagnaId:" + model.Id + ", due to:" + error);
            }

            dsList = yagnaService.GetYagnaFormValue(yajmanDetailModel.CityId, karyakarDetailModel.CityId);
            if (dsList != null)
            {
                BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailModel, model, accountDetailModel, transactionDetailModel, null);
            }

            model.Yajmans = yajmanDetailModel;
            model.Karyakar = karyakarDetailModel;
            model.AccountDetail = accountDetailModel;
            model.TransactionDetail = transactionDetailModel;
            model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
            return View(model);
        }

        public ActionResult RegistrationSummary(RegistrationSummaryModel summaryModel)
        {
            return View(summaryModel);
        }

        public ActionResult DeleteSeva(int id = 0)
        {
            try
            {
                var currentUser = contextService.CurrentUser;
                YagnaSevaDetail sevaDetail = new YagnaSevaDetail();
                sevaDetail.Id = id;
                sevaDetail.UpdatedBy = currentUser.Id;
                yagnaService.DeleteYagnaSevaDetail(sevaDetail);
            }
            catch (Exception error)
            {
                Logger.Log("error while delete yajman detail due to:" + error);
            }


            return RedirectToRoute("SearchYagnaSeva");



        }

        private void BindDropdownlist(DataSet ds, BasicDetailModel karyakarDetailModel, BasicDetailModel yajmanDetailModel, YagnaSevaModel model, AccountDetailModel accountDetailModel, TransactionDetailModel transactionDetailModel, ReferralYagnaSevaModel referralModel)
        {
            try
            {
                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        yajmanDetailModel.CityList = BindDropdownFromDataTable(ds.Tables[0], CommonHelper.PlaceHolderDrdForCity, "Title", "Id", "City");
                        if (karyakarDetailModel != null)
                        {
                            karyakarDetailModel.CityList = BindDropdownFromDataTable(ds.Tables[0], CommonHelper.PlaceHolderDrdForCity, "Title", "Id", "City");
                        }
                    }
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        yajmanDetailModel.Mandals = BindDropdownFromDataTable(ds.Tables[1], CommonHelper.PlaceHolderDrdForMandal, "Title", "Id", "Mandal");
                    }
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        if (karyakarDetailModel != null)
                        {
                            karyakarDetailModel.Mandals = BindDropdownFromDataTable(ds.Tables[2], CommonHelper.PlaceHolderDrdForMandal, "Title", "Id", "Mandal");
                        }
                    }
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        if (referralModel != null)
                        {
                            referralModel.SevaGrades = BindDropdownFromDataTable(ds.Tables[3], CommonHelper.PlaceHolderDrdForSevaGrade, "Grade", "Id", "Grade");
                        }
                        if (model != null)
                        {
                            model.SevaGrades = BindDropdownFromDataTable(ds.Tables[3], CommonHelper.PlaceHolderDrdForSevaGrade, "Grade", "Id", "Grade");
                        }
                    }
                    //if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                    //{
                    // model.YagnaDays = BindDropdownFromDataTable(ds.Tables[4], CommonHelper.PlaceHolderDrdForYagnaDay, "YagnaDay", "Id", "City");
                    //  }

                    if (transactionDetailModel != null)
                    {
                        transactionDetailModel.TransactionTypes = GetTransactionTypes("");
                    }


                }
            }
            catch (Exception error)
            {
                Logger.Log("error while binding dropdownlist for manage yagna seva form due to:" + error);
            }
        }

        private List<SelectListItem> BindDropdownFromDataTable(DataTable dtRecords, string prefix, string textFieldName, string valuefieldName, string type)
        {
            var list = new List<SelectListItem>();
            try
            {
                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (DataRow record in dtRecords.Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = record[textFieldName].ToString(),
                        Value = record[valuefieldName].ToString()
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting " + type + " List from dataTable due to:" + error);
            }
            return list;
        }

        public JsonResult SearchKaryakarDetailByMobile(string searchTxt)
        {
            try
            {
                List<BasicDetailModel> basicDetailList = new List<BasicDetailModel>();

                if (!string.IsNullOrWhiteSpace(searchTxt))
                {
                    var karyakarList = userDetailService.GetKaryakarDetailByMobile(searchTxt);
                    PrepareList(basicDetailList, karyakarList, searchTxt);
                    return Json(new { data = basicDetailList, status = "success", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while GetKaryakarDetailByMobile for search Text:" + searchTxt + ",due to:" + error);
            }
            return Json(new { data = "", status = "failure", JsonRequestBehavior.AllowGet });
        }


        public JsonResult SearcReferralDetailhByMobile(string searchTxt)
        {
            try
            {
                List<BasicDetailModel> basicDetailList = new List<BasicDetailModel>();

                if (!string.IsNullOrWhiteSpace(searchTxt))
                {
                    var karyakarList = yagnaService.GetReferralDetailByMobile(searchTxt);
                    PrepareList(basicDetailList, karyakarList, searchTxt);
                    return Json(new { data = basicDetailList, status = "success", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while GetReferralDetailByMobile for search Text:" + searchTxt + ",due to:" + error);
            }
            return Json(new { data = "", status = "failure", JsonRequestBehavior.AllowGet });
        }
        private void PrepareList(List<BasicDetailModel> basicDetailList, List<UserDetail> karyakarList, string searchTxt)
        {
            if (karyakarList != null && karyakarList.Count > 0)
            {
                foreach (var sevak in karyakarList)
                {
                    var detail = new BasicDetailModel();
                    detail.FirstName = sevak.FirstName;
                    detail.MiddleName = sevak.MiddleName;
                    detail.LastName = sevak.LastName;
                    detail.Mobile = sevak.Mobile;
                    detail.Id = sevak.Id;
                    detail.CityId = sevak.CityId;
                    detail.MandalId = sevak.MandalId;
                    basicDetailList.Add(detail);
                }
            }
            var basicDetailModel = new BasicDetailModel();
            basicDetailModel.FirstName = "New";
            basicDetailModel.MiddleName = basicDetailModel.LastName = "";
            basicDetailModel.Mobile = searchTxt;
            basicDetailModel.CityId = basicDetailModel.MandalId = 0;
            basicDetailList.Add(basicDetailModel);

        }



        #region manage EMI/Transaction

        public ActionResult EMI(int id)
        {


            var model = new YagnaSevaModel();
            try
            {
                var karyakarDetailModel = new BasicDetailModel();
                var yajmanDetailmodel = new BasicDetailModel();

                var accountDetailModel = new AccountDetailModel();
                var transactionDetailsModel = new List<TransactionDetailModel>();
                var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();

                var transactionModel = new TransactionDetailModel();

                DataSet ds = new DataSet();
                if (id > 0)
                {
                    var sevaDetail = yagnaService.GetTransactionDetailById(id, 0);
                    if (sevaDetail != null && sevaDetail.Tables.Count > 0)
                    {
                        model = sevaDetail.Tables[0].YagnaEntityToModel();
                        yajmanDetailmodel = sevaDetail.Tables[1].ToModel();
                        karyakarDetailModel = sevaDetail.Tables[2].ToModel();
                        accountDetailModel = sevaDetail.Tables[3].AccountDetailToModel();


                        if (sevaDetail.Tables[4] != null && sevaDetail.Tables[4].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[4].Rows)
                            {
                                svikrutiPatrakDetail.FormNo = dRow["FormNo"].ToString();
                            }
                        }

                        transactionDetailsModel = sevaDetail.Tables[5].TransactionDetailToModel();
                        if (sevaDetail.Tables[7] != null && sevaDetail.Tables[7].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[7].Rows)
                            {
                                accountDetailModel.PaidAmount = dRow["PaidAmount"].ToString();
                                accountDetailModel.DueAmount = dRow["DueAmount"].ToString();
                            }
                        }
                    }
                    //model.Id = id;
                }


                DataSet dsList = yagnaService.GetYagnaFormValue(yajmanDetailmodel.CityId, karyakarDetailModel.CityId);
                if (dsList != null)
                {
                    BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailmodel, model, accountDetailModel, transactionModel, null);
                }

                if (model.SevaGrades.Count > 0)
                {
                    var lastSevaGraded = model.SevaGrades.OrderByDescending(t => t.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(lastSevaGraded.Value))
                    {
                        model.SevaGradeId = Convert.ToInt32(lastSevaGraded.Value);
                    }
                }


                model.Yajmans = yajmanDetailmodel;
                model.Karyakar = karyakarDetailModel;
                model.AccountDetail = accountDetailModel;
                model.TransactionDetail = transactionModel;
                model.TransactionDetails = transactionDetailsModel;
                model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
                model.ErrorSummary.Add(new ErrorSummaryModel());
            }
            catch (Exception error)
            {
                Logger.Log("error while manage yagna seva for id:" + id + ", due to:" + error);
            }
            return View(model);
        }

        public ActionResult AddEMI(int aid = 0)
        {

            var model = new YagnaSevaModel();
            try
            {
                var karyakarDetailModel = new BasicDetailModel();
                var yajmanDetailmodel = new BasicDetailModel();

                var accountDetailModel = new AccountDetailModel();
                var transactionDetailModel = new TransactionDetailModel();
                var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();


                DataSet ds = new DataSet();
                if (aid > 0)
                {
                    var sevaDetail = yagnaService.GetTransactionDetailById(aid, 0);
                    if (sevaDetail != null && sevaDetail.Tables.Count > 0)
                    {
                        model = sevaDetail.Tables[0].YagnaEntityToModel();
                        yajmanDetailmodel = sevaDetail.Tables[1].ToModel();
                        karyakarDetailModel = sevaDetail.Tables[2].ToModel();
                        // accountDetailModel = sevaDetail.Tables[3].AccountDetailToModel();


                        if (sevaDetail.Tables[4] != null && sevaDetail.Tables[4].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[4].Rows)
                            {
                                svikrutiPatrakDetail.FormNo = dRow["FormNo"].ToString();
                            }
                        }
                        // transactionDetailModel = sevaDetail.Tables[6].TransactionToModel();
                        if (sevaDetail.Tables[7] != null && sevaDetail.Tables[7].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[7].Rows)
                            {
                                accountDetailModel.DueAmount = dRow["DueAmount"].ToString();
                            }
                        }
                    }
                }


                accountDetailModel.DateOfReceipt = DateTime.Now.ToString("MM/dd/yyyy");

                DataSet dsList = yagnaService.GetYagnaFormValue(yajmanDetailmodel.CityId, karyakarDetailModel.CityId);
                if (dsList != null)
                {
                    BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailmodel, model, accountDetailModel, transactionDetailModel, null);
                }

                if (model.SevaGrades.Count > 0)
                {
                    var lastSevaGraded = model.SevaGrades.OrderByDescending(t => t.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(lastSevaGraded.Value))
                    {
                        model.SevaGradeId = Convert.ToInt32(lastSevaGraded.Value);
                    }
                }


                model.Yajmans = yajmanDetailmodel;
                model.Karyakar = karyakarDetailModel;
                model.AccountDetail = accountDetailModel;
                model.TransactionDetail = transactionDetailModel;
                model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
                model.ErrorSummary.Add(new ErrorSummaryModel());
            }
            catch (Exception error)
            {
                Logger.Log("error while manage yagna seva for id:" + aid + ", due to:" + error);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEMI(YagnaSevaModel model)
        {
            var karyakarDetailModel = model.Karyakar;
            var yajmanDetailModel = model.Yajmans;
            var accountDetailModel = model.AccountDetail;
            var transactionDetailModel = model.TransactionDetail;
            var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();
            List<ErrorSummaryModel> errorList = new List<ErrorSummaryModel>();


            DataSet dsList = new DataSet();

            try
            {
                #region Disable Validation for Karyakar address + pincode

                ModelState.Remove("Karyakar.Address");
                ModelState.Remove("Karyakar.PinCode");
                #endregion

                if (ModelState.IsValid)
                {
                    var currentUser = contextService.CurrentUser;

                    YagnaDetail newYagna = new YagnaDetail();
                    model.CreatedBy = model.UpdatedBy = currentUser.Id;
                    newYagna = model.YagnaModelToEntity();

                    newYagna.KaryakarDetail = model.Karyakar.ToEntity();
                    newYagna.YajmanDetail = model.Yajmans.ToEntity();

                    newYagna.Account = model.AccountDetail.ToEntity();
                    newYagna.Transaction = model.TransactionDetail.ToEntity();

                    newYagna.SvikrutiPatraDetail = model.SvikrutiPatrakDetail.ToEntity();


                    dsList = yagnaService.CheckRegistrationDetail(newYagna);
                    if (!model.IsContinue)
                    {
                        if (dsList.Tables.Count > 0)
                        {
                            //if (dsList.Tables[0] != null && dsList.Tables[0].Rows.Count > 0)
                            //{
                            //    errorList = dsList.Tables[0].ToModel(model.Id, newYagna.Account.Id);
                            //    if (errorList.Count > 0)
                            //    {
                            //        model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateYagnaDetail;
                            //        model.ErrorSummary = errorList;
                            //        model.ErrorType = RegistrationErrorTypes.FormNo;
                            //    }
                            //}
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[2] != null && dsList.Tables[2].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[2].ToModel(model.Id, newYagna.Account.Id);
                                    if (errorList.Count > 0)
                                    {
                                        model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateTransactionDetail;
                                        model.ErrorSummary = errorList;
                                        model.ErrorType = RegistrationErrorTypes.TransactionNumber;
                                    }
                                }
                            }
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[1] != null && dsList.Tables[1].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[1].ToModel(model.Id, newYagna.Account.Id);
                                    model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateAccountNo;
                                    model.ErrorSummary = errorList;

                                    model.ErrorType = RegistrationErrorTypes.Account;
                                }
                            }
                        }
                    }
                    if (errorList.Count == 0)
                    {
                        yagnaService.SaveOrUpdateTransactionDetail(newYagna);

                        var successModel = new RegistrationSummaryModel();
                        successModel = newYagna.ToModel(Convert.ToInt32(model.PRN));
                        if (model.Id == 0)
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullRegistrationPopUpHeader;
                        }
                        else
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullUpdatePopUpHeader;
                        }
                        return RedirectToRoute("RegistrationSummary", successModel);
                    }

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
                    Logger.Log("Failure in modelstate validation while Save or update yagna seva due to" + errorMsg);
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while save or update yagna seva detail for yagnaId:" + model.Id + ", due to:" + error);
            }

            dsList = yagnaService.GetYagnaFormValue(yajmanDetailModel.CityId, karyakarDetailModel.CityId);
            if (dsList != null)
            {
                BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailModel, model, accountDetailModel, transactionDetailModel, null);
            }

            model.Yajmans = yajmanDetailModel;
            model.Karyakar = karyakarDetailModel;
            model.AccountDetail = accountDetailModel;
            model.TransactionDetail = transactionDetailModel;
            model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
            return View(model);
        }

        public ActionResult ManageEMI(int pid = 0, int aId = 0)
        {

            var model = new YagnaSevaModel();
            try
            {
                var karyakarDetailModel = new BasicDetailModel();
                var yajmanDetailmodel = new BasicDetailModel();

                var accountDetailModel = new AccountDetailModel();
                var transactionDetailModel = new TransactionDetailModel();
                var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();


                DataSet ds = new DataSet();
                if (pid > 0)
                {
                    var sevaDetail = yagnaService.GetTransactionDetailById(pid, aId);
                    if (sevaDetail != null && sevaDetail.Tables.Count > 0)
                    {
                        model = sevaDetail.Tables[0].YagnaEntityToModel();
                        yajmanDetailmodel = sevaDetail.Tables[1].ToModel();
                        karyakarDetailModel = sevaDetail.Tables[2].ToModel();
                        accountDetailModel = sevaDetail.Tables[3].AccountDetailToModel();


                        if (sevaDetail.Tables[4] != null && sevaDetail.Tables[4].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[4].Rows)
                            {
                                svikrutiPatrakDetail.FormNo = dRow["FormNo"].ToString();
                            }
                        }
                        transactionDetailModel = sevaDetail.Tables[6].TransactionToModel();
                        if (sevaDetail.Tables[7] != null && sevaDetail.Tables[7].Rows.Count > 0)
                        {
                            foreach (DataRow dRow in sevaDetail.Tables[7].Rows)
                            {
                                accountDetailModel.DueAmount = dRow["DueAmount"].ToString();
                            }
                        }
                    }

                }
                if (accountDetailModel != null && !string.IsNullOrWhiteSpace(accountDetailModel.DateOfReceipt))
                {
                    accountDetailModel.DateOfReceipt = DateTime.Now.ToString("MM/dd/yyyy");
                }
                DataSet dsList = yagnaService.GetYagnaFormValue(yajmanDetailmodel.CityId, karyakarDetailModel.CityId);
                if (dsList != null)
                {
                    BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailmodel, model, accountDetailModel, transactionDetailModel, null);
                }

                if (model.SevaGrades.Count > 0)
                {
                    var lastSevaGraded = model.SevaGrades.OrderByDescending(t => t.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(lastSevaGraded.Value))
                    {
                        model.SevaGradeId = Convert.ToInt32(lastSevaGraded.Value);
                    }
                }


                model.Yajmans = yajmanDetailmodel;
                model.Karyakar = karyakarDetailModel;
                model.AccountDetail = accountDetailModel;
                model.TransactionDetail = transactionDetailModel;
                model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
                model.ErrorSummary.Add(new ErrorSummaryModel());
            }
            catch (Exception error)
            {
                Logger.Log("error while manage yagna seva for id:" + pid + ", due to:" + error);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageEMI(YagnaSevaModel model)
        {
            var karyakarDetailModel = model.Karyakar;
            var yajmanDetailModel = model.Yajmans;
            var accountDetailModel = model.AccountDetail;
            var transactionDetailModel = model.TransactionDetail;
            var svikrutiPatrakDetail = new SvikrutiPatrakDetailModel();
            List<ErrorSummaryModel> errorList = new List<ErrorSummaryModel>();


            DataSet dsList = new DataSet();

            try
            {
                #region Disable Validation for Karyakar address + pincode

                ModelState.Remove("Karyakar.Address");
                ModelState.Remove("Karyakar.PinCode");
                #endregion

                if (ModelState.IsValid)
                {
                    var currentUser = contextService.CurrentUser;

                    YagnaDetail newYagna = new YagnaDetail();
                    model.CreatedBy = model.UpdatedBy = currentUser.Id;
                    newYagna = model.YagnaModelToEntity();

                    newYagna.KaryakarDetail = model.Karyakar.ToEntity();
                    newYagna.YajmanDetail = model.Yajmans.ToEntity();

                    newYagna.Account = model.AccountDetail.ToEntity();
                    newYagna.Transaction = model.TransactionDetail.ToEntity();

                    newYagna.SvikrutiPatraDetail = model.SvikrutiPatrakDetail.ToEntity();


                    dsList = yagnaService.CheckRegistrationDetail(newYagna);
                    if (!model.IsContinue)
                    {
                        if (dsList.Tables.Count > 0)
                        {
                            //if (dsList.Tables[0] != null && dsList.Tables[0].Rows.Count > 0)
                            //{
                            //    errorList = dsList.Tables[0].ToModel(model.Id, newYagna.Account.Id);
                            //    if (errorList.Count > 0)
                            //    {
                            //        model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateYagnaDetail;
                            //        model.ErrorSummary = errorList;
                            //        model.ErrorType = RegistrationErrorTypes.FormNo;
                            //    }
                            //}
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[2] != null && dsList.Tables[2].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[2].ToModel(model.Id, newYagna.Account.Id);
                                    if (errorList.Count > 0)
                                    {
                                        model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateTransactionDetail;
                                        model.ErrorSummary = errorList;
                                        model.ErrorType = RegistrationErrorTypes.TransactionNumber;
                                    }
                                }
                            }
                            if (model.ErrorSummary == null)
                            {
                                if (dsList.Tables[1] != null && dsList.Tables[1].Rows.Count > 0)
                                {
                                    errorList = dsList.Tables[1].ToModel(model.Id, newYagna.Account.Id);
                                    model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateAccountNo;
                                    model.ErrorSummary = errorList;
                                    model.ErrorType = RegistrationErrorTypes.Account;
                                }
                            }
                        }
                    }
                    if (errorList.Count == 0)
                    {
                        yagnaService.SaveOrUpdateTransactionDetail(newYagna);

                        var successModel = new RegistrationSummaryModel();
                        successModel = newYagna.ToModel(Convert.ToInt32(model.PRN));
                        if (model.Id == 0)
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullRegistrationPopUpHeader;
                        }
                        else
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullUpdatePopUpHeader;
                        }
                        return RedirectToRoute("RegistrationSummary", successModel);
                    }

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
                    Logger.Log("Failure in modelstate validation while Save or update yagna seva due to" + errorMsg);
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while save or update yagna seva detail for yagnaId:" + model.Id + ", due to:" + error);
            }

            dsList = yagnaService.GetYagnaFormValue(yajmanDetailModel.CityId, karyakarDetailModel.CityId);
            if (dsList != null)
            {
                BindDropdownlist(dsList, karyakarDetailModel, yajmanDetailModel, model, accountDetailModel, transactionDetailModel, null);
            }

            model.Yajmans = yajmanDetailModel;
            model.Karyakar = karyakarDetailModel;
            model.AccountDetail = accountDetailModel;
            model.TransactionDetail = transactionDetailModel;
            model.SvikrutiPatrakDetail = svikrutiPatrakDetail;
            return View(model);
        }


        public ActionResult DeleteEMI(int id, int aId)
        {
            if (id > 0)
                yagnaService.DeleteTransactionDetailByAccountId(aId);
            return RedirectToRoute("EMI", new { id = id });
        }


        #endregion

        public JsonResult GetDueAmount(int id, int yid, int amount)
        {
            try
            {
                var dueAmount = yagnaService.GetDueAmount(id, yid, amount);
                return Json(new { d = dueAmount, status = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception error)
            {
                Logger.Log("error while get due amount for yajmanid:" + id + ", amount:" + amount + ", due to:" + error);
            }
            return Json(new { d = 0, status = "failure" }, JsonRequestBehavior.AllowGet);

        }

        #region Refferal yagna seva
        public ActionResult ReferralList()
        {
            var model = new YagnaSevaListModel();
            var filterModel = new FilterDataModel();
            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, filterModel.CityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;
            return View(model);
        }

        public ActionResult FilterReferralSeva(YagnaSevaListModel yagnaData)
        {
            YagnaSevaListModel model = new YagnaSevaListModel();
            try
            {
                if (yagnaData.PageNumber <= 0) yagnaData.PageNumber = 1;
                DataSet dsYagnaList = new DataSet();
                if (yagnaData.Filter != null)
                {
                    var filter = yagnaData.Filter;
                    dsYagnaList = yagnaService.GetAllReferralSeva(filter.Name == null ? "" : filter.Name, filter.Mobile == null ? "" : filter.Mobile, filter.CityId, filter.MandalId, filter.SevaGradeId, filter.PRN == null ? "" : filter.PRN, filter.BookNo, filter.ReceiptNo, yagnaData.PageNumber);
                }
                else
                {
                    dsYagnaList = yagnaService.GetAllReferralSeva("", "", 0, 0, 0, "", "", "", yagnaData.PageNumber);

                }
                LoadReferralSevaList(model, dsYagnaList);
                if (dsYagnaList != null && dsYagnaList.Tables.Count > 0)
                {
                    if (dsYagnaList.Tables[1] != null && dsYagnaList.Tables[1].Rows.Count > 0)
                    {
                        //foreach (DataRow yagnaRow in dsYagnaList.Tables[1].Rows)
                        //{
                        //    model.TotalAmount = Convert.ToDecimal(yagnaRow["TotalAmount"].ToString());
                        //}
                    }
                    if (dsYagnaList.Tables[2] != null && dsYagnaList.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow yagnaRow in dsYagnaList.Tables[2].Rows)
                        {
                            int totalPages = ((Convert.ToInt32(yagnaRow["TotalRecord"].ToString()) - 1) / CommonHelper.PageSize);
                            model.TotalPages = 0;
                            if (totalPages > 0)
                            {
                                model.TotalPages = totalPages + 1;
                            }
                            model.PageNumber = yagnaData.PageNumber;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while search yagna seva detail, Due to:" + error);
            }
            return PartialView("_ReferralSevaList", model);
        }

        private void LoadReferralSevaList(YagnaSevaListModel model, DataSet yagnaSevaList)
        {
            if (yagnaSevaList != null && yagnaSevaList.Tables.Count > 0)
            {
                if (yagnaSevaList.Tables[0] != null && yagnaSevaList.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow yagnaRow in yagnaSevaList.Tables[0].Rows)
                    {
                        var sevaModel = new YagnaSevaModel();
                        var yajmanModel = new BasicDetailModel();
                        var userModel = new BasicDetailModel();

                        /// Yagna Seva Detail
                        sevaModel.Id = Convert.ToInt32(yagnaRow["Id"].ToString());
                        sevaModel.SankalpAmount = yagnaRow["SankalpAmount"].ToString();
                        sevaModel.SevaGradeTitle = yagnaRow["Grade"].ToString();

                        sevaModel.Remarks = yagnaRow["Remarks"].ToString();
                        sevaModel.PRN = yagnaRow["PRN"].ToString();

                        ///Yajman User Detail
                        yajmanModel.FirstName = yagnaRow["YajmanFirstName"].ToString();
                        yajmanModel.MiddleName = yagnaRow["YajmanMiddleName"].ToString();
                        yajmanModel.LastName = yagnaRow["YajmanLastName"].ToString();
                        yajmanModel.Mobile = yagnaRow["YajmanMobile"].ToString();
                        yajmanModel.CityName = yagnaRow["YajmanCity"].ToString();
                        yajmanModel.MandalName = yagnaRow["YajmanMandal"].ToString();

                        /// Seva Bring By user Detail
                        userModel.FirstName = yagnaRow["FirstName"].ToString();
                        userModel.MiddleName = yagnaRow["MiddleName"].ToString();
                        userModel.LastName = yagnaRow["LastName"].ToString();
                        userModel.Mobile = yagnaRow["Mobile"].ToString();
                        userModel.CityName = yagnaRow["CityName"].ToString();
                        userModel.MandalName = yagnaRow["MandalName"].ToString();


                        sevaModel.Yajmans = yajmanModel;
                        sevaModel.Karyakar = userModel;
                        model.YagnaSevaList.Add(sevaModel);
                    }
                }

            }
        }

        public ActionResult ManageReferral(int id = 0)
        {
            var model = new ReferralYagnaSevaModel();
            try
            {
                var referralDetailModel = new RefferalDetailModel();
                var yajmanDetailmodel = new BasicDetailModel();
                model.AvailableForYagna = true;

                DataSet ds = new DataSet();
                if (id > 0)
                {
                    var sevaDetail = yagnaService.GetRefferalSevaDetailById(id);
                    if (sevaDetail != null && sevaDetail.Tables.Count > 0)
                    {
                        model = sevaDetail.Tables[0].ReferralEntityModel();
                        yajmanDetailmodel = sevaDetail.Tables[1].ToModel();
                        referralDetailModel = sevaDetail.Tables[2].ReferralModel();
                    }

                }
                else
                {
                    model.Id = id;
                }
                DataSet dsList = yagnaService.GetYagnaFormValue(yajmanDetailmodel.CityId, 0);
                if (dsList != null)
                {
                    BindDropdownlist(dsList, null, yajmanDetailmodel, null, null, null, model);
                }

                if (model.SevaGrades.Count > 0)
                {
                    var lastSevaGraded = model.SevaGrades.OrderByDescending(t => t.Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(lastSevaGraded.Value))
                    {
                        model.SevaGradeId = Convert.ToInt32(lastSevaGraded.Value);
                    }
                }


                model.Yajmans = yajmanDetailmodel;
                model.RefferalDetail = referralDetailModel;

            }
            catch (Exception error)
            {
                Logger.Log("error while manage yagna seva for id:" + id + ", due to:" + error);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageReferral(ReferralYagnaSevaModel model)
        {
            var referralDetailModel = model.RefferalDetail;
            var yajmanDetailModel = model.Yajmans;
            List<ErrorSummaryModel> errorList = new List<ErrorSummaryModel>();
            DataSet dsList = new DataSet();

            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = contextService.CurrentUser;

                    YagnaDetail newYagna = new YagnaDetail();
                    model.CreatedBy = model.UpdatedBy = currentUser.Id;
                    newYagna = model.YagnaModelToEntity();

                    newYagna.ReferralDetail = model.RefferalDetail.ToEntity();
                    newYagna.YajmanDetail = model.Yajmans.ToEntity();
                    if (!model.IsContinue)
                    {
                        dsList = yagnaService.GetYajmanDetailByMobileNumber(newYagna.YajmanDetail.Mobile);
                        if (dsList.Tables.Count > 0)
                        {
                            if (dsList.Tables[0] != null && dsList.Tables[0].Rows.Count > 0)
                            {
                                errorList = dsList.Tables[0].DuplicateYajmanToModel(model.Id);
                                if (errorList.Count > 0)
                                {
                                    model.ErrorMessage = CommonHelper.ErrorMsgForDuplicateYajmanDetail;
                                    model.ErrorSummary = errorList;
                                }
                            }
                        }
                    }
                    if (errorList.Count == 0)
                    {
                        int prn = yagnaService.SaveOrUpdateReferralDetail(newYagna);

                        var successModel = new RegistrationSummaryModel();
                        successModel = newYagna.ToModel(prn);
                        if (model.Id == 0)
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullRegistrationPopUpHeader;
                        }
                        else
                        {
                            successModel.PopupHeader = CommonHelper.SuccessFullUpdatePopUpHeader;
                        }

                        return RedirectToRoute("RegistrationSummary", successModel);

                    }
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
                    Logger.Log("Failure in modelstate validation while Save or update referral yagna seva due to" + errorMsg);
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while save or update referral yagna seva detail for yagnaId:" + model.Id + ", due to:" + error);
            }

            dsList = yagnaService.GetYagnaFormValue(yajmanDetailModel.CityId, 0);
            if (dsList != null)
            {
                BindDropdownlist(dsList, null, yajmanDetailModel, null, null, null, model);
            }

            model.Yajmans = yajmanDetailModel;
            model.RefferalDetail = referralDetailModel;
            return View(model);
        }

        public ActionResult DeleteReferralSeva(int id = 0)
        {
            try
            {
                var currentUser = contextService.CurrentUser;
                YagnaSevaDetail sevaDetail = new YagnaSevaDetail();
                sevaDetail.Id = id;
                sevaDetail.UpdatedBy = currentUser.Id;
                yagnaService.DeleteReferralYagnaSeva(sevaDetail);
            }
            catch (Exception error)
            {
                Logger.Log("error while delete referral yagna seva detail due to:" + error);
            }


            return RedirectToRoute("FilterReferralSeva");



        }
        #endregion
    }
}