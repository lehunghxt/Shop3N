using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Web;

namespace ManagerUse1
{
    public class EHDHub : Hub
    {
        static EHDHub()
        {
        }

        public static void Send(int userId, string message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext("EHDHub");
            hub.Clients.All.logMessage(string.Format("{0}|{1}|{2}", HttpContext.Current.Session.SessionID, userId, message));
        }
    }
}