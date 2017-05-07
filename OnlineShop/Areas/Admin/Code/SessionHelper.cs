using System.Web;

namespace OnlineShop.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["login"] = session;
        }

        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["login"];
            return session as UserSession;
        }
    }
}