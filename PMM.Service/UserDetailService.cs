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
    public class UserDetailService : IUserDetailService
    {
        private readonly IRepository<UserDetail> sevaDetailRepository;
        private readonly IRepository<UserType> userTypeRepository;

        public UserDetailService(IRepository<UserDetail> _sevaDetailRepository, IRepository<UserType> _userTypeRepository)
        {
            this.sevaDetailRepository = _sevaDetailRepository;
            this.userTypeRepository = _userTypeRepository;
        }

        public List<UserDetail> GetAll()
        {
            return sevaDetailRepository.Table.Where(t => t.IsDeleted != true).ToList();
        }

        public UserDetail GetSevaDetailById(int id)
        {
            return sevaDetailRepository.Table.Where(t => t.Id == id).FirstOrDefault();
        }

        public void SaveOrUpdateSevaDetail(UserDetail seva)
        {
            if (seva.Id > 0)
            {
                var sevaData = sevaDetailRepository.Table.Where(t => t.Id == seva.Id).FirstOrDefault();
                if (sevaData != null && sevaData.Id > 0)
                {
                    sevaData.FirstName = seva.FirstName;
                    sevaData.MiddleName = seva.MiddleName;
                    sevaData.LastName = seva.LastName;
                    sevaData.CityId = seva.CityId;
                    sevaData.MandalId = seva.MandalId;
                    sevaData.Address = seva.Address;
                    sevaData.PinCode = seva.PinCode;
                    sevaData.Mobile = seva.Mobile;
                    sevaDetailRepository.Update(sevaData);
                }
            }
            else
            {
                sevaDetailRepository.Insert(seva);
            }
        }

        public List<UserDetail> GetKaryakarDetailByMobile(string mobile)
        {
            List<UserDetail> userList = new List<UserDetail>();
            SqlParameter paramMobile = new SqlParameter("@mobileno", mobile);
            DataTable dtList = new DataTable();
            dtList=sevaDetailRepository.ExecuteStoreProcedureList(CommonHelper.SP_KarykarListByMobile, paramMobile);
            if(dtList.Rows.Count>0)
            {
                foreach(DataRow drow in dtList.Rows)
                {
                    UserDetail newDetail = new UserDetail();
                    newDetail.FirstName = drow["FirstName"].ToString();
                    newDetail.MiddleName = drow["MiddleName"].ToString();
                    newDetail.LastName = drow["LastName"].ToString();
                    newDetail.Mobile = drow["Mobile"].ToString();
                    newDetail.Id =Convert.ToInt32( drow["Id"].ToString());
                    newDetail.CityId = Convert.ToInt32(drow["CityId"].ToString());
                    newDetail.MandalId = Convert.ToInt32(drow["MandalId"].ToString());
                    userList.Add(newDetail);
                }
            }
            return userList;
        }

        #region admin user
        public virtual UserDetail GetUserByUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in sevaDetailRepository.Table
                        orderby c.Id
                        where c.FirstName.ToLower().Trim() == username.ToLower().Trim()
                        select c;
            var user = query.FirstOrDefault();
            return user;
        }

        public virtual UserDetail ValidateUser(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return null;

                var userData = (from c in sevaDetailRepository.Table.Where(t => t.IsDeleted != true)
                                orderby c.Id
                                where (c.FirstName.ToLower().Trim() == username.ToLower().Trim())
                                select c).FirstOrDefault();
                if (userData != null)
                {
                    string _encrptedPwd = EncryptService.DecryptString(userData.Password, CommonHelper.EncryptionKey);

                    if (_encrptedPwd.Trim() == password.Trim())
                    {
                        return userData;
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log("error while validate user detail for username:" + username + ",due to:" + error);
            }
            return null;
        }
        #endregion
    }
}
