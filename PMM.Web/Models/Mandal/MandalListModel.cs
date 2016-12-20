using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Mandal
{
    public class MandalListModel
    {
        public MandalListModel()
        {
            Mandals = new List<MandalDetailModel>();
        }
        public IList<MandalDetailModel> Mandals { get; set; }
    }
}