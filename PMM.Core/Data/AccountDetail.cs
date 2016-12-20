using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
   public class AccountDetail:BaseEntity
    {
        public int YajmanId { get; set; }
        public int BookNo { get; set; }
        public int ReceiptNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfReceipt { get; set; }
    }
}
