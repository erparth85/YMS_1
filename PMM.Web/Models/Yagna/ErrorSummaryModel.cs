using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMM.Web.Models.Yagna
{
    public class ErrorSummaryModel
    {
        public int YagnaSevaId { get; set; }
        public string YajmanFirstName { get; set; }
        public string YajmanMiddleName { get; set; }
        public string YajmanLastName { get; set; }
        public string YajmanMobileNo { get; set; }

        public string YajmanFullName
        {
            get
            {
                string name = "";
                if (!string.IsNullOrWhiteSpace(YajmanFirstName))
                {
                    name += YajmanFirstName;
                }
                if (!string.IsNullOrWhiteSpace(YajmanMiddleName))
                {
                    name += " " + YajmanMiddleName;
                }
                if (!string.IsNullOrWhiteSpace(YajmanLastName))
                {
                    name += " " + YajmanLastName;
                }
                return name;
            }
        }

        public string KaryakarFirstName { get; set; }
        public string KaryakarMiddleName { get; set; }
        public string KaryakarLastName { get; set; }

        public string KaryakarFullName
        {
            get
            {
                string name = "";
                if (!string.IsNullOrWhiteSpace(KaryakarFirstName))
                {
                    name += KaryakarFirstName;
                }
                if (!string.IsNullOrWhiteSpace(KaryakarMiddleName))
                {
                    name += " " + KaryakarMiddleName;
                }
                if (!string.IsNullOrWhiteSpace(KaryakarLastName))
                {
                    name += " " + KaryakarLastName;
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

        public int AccountId { get; set; }
        public string BookNo { get; set; }
        public string ReceiptNo { get; set; }
        public string TransactionNumber { get; set; }
        public string BankName { get; set; }

        public string AvailableForYagna { get; set; }
        public string PaidAmount { get; set; }
    }
}