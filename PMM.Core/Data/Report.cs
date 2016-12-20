using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class Report : BaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
    }
}
