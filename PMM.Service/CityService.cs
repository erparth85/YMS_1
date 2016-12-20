using PMM.Core.Data;
using PMM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> cityRepository;

        public CityService(IRepository<City> _cityRepository)
        {
            this.cityRepository = _cityRepository;
        }
        public IList<City> GetAll()
        {
            return cityRepository.Table.Where(t => t.IsDeleted != true).OrderBy(t => t.Title).ToList();
        }
        public City GetCityById(int id)
        {
            return cityRepository.Table.Where(t => t.Id == id).FirstOrDefault();
        }

        public int SaveOrUpdateCity(City city)
        {

            if (!string.IsNullOrWhiteSpace(city.Title))
            {
                if (city.Id > 0)
                {
                    var cityDetail = cityRepository.Table.Where(t => t.Id == city.Id).FirstOrDefault();
                    if (cityDetail != null && cityDetail.Id > 0)
                    {
                        cityDetail.Title = city.Title;
                        cityDetail.UpdatedBy = city.UpdatedBy;
                        cityDetail.UpdatedDate = city.UpdatedDate;
                        cityRepository.Update(cityDetail);
                        return cityDetail.Id;
                    }

                }
                else
                {

                    cityRepository.Insert(city);
                    return city.Id;

                }
            }

            return 0;
        }

        public void DeleteCity(City city)
        {
            if (city.Id > 0)
            {
                var cityData = cityRepository.Table.Where(t => t.Id == city.Id).FirstOrDefault();
                if (cityData != null)
                {
                    cityData.IsDeleted = true;
                    cityData.UpdatedBy = city.UpdatedBy;
                    cityData.UpdatedDate = city.UpdatedDate;
                    cityRepository.Update(cityData);
                }
            }
        }

        public bool IsCityNameIsAlreadyExist(City city)
        {
            var cityDetail = cityRepository.Table.Where(t => t.Title.ToLower().Trim() == city.Title.Trim().ToLower() && t.IsDeleted != true).FirstOrDefault();
            if (cityDetail != null)
            {
                if (cityDetail.Id != city.Id)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
