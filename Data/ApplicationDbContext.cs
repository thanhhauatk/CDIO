using Microsoft.EntityFrameworkCore;
using CDIO.Models; // để DbContext nhận ra model Room

namespace CDIO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        // Bạn có thể thêm các DbSet khác như Booking, User... ở đây
    }
}
