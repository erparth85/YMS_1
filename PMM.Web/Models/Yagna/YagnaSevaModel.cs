using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(YagnaDetailValidator))]
    public class YagnaSevaModel : BaseModel
    {
        public YagnaSevaModel()
        {
            //  YagnaDays = new List<SelectListItem>();
            SevaGrades = new List<SelectListItem>();
        }
        public string SankalpAmount { get; set; }

        public string Remarks { get; set; }

        public BasicDetailModel Yajmans { get; set; }
        public BasicDetailModel Karyakar { get; set; }

        //public int YagnaDayId { get; set; }
        //public IList<SelectListItem> YagnaDays { get; set; }

        public int SevaGradeId { get; set; }
        public IList<SelectListItem> SevaGrades { get; set; }
        public string SevaGradeTitle { get; set; }

        public AccountDetailModel AccountDetail { get; set; }
        public TransactionDetailModel TransactionDetail { get; set; }

        public int YajmanId { get; set; }
        public int KaryakarId { get; set; }

        public int KundId { get; set; }
        public string PRN { get; set; }

        public SvikrutiPatrakDetailModel SvikrutiPatrakDetail { get; set; }

        #region error summary
        public List<ErrorSummaryModel> ErrorSummary { get; set; }
        public bool IsContinue { get; set; }
        public string ErrorMessage { get; set; }
        public RegistrationErrorTypes ErrorType { get; set; }
        #endregion

        public List<TransactionDetailModel> TransactionDetails { get; set; }

        public bool AvailableForYagna { get; set; }

        public string SeatingReqYajmanId { get; set; }

        public bool IsChairRequired { get; set; }
    }
}