using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
   public interface ISevaGradeService
    {
        List<SevaGrade> GetAll();
        SevaGrade GetSevaGradeById(int id);
        void SaveOrUpdateSevaGrade(SevaGrade sevagradeData);
        void DeleteSevaGradeDetailById(SevaGrade grade);
    }
}
