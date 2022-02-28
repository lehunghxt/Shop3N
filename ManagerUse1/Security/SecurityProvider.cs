// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityProvider.cs" company="ACOM Solutions, Inc.">
//   Copyright (c) 2014 ACOM Solutions, Inc. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using Einvoince.Web.Security;

namespace Einvoince.Web.Security
{
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Security;

    //using Einvoince.Service;
    using Library;
    //using Domain;
    using System.Data;
    //using Service.Cache;
    using Shop.Service;
    using Shop.Domain;
    using ManagerUse1;

    /// <summary>
    ///     The security provider.
    /// </summary>
    public class SecurityProvider
    {
        #region Fields

        /// <summary>
        /// The authenticate type.
        /// </summary>
        public const string AuthenticateType = "Forms";

        /// <summary>
        ///     The basic scheme.
        /// </summary>
        public const string BasicScheme = "Basic";

        /// <summary>
        /// The http request wrapper.
        /// </summary>
        private readonly HttpContextBase httpContext;

        private readonly UserBLL userBLL;
        //private readonly CustomerBLL customerBLL;
        //private UserCachingProvider Provider;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityProvider"/> class.
        /// </summary>
        public SecurityProvider()
        {
            this.httpContext = new HttpContextWrapper(HttpContext.Current);
            this.userBLL = new UserBLL();
            //customerBLL = new CustomerBLL();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get principal.
        /// </summary>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <param name="authenticationType">
        /// The authentication Type.
        /// </param>
        /// <param name="encodeUserdata">
        /// The encode user data.
        /// </param>
        /// <returns>
        /// The <see cref="IPrincipal"/>.
        /// </returns>
        public IPrincipal GetPrincipal(string userName, string authenticationType, string encodeUserdata = null)
        {
            try
            {
                FormTicketData userData = this.GetFormTicketDataFromUserData(encodeUserdata);

                if (userData == null)
                {
                    var user = userBLL.GetUserByUserName(userName);
                    if (user == null) HttpContext.Current.Response.StatusCode = 401;
                    else
                    {
                        userData.Username = user.Username;
                        userData.Password = user.Password;
                        userData.Address = user.Address;
                        userData.Email = user.Email;
                        userData.Status = user.Status;
                        userData.CreateDate = user.CreateDate;
                        userData.CreateBy = user.CreateBy;
                        userData.Roles = user.Roles;

                        //tocken
                        //userData.Tocken = Encrypt.EnCode(userData.UserName + "|" + userData.Pass, DateTime.Now.DayOfYear);
                    }
                }

                if (userData != null)
                {
                    IIdentity identity = new GenericIdentity(userName, authenticationType);
                    //var roles = UserCachingProvider.Instance.GetUserRoles(userData.Id);
                    //if (roles == null || roles.Length < 1)
                    //{
                        //roles = userBLL.GetAllRoleByUserName(userName).Select(e => e.ID).ToArray();
                        //UserCachingProvider.Instance.CacheRoles(userData.Id, string.Join(",",roles));
                    //}
                    var userPrincipal = new UserPrincipal(identity, null);
                    userPrincipal.Id = userData.Id;
                    userPrincipal.Username = userData.Username;
                    userPrincipal.Email = userData.Email;
                    userPrincipal.Password = userData.Password;
                    userPrincipal.Address = userData.Address;
                    userPrincipal.Status = userData.Status;
                    userPrincipal.UserType = userData.UserType;
                    userPrincipal.CreateBy = userData.CreateBy;
                    userPrincipal.CreateDate = userData.CreateDate;
                    return userPrincipal;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 401;
            }

            return null;
        }

        /// <summary>
        /// The set principal.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        private void SetPrincipal(UserPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;

            if (this.httpContext != null)
            {
                this.httpContext.User = principal;
            }
        }

        /// <summary>
        /// The initialize principal.
        /// </summary>
        public void InitializePrincipal()
        {
            var principal = this.httpContext.User;
            if (principal == null || principal.Identity == null || !this.httpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var identity = principal.Identity;
            string userData = null;

            var formsIdentity = identity as FormsIdentity;
            if (formsIdentity != null && formsIdentity.AuthenticationType.Equals("Forms", StringComparison.InvariantCultureIgnoreCase))
            {
                // For forms authentication we get user data from cookie ticket to avoid hit database
                userData = formsIdentity.Ticket.UserData;
            }

            var replacePricipal = this.GetPrincipal(identity.Name, identity.AuthenticationType, userData);
            if (replacePricipal != null)
            {
                this.httpContext.User = replacePricipal;
            }
        }

        public void LogIn(UserModel userData, bool createPersistentCookie)
        {
            IIdentity identity = new GenericIdentity(userData.Username);
            string[] roles = { };
            var userPrincipal = new UserPrincipal(identity, roles);
            userPrincipal.Id = userData.Id;
            userPrincipal.Username = userData.Username;
            userPrincipal.Password = userData.Password;
            userPrincipal.Address = userData.Address;
            userPrincipal.CreateDate = userData.CreateDate;
            userPrincipal.CreateBy = userData.CreateBy;
            userPrincipal.Status = userData.Status;
            userPrincipal.UserType = userData.UserType;
            userPrincipal.Roles = userData.Roles;
            userPrincipal.Email = userData.Email;
            // Set to Principal
            this.SetPrincipal(userPrincipal);

            // set to cookie
            this.httpContext.Response.SetAuthCookie(userPrincipal.Username, createPersistentCookie, GetFormTicketData(userData));

            EHDHub.Send((int)userData.Id, "Login");
        }

        /// <summary>
        ///     The log out.
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// The validate user.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The roles
        /// </returns>
        public int ValidateUser(string userName, string password, bool isSavePass = true)
        {
            int resMess = -3;
            var user = userBLL.GetUserByUserName(userName);
            if (user == null) resMess = -2;
            else if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) resMess = -1;
            else if (user.Status == 1) resMess = 1;
            else if (user.Status == 0)
            {
                this.LogIn(user, isSavePass);
                resMess = 0;
            }
            return resMess;
        }
        #endregion

        #region Methods

        /// <summary>
        /// The get form ticket data from user data.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <returns>
        /// The <see cref="FormTicketData"/>.
        /// </returns>
        private FormTicketData GetFormTicketDataFromUserData(string userData)
        {
            if (string.IsNullOrEmpty(userData))
                return null;
            return JsonHelper.DeserializeObject<FormTicketData>(userData);
        }
        private FormTicketData GetFormTicketData(UserModel userData)
        {
            var result = new FormTicketData
            {
                Id = userData.Id,
                Username = userData.Username,
                Password = userData.Password,
                Address = userData.Address,
                CreateDate = userData.CreateDate,
                CreateBy = userData.CreateBy,
                Status = userData.Status,
                UserType = userData.UserType,
                Roles = userData.Roles,
                Email = userData.Email,
            };
            return result;
        }
        #endregion
    }
}