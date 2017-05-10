using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
