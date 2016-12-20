using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(SevaDetailValidator))]
    public class BasicDetailModel : BaseModel
    {
        public BasicDetailModel()
        {
            CityList = new List<SelectListItem>();
            Mandals = new List<SelectListItem>();
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }

        public IList<SelectListItem> CityList { get; set; }
        public IList<SelectListItem> Mandals { get; set; }

        public int CityId { get; set; }
        public int MandalId { get; set; }

        public string CityName { get; set; }
        public string MandalName { get; set; }
        public string CityMandal
        {
            get
            {
                return CityName + '-' + MandalName;
            }
        }

        public string FullName
        {
            get
            {
                string name = "";
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    name += FirstName;
                }
                if (!string.IsNullOrWhiteSpace(MiddleName))
                {
                    name += " " + MiddleName;
                }
                if (!string.IsNullOrWhiteSpace(LastName))
                {
                    name += " " + LastName;
                }
                return name;
            }
        }

        public string PANNumber { get; set; }

    }
}