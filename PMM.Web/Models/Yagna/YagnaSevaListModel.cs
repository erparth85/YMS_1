using PMM.Web.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    public class YagnaSevaListModel: BasePagingModel
    {
        public YagnaSevaListModel()
        {
            YagnaSevaList = new List<YagnaSevaModel>();
        }
        public IList<YagnaSevaModel> YagnaSevaList { get; set; }

        public FilterDataModel Filter { get; set; }

        public decimal TotalAmount { get; set; }

    }
}