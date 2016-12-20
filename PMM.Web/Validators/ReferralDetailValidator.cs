using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class ReferralDetailValidator : AbstractValidator<RefferalDetailModel>
    {
        public ReferralDetailValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForFirstName);
            RuleFor(x => x.Mobile).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForMobileNo).Must(x => x.Length > 9).WithMessage(CommonHelper.ValMsgTxtForMobileMinLength);


        }
    }
}