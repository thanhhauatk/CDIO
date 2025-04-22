using System.Diagnostics;
using CDIO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CDIO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult ManagerHome()
        {
            return View();
        }

        public IActionResult StaffHome()
        {
            return View();
        }

        public IActionResult UserHome()
        {
            return View();
        }
    }


}
