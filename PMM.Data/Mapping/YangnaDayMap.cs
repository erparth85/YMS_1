using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
   public class YangnaDayMap : EntityTypeConfiguration<YagnaDay>
    {
        public YangnaDayMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Keyword).IsRequired();
            Property(t => t.YagnaDate).IsRequired();
            ToTable("yagna_YagnaDays");
        }
    }
}
