using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class SevaTypeDetailValidator : AbstractValidator<SevaTypeDetailModel>
    {
        public SevaTypeDetailValidator()
        {
            RuleFor(x => x.SevaGradeId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForSevaGrade);
            RuleFor(x => x.SevaTypeText).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForSevaType);
            RuleFor(x => x.Amount).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAmount);
        }
    }
}