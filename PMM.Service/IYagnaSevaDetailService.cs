using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public interface IYagnaSevaDetailService
    {
        DataSet GetAll(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo, int currentPage);
        int SaveOrUpdateYagnaDetail(YagnaDetail yagnaDetail);
        DataSet GetSevaDetailById(int id);
        List<decimal> GetAmountList();
        void DeleteYagnaSevaDetail(YagnaSevaDetail yagnaSeva);
        DataSet GetYagnaFormValue(int yajmanCityId, int karyakarCityId);
        bool CheckGeneratedPRN(string prn);

        DataSet GetTransactionDetailById(int prn, int accountId);
        void SaveOrUpdateTransactionDetail(YagnaDetail yagna);
        void DeleteTransactionDetailByAccountId(int accountId);
        DataSet CheckRegistrationDetail(YagnaDetail yagnaSeva);
        int GetDueAmount(int id, int yId, int paidAmount);

        DataSet GetRefferalSevaDetailById(int id);
        int SaveOrUpdateReferralDetail(YagnaDetail yagna);
        DataSet GetAllReferralSeva(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo, int currentPage);
        void DeleteReferralYagnaSeva(YagnaSevaDetail yagnaSeva);
        DataSet GetYajmanDetailByMobileNumber(string mobileno);

        List<UserDetail> GetReferralDetailByMobile(string mobile);
    }
}
