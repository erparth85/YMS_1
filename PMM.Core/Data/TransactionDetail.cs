using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
   public class TransactionDetail:BaseEntity
    {
        public int YajmanId { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string BankName { get; set; }
    }
}
