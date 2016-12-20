using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMM.Core;
using PMM.Core.Data;
using PMM.Service;

namespace PMM.Web.Controllers
{
    public class BaseController : Controller
    {

        [NonAction]
        protected bool IsCurrentUserRegistered(IWorkContext workContext)
        {
            return workContext != null && workContext.CurrentUser != null;
        }

        [NonAction]
        protected IList<SelectListItem> GetCityList(ICityService cityService,string prefix,bool isDefaultSelected)
        {
            var list = new List<SelectListItem>();
            try
            {
                var cityList = cityService.GetAll();

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var city in cityList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = city.Title.ToString(),
                        Value = city.Id.ToString(),
                        Selected= isDefaultSelected
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting city List from base controller due to:" + error);
            }
            return list;
        }

        [NonAction]
        protected IList<SelectListItem> GetAreaList(IMandalService areaService, string prefix)
        {
            var list = new List<SelectListItem>();
            try
            {
                var areaList = areaService.GetAll();

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var areaDetail in areaList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = areaDetail.Title.ToString(),
                        Value = areaDetail.Id.ToString()
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting area List from base controller due to:" + error);
            }
            return list;
        }

        [NonAction]
        protected IList<SelectListItem> GetMandalByCityId(IMandalService mandalService, string prefix,int cityId,bool isDefaultSelected)
        {
            var list = new List<SelectListItem>();
            try
            {
                var mandalList = mandalService.GetMandalListByCityId(cityId);

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var mandalDetail in mandalList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = mandalDetail.Title.ToString(),
                        Value = mandalDetail.Id.ToString(),
                        Selected= isDefaultSelected
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting mandal List from base controller due to:" + error);
            }
            return list;
        }


        [NonAction]
        protected IList<SelectListItem> GetMandalByCityId(IMandalService mandalService, string prefix, string cityId, bool isDefaultSelected)
        {
            var list = new List<SelectListItem>();
            try
            {
                var mandalList = mandalService.GetMandalListByCity(cityId);

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var mandalDetail in mandalList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = mandalDetail.Title.ToString(),
                        Value = mandalDetail.Id.ToString(),
                        Selected = isDefaultSelected
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting mandal List for selected city from base controller due to:" + error);
            }
            return list;
        }



        [NonAction]
        protected IList<SelectListItem> GetAmountList(IYagnaSevaDetailService yagnaService, string prefix)
        {
            var list = new List<SelectListItem>();
            try
            {
                var amountList = yagnaService.GetAmountList();

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var amt in amountList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = amt.ToString(),
                        Value = amt.ToString()
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting amount List from base controller due to:" + error);
            }
            return list;
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetTransactionTypes(string prefix)
        {
            var list = new List<SelectListItem>();
            try
            {
                IEnumerable<TransactionType> transactionList = Enum.GetValues(typeof(TransactionType))
                                                            .Cast<TransactionType>();

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var type in transactionList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = type.ToString(),
                        Value = ((int)type).ToString()
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting transactionList from base controller due to:" + error);
            }
            return list;
        }

        [NonAction]
        protected IEnumerable<SelectListItem> GetSevaGrades(ISevaGradeService gradeService,string prefix)
        {
            var list = new List<SelectListItem>();
            try
            {
                var gradeList = gradeService.GetAll();

                if (!string.IsNullOrEmpty(prefix))
                    list.Add(new SelectListItem
                    {
                        Text = prefix,
                        Value = "0"
                    });

                foreach (var grade in gradeList)
                {
                    list.Add(new SelectListItem
                    {
                        Text = grade.Grade,
                        Value = grade.Id.ToString()
                    });
                }
            }
            catch (Exception error)
            {
                Logger.Log("Error while getting SevaGrade List from base controller due to:" + error);
            }
            return list;
        }

    }
}
