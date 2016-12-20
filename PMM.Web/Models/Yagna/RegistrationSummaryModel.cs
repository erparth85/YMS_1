using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    public class RegistrationSummaryModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                string name = "";
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    name += FirstName;
                }
                if (!string.IsNullOrWhiteSpace(MiddleName))
                {
                    name += " " + MiddleName;
                }
                if (!string.IsNullOrWhiteSpace(LastName))
                {
                    name += " " + LastName;
                }
                return name;
            }
        }

        public string MobileNumber { get; set; }
        public int PRN { get; set; }
        public string Message { get; set; }
        public string PopupHeader { get; set; }
        public string SankalpAmount { get; set; }
        public string FormNo { get; set; }
    }
}