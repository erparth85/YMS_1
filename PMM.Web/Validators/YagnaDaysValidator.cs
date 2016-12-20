using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class YagnaDaysValidator : AbstractValidator<YagnaDayDetailModel>
    {
        public YagnaDaysValidator()
        {
            RuleFor(x => x.YagnaDay).NotEmpty().WithMessage(CommonHelper.ValMsgDrdForYagnaDay);
            RuleFor(x => x.YagnaDate).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForYagnaDate);
        }
    }
}