using PMM.Core;
using PMM.Core.Data;
using PMM.Web.Models;
using PMM.Web.Models.Mandal;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PMM.Web.Extension
{
    public static class MappingExtension
    {
        public static CityModel ToModel(this City entity)
        {
            if (entity == null)
                return null;

            var model = new CityModel();
            try
            {
                model.Id = entity.Id;
                model.Title = entity.Title;

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model CityModel from entity City due to:" + error);
            }
            return model;
        }

        public static City ToEntity(this CityModel model)
        {
            if (model == null)
                return null;

            var entity = new City();
            try
            {
                entity.Id = model.Id;
                entity.Title = model.Title.ToUpper();
                entity.CreatedBy = model.CreatedBy;
                entity.UpdatedBy = model.UpdatedBy;
                entity.CreatedDate = model.CreatedDate;
                entity.UpdatedDate = model.UpdatedDate;
                entity.IsDeleted = false;

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity City  from model CityModel due to:" + error);
            }
            return entity;
        }


        public static MandalDetailModel ToModel(this Mandal entity)
        {
            if (entity == null)
                return null;

            var model = new MandalDetailModel();
            try
            {
                model.Id = entity.Id;
                model.MandalName = entity.Title;
                model.CityId = entity.CityId;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model MandalDetailModel from entity Mandal due to:" + error);
            }
            return model;
        }

        public static Mandal ToEntity(this MandalDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new Mandal();
            try
            {
                entity.Id = model.Id;
                entity.Title = model.MandalName.ToUpper();
                entity.CityId = model.CityId;
                entity.CreatedBy = model.CreatedBy;
                entity.UpdatedBy = model.UpdatedBy;
                entity.CreatedDate = model.CreatedDate;
                entity.UpdatedDate = model.UpdatedDate;
                entity.IsDeleted = false;

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity Mandal from model MandalDetailModel due to:" + error);
            }
            return entity;
        }


        public static BasicDetailModel ToModel(this DataTable dt)
        {
            if (dt == null)
                return null;

            var model = new BasicDetailModel();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow sevaData in dt.Rows)
                    {
                        model.Id = Convert.ToInt32(sevaData["Id"].ToString());
                        model.FirstName = sevaData["FirstName"].ToString();
                        model.MiddleName = sevaData["MiddleName"].ToString();
                        model.LastName = sevaData["LastName"].ToString();
                        model.Mobile = sevaData["Mobile"].ToString();
                        model.CityId = Convert.ToInt32(sevaData["CityId"].ToString());
                        model.MandalId = Convert.ToInt32(sevaData["MandalId"].ToString());
                        model.Address = sevaData["Address"].ToString();
                        model.PinCode = sevaData["PinCode"].ToString();
                        model.CityName = sevaData["CityName"].ToString();
                        model.MandalName = sevaData["MandalName"].ToString();
                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model SevaDetailModel from entity SevaDetail due to:" + error);
            }
            return model;
        }

        public static YagnaSevaModel YagnaEntityToModel(this DataTable dt)
        {
            if (dt == null)
                return null;

            var model = new YagnaSevaModel();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow sevaData in dt.Rows)
                    {
                        model.Id = Convert.ToInt32(sevaData["Id"].ToString());
                        model.Remarks = sevaData["Remarks"].ToString();
                        model.SankalpAmount = sevaData["SankalpAmount"].ToString();
                        model.YajmanId = Convert.ToInt32(sevaData["YajmanId"].ToString());
                        model.KaryakarId = Convert.ToInt32(sevaData["KaryakarId"].ToString());
                        //model.YagnaDayId = Convert.ToInt32(sevaData["YagnaDayId"].ToString());
                        //model.KundId = Convert.ToInt32(sevaData["KundId"].ToString());
                        model.SevaGradeId = Convert.ToInt32(sevaData["SevaGradeId"].ToString());
                        model.PRN = sevaData["PRN"].ToString();
                        model.AvailableForYagna = Convert.ToBoolean(sevaData["AvailableForYagna"].ToString());
                        model.SeatingReqYajmanId = sevaData["RefYajmanId"].ToString();
                        model.IsChairRequired = Convert.ToBoolean(sevaData["IsChairRequired"].ToString());
                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model YagnaSevaModel from datatable due to:" + error);
            }
            return model;
        }


        public static UserDetail ToEntity(this BasicDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new UserDetail();
            try
            {
                entity.Id = model.Id;
                entity.FirstName = model.FirstName.ToUpper();
                entity.MiddleName = model.MiddleName == null ? "" : model.MiddleName.ToUpper();
                entity.LastName = model.LastName.ToUpper();
                entity.Mobile = model.Mobile;
                entity.CityId = model.CityId;
                entity.MandalId = model.MandalId;
                entity.Address = model.Address.ToUpper();
                entity.PinCode = model.PinCode == null ? "" : model.PinCode;
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity UserDetail from model BasicDetailModel due to:" + error);
            }
            return entity;
        }

        public static YagnaDetail YagnaModelToEntity(this YagnaSevaModel model)
        {
            if (model == null)
                return null;

            var entity = new YagnaDetail();
            try
            {
                entity.Id = model.Id;
                entity.Amount = Convert.ToDecimal(model.SankalpAmount);
                entity.Remarks = (model.Remarks == null ? "" : model.Remarks.ToUpper());
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
                entity.CreatedDate = entity.UpdatedDate = model.UpdatedDate;
                entity.PRN = model.PRN;
                //entity.YagnaDayId = model.YagnaDayId;
                entity.SevaGradeId = model.SevaGradeId;
                entity.AvailableForYagna = model.AvailableForYagna;
                entity.SeatingReqYajmanId = Convert.ToInt32(model.SeatingReqYajmanId);
                entity.IsChairRequired = model.IsChairRequired;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity YagnaDetail from model YagnaSevaModel due to:" + error);
            }
            return entity;
        }

        #region ACCOUNT DETAIL
        public static AccountDetail ToEntity(this AccountDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new AccountDetail();
            try
            {
                entity.Id = model.Id;
                entity.BookNo = Convert.ToInt32(model.BookNo);
                entity.ReceiptNo = Convert.ToInt32(model.ReceiptNo);
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
                entity.Amount = Convert.ToDecimal(model.PaidAmount);
                if (!string.IsNullOrWhiteSpace(model.DateOfReceipt))
                {
                    entity.DateOfReceipt = DateTime.ParseExact(model.DateOfReceipt.Replace("-", "/"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity AccountDetail from model AccountDetailModel due to:" + error);
            }
            return entity;
        }

        public static AccountDetailModel AccountDetailToModel(this DataTable dtRecord)
        {
            if (dtRecord == null)
                return null;

            var model = new AccountDetailModel();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    foreach (DataRow account in dtRecord.Rows)
                    {
                        model.Id = Convert.ToInt32(account["Id"].ToString());
                        model.BookNo = account["BookNo"].ToString();
                        model.ReceiptNo = account["ReceiptNo"].ToString();
                        model.PaidAmount = account["PaidAmount"].ToString();
                        model.DateOfReceipt = Convert.ToDateTime(account["DateOfReceipt"].ToString()).ToString("MM/dd/yyyy").Replace("-", "/");
                        model.DueAmount = account["DueAmount"].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity YagnaDetail from model YagnaSevaModel due to:" + error);
            }
            return model;
        }
        #endregion

        #region TRANSACTION DETAIL
        public static TransactionDetail ToEntity(this TransactionDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new TransactionDetail();
            try
            {
                entity.Id = model.Id;
                entity.TransactionTypeId = model.TransactionTypeId;
                entity.TransactionNumber = model.TransactionNumber;
                if (!string.IsNullOrWhiteSpace(model.DateOfIssue))
                {
                    entity.DateOfIssue = DateTime.ParseExact(model.DateOfIssue.Replace("-", "/"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    entity.DateOfIssue = CommonHelper.DefaultDateTime;
                }
                entity.BankName = model.BankName == null ? "" : model.BankName.ToUpper();
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity TransactionDetail from model TransactionDetailModel due to:" + error);
            }
            return entity;
        }

        public static TransactionDetailModel TransactionToModel(this DataTable dtRecord)
        {
            if (dtRecord == null)
                return null;

            var model = new TransactionDetailModel();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    foreach (DataRow account in dtRecord.Rows)
                    {
                        model.Id = Convert.ToInt32(account["Id"].ToString());
                        model.TransactionTypeId = Convert.ToInt32(account["TransactionTypeId"].ToString());
                        model.TransactionNumber = account["TransactionNumber"].ToString();
                        model.DateOfIssue = Convert.ToDateTime(account["DateOfIssue"]).ToString("MM/dd/yyyy");
                        model.BankName = account["BankName"].ToString();
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity TransactionDetail from model TransactionDetailModel due to:" + error);
            }
            return model;
        }

        public static List<TransactionDetailModel> TransactionDetailToModel(this DataTable dtRecord)
        {
            if (dtRecord == null)
                return null;

            var List = new List<TransactionDetailModel>();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    foreach (DataRow account in dtRecord.Rows)
                    {
                        var model = new TransactionDetailModel();
                        model.Id = Convert.ToInt32(account["Id"].ToString());
                        model.TransactionTypeId = Convert.ToInt32(account["TransactionTypeId"].ToString());
                        model.TransactionNumber = account["TransactionNumber"].ToString();
                        model.DateOfIssue = Convert.ToDateTime(account["DateOfIssue"]).ToString("MM/dd/yyyy");
                        model.BankName = account["BankName"].ToString();
                        model.BookNumber = account["BookNo"].ToString();
                        model.ReceiptNumber = account["ReceiptNo"].ToString();
                        model.PRN = account["PRN"].ToString();
                        model.Amount = account["Amount"].ToString();
                        model.AccountId = Convert.ToInt32(account["AccountId"].ToString());
                        List.Add(model);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity TransactionDetail from model TransactionDetailModel due to:" + error);
            }
            return List;
        }
        #endregion

        #region SvikrutiPatra Detail
        public static SvikrutiPatrakDetail ToEntity(this SvikrutiPatrakDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new SvikrutiPatrakDetail();
            try
            {
                entity.Id = model.Id;
                entity.FormNo = model.FormNo;
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
                entity.IsDeleted = false;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity SvikrutiPatrakDetail from model SvikrutiPatrakDetailModel due to:" + error);
            }
            return entity;
        }

        #endregion

        public static List<ErrorSummaryModel> ToModel(this DataTable dt, int Id)
        {
            if (dt == null)
                return null;

            var model = new List<ErrorSummaryModel>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        var errorSummary = new ErrorSummaryModel();
                        errorSummary.YagnaSevaId = Convert.ToInt32(dRow["YagnaSevaId"].ToString());
                        errorSummary.AccountId = Convert.ToInt32(dRow["AccountId"].ToString());

                        if (Id != errorSummary.YagnaSevaId)
                        {

                            errorSummary.YajmanFirstName = dRow["YajmanFirstName"].ToString();
                            errorSummary.YajmanMiddleName = dRow["YajmanMiddleName"].ToString();
                            errorSummary.YajmanLastName = dRow["YajmanLastName"].ToString();
                            errorSummary.YajmanMobileNo = dRow["YajmanMobile"].ToString();
                            errorSummary.PRN = Convert.ToInt32(dRow["PRN"].ToString());
                            errorSummary.KaryakarFirstName = dRow["KaryakarFirstName"].ToString();
                            errorSummary.KaryakarMiddleName = dRow["KaryakarMiddleName"].ToString();
                            errorSummary.KaryakarLastName = dRow["KaryakarLastName"].ToString();
                            errorSummary.MobileNumber = dRow["KaryakarMobile"].ToString();
                            errorSummary.BookNo = dRow["BookNo"].ToString();
                            errorSummary.ReceiptNo = dRow["ReceiptNo"].ToString();
                            errorSummary.FormNo = dRow["FormNo"].ToString();
                            errorSummary.SankalpAmount = dRow["SankalpAmount"].ToString();
                            errorSummary.TransactionNumber = dRow["TransactionNumber"].ToString();
                            errorSummary.BankName = dRow["BankName"].ToString();
                            errorSummary.PaidAmount = dRow["PaidAmount"].ToString();
                            model.Add(errorSummary);
                        }

                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model List<ErrorSummaryModel> from dataTable due to:" + error);
            }
            return model;
        }

        public static List<ErrorSummaryModel> ToModel(this DataTable dt, int Id, int accountId)
        {
            if (dt == null)
                return null;

            var model = new List<ErrorSummaryModel>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        var errorSummary = new ErrorSummaryModel();
                        errorSummary.YagnaSevaId = Convert.ToInt32(dRow["YagnaSevaId"].ToString());
                        errorSummary.AccountId = Convert.ToInt32(dRow["AccountId"].ToString());

                        if (Id != errorSummary.YagnaSevaId)
                        {
                            errorSummary.YajmanFirstName = dRow["YajmanFirstName"].ToString();
                            errorSummary.YajmanMiddleName = dRow["YajmanMiddleName"].ToString();
                            errorSummary.YajmanLastName = dRow["YajmanLastName"].ToString();
                            errorSummary.YajmanMobileNo = dRow["YajmanMobile"].ToString();
                            errorSummary.PRN = Convert.ToInt32(dRow["PRN"].ToString());
                            errorSummary.KaryakarFirstName = dRow["KaryakarFirstName"].ToString();
                            errorSummary.KaryakarMiddleName = dRow["KaryakarMiddleName"].ToString();
                            errorSummary.KaryakarLastName = dRow["KaryakarLastName"].ToString();
                            errorSummary.MobileNumber = dRow["KaryakarMobile"].ToString();
                            errorSummary.BookNo = dRow["BookNo"].ToString();
                            errorSummary.ReceiptNo = dRow["ReceiptNo"].ToString();
                            errorSummary.FormNo = dRow["FormNo"].ToString();
                            errorSummary.SankalpAmount = dRow["SankalpAmount"].ToString();
                            errorSummary.TransactionNumber = dRow["TransactionNumber"].ToString();
                            errorSummary.BankName = dRow["BankName"].ToString();
                            errorSummary.PaidAmount = dRow["PaidAmount"].ToString();
                            model.Add(errorSummary);
                        }

                        if (Id == errorSummary.YagnaSevaId && accountId != errorSummary.AccountId)
                        {
                            errorSummary.YajmanFirstName = dRow["YajmanFirstName"].ToString();
                            errorSummary.YajmanMiddleName = dRow["YajmanMiddleName"].ToString();
                            errorSummary.YajmanLastName = dRow["YajmanLastName"].ToString();
                            errorSummary.YajmanMobileNo = dRow["YajmanMobile"].ToString();
                            errorSummary.PRN = Convert.ToInt32(dRow["PRN"].ToString());
                            errorSummary.KaryakarFirstName = dRow["KaryakarFirstName"].ToString();
                            errorSummary.KaryakarMiddleName = dRow["KaryakarMiddleName"].ToString();
                            errorSummary.KaryakarLastName = dRow["KaryakarLastName"].ToString();
                            errorSummary.MobileNumber = dRow["KaryakarMobile"].ToString();
                            errorSummary.BookNo = dRow["BookNo"].ToString();
                            errorSummary.ReceiptNo = dRow["ReceiptNo"].ToString();
                            errorSummary.FormNo = dRow["FormNo"].ToString();
                            errorSummary.SankalpAmount = dRow["SankalpAmount"].ToString();
                            errorSummary.TransactionNumber = dRow["TransactionNumber"].ToString();
                            errorSummary.BankName = dRow["BankName"].ToString();
                            errorSummary.PaidAmount = dRow["PaidAmount"].ToString();
                            model.Add(errorSummary);
                        }


                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model List<ErrorSummaryModel> from dataTable due to:" + error);
            }
            return model;
        }

        public static RegistrationSummaryModel ToModel(this YagnaDetail yagnaDetail, int prn)
        {
            if (yagnaDetail == null)
                return null;

            var model = new RegistrationSummaryModel();
            try
            {
                model.FirstName = yagnaDetail.YajmanDetail.FirstName;
                model.MiddleName = yagnaDetail.YajmanDetail.MiddleName;
                model.LastName = yagnaDetail.YajmanDetail.LastName;
                model.MobileNumber = yagnaDetail.YajmanDetail.Mobile;
                model.SankalpAmount = yagnaDetail.Amount.ToString();
                model.PRN = prn;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model RegistrationSummaryModel from YagnaDetail due to:" + error);
            }
            return model;
        }

        #region referral detail
        public static ReferralYagnaSevaModel ReferralEntityModel(this DataTable dt)
        {
            if (dt == null)
                return null;

            var model = new ReferralYagnaSevaModel();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow sevaData in dt.Rows)
                    {
                        model.Id = Convert.ToInt32(sevaData["Id"].ToString());
                        model.Remarks = sevaData["Remarks"].ToString();
                        model.SankalpAmount = sevaData["SankalpAmount"].ToString();
                        model.YajmanId = Convert.ToInt32(sevaData["YajmanId"].ToString());
                        model.KaryakarId = Convert.ToInt32(sevaData["KaryakarId"].ToString());
                        //model.YagnaDayId = Convert.ToInt32(sevaData["YagnaDayId"].ToString());
                        //model.KundId = Convert.ToInt32(sevaData["KundId"].ToString());
                        model.SevaGradeId = Convert.ToInt32(sevaData["SevaGradeId"].ToString());
                        model.PRN = sevaData["PRN"].ToString();
                        model.AvailableForYagna = Convert.ToBoolean(sevaData["AvailableForYagna"].ToString());
                        model.SeatingReqYajmanId = sevaData["RefYajmanId"].ToString();
                        model.IsChairRequired = Convert.ToBoolean(sevaData["IsChairRequired"].ToString());
                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model ReferralYagnaSevaModel from datatable due to:" + error);
            }
            return model;
        }

        public static RefferalDetailModel ReferralModel(this DataTable dt)
        {
            if (dt == null)
                return null;

            var model = new RefferalDetailModel();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow sevaData in dt.Rows)
                    {
                        model.Id = Convert.ToInt32(sevaData["Id"].ToString());
                        model.Name = sevaData["FirstName"].ToString();
                        model.Mobile = sevaData["Mobile"].ToString();
                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model RefferalDetailModel from dataTable due to:" + error);
            }
            return model;
        }

        public static YagnaDetail YagnaModelToEntity(this ReferralYagnaSevaModel model)
        {
            if (model == null)
                return null;

            var entity = new YagnaDetail();
            try
            {
                entity.Id = model.Id;
                entity.Amount = Convert.ToDecimal(model.SankalpAmount);
                entity.Remarks = (model.Remarks == null ? "" : model.Remarks.ToUpper());
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
                entity.CreatedDate = entity.UpdatedDate = model.UpdatedDate;
                entity.PRN = model.PRN;
                //entity.YagnaDayId = model.YagnaDayId;
                entity.SevaGradeId = model.SevaGradeId;
                entity.AvailableForYagna = model.AvailableForYagna;
                entity.SeatingReqYajmanId = Convert.ToInt32(model.SeatingReqYajmanId);
                entity.IsChairRequired = model.IsChairRequired;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity YagnaDetail from model ReferralYagnaSevaModel due to:" + error);
            }
            return entity;
        }

        public static UserDetail ToEntity(this RefferalDetailModel model)
        {
            if (model == null)
                return null;

            var entity = new UserDetail();
            try
            {
                entity.Id = model.Id;
                entity.FirstName = model.Name.Trim().ToUpper();
                entity.Mobile = model.Mobile;
                entity.CreatedBy = entity.UpdatedBy = model.UpdatedBy;
            }
            catch (Exception error)
            {
                Logger.Log("Error while binding entity UserDetail from model RefferalDetailModel due to:" + error);
            }
            return entity;
        }

        public static List<ErrorSummaryModel> DuplicateYajmanToModel(this DataTable dt, int Id)
        {
            if (dt == null)
                return null;

            var model = new List<ErrorSummaryModel>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dRow in dt.Rows)
                    {
                        var errorSummary = new ErrorSummaryModel();
                        errorSummary.YagnaSevaId = Convert.ToInt32(dRow["YagnaSevaId"].ToString());

                        if (Id != errorSummary.YagnaSevaId)
                        {

                            errorSummary.YajmanFirstName = dRow["YajmanFirstName"].ToString();
                            errorSummary.YajmanMiddleName = dRow["YajmanMiddleName"].ToString();
                            errorSummary.YajmanLastName = dRow["YajmanLastName"].ToString();
                            errorSummary.YajmanMobileNo = dRow["YajmanMobile"].ToString();

                            errorSummary.KaryakarFirstName = dRow["KaryakarFirstName"].ToString();
                            errorSummary.KaryakarMiddleName = dRow["KaryakarMiddleName"].ToString();
                            errorSummary.KaryakarLastName = dRow["KaryakarLastName"].ToString();
                            errorSummary.MobileNumber = dRow["KaryakarMobile"].ToString();
                            errorSummary.PRN = Convert.ToInt32(dRow["PRN"].ToString());
                            errorSummary.AvailableForYagna = dRow["AvailableForYagna"].ToString();
                            errorSummary.SankalpAmount = dRow["SankalpAmount"].ToString();
                            errorSummary.PaidAmount = dRow["PaidAmount"].ToString();
                            model.Add(errorSummary);
                        }
                    }
                }

            }
            catch (Exception error)
            {
                Logger.Log("Error while binding model List<ErrorSummaryModel> from dataTable due to:" + error);
            }
            return model;
        }
        #endregion
    }
}