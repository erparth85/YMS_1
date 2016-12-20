using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
   public interface IMandalService
    {
        List<Mandal> GetAll();
        Mandal GetMandalById(int id);
        List<Mandal> GetMandalListByCityId(int cityId);
        int SaveOrUpdateMandal(Mandal mandal);
        bool IsMandalNameExist(Mandal mandal);
        void DeleteMandalDetailById(Mandal mandal);
        List<Mandal> GetMandalListByCity(string cityId);
    }
}
