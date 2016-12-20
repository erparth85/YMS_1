using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class SeatingRequest : BaseEntity
    {
        public int YajmanId { get; set; }
        public int RefYajmanId { get; set; }
    }
}
