using CDIO.Data;
using CDIO.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình DbContext để kết nối SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Thêm dịch vụ MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// 3. Cấu hình Session
builder.Services.AddDistributedMemoryCache(); // Bộ nhớ trong cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Timeout sau 30 phút không hoạt động
    options.Cookie.HttpOnly = true;                 // Chỉ cho phép truy cập bằng HTTP (bảo mật hơn)
    options.Cookie.IsEssential = true;              // Cookie bắt buộc (cho phép dùng ngay cả khi user từ chối tracking)
});

// 4. Build app
var app = builder.Build();

// 5. Cấu hình middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Bảo mật HTTPS
}

app.UseHttpsRedirection();      // Chuyển HTTP -> HTTPS
app.UseStaticFiles();           // Cho phép truy cập file tĩnh: css, js, ảnh

app.UseRouting();               // Kích hoạt routing
app.UseSession();               // Kích hoạt Session (phải đặt trước UseAuthorization)
app.UseAuthorization();         // Kích hoạt kiểm tra quyền truy cập (nếu dùng)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
