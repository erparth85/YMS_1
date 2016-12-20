using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class SvikrutiPatrakModelDetailValidator : AbstractValidator<SvikrutiPatrakDetailModel>
    {
        public SvikrutiPatrakModelDetailValidator()
        {
            //RuleFor(x => x.FormNo).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForFormNo);
        }
    }
}