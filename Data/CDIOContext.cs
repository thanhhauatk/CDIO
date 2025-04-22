using CDIO.Models;
using Microsoft.EntityFrameworkCore;

namespace CDIO.Data
{
    public class CDIOContext : DbContext
    {
        public CDIOContext(DbContextOptions<CDIOContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
