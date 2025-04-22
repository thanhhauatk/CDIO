using Microsoft.EntityFrameworkCore;
using CDIO.Models;

namespace CDIO.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // Các DbSet đại diện cho bảng trong database
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public DbSet<Room> Rooms { get; set; }

        // Cấu hình mô hình và các bảng liên quan
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình bảng TAIKHOAN
            modelBuilder.Entity<TAIKHOAN>(entity =>
            {
                entity.HasKey(e => e.MaTK);

                entity.Property(e => e.TenDangNhap)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Email)
                      .HasMaxLength(100);

                entity.Property(e => e.Quyen)
                      .HasMaxLength(20)
                      .HasDefaultValue("user");

                entity.Property(e => e.NgayTao)
                      .HasDefaultValueSql("GETDATE()");
            });

            // Cấu hình bảng Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RoomName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.RoomType)
                      .HasMaxLength(50);

                entity.Property(e => e.Price)
                      .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Description)
                      .HasMaxLength(1000);

                entity.Property(e => e.ImageUrl)
                      .HasMaxLength(255);

                // Cấu hình enum 'Status' dưới dạng int
                entity.Property(e => e.Status)
                      .HasConversion<int>(); // Sử dụng giá trị int cho cột Status
            });


            // Cấu hình bảng User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.MaTK);

                entity.Property(e => e.TenDangNhap)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Email)
                      .HasMaxLength(100);

                entity.Property(e => e.Quyen)
                      .HasMaxLength(20)
                      .HasDefaultValue("user");

                entity.Property(e => e.NgayTao)
                      .HasDefaultValueSql("GETDATE()");
            });

            // Cấu hình bảng Role (nếu có, ví dụ như cho phân quyền)
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleName)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
