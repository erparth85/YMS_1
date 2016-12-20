using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(AccountDetailValidator))]
    public class AccountDetailModel:BaseModel
    {
        public string BookNo { get; set; }
        public string ReceiptNo { get; set; }
        public string PaidAmount { get; set; }
        public string DateOfReceipt { get; set; }
        public string DueAmount { get; set; }

    }
}