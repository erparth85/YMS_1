using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class YagnaDetailValidator : AbstractValidator<YagnaSevaModel>
    {
        public YagnaDetailValidator()
        {
            RuleFor(x => x.SankalpAmount).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAmount);
            RuleFor(x => x.SevaGradeId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForSevaGrade);
           // RuleFor(x => x.YagnaDayId).NotEqual(0).WithMessage(CommonHelper.ValMsgDrdForYagnaDay);


        }
    }
}