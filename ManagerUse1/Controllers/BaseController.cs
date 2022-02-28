using Einvoince.Web.Security;
using log4net;
using Shop.Domain;
using Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ManagerUse1.Controllers
{
    public class BaseController : Controller
    {
        public UserPrincipal CurrentUser { get; set; }
        protected static readonly ILog log = LogManager.GetLogger(typeof(BaseController));
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) { 
                var tmp = Request.Cookies[FormsAuthentication.FormsCookieName];
                var test = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                if (test != null)
                {
                    CurrentUser = (UserPrincipal)HttpContext.Cache[test.Name];
                    if(CurrentUser == null)
                    {
                        var thisUser = new UserBLL().GetUserByUserName(test.Name);
                        if (thisUser != null)
                        {
                            var identity = new GenericIdentity(thisUser.Username);
                            var principal = new UserPrincipal(identity, null);
                            principal.Username = thisUser.Username;
                            principal.Password = thisUser.Password;
                            principal.Address = thisUser.Address;
                            principal.Status = thisUser.Status;
                            principal.UserType = thisUser.UserType;
                            principal.CreateBy = thisUser.CreateBy;
                            principal.CreateDate = thisUser.CreateDate;
                            HttpContext.Cache[test.Name] = principal;
                            CurrentUser = principal;
                        }
                    }
                }    
            }
            if (CurrentUser == null) Response.Redirect("/Auth/Login");
        }
        protected virtual void CheckRole(string role)
        {
            //if (this.CurrentUser == null) Response.Redirect("/login");
            //else if (!this.CurrentUser.Roles.Contains(role)) throw new HttpUnhandledException("Bạn không có quyền thực hiện chức năng này");
        }
    }
}