using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public interface ISevaTypeService
    {
        List<SevaType> GetAll();
        SevaType GetSevaTypeById(int id);
        bool SaveOrUpdateSevaTypeDetail(SevaType sevaTypeDetail);
        bool DeleteSevaTypeById(SevaType sevaTypeDetail);
    }
}
