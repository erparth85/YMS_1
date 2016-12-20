using PMM.Core.Data;
using PMM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public class SevaTypeService : ISevaTypeService
    {
        private readonly IRepository<SevaType> sevaTypeService;
        private readonly IRepository<SevaGrade> sevaGradeService;

        public SevaTypeService(IRepository<SevaType> _sevaTypeService, IRepository<SevaGrade> _sevaGradeService)
        {
            sevaTypeService = _sevaTypeService;
            sevaGradeService = _sevaGradeService;
        }


        public List<SevaType> GetAll()
        {
            List<SevaType> sevaTypeList = new List<SevaType>();
            var sevaTypeData = (from svtype in sevaTypeService.Table
                                join sgrade in sevaGradeService.Table on svtype.SevaGradeId equals sgrade.Id
                                where svtype.IsDeleted != true
                                select new { svtype, sgrade }).OrderBy(t => t.svtype.SevaTypeText).ToList();
            if (sevaTypeData != null)
            {
                foreach (var data in sevaTypeData)
                {
                    SevaType seva = new SevaType();
                    seva.Id = data.svtype.Id;
                    seva.SevaGradeText = data.sgrade.Grade;
                    seva.SevaGradeId = data.sgrade.Id;
                    seva.SevaTypeText = data.svtype.SevaTypeText;
                    seva.Amount = data.svtype.Amount;
                    sevaTypeList.Add(seva);
                }
            }
            return sevaTypeList;
        }

        public SevaType GetSevaTypeById(int id)
        {
            return sevaTypeService.Table.Where(t => t.Id == id).FirstOrDefault();
        }


        public bool SaveOrUpdateSevaTypeDetail(SevaType sevaTypeDetail)
        {
            bool IsUpdated = false;
            if (sevaTypeDetail.Id > 0)
            {
                var typeData = sevaTypeService.Table.Where(t => t.Id == sevaTypeDetail.Id).FirstOrDefault();
                if (typeData != null)
                {
                    typeData.SevaGradeId = sevaTypeDetail.SevaGradeId;
                    typeData.SevaTypeText = sevaTypeDetail.SevaTypeText;
                    typeData.Amount = sevaTypeDetail.Amount;
                    typeData.UpdatedBy = sevaTypeDetail.UpdatedBy;
                    typeData.UpdatedDate = sevaTypeDetail.UpdatedDate;
                    sevaTypeService.Update(typeData);
                    IsUpdated = true;
                }
            }
            else
            {
                sevaTypeService.Insert(sevaTypeDetail);
                IsUpdated = true;
            }
            return IsUpdated;
        }


        public bool DeleteSevaTypeById(SevaType sevaTypeDetail)
        {

            var typeData = sevaTypeService.Table.Where(t => t.Id == sevaTypeDetail.Id).FirstOrDefault();
            if (typeData != null)
            {
                typeData.IsDeleted = true;
                typeData.UpdatedBy = sevaTypeDetail.UpdatedBy;
                typeData.UpdatedDate = sevaTypeDetail.UpdatedDate;
                sevaTypeService.Update(typeData);
                return true;
            }

            return false;
        }
    }
}
