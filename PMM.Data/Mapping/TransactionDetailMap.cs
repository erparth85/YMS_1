using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
    public class TransactionDetailMap : EntityTypeConfiguration<TransactionDetail>
    {
        public TransactionDetailMap()
        {
            HasKey(t => t.Id);
            Property(t => t.TransactionTypeId).IsRequired();
            Property(t => t.YajmanId).IsRequired();
            ToTable("yagna_TransactionDetails");
        }
    }
}
