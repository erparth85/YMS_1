using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PMM.Core;
using PMM.Core.Data;

namespace PMM.Service
{
    public partial class WorkContextService : IWorkContext
    {
        private const string UserCookie = "PMM.user";

        private readonly HttpContextBase _httpContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IWebHelper _webHelper;

        private UserDetail _cachedUser;

        public WorkContextService(HttpContextBase httpContext,
            IAuthenticationService authenticationService,
            IWebHelper webHelper)
        {
            this._httpContext = httpContext;
            this._authenticationService = authenticationService;
            this._webHelper = webHelper;
        }

        protected UserDetail GetCurrentUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            UserDetail user = null;
            if (_httpContext != null)
            {

                //already registered user
                //if (user == null || user.IsDeleted || !user.Active)
                //{
                if (user == null)
                {
                    user = _authenticationService.GetAuthenticatedUser();
                }


            }

            //validation
            //if (user != null && !user.Deleted && user.Active && user.Id > 0)
            //{
            if (user != null && user.Id>0)
            {
                ////update last activity date
                //if (user.LastActivityDateUtc.AddMinutes(1.0) < DateTime.UtcNow)
                //{
                //    user.LastActivityDateUtc = DateTime.UtcNow;
                //    _customerService.UpdateCustomer(user);
                //}

                //update IP address
                //string currentIpAddress = _webHelper.GetCurrentIpAddress();
                //if (!String.IsNullOrEmpty(currentIpAddress))
                //{
                //    if (!currentIpAddress.Equals(user.LastIpAddress))
                //    {
                //        user.LastIpAddress = currentIpAddress;
                //        _customerService.UpdateCustomer(user);
                //    }
                //}

                _cachedUser = user;
            }
            else
            {
                //set to guest as well
                //_cachedCustomer = user;
            }

            return _cachedUser;
        }

        protected HttpCookie GetCustomerCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[UserCookie];
        }

        protected void SetCustomerCookie(int customerid)
        {
            var cookie = new HttpCookie(UserCookie);
            cookie.Value = customerid.ToString();
            if (customerid>0)
            {
                cookie.Expires = DateTime.Now.AddMonths(-1);
            }
            else
            {
                int cookieExpires = 24 * 365; //TODO make configurable
                cookie.Expires = DateTime.Now.AddHours(cookieExpires);
            }
            if (_httpContext != null && _httpContext.Response != null)
            {
                _httpContext.Response.Cookies.Remove(UserCookie);
                _httpContext.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        public UserDetail CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
            set
            {
                SetCustomerCookie(value.Id);
                _cachedUser = value;
            }
        }







    }
}
