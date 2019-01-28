using System.Web.Mvc;

namespace WebWhatsUp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}