using PMM.Core;
using PMM.Core.Data;
using PMM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace PMM.Service
{
    public class YagnaSevaDetailService : IYagnaSevaDetailService
    {
        private readonly IRepository<YagnaSevaDetail> yagnaRepository;

        public YagnaSevaDetailService(IRepository<YagnaSevaDetail> _yagnaRepository)
        {
            this.yagnaRepository = _yagnaRepository;
        }

        public DataSet GetAll(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo, int currentPage)
        {
            SqlParameter paramName = new SqlParameter("@name", name);
            SqlParameter paramMobile = new SqlParameter("@mobile", mobile);
            SqlParameter paramCity = new SqlParameter("@cityId", cityId);
            SqlParameter paramMandal = new SqlParameter("@mandalId", mandalId);
            SqlParameter paramAmount = new SqlParameter("@sevaGrade", grade);
            SqlParameter paramPRN = new SqlParameter("@prn", prn);
            //SqlParameter paramBookNo = new SqlParameter("@bookNo", bookNo);
            //SqlParameter paramReceiptNo = new SqlParameter("@receiptNo", receiptNo);
            SqlParameter paramPageIndex = new SqlParameter("@pageIndex", currentPage);
            SqlParameter paramPageSize = new SqlParameter("@pageSize", CommonHelper.PageSize);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_YagnaSevaList, paramName, paramMobile, paramCity, paramMandal, paramAmount, paramPRN,
                paramPageIndex, paramPageSize);
        }
        public DataSet GetSevaDetailById(int id)
        {
            SqlParameter paramId = new SqlParameter("@yagnaId", id);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_YagnaSevaDetailById, paramId);
        }

        public int SaveOrUpdateYagnaDetail(YagnaDetail yagna)
        {
            SqlParameter paramAmount = new SqlParameter("@sankalpAmount", yagna.Amount);
            SqlParameter paramModifiedBy = new SqlParameter("@modifiedBy", yagna.UpdatedBy);
            SqlParameter paramRemarks = new SqlParameter("@remarks", yagna.Remarks);

            var yajmanDetail = yagna.YajmanDetail;
            SqlParameter paramId = new SqlParameter("@yagnaId", yagna.Id);
            SqlParameter paramFirstName = new SqlParameter("@firstName", yajmanDetail.FirstName);
            SqlParameter paramMiddleName = new SqlParameter("@middleName", yajmanDetail.MiddleName);
            SqlParameter paramLastName = new SqlParameter("@lastName", yajmanDetail.LastName);
            SqlParameter paramMobile = new SqlParameter("@mobile", yajmanDetail.Mobile);
            SqlParameter paramCityId = new SqlParameter("@cityId", yajmanDetail.CityId);
            SqlParameter paramMandalId = new SqlParameter("@mandalId", yajmanDetail.MandalId);
            SqlParameter paramAddress = new SqlParameter("@address", yajmanDetail.Address);
            SqlParameter paramPincdoe = new SqlParameter("@pincode", yajmanDetail.PinCode);

            SqlParameter paramSevaGradeId = new SqlParameter("@sevaGradeId", yagna.SevaGradeId);

            var sevaBringByKaryakar = yagna.KaryakarDetail;
            SqlParameter paramKaryakarFirstName = new SqlParameter("@user_firstName", sevaBringByKaryakar.FirstName);
            SqlParameter paramKaryakarMiddleName = new SqlParameter("@user_middleName", sevaBringByKaryakar.MiddleName);
            SqlParameter paramKaryakarLastName = new SqlParameter("@user_lastName", sevaBringByKaryakar.LastName);
            SqlParameter paramKaryakarMobile = new SqlParameter("@user_mobile", sevaBringByKaryakar.Mobile);
            SqlParameter paramKaryakarCityId = new SqlParameter("@user_cityId", sevaBringByKaryakar.CityId);
            SqlParameter paramKaryakarMandalId = new SqlParameter("@user_mandalId", sevaBringByKaryakar.MandalId);
            SqlParameter paramKaryakarId = new SqlParameter("@karykarId", sevaBringByKaryakar.Id);

            var svikrutiPatrakDetail = yagna.SvikrutiPatraDetail;
            SqlParameter paramFormNo = new SqlParameter("@formNo", svikrutiPatrakDetail.FormNo);

            var accountDetail = yagna.Account;
            SqlParameter paramAccountId = new SqlParameter("@accountId", accountDetail.Id);
            SqlParameter paramBookNo = new SqlParameter("@bookNo", accountDetail.BookNo);
            SqlParameter paramReceiptNo = new SqlParameter("@receiptNo", accountDetail.ReceiptNo);
            SqlParameter paramPaidAmount = new SqlParameter("@paidAmount", accountDetail.Amount);
            SqlParameter paramDateOfReceipt = new SqlParameter("@dateofreceipt", accountDetail.DateOfReceipt);

            var transactionDetail = yagna.Transaction;
            SqlParameter paramTransId = new SqlParameter("@transactionId", transactionDetail.Id);
            SqlParameter paramTransTypeId = new SqlParameter("@transactionTypeId", transactionDetail.TransactionTypeId);
            SqlParameter paramTransNo = new SqlParameter("@transactionNo", transactionDetail.TransactionNumber);
            SqlParameter paramDateOfissue = new SqlParameter("@dateOfIssue", transactionDetail.DateOfIssue);
            SqlParameter paramBankName = new SqlParameter("@bankName", transactionDetail.BankName);

            SqlParameter paramAvailForYagna = new SqlParameter("@availableForYagna", yagna.AvailableForYagna);
            SqlParameter paramRequestedYajmanId = new SqlParameter("@seatreqYajman", yagna.SeatingReqYajmanId);

            SqlParameter paramIsChairRequired = new SqlParameter("@isChairReq", yagna.IsChairRequired);
            SqlParameter paramPRNOut = new SqlParameter("@prn", SqlDbType.Int);
            paramPRNOut.Direction = ParameterDirection.Output;

            yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_YagnaSevaSaveOrUpdate, paramId, paramAmount, paramFirstName, paramMiddleName, paramLastName
               , paramMobile, paramCityId, paramMandalId, paramAddress, paramPincdoe, paramKaryakarFirstName, paramKaryakarMiddleName, paramKaryakarLastName,
               paramKaryakarMobile, paramKaryakarCityId, paramKaryakarMandalId, paramFormNo,
              paramSevaGradeId,
               paramAccountId, paramBookNo, paramReceiptNo, paramPaidAmount, paramDateOfReceipt,
              paramTransId, paramTransTypeId, paramTransNo, paramDateOfissue, paramBankName,
               paramKaryakarId,
               paramRemarks, paramModifiedBy, paramAvailForYagna, paramRequestedYajmanId,paramIsChairRequired, paramPRNOut);

            return Convert.ToInt32(paramPRNOut.Value.ToString());
        }

        public List<decimal> GetAmountList()
        {
            return yagnaRepository.Table.Where(t => t.IsDeleted != true).Select(x => x.SankalpAmount).Distinct().ToList();
        }

        public void DeleteYagnaSevaDetail(YagnaSevaDetail yagnaSeva)
        {
            SqlParameter paramId = new SqlParameter("@yagnaId", yagnaSeva.Id);
            SqlParameter paramModifiedBy = new SqlParameter("@modifiedBy", yagnaSeva.UpdatedBy);
            yagnaRepository.ExecuteStoreProcedureList(CommonHelper.SP_YagnaSevaDelete, paramId, paramModifiedBy);
        }

        public DataSet GetYagnaFormValue(int yajmanCityId, int karyakarCityId)
        {
            SqlParameter paramYajmanCity = new SqlParameter("@yajman_cityId", yajmanCityId);
            SqlParameter paramKaryakarCity = new SqlParameter("@karyakar_cityId", karyakarCityId);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_YagnaFormValueGet, paramYajmanCity, paramKaryakarCity);
        }

        public bool CheckGeneratedPRN(string prn)
        {
            var prnDetail = yagnaRepository.Table.Where(t => t.PRN.ToLower().Trim() == prn.ToLower().Trim()).FirstOrDefault();
            if (prnDetail != null) return true;
            return false;
        }

        #region MANAGE EMI

        public DataSet GetTransactionDetailById(int prn, int accountId)
        {
            SqlParameter paramId = new SqlParameter("@prn", prn);
            SqlParameter paramAccountId = new SqlParameter("@accountId", accountId);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_GetTransactionDetailById, paramId, paramAccountId);
        }

        public void SaveOrUpdateTransactionDetail(YagnaDetail yagna)
        {

            SqlParameter paramModifiedBy = new SqlParameter("@modifiedBy", yagna.UpdatedBy);

            var yajmanDetail = yagna.YajmanDetail;
            SqlParameter paramId = new SqlParameter("@yagnaId", yagna.Id);
            SqlParameter paramYajmanId = new SqlParameter("@yajmanId", yajmanDetail.Id);
            SqlParameter paramSankalpAmount = new SqlParameter("@sankalpamount", yagna.Amount);
            var accountDetail = yagna.Account;
            SqlParameter paramAccountId = new SqlParameter("@accountId", accountDetail.Id);
            SqlParameter paramBookNo = new SqlParameter("@bookNo", accountDetail.BookNo);
            SqlParameter paramReceiptNo = new SqlParameter("@receiptNo", accountDetail.ReceiptNo);
            SqlParameter paramPaidAmount = new SqlParameter("@paidAmount", accountDetail.Amount);
            SqlParameter paramDateOfReceipt = new SqlParameter("@dateOfReceipt", accountDetail.DateOfReceipt);

            var transactionDetail = yagna.Transaction;
            SqlParameter paramTransTypeId = new SqlParameter("@transactionTypeId", transactionDetail.TransactionTypeId);
            SqlParameter paramTransNo = new SqlParameter("@transactionNo", transactionDetail.TransactionNumber);
            SqlParameter paramDateOfissue = new SqlParameter("@dateOfIssue", transactionDetail.DateOfIssue);
            SqlParameter paramBankName = new SqlParameter("@bankName", transactionDetail.BankName);


            yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_TransactionDetailSaveOrUpdate, paramId, paramAccountId,
               paramBookNo, paramReceiptNo, paramPaidAmount, paramDateOfReceipt,
               paramTransTypeId, paramTransNo, paramDateOfissue, paramBankName,
              paramModifiedBy, paramYajmanId, paramSankalpAmount);

        }

        public void DeleteTransactionDetailByAccountId(int accountId)
        {
            SqlParameter paramId = new SqlParameter("@accountId", accountId);
            yagnaRepository.ExecuteStoreProcedureList(CommonHelper.SP_DeleteTransactionDetailByAccountId, paramId);
        }

        #endregion

        public DataSet CheckRegistrationDetail(YagnaDetail yagnaSeva)
        {

            var accountDetail = yagnaSeva.Account;
            SqlParameter paramBookNo = new SqlParameter("@accountBookNo", accountDetail.BookNo);
            SqlParameter paramReceiptNo = new SqlParameter("@accountReceiptNo", accountDetail.ReceiptNo);

            var svikrutiPatrakDetail = yagnaSeva.SvikrutiPatraDetail;
            SqlParameter paramFormNo = new SqlParameter("@formNo", svikrutiPatrakDetail.FormNo);

            var transactionDetail = yagnaSeva.Transaction;
            SqlParameter paramTransNo = new SqlParameter("@transactionNo", transactionDetail.TransactionNumber);
            SqlParameter paramBankName = new SqlParameter("@bankName", transactionDetail.BankName);

            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_CheckYajmanRegistrationDetail, paramBookNo, paramReceiptNo, paramFormNo,
                paramTransNo, paramBankName);
        }


        public int GetDueAmount(int id, int yId, int paidAmount)
        {
            SqlParameter paramYajmanId = new SqlParameter("@yajmanId", yId);
            SqlParameter paramPaidAmount = new SqlParameter("@paidAmount", paidAmount);
            SqlParameter paramAccountId = new SqlParameter("@accountId", id);

            SqlParameter paramDueAmount = new SqlParameter("@dueAmount", SqlDbType.NVarChar, 50);
            paramDueAmount.Direction = ParameterDirection.Output;

            yagnaRepository.ExecuteStoreProcedureList("usp_GetDueAmount", paramYajmanId, paramPaidAmount, paramAccountId, paramDueAmount);
            return Convert.ToInt32(paramDueAmount.Value.ToString());
        }

        #region Refferral Detail
        public DataSet GetRefferalSevaDetailById(int id)
        {
            SqlParameter paramId = new SqlParameter("@yagnaId", id);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_ReferralYagnaSevaDetailById, paramId);
        }

        public int SaveOrUpdateReferralDetail(YagnaDetail yagna)
        {
            var yajmanDetail = yagna.YajmanDetail;
            SqlParameter paramId = new SqlParameter("@yagnaId", yagna.Id);
            SqlParameter paramFirstName = new SqlParameter("@firstName", yajmanDetail.FirstName);
            SqlParameter paramMiddleName = new SqlParameter("@middleName", yajmanDetail.MiddleName);
            SqlParameter paramLastName = new SqlParameter("@lastName", yajmanDetail.LastName);
            SqlParameter paramMobile = new SqlParameter("@mobile", yajmanDetail.Mobile);
            SqlParameter paramCityId = new SqlParameter("@cityId", yajmanDetail.CityId);
            SqlParameter paramMandalId = new SqlParameter("@mandalId", yajmanDetail.MandalId);
            SqlParameter paramAddress = new SqlParameter("@address", yajmanDetail.Address);
            SqlParameter paramPincdoe = new SqlParameter("@pincode", yajmanDetail.PinCode);

            SqlParameter paramSevaGradeId = new SqlParameter("@sevaGradeId", yagna.SevaGradeId);

            var referralDetail = yagna.ReferralDetail;
            SqlParameter paramKaryakarFirstName = new SqlParameter("@user_firstName", referralDetail.FirstName);
            SqlParameter paramKaryakarMobile = new SqlParameter("@user_mobile", referralDetail.Mobile);
            SqlParameter paramKaryakarId = new SqlParameter("@karykarId", referralDetail.Id);


            SqlParameter paramAvailForYagna = new SqlParameter("@availableForYagna", yagna.AvailableForYagna);
            SqlParameter paramRequestedYajmanId = new SqlParameter("@seatreqYajman", yagna.SeatingReqYajmanId);

            SqlParameter paramAmount = new SqlParameter("@sankalpAmount", yagna.Amount);
            SqlParameter paramModifiedBy = new SqlParameter("@modifiedBy", yagna.UpdatedBy);
            SqlParameter paramRemarks = new SqlParameter("@remarks", yagna.Remarks);

            SqlParameter paramIsChairRequired = new SqlParameter("@isChairReq", yagna.IsChairRequired);

            SqlParameter paramPRNOut = new SqlParameter("@prn", SqlDbType.Int);
            paramPRNOut.Direction = ParameterDirection.Output;
            
            yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_ReferralYagnaSevaDetailSaveOrUpdate, paramId, paramAmount, paramFirstName, paramMiddleName, paramLastName
               , paramMobile, paramCityId, paramMandalId, paramAddress, paramPincdoe, paramKaryakarFirstName,
               paramKaryakarMobile,
              paramSevaGradeId,
               paramKaryakarId,
               paramRemarks, paramModifiedBy, paramAvailForYagna, paramRequestedYajmanId, paramIsChairRequired, paramPRNOut);

            return Convert.ToInt32(paramPRNOut.Value.ToString());
        }

        public DataSet GetAllReferralSeva(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo, int currentPage)
        {
            SqlParameter paramName = new SqlParameter("@name", name);
            SqlParameter paramMobile = new SqlParameter("@mobile", mobile);
            SqlParameter paramCity = new SqlParameter("@cityId", cityId);
            SqlParameter paramMandal = new SqlParameter("@mandalId", mandalId);
            SqlParameter paramAmount = new SqlParameter("@sevaGrade", grade);
            SqlParameter paramPRN = new SqlParameter("@prn", prn);
            SqlParameter paramPageIndex = new SqlParameter("@pageIndex", currentPage);
            SqlParameter paramPageSize = new SqlParameter("@pageSize", CommonHelper.PageSize);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_ReferralYagnaSevaList, paramName, paramMobile, paramCity, paramMandal, paramAmount, paramPRN,
                paramPageIndex, paramPageSize);
        }

        public void DeleteReferralYagnaSeva(YagnaSevaDetail yagnaSeva)
        {
            SqlParameter paramId = new SqlParameter("@yagnaId", yagnaSeva.Id);
            SqlParameter paramModifiedBy = new SqlParameter("@modifiedBy", yagnaSeva.UpdatedBy);
            yagnaRepository.ExecuteStoreProcedureList(CommonHelper.SP_ReferralYagnaSevaDelete, paramId, paramModifiedBy);
        }

        public DataSet GetYajmanDetailByMobileNumber(string mobileno)
        {
            
            SqlParameter paramMobileNo = new SqlParameter("@mobileno", mobileno);
            return yagnaRepository.ExecuteStoreProcedure(CommonHelper.SP_GetYajmanDetailByMobileNo, paramMobileNo);
        }
        #endregion


        public List<UserDetail> GetReferralDetailByMobile(string mobile)
        {
            List<UserDetail> userList = new List<UserDetail>();
            SqlParameter paramMobile = new SqlParameter("@mobileno", mobile);
            DataTable dtList = new DataTable();
            dtList = yagnaRepository.ExecuteStoreProcedureList(CommonHelper.sP_ReferralDetailByMobile, paramMobile);
            if (dtList.Rows.Count > 0)
            {
                foreach (DataRow drow in dtList.Rows)
                {
                    UserDetail newDetail = new UserDetail();
                    newDetail.FirstName = drow["FirstName"].ToString();
                    newDetail.MiddleName = drow["MiddleName"].ToString();
                    newDetail.LastName = drow["LastName"].ToString();
                    newDetail.Mobile = drow["Mobile"].ToString();
                    newDetail.Id = Convert.ToInt32(drow["Id"].ToString());
                    newDetail.CityId = Convert.ToInt32(drow["CityId"].ToString());
                    newDetail.MandalId = Convert.ToInt32(drow["MandalId"].ToString());
                    userList.Add(newDetail);
                }
            }
            return userList;
        }
    }
}
