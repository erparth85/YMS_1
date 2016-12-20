using Microsoft.Reporting.WebForms;
using PMM.Core;
using PMM.Service;
using PMM.Web.Models.Report;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PMM.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService reportService;
        private readonly ICityService cityService;
        private readonly IMandalService mandalService;
        private readonly ISevaGradeService isevaGradeService;

        public ReportController(IReportService _reportService, ICityService _cityService, IMandalService _mandalService, ISevaGradeService _isevaGradeService)
        {
            reportService = _reportService;
            cityService = _cityService;
            mandalService = _mandalService;
            isevaGradeService = _isevaGradeService;
        }


        #region yajman list
        public ActionResult YajmansList()
        {
            var model = new YagnaSevaListModel();

            var filterModel = new FilterDataModel();
            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, filterModel.CityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;
            try
            {
                DataSet dsData = reportService.GetYajmansList("", "", 0, 0, 0, "", "0", "0");
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], filterModel, "DsYajmansList", CommonHelper.LocalReportLocation + CommonHelper.RPT_YajmanList, null);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        filterModel.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: error while loading yajman list report due to:" + error);
            }

            return View(model);
        }

        public ActionResult FilterYajmanReport(YagnaSevaListModel yagnaData)
        {
            YagnaSevaListModel model = new YagnaSevaListModel();
            ReportViewer rptViewer = new ReportViewer();
            DataSet dsData = new DataSet();
            var filterModel = new FilterDataModel();
            int cityId = 0;

            try
            {
                if (yagnaData.PageNumber <= 0) yagnaData.PageNumber = 1;

                if (yagnaData.Filter != null)
                {
                    var filter = yagnaData.Filter;
                    cityId = filter.CityId;
                    dsData = reportService.GetYajmansList(filter.Name == null ? "" : filter.Name, filter.Mobile == null ? "" : filter.Mobile, cityId, filter.MandalId, filter.SevaGradeId, filter.PRN == null ? "" : filter.PRN, filter.BookNo, filter.ReceiptNo);
                }
                else
                {

                    dsData = reportService.GetYajmansList("", "", 0, 0, 0, "", "0", "0");

                }
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], yagnaData.Filter, "DsYajmansList", CommonHelper.LocalReportLocation + CommonHelper.RPT_YajmanList, null);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        filterModel.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: Error while filter yajman report list, Due to:" + error);
            }

            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, cityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;
            return PartialView("_LoadReport", model);
        }

        private List<SelectListItem> GetColumnsListForReport(DataTable dtData)
        {
            var list = new List<SelectListItem>();
            try
            {
                foreach (DataRow dtRow in dtData.Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = dtRow["ColumnName"].ToString(),
                        Value = dtRow["ColumnName"].ToString(),
                        Selected = true
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting GetColumnsListForYajman from base controller due to:" + error);
            }
            return list;
        }
        #endregion

        #region referral yagna seva list
        public ActionResult ReferralSevaList()
        {
            var model = new YagnaSevaListModel();
            var filterModel = new FilterDataModel();
            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, filterModel.CityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;

            try
            {
                DataSet dsData = reportService.GetReferralSevaList("", "", 0, 0, 0, "");
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], filterModel, "DsReferralYagnaSevaList", CommonHelper.LocalReportLocation + CommonHelper.RPT_RefferalYajmanList, null);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        filterModel.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: error while loading refferal yajman list report due to:" + error);
            }
            return View(model);
        }

        public ActionResult FilterReferralSevaReport(YagnaSevaListModel yagnaData)
        {
            YagnaSevaListModel model = new YagnaSevaListModel();
            ReportViewer rptViewer = new ReportViewer();
            DataSet dsData = new DataSet();
            var filterModel = new FilterDataModel();


            int cityId = 0;
            try
            {
                if (yagnaData.PageNumber <= 0) yagnaData.PageNumber = 1;

                if (yagnaData.Filter != null)
                {
                    var filter = yagnaData.Filter;
                    cityId = filter.CityId;
                    dsData = reportService.GetReferralSevaList(filter.Name == null ? "" : filter.Name, filter.Mobile == null ? "" : filter.Mobile, cityId, filter.MandalId, filter.SevaGradeId, filter.PRN == null ? "" : filter.PRN);
                }
                else
                {

                    dsData = reportService.GetReferralSevaList("", "", 0, 0, 0, "");

                }

                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], yagnaData.Filter, "DsReferralYagnaSevaList", CommonHelper.LocalReportLocation + CommonHelper.RPT_RefferalYajmanList, null);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        filterModel.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }


            }
            catch (Exception error)
            {
                Logger.Log("Report: Error while filter referral seva report list, Due to:" + error);
            }
            filterModel.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            filterModel.MandalList = GetMandalByCityId(mandalService, CommonHelper.PlaceHolderDrdForMandal, cityId, false);
            filterModel.SevaGrades = GetSevaGrades(isevaGradeService, CommonHelper.PlaceHolderDrdForSevaGrade);
            model.Filter = filterModel;
            return PartialView("_LoadReport", model);
        }
        #endregion

        #region city wise summary

        public ActionResult CityWiseSummary()
        {
            var model = new FilterSummaryModel();
            model.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, true);
            try
            {
                DataSet dsData = reportService.GetCityWiseSummaryReport(null);
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsCityWiseSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_CityWiseSummary,null);
                    }

                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: error while loading city wise summary list report due to:" + error);
            }

            return View(model);
        }

        public ActionResult FilterCityWiseSummary(FilterSummaryModel filter)
        {
            FilterSummaryModel model = new FilterSummaryModel();
            ReportViewer rptViewer = new ReportViewer();
            DataSet dsData = new DataSet();

            try
            {
                if (filter != null)
                {
                    dsData = reportService.GetCityWiseSummaryReport(string.Join(",", filter.CityId));
                }
                else
                {

                    dsData = reportService.GetCityWiseSummaryReport(null);

                }
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsCityWiseSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_CityWiseSummary, filter.ReportSelectedColumns);
                    }

                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: Error while filter city wise summary report list, Due to:" + error);
            }

            model.CityList = GetCityList(cityService, CommonHelper.PlaceHolderDrdForCity, false);
            return PartialView("_LoadReport", model);
        }

        #endregion

        #region mandal wise summary

        public ActionResult MandalWiseSummary()
        {
            var model = new FilterSummaryModel();
            model.CityList = GetCityList(cityService, "", false);

            model.MandalList = GetMandalByCityId(mandalService, "", 0, true);
            try
            {
                DataSet dsData = reportService.GetMandalWiseSummaryReport(null);
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsMandalWiseSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_MandalWiseSummary,null);
                    }

                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: error while loading mandal wise summary list report due to:" + error);
            }

            return View(model);
        }

        public ActionResult FilterMandalWiseSummary(FilterSummaryModel filter)
        {
            FilterSummaryModel model = new FilterSummaryModel();
            ReportViewer rptViewer = new ReportViewer();
            DataSet dsData = new DataSet();

            try
            {
                if (filter != null)
                {
                    dsData = reportService.GetMandalWiseSummaryReport(string.Join(",", filter.MandalId));
                }
                else
                {

                    dsData = reportService.GetMandalWiseSummaryReport(null);

                }
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsMandalWiseSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_MandalWiseSummary,filter.ReportSelectedColumns);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: Error while filter mandal report list, Due to:" + error);
            }

            model.City = filter.City;
            model.MandalList = GetMandalByCityId(mandalService, "", model.City, true);
            return PartialView("_LoadReport", model);
        }


        #endregion

        #region yajman wise summary

        public ActionResult Summary()
        {
            var model = new FilterSummaryModel();
            model.CityList = GetCityList(cityService, "", false);

            model.MandalList = GetMandalByCityId(mandalService, "", 0, true);
            try
            {
                DataSet dsData = reportService.GetYajmanSummaryReport(null);
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsYajmanSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_YajmanSummry,null);
                    }
                    
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: error while loading yajman wise summary list report due to:" + error);
            }

            return View(model);
        }

        public ActionResult FilterSummary(FilterSummaryModel filter)
        {
            FilterSummaryModel model = new FilterSummaryModel();
            ReportViewer rptViewer = new ReportViewer();
            DataSet dsData = new DataSet();

            try
            {
                if (filter != null)
                {
                    dsData = reportService.GetYajmanSummaryReport(string.Join(",", filter.MandalId));
                }
                else
                {

                    dsData = reportService.GetYajmanSummaryReport(null);

                }
                if (dsData.Tables.Count > 0)
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        CreateReport(dsData.Tables[0], null, "DsYajmanSummary", CommonHelper.LocalReportLocation + CommonHelper.RPT_YajmanSummry,filter.ReportSelectedColumns);
                    }
                    if (dsData.Tables[1].Rows.Count > 0)
                    {
                        model.ColumnsList = GetColumnsListForReport(dsData.Tables[1]);
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("Report: Error while filter mandal report list, Due to:" + error);
            }

            model.City = filter.City;
            model.MandalList = GetMandalByCityId(mandalService, "", model.City, true);
            return PartialView("_LoadReport", model);
        }


        #endregion
        private void CreateReport(DataTable dtData, FilterDataModel filter, string dataSource, string reportName, string[] selectedColummns)
        {
            ReportViewer rptViewer = new ReportViewer();
            rptViewer.ProcessingMode = ProcessingMode.Local;
            rptViewer.SizeToReportContent = true;
            rptViewer.ZoomMode = ZoomMode.PageWidth;
            rptViewer.Width = Unit.Percentage(99);
            rptViewer.Height = Unit.Pixel(1000);
            rptViewer.AsyncRendering = true;



            ReportDataSource rdsArea = new ReportDataSource(dataSource, dtData);
            rptViewer.LocalReport.DataSources.Add(rdsArea);

            rptViewer.LocalReport.ReportPath = Server.MapPath(reportName);

            if (filter != null && filter.ReportSelectedColumns != null)
            {
                ReportParameter[] rpArray;

                ReportParameter rptDisplayField = new ReportParameter("displayField", filter.ReportSelectedColumns);
                rpArray = new ReportParameter[] { rptDisplayField };
                rptViewer.LocalReport.SetParameters(rpArray);
            }
            else if (selectedColummns != null && selectedColummns.Length > 0)
            {
                ReportParameter[] rpArray;

                ReportParameter rptDisplayField = new ReportParameter("displayField", selectedColummns);
                rpArray = new ReportParameter[] { rptDisplayField };
                rptViewer.LocalReport.SetParameters(rpArray);

            }

            rptViewer.LocalReport.Refresh();

            ViewBag.ReportViewer = rptViewer;
        }

    }
}
