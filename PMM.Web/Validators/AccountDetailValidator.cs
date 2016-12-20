using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class AccountDetailValidator : AbstractValidator<AccountDetailModel>
    {
        public AccountDetailValidator()
        {
            RuleFor(x => x.BookNo).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAccountBookNo);
            RuleFor(x => x.ReceiptNo).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAccountBookReceiptNo);
            RuleFor(x => x.PaidAmount).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForAmount);
            RuleFor(x => x.DateOfReceipt).NotEmpty().WithMessage(CommonHelper.ValMsgTxtForDateOfReceipt);
        }
    }
}