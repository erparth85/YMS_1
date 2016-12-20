using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
    public class SvikrutiPatrakDetailMap : EntityTypeConfiguration<SvikrutiPatrakDetail>
    {
        public SvikrutiPatrakDetailMap()
        {
            HasKey(t => t.Id);
            Property(t => t.FormNo).IsRequired();
            ToTable("yagna_SvikrutiPatrakDetails");
        }
    }
}
