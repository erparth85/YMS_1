using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Mandal
{
    [Validator(typeof(AreaDetailValidator))]
    public class MandalDetailModel:BaseModel
    {
        public MandalDetailModel()
        {
            CityList = new List<SelectListItem>();
        }
        public IList<SelectListItem> CityList { get; set; }
        public int CityId { get; set; }
        public string MandalName { get; set; }
        public string CityTitle { get; set; }
    }
}