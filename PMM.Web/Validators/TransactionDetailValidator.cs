using FluentValidation;
using PMM.Core;
using PMM.Web.Models.Yagna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Validators
{
    public class TransactionDetailValidator : AbstractValidator<TransactionDetailModel>
    {
        public TransactionDetailValidator()
        {
            RuleFor(x => x.TransactionTypeId).NotEqual(0).WithMessage(CommonHelper.ValMsgTxtForTransactionType);
            //RuleFor(x => x.TransactionTypeId).GreaterThan(1).When(t => t.TransactionNumber <= 0).WithMessage(CommonHelper.ValMsgTxtForTransactionNumber);
            
            // RuleFor(x => x.DateOfIssue).NotEmpty().When(t => t.TransactionTypeId > 1).WithMessage(CommonHelper.ValMsgTxtForDateOfIssue);
            RuleFor(x => x.BankName).NotEmpty().When(t => t.TransactionTypeId > 2).WithMessage(CommonHelper.ValMsgTxtForBankName);
            RuleFor(x => x.TransactionNumber).NotEmpty().NotEqual("0").When(t => t.TransactionTypeId > 2).WithMessage(CommonHelper.ValMsgTxtForTransactionNumber);


        }

    }
}