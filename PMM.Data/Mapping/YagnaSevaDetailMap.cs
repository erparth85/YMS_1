using PMM.Core.Data;
using System.Data.Entity.ModelConfiguration;

namespace PMM.Data.Mapping
{
    public class YagnaSevaDetailMap : EntityTypeConfiguration<YagnaSevaDetail>
    {
        public YagnaSevaDetailMap()
        {
            HasKey(t => t.Id);
            ToTable("yagna_YagnaSevaDetails");
        }
    }
}
