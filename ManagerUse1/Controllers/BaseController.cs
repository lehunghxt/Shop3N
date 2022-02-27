using Einvoince.Web.Security;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ManagerUse1.Controllers
{
    public class BaseController : Controller
    {
        public new UserPrincipal CurrentUser { get; set; }
        protected static readonly ILog log = LogManager.GetLogger(typeof(BaseController));
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.CurrentUser = base.User as UserPrincipal;
            if (CurrentUser == null) Response.Redirect("/Auth/Login");
        }
        protected virtual void CheckRole(string role)
        {
            //if (this.CurrentUser == null) Response.Redirect("/login");
            //else if (!this.CurrentUser.Roles.Contains(role)) throw new HttpUnhandledException("Bạn không có quyền thực hiện chức năng này");
        }
    }
}