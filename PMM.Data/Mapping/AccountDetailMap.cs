using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
    public class AccountDetailMap : EntityTypeConfiguration<AccountDetail>
    {
        public AccountDetailMap()
        {
            HasKey(t => t.Id);
            Property(t => t.BookNo).IsRequired();
            Property(t => t.ReceiptNo).IsRequired();
            ToTable("yagna_AccountDetails");
            
           
        }
    }
}
