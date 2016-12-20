using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(ReferralDetailValidator))]
    public class RefferalDetailModel : BaseModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }

    }
}