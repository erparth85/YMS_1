using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    public class SevaGradeDetailModel:BaseModel
    {
        public string Grade { get; set; }
    }

    public class SevaGradeListModel
    {
        public SevaGradeListModel()
        {
            SevaGrades = new List<SevaGradeDetailModel>();
        }
        public IList<SevaGradeDetailModel> SevaGrades { get; set; }
    }
}