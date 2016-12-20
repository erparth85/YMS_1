using PMM.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Data.Mapping
{
    public class SeatingRequestMapping : EntityTypeConfiguration<SeatingRequest>
    {
        public SeatingRequestMapping()
        {
            HasKey(t => t.Id);
            ToTable("yagna_YajmanSeatingRequestDetails");
        }
    }
}
