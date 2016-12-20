using PMM.Core.Data;
using System.Data.Entity.ModelConfiguration;

namespace PMM.Data.Mapping
{
    public class MandalMap : EntityTypeConfiguration<Mandal>
    {
        public MandalMap()
        {
            HasKey(t => t.Id);
            Ignore(t => t.CityName);
            ToTable("yagna_Mandals");
        }
    }
}
