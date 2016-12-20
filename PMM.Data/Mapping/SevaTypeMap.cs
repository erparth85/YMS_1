using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
    public class SevaTypeMap : EntityTypeConfiguration<SevaType>
    {
        public SevaTypeMap()
        {
            HasKey(t => t.Id);
            Property(t => t.SevaGradeId).IsRequired();
            Property(t => t.SevaTypeText).IsRequired();
            Ignore(t => t.SevaGradeText);
            ToTable("yagna_SevaTypes");

        }
    }
}
