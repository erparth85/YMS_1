using PMM.Core;
using PMM.Core.Data;
using PMM.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public class MandalService : IMandalService
    {
        private readonly IRepository<Mandal> mandalRepository;
        private readonly IRepository<City> cityRepository;


        public MandalService(IRepository<Mandal> _mandalRepository, IRepository<City> _cityRepository)
        {
            this.mandalRepository = _mandalRepository;
            this.cityRepository = _cityRepository;
        }

        public List<Mandal> GetAll()
        {
            List<Mandal> mandalList = new List<Mandal>();
            var mandalData = (from mnd in mandalRepository.Table
                              join ct in cityRepository.Table on mnd.CityId equals ct.Id
                              where mnd.IsDeleted != true && ct.IsDeleted != true
                              orderby ct.Title, mnd.Title
                              select new { mnd, ct }).ToList();
            if (mandalData.Count > 0)
            {
                foreach (var mandal in mandalData)
                {
                    Mandal mandalDetail = new Mandal();
                    mandalDetail.Id = mandal.mnd.Id;
                    mandalDetail.CityName = mandal.ct.Title;
                    mandalDetail.Title = mandal.mnd.Title;
                    mandalList.Add(mandalDetail);
                }
            }
            return mandalList;

        }

        public Mandal GetMandalById(int id)
        {
            return mandalRepository.Table.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<Mandal> GetMandalListByCityId(int cityId)
        {
            List<Mandal> mandalList = new List<Mandal>();
            var mandalData = (from mnd in mandalRepository.Table
                                  //join ct in cityRepository.Table on mnd.CityId equals ct.Id
                              where mnd.IsDeleted != true && (mnd.CityId == cityId || mnd.CityId == 0)
                              orderby mnd.Title
                              select mnd).ToList();
            if (mandalData.Count > 0)
            {
                foreach (var mandal in mandalData)
                {
                    Mandal mandalDetail = new Mandal();
                    mandalDetail.Id = mandal.Id;
                    mandalDetail.Title = mandal.Title;
                    mandalList.Add(mandalDetail);
                }
            }
            return mandalList;
        }

        public List<Mandal> GetMandalListByCity(string cityId)
        {
            List<Mandal> mandalList = new List<Mandal>();
            SqlParameter paramSelectedCity = new SqlParameter("@cityId", cityId);
            var dtMandals = mandalRepository.ExecuteStoreProcedureList(CommonHelper.SP_MandalListByCity, paramSelectedCity);

            if(dtMandals.Rows.Count>0)
            {
                foreach (DataRow dRow in dtMandals.Rows)
                {
                    Mandal mandalDetail = new Mandal();
                    mandalDetail.Id = Convert.ToInt32(dRow["Id"].ToString());
                    mandalDetail.Title = dRow["Title"].ToString();
                    mandalList.Add(mandalDetail);
                }
            }
            return mandalList;
        }


        public int SaveOrUpdateMandal(Mandal mandal)
        {
            if (!string.IsNullOrWhiteSpace(mandal.Title))
            {
                if (mandal.Id > 0)
                {
                    var mandalDetail = mandalRepository.Table.Where(t => t.Id == mandal.Id).FirstOrDefault();
                    if (mandalDetail != null && mandalDetail.Id > 0)
                    {
                        mandalDetail.Title = mandal.Title;
                        mandalDetail.IsDeleted = mandal.IsDeleted;
                        mandalDetail.CityId = mandal.CityId;
                        mandalDetail.UpdatedBy = mandal.UpdatedBy;
                        mandalDetail.UpdatedDate = mandal.UpdatedDate;
                        mandalRepository.Update(mandalDetail);
                        return mandalDetail.Id;
                    }

                }
                else
                {
                    mandalRepository.Insert(mandal);
                    return mandal.Id;
                }
            }
            return 0;
        }

        public void DeleteMandalDetailById(Mandal mandal)
        {
            var mandalDetail = mandalRepository.Table.Where(t => t.Id == mandal.Id).FirstOrDefault();
            if (mandalDetail != null && mandalDetail.Id > 0)
            {
                mandalDetail.IsDeleted = mandal.IsDeleted;
                mandalDetail.UpdatedBy = mandal.UpdatedBy;
                mandalDetail.UpdatedDate = mandal.UpdatedDate;
                mandalRepository.Update(mandalDetail);
            }
        }
        public bool IsMandalNameExist(Mandal mandal)
        {
            var mandalDetail = mandalRepository.Table.Where(t => t.Title.Trim().ToLower() == mandal.Title.Trim().ToLower() && t.IsDeleted != true && t.CityId == mandal.CityId).FirstOrDefault();
            if (mandalDetail != null && mandalDetail.Id > 0)
            {
                if (mandalDetail.Id != mandal.Id)
                    return true;
            }
            return false;
        }
    }
}
