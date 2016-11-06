//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using LogonWEB.Models;

using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LogonWEB.Models;
using System.Collections.Generic;

namespace LogonWEB.Manager
{
    public class SessionManager
    {
        public static bool sessionLogada()
        {
            var sessionId = HttpContext.Current.Session["LogedUserID"];
            int idUserSession = Convert.ToInt32(sessionId);
            
            if (sessionId != null && idUserSession > 0)
            {
                return true;
            }
                return false;
        }
        public static bool sessionAdminLogada()
        {
            var sessionAdmin = HttpContext.Current.Session["LogedUserAdmin"];
            if (sessionAdmin != null && sessionAdmin.Equals(true))
            {
                return true;
            }
            return false;
        }
    }
}