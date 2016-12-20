using System;
namespace PMM.Core
{
  public abstract class BaseEntity
    {
      //sadf
      public int Id { get; set; }
      public DateTime? CreatedDate { get; set; }
      public DateTime? UpdatedDate { get; set; }
      public int? CreatedBy { get; set; }
      public int? UpdatedBy { get; set; }
      public bool? IsDeleted { get; set; }
    }
}
