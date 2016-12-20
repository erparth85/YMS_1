using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
   public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {
        public UserDetailMap()
        {
            HasKey(t => t.Id);
            Ignore(t => t.UserTypeId);
            ToTable("yagna_UserDetails");
        }
    }
}
