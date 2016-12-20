using FluentValidation.Attributes;
using PMM.Core.Data;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(TransactionDetailValidator))]
    public class TransactionDetailModel : BaseModel
    {
        public TransactionDetailModel()
        {
            TransactionTypes = new List<SelectListItem>();
        }
        public int TransactionTypeId { get; set; }


        public string TransactionNumber { get; set; }
        public string DateOfIssue { get; set; }
        public string BankName { get; set; }
        public IEnumerable<SelectListItem> TransactionTypes { get; set; }

        public string TransactionTypeName
        {
            get
            {
                TransactionType title = (TransactionType)TransactionTypeId;
                return title.ToString();
            }
        }

        public int AccountId { get; set; }
        public string BookNumber { get; set; }
        public string ReceiptNumber { get; set; }
        public string PRN { get; set; }
        public string Amount { get; set; }
    }
}