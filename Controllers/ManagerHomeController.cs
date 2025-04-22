using Microsoft.AspNetCore.Mvc;

namespace CDIO.Controllers
{
    public class ManagerHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

}
