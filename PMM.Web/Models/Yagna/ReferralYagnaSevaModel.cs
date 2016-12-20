using FluentValidation.Attributes;
using PMM.Web.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models.Yagna
{
   
    public class ReferralYagnaSevaModel : BaseModel
    {
        public ReferralYagnaSevaModel()
        {
            SevaGrades = new List<SelectListItem>();
        }
        public string SankalpAmount { get; set; }

        public string Remarks { get; set; }

        public BasicDetailModel Yajmans { get; set; }

        public int SevaGradeId { get; set; }

        public IList<SelectListItem> SevaGrades { get; set; }

        public string SevaGradeTitle { get; set; }

        public bool AvailableForYagna { get; set; }

        public string SeatingReqYajmanId { get; set; }

        public int YajmanId { get; set; }

        public int KaryakarId { get; set; }

        public int KundId { get; set; }

        public string PRN { get; set; }

        public RefferalDetailModel RefferalDetail { get; set; }


        #region error summary
        public List<ErrorSummaryModel> ErrorSummary { get; set; }
        public bool IsContinue { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        public bool IsChairRequired { get; set; }

    }
}