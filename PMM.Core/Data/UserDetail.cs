using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core.Data
{
    public class UserDetail : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public int MandalId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        
    }
}
