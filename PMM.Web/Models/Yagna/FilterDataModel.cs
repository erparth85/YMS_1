using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    public class FilterDataModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public IList<SelectListItem> CityList { get; set; }
        public IList<SelectListItem> MandalList { get; set; }
        public int CityId { get; set; }
        public int MandalId { get; set; }

        public IEnumerable<SelectListItem> SevaGrades { get; set; }
        public int SevaGradeId { get; set; }
        public string PRN { get; set; }

        public string BookNo { get; set; }
        public string ReceiptNo { get; set; }

        public IEnumerable<SelectListItem> ColumnsList { get; set; }
        public string[] ReportSelectedColumns { get; set; }
    }
}