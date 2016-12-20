using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Mandal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class AreaDetailValidator : AbstractValidator<MandalDetailModel>
    {
        public AreaDetailValidator()
        {
            RuleFor(x => x.CityId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForCity);
            RuleFor(x => x.MandalName).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForMandal);
        }
    }
}