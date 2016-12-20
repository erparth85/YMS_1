using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using PMM.Core;
using PMM.Core.Data;

namespace PMM.Service
{
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserDetailService _userService;
        private readonly TimeSpan _expirationTimeSpan;

        private UserDetail _cachedUser;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="userService">User service</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            IUserDetailService userService)
        {
            this._httpContext = httpContext;
            this._userService = userService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }


        public virtual void SignIn(UserDetail user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1,
                user.FirstName,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                 user.FirstName,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        public virtual void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        public virtual UserDetail GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            //if (user != null && user.Active && !user.Deleted && user.IsRegistered())
            //    _cachedUser = user;
            if (user != null)
                _cachedUser = user;
            return _cachedUser;
        }

        public virtual UserDetail GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userEmail = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userEmail))
                return null;
            var user = _userService.GetUserByUserName(userEmail);
            return user;
        }
    }
}
