using PMM.Core.Data;
using System.Collections.Generic;

namespace PMM.Service
{
    public interface ICityService
    {
        IList<City> GetAll();
        City GetCityById(int id);
        int SaveOrUpdateCity(City city);
        void DeleteCity(City city);
        bool IsCityNameIsAlreadyExist(City city);
    }
}
