using Microsoft.AspNetCore.Mvc;
using CDIO.Models;
using CDIO.Data;
using System.Linq;

namespace CDIO.Controllers
{
    public class UserHomeController : Controller
    {
        private readonly AppDbContext _context;

        public UserHomeController(AppDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách phòng
        public IActionResult Index()
        {
            var rooms = _context.Rooms.ToList();
            return View(rooms);
        }

        // Xem chi tiết phòng theo Id
        public IActionResult Details(int id)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }
    }
}
