using FluentValidation.Attributes;
using PMM.Web.Validators;

namespace PMM.Web.Models.Yagna
{
    [Validator(typeof(SvikrutiPatrakModelDetailValidator))]
    public class SvikrutiPatrakDetailModel:BaseModel
    {
        public string FormNo { get; set; }
    }
}