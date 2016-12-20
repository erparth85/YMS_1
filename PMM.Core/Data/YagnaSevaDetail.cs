using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class YagnaSevaDetail:BaseEntity
    {
        public int YajmanId { get; set; }
        public int KaryakarId { get; set; }
        public decimal SankalpAmount { get; set; }
        public string PRN { get; set; }
        public string Remarks { get; set; }
        public int YagnaDayId { get; set; }
        public int KundId { get; set; }
        public int SevaGradeId { get; set; }
        public bool AvailableForYagna { get; set; }
    }
}
