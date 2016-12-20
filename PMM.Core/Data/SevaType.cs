namespace PMM.Core.Data
{
    public class SevaType:BaseEntity
    {
        public int SevaGradeId { get; set; }
        public string SevaTypeText { get; set; }
        public decimal Amount { get; set; }

        public string SevaGradeText { get; set; }
    }
}
