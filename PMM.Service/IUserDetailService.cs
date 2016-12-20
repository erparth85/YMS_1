using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Service
{
    public interface IUserDetailService
    {
        List<UserDetail> GetAll();
        UserDetail GetSevaDetailById(int id);
        void SaveOrUpdateSevaDetail(UserDetail seva);
        List<UserDetail> GetKaryakarDetailByMobile(string mobile);

        UserDetail GetUserByUserName(string email);
        UserDetail ValidateUser(string username, string password);
    }
}
