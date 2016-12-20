using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class SevaDetailValidator : AbstractValidator<BasicDetailModel>
    {
        public SevaDetailValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForFirstName);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForLastName);
            RuleFor(x => x.Mobile).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForMobileNo);
            RuleFor(x => x.CityId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForCity);
            RuleFor(x => x.MandalId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForMandal);
            RuleFor(x => x.Address).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAddress);
            //RuleFor(x => x.PinCode).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForPinCode);
        }
    }
}