using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(YagnaDaysValidator))]
    public class YagnaDayDetailModel:BaseModel
    {
        public string YagnaDay { get; set; }
        public DateTime YagnaDate { get; set; }


    }
}