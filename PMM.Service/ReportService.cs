using PMM.Core.Data;
using PMM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMM.Core;

namespace PMM.Service
{
    public class ReportService:IReportService
    {
        private readonly IRepository<Report> reportService;

        public ReportService(IRepository<Report> _reportService)
        {
            this.reportService = _reportService;
        }
        public DataSet GetYajmansList(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo)
        {
            SqlParameter paramName = new SqlParameter("@name", name);
            SqlParameter paramMobile = new SqlParameter("@mobile", mobile);
            SqlParameter paramCity = new SqlParameter("@cityId", cityId);
            SqlParameter paramMandal = new SqlParameter("@mandalId", mandalId);
            SqlParameter paramAmount = new SqlParameter("@sevaGrade", grade);
            SqlParameter paramPRN = new SqlParameter("@prn", prn);
            SqlParameter paramBookNo = new SqlParameter("@bookNo", bookNo);
            SqlParameter paramReceiptNo = new SqlParameter("receiptNo", receiptNo);
            return reportService.ExecuteStoreProcedure(CommonHelper.SP_RPT_YajmanList, paramName, paramMobile, paramCity, paramMandal, paramAmount, paramPRN, paramBookNo, paramReceiptNo);
        }

        public DataSet GetReferralSevaList(string name, string mobile, int cityId, int mandalId, int grade, string prn)
        {
            SqlParameter paramName = new SqlParameter("@name", name);
            SqlParameter paramMobile = new SqlParameter("@mobile", mobile);
            SqlParameter paramCity = new SqlParameter("@cityId", cityId);
            SqlParameter paramMandal = new SqlParameter("@mandalId", mandalId);
            SqlParameter paramAmount = new SqlParameter("@sevaGrade", grade);
            SqlParameter paramPRN = new SqlParameter("@prn", prn);
            return reportService.ExecuteStoreProcedure(CommonHelper.SP_RPT_ReferralSevaList, paramName, paramMobile, paramCity, paramMandal, paramAmount, paramPRN);
        }

        public DataSet GetCityWiseSummaryReport(string selectedCityList)
        {
            SqlParameter paramCity = new SqlParameter("@cityId", selectedCityList);
            return reportService.ExecuteStoreProcedure(CommonHelper.SP_RPT_CityWiseSummary, paramCity);
        }

        public DataSet GetMandalWiseSummaryReport(string selectedMandalList)
        {
            SqlParameter paramMandal = new SqlParameter("@mandalId", selectedMandalList);
            return reportService.ExecuteStoreProcedure(CommonHelper.SP_RPT_MandalWiseSummary, paramMandal);
        }

        public DataSet GetYajmanSummaryReport(string selectedMandalList)
        {
            SqlParameter paramMandal = new SqlParameter("@mandalId", selectedMandalList);
            return reportService.ExecuteStoreProcedure(CommonHelper.SP_RPT_YajmanSummary, paramMandal);
        }
    }
}
