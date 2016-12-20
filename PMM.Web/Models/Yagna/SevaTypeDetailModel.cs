using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(SevaTypeDetailValidator))]
    public class SevaTypeDetailModel:BaseModel
    {
        public SevaTypeDetailModel()
        {
            SevaGrades = new List<SelectListItem>();
        }
        public int SevaGradeId { get; set; }
        public string SevaTypeText { get; set; }
        public decimal Amount { get; set; }

        public IEnumerable<SelectListItem> SevaGrades { get; set; }

        public string SevaGradeText { get; set; }
        
    }

    public class SevaTypeListModel
    {
        public SevaTypeListModel()
        {
            SevaTypes = new List<SevaTypeDetailModel>();
        }
        public IList<SevaTypeDetailModel> SevaTypes { get; set; }
    }
}