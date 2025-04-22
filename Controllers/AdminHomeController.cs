using Microsoft.AspNetCore.Mvc;

namespace CDIO.Controllers
{
    public class AdminHomeController : Controller
    {
        // Trang chủ Admin
        public ActionResult Index()
        {
            // Kiểm tra xem tên người dùng đã được lưu trong session chưa   
            if (HttpContext.Session.GetString("Username") != null)
            {
                // Lấy tên người dùng từ session
                ViewBag.Username = HttpContext.Session.GetString("Username");
            }
            else
            {
                // Nếu chưa có, có thể cho giá trị mặc định hoặc thông báo lỗi
                ViewBag.Username = "Chưa đăng nhập";
            }

            return View();
        }

        // Một action để đăng nhập và lưu tên người dùng vào session
        public IActionResult Login(string username)
        {
            // Lưu tên người dùng vào session
            HttpContext.Session.SetString("Username", username);

            return RedirectToAction("Index"); // Sau khi đăng nhập thành công, chuyển về trang Index
        }

        // Action để đăng xuất và xóa thông tin tên người dùng khỏi session
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index");
        }
    }
}
