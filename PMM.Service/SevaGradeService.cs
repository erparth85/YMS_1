using PMM.Core.Data;
using PMM.Data;
using System.Collections.Generic;
using System.Linq;

namespace PMM.Service
{
    public class SevaGradeService : ISevaGradeService
    {
        private readonly IRepository<SevaGrade> sevaGradeRepository;

        public SevaGradeService(IRepository<SevaGrade> _sevaGradeRepository)
        {
            sevaGradeRepository = _sevaGradeRepository;
        }
        public List<SevaGrade> GetAll()
        {
            return sevaGradeRepository.Table.Where(t => t.IsDeleted != true).OrderBy(t => t.Grade).ToList();
        }

        public SevaGrade GetSevaGradeById(int id)
        {
            return sevaGradeRepository.Table.Where(t => t.Id == id).FirstOrDefault();
        }

        public void SaveOrUpdateSevaGrade(SevaGrade sevagradeData)
        {
            if (sevagradeData.Id > 0)
            {
                var sevaGradeDetail = sevaGradeRepository.Table.Where(t => t.Id == sevagradeData.Id).FirstOrDefault();
                if (sevaGradeDetail != null && sevaGradeDetail.Id > 0)
                {
                    sevaGradeDetail.Grade = sevagradeData.Grade;
                    sevaGradeDetail.UpdatedBy = sevagradeData.UpdatedBy;
                    sevaGradeDetail.UpdatedDate = sevagradeData.UpdatedDate;
                    sevaGradeRepository.Update(sevaGradeDetail);
                }
            }
            else
            {
                sevaGradeRepository.Insert(sevagradeData);
            }
        }

        public void DeleteSevaGradeDetailById(SevaGrade grade)
        {
            var data = sevaGradeRepository.Table.Where(t => t.Id == grade.Id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.UpdatedBy = grade.UpdatedBy;
                data.UpdatedDate = grade.UpdatedDate;
                sevaGradeRepository.Update(data);
            }
        }
    }
}
