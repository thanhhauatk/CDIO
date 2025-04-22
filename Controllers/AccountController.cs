using CDIO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace CDIO.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Hiển thị trang đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", model.TenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", model.MatKhau); // plain text

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["Quyen"].ToString();
                    HttpContext.Session.SetString("TenDangNhap", model.TenDangNhap);
                    HttpContext.Session.SetString("Quyen", role);

                    // Điều hướng dựa theo quyền
                    if (role == "admin")
                        return RedirectToAction("Index", "AdminHome");
                    else if (role == "staff")
                        return RedirectToAction("Index", "StaffHome");
                    else if (role == "manager")
                        return RedirectToAction("Index", "ManagerHome");
                    else
                        return RedirectToAction("Index", "UserHome"); // mặc định
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }

            return View(model);
        }


        // Xử lý đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu và xác nhận mật khẩu không khớp");
                    return View(model);
                }

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE TenDangNhap = @TenDangNhap";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenDangNhap", model.Username);
                    int userCount = (int)cmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                        ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                        return View(model);
                    }

                    // Chèn tài khoản mới vào cơ sở dữ liệu
                    string insertQuery = "INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, Email, Quyen, NgayTao) VALUES (@TenDangNhap, @MatKhau, @Email, @Quyen, @NgayTao)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@TenDangNhap", model.Username);
                    insertCmd.Parameters.AddWithValue("@MatKhau", model.Password); // plain text
                    insertCmd.Parameters.AddWithValue("@Email", model.Email);
                    insertCmd.Parameters.AddWithValue("@Quyen", "user"); // Mặc định quyền là "user"
                    insertCmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);

                    insertCmd.ExecuteNonQuery();

                    TempData["SuccessMessage"] = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        // Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
