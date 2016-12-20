using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
   public interface IReportService
    {
        DataSet GetYajmansList(string name, string mobile, int cityId, int mandalId, int grade, string prn, string bookNo, string receiptNo);
        DataSet GetReferralSevaList(string name, string mobile, int cityId, int mandalId, int grade, string prn);
        DataSet GetCityWiseSummaryReport(string selectedCityList);
        DataSet GetMandalWiseSummaryReport(string selectedMandalList);
        DataSet GetYajmanSummaryReport(string selectedMandalList);
    }
}
