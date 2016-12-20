using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class Mandal : BaseEntity
    {
        public int CityId { get; set; }
        public string Title { get; set; }
        public string CityName { get; set; }
    }
}
