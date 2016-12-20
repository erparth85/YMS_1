using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class YagnaDetail:BaseEntity
    {
        public decimal Amount { get; set; }
        public string Remarks { get; set; }

        public UserDetail KaryakarDetail { get; set; }
        public UserDetail YajmanDetail { get; set; }

        public int YagnaDayId { get; set;}
        public int KundId { get; set; }
        public int SevaGradeId { get; set; }
        public string PRN { get; set; }

        public AccountDetail Account { get; set; }
        public TransactionDetail Transaction { get; set; }
        public SvikrutiPatrakDetail SvikrutiPatraDetail { get; set; }
        public bool AvailableForYagna { get; set; }

        public int SeatingReqYajmanId { get; set; }

        public UserDetail ReferralDetail { get; set; }

        public bool IsChairRequired { get; set; }
    }
}
