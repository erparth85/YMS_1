using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class YagnaDay : BaseEntity
    {
        public string Keyword { get; set; }
        public DateTime YagnaDate { get; set; }
    }
}
