using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Report
{
    public class FilterSummaryModel
    {
        public string[] CityId { get; set; }
        public string[] MandalId { get; set; }
        public string[] KaryakarId { get; set; }

        public IEnumerable<SelectListItem> CityList { get; set; }
        public IEnumerable<SelectListItem> MandalList { get; set; }
        public IEnumerable<SelectListItem> KaryakarList { get; set; }

        public int City { get; set; }

        public IEnumerable<SelectListItem> ColumnsList { get; set; }
        public string[] ReportSelectedColumns { get; set; }
    }
}