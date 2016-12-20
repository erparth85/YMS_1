using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
    }
}