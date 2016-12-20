using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMM.Web.Models
{
    public class CityListModel
    {
        public CityListModel()
        {
            Cities = new List<CityModel>();
        }
        public IList<CityModel> Cities { get; set; }
    }
}