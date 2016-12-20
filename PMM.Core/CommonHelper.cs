using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMM.Core
{
    public partial class CommonHelper
    {


        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetTEEUserTypeConverter(destinationType);
                TypeConverter sourceConverter = GetTEEUserTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsAssignableFrom(value.GetType()))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T To<T>(object value)
        {
            //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return (T)To(value, typeof(T));
        }

        public static TypeConverter GetTEEUserTypeConverter(Type type)
        {

            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();


            return TypeDescriptor.GetConverter(type);
        }


        public static string GeneratePNRNumber()
        {
            Random random = new Random();
            int length = Convert.ToInt32(ConfigurationManager.AppSettings["PNRLength"]);
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static int PageSize = 25;

        public static DateTime DefaultDateTime = DateTime.UtcNow;

        public static string EncryptionKey = "User@a1203*.*$@";
        public static string EmptyUsername = "Please enter username";
        public static string EmptyPassword = "Please enter password";

        public static string ValMsgEmptyNoRecord = "No Record Found.";

        //public static string PlaceHolderTxtForYagnaBookNo = "Enter BookNo...";
        public static string PlaceHolderTxtForFormtNo = "Enter form no...";
        //public static string ValMsgTxtForYagnaBookNo = "Please enter book no.";
        public static string ValMsgTxtForFormNo = "Please enter form No.";

        #region city
        public static string ValErrorMsgForDuplicateCity = "City name is already exist.";
        #endregion

        #region placeholder for Manage Mandal
        public static string PlaceHolderDrdForCity = "Select City...";
        public static string PlaceHolderDrdForMandal = "Select Mandal...";
        public static string PlaceHolderTxtForMandal = "Enter mandal name...";


        public static string ValMsgDrdForCity = "Please select city.";
        public static string ValMsgTxtForMandal = "Please enter mandal name.";

        public static string ValErrorMsgForDuplicateMandal = "Mandal name is already exist.";
        #endregion

        #region placeholder for Manage YagnaSeva
        public static string PlaceHolderTxtForFirstName = "Enter FirstName...";
        public static string PlaceHolderTxtForMiddleName = "Enter FatherName...";
        public static string PlaceHolderTxtForLastName = "Enter SurName...";
        public static string PlaceHolderTxtForMobileNo = "Enter MobileNo...";
        public static string PlaceHolderTxtForAddress = "Enter Address...";
        public static string PlaceHolderTxtForPinCode = "Enter PinCode...";
        public static string PlaceHolderDrdForSankalpAmount = "Enter Amount...";
        public static string PlaceHolderDrdForAssignedDate = "Select Assigned Date...";
        public static string PlaceHolderDrdForRemarks = "Enter Remarks...";
        public static string PlaceHolderTxtForPANNo = "Enter PAN No...";
        public static string PlaceHolderTxtForPRNNo = "Enter YAJMAN ID...";

        public static string ValMsgTxtForFirstName = "Please enter first name.";
        public static string ValMsgTxtForLastName = "Please enter last name.";
        public static string ValMsgTxtForMobileNo = "Please enter mobile number.";
        public static string ValMsgDrdForMandal = "Please select mandal name.";
        public static string ValMsgTxtForAddress = "Please enter address.";
        public static string ValMsgTxtForPinCode = "Please enter pincode.";
        public static string ValMsgTxtForAmount = "Please enter amount.";


        public static string ValMsgTxtForMobileMinLength = "Min 10 digit number is required.";
        public static string ValMsgTxtForMobileMaxLength = "Max 15 digit number is allowed.";

        public static string ValMsgTxtForPinCodeMinLength = "Min 6 digit number is required.";
        public static string ValMsgTxtForPinCodeMaxLength = "Max 6 digit number is allowed.";
        #endregion

        #region yagna days detail
        public static string ValMsgDrdForYagnaDay = "Please select yagnaday.";
        public static string ValMsgTxtForYagnaDate = "Please select yagna date.";

        public static string PlaceHolderDrdForYagnaDay = "Select YagnaDay...";
        #endregion

        #region seva grade detail
        public static string ValMsgDrdForSevaGrade = "Please select Seva Grade.";
        public static string ValMsgTxtForSevaType = "Please enter SevaType.";

        public static string ValMsgTxtForSevaGrade = "Please enter Seva Grade.";
        public static string PlaceHolderDrdForSevaGrade = "Select SevaGrade...";
        public static string PlaceHolderTxtForSevaType = "Enter SevaType...";
        #endregion

        #region Account Detail
        public static string PlaceHolderTxtForBookNo = "Enter BookNo...";
        public static string PlaceHolderTxtForReceiptNo = "Enter ReceiptNo...";
        public static string PlaceHolderTxtForDateOfReceipt = "Select Receipt Date...";

        public static string ValMsgTxtForAccountBookNo = "Please enter Book No.";
        public static string ValMsgTxtForAccountBookReceiptNo = "Please enter Book Receipt No.";
        public static string ValMsgTxtForDateOfReceipt = "Please select account receipt date.";
        #endregion

        #region Transaction Type
        public static string ValMsgTxtForTransactionType = "Please select TransactionType.";
        public static string ValMsgTxtForTransactionNumber = "Please enter Transaction Number.";
        public static string ValMsgTxtForDateOfIssue = "Please select Date of Issue.";
        public static string ValMsgTxtForBankName = "Please enter Name of Bank.";

        public static string PlaceHolderDrdForTransactionType = "Select TransactionType...";
        public static string PlaceHolderTxtForTransactionNumber = "Enter Transaction Number...";
        public static string PlaceHolderTxtForDateOfIssue = "Enter DateOfIssue...";
        public static string PlaceHolderTxtForBankName = "Enter Bank Name...";
        #endregion


        public static string DefaultSelectedCityText = ConfigurationManager.AppSettings["DefaultSelectedCity"];
        public static string UserTypeTextForKaryakar = ConfigurationManager.AppSettings["UserTypeTextForKaryakar"];
        public static string UserTypeTextForKaryakarSevak = ConfigurationManager.AppSettings["UserTypeTextForKaryakarSevak"];


        #region stored procedure list
        public static string SP_YagnaSevaList = "usp_YagnaSevaList";
        public static string SP_YagnaSevaDetailById = "usp_GetYagnaSevaDetailById";
        public static string SP_YagnaSevaSaveOrUpdate = "usp_YaganaDetailSaveOrUpdate";
        public static string SP_YagnaSevaDelete = "usp_YagnaSevaDelete";
        public static string SP_YagnaFormValueGet = "usp_YagnaFormValueGet";
        public static string SP_ManageEMI = "usp_TransactionDetailSaveOrUpdate";
        public static string SP_GetTransactionDetailById = "usp_GetTransactionDetailByPRN";
        public static string SP_TransactionDetailSaveOrUpdate = "usp_TransactionDetailSaveOrUpdate";
        public static string SP_DeleteTransactionDetailByAccountId = "usp_TransactionDetailDelete";
        public static string SP_KarykarListByMobile = "usp_FindKarykarByMobileNumber";
        public static string SP_CheckYajmanRegistrationDetail = "usp_CheckYajmanRegistraionDetail";
        public static string SP_GetYajmanDetailByMobileNo = "usp_FindYajmanByMobileNumber";

        public static string SP_ReferralYagnaSevaDetailById = "usp_ReferralYagnaSevaDetailById";
        public static string SP_ReferralYagnaSevaDetailSaveOrUpdate = "usp_ReferralYagnaSevaSaveOrUpdate";
        public static string SP_ReferralYagnaSevaList = "usp_ReferralYagnaSevaList";
        public static string SP_ReferralYagnaSevaDelete = "usp_ReferralYagnaSevaDelete";
        public static string sP_ReferralDetailByMobile = "usp_FindReferralByMobileNumber";
        public static string SP_MandalListByCity = "usp_MandalListByCity";
        #endregion

        #region stored procedure for ssrs report
        public static string LocalReportLocation = ConfigurationManager.AppSettings["ReportPath"];

        #region ssrs report name
        public static string RPT_YajmanList = "Yajmans.rdl";
        public static string RPT_RefferalYajmanList = "ReferralYagnaSeva.rdl";
        public static string RPT_CityWiseSummary = "CityWiseSummary.rdl";
        public static string RPT_MandalWiseSummary = "MandalWiseSummary.rdl";
        public static string RPT_YajmanSummry = "Summary.rdl";
        #endregion
        public static string SP_RPT_YajmanList = "usp_rpt_YagnaSevaList";
        public static string SP_RPT_ReferralSevaList = "usp_rpt_ReferralYagnaSevaList";
        public static string SP_RPT_CityWiseSummary = "usp_rpt_CityWiseSummary";
        public static string SP_RPT_MandalWiseSummary = "usp_rpt_MandalWiseSummary";
        public static string SP_RPT_YajmanSummary = "usp_rpt_YajmanSummary";
        #endregion

        public static string SuccessFullRegistrationPopUpHeader = "Registration done successfully.";
        public static string SuccessFullUpdatePopUpHeader = "Yajman Detail successfully updated.";

        public static string ErrorMsgForDuplicateAccountNo = "Account book or receipt no is already used.";
        public static string ErrorMsgForDuplicateYagnaDetail = "Form no is already used.";
        public static string ErrorMsgForDuplicateTransactionDetail = "Transaction detail  is already used.";
        public static string ErrorMsgForDuplicateYajmanDetail = "Yajman with same mobile number already exist.";

    }
}
