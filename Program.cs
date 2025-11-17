using BangLuong.Data;
using BangLuong.Services;
using BangLuong.Services.Interfaces;
using BangLuong.Services.Implementations;
using BangLuong.IdentityPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// -------------------------------------------------------
// CẤU HÌNH ỨNG DỤNG
// -------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------
// KẾT NỐI DATABASE
// -------------------------------------------------------
builder.Services.AddDbContext<BangLuongDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BangLuongDbContext")
                         ?? throw new InvalidOperationException("❌ Connection string 'BangLuongDbContext' not found.")));

// -------------------------------------------------------
// CẤU HÌNH IDENTITY
// -------------------------------------------------------
builder.Services.AddIdentity<NguoiDung, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<BangLuongDbContext>()
.AddDefaultTokenProviders();

// -------------------------------------------------------
// CẤU HÌNH AUTHENTICATION COOKIE
// -------------------------------------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    // Đặt đường dẫn đăng nhập mặc định
    options.LoginPath = "/NguoiDung/Login";
    
    // Đặt đường dẫn khi bị từ chối truy cập
    options.AccessDeniedPath = "/NguoiDung/AccessDenied";
    
    // Cấu hình cookie
    options.Cookie.Name = ".AspNetCore.Identity.Application";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    
    // Thời gian hết hạn: 30 phút
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    
    // Sliding expiration - mỗi request sẽ reset thời gian
    options.SlidingExpiration = true;
});

// -------------------------------------------------------
// CẤU HÌNH IDENTITY POLICY CHI TIẾT
// -------------------------------------------------------
builder.Services.Configure<IdentityOptions>(opts =>
{
    // Password Policy
    opts.Password.RequiredLength = 8;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireDigit = true;
    opts.Password.RequireNonAlphanumeric = false;

    // User Policy
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

    // SignIn Policy
    opts.SignIn.RequireConfirmedEmail = false;
});

// -------------------------------------------------------
// CẤU HÌNH SESSION
// -------------------------------------------------------
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// -------------------------------------------------------
// SWAGGER
// -------------------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BangLuong API", Version = "v1" });
});

// -------------------------------------------------------
// AUTO MAPPER
// -------------------------------------------------------
builder.Services.AddAutoMapper(typeof(Program));

// -------------------------------------------------------
// ADD MVC
// -------------------------------------------------------
builder.Services.AddControllersWithViews();

// -------------------------------------------------------
// ĐĂNG KÝ DEPENDENCY INJECTION CHO SERVICES
// -------------------------------------------------------
builder.Services.AddTransient<IPasswordValidator<NguoiDung>, CustomPasswordPolicy>();
builder.Services.AddScoped<IPhongBanService, PhongBanService>();
builder.Services.AddScoped<IChucVuService, ChucVuService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<INguoiPhuThuocService, NguoiPhuThuocService>();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
builder.Services.AddScoped<IDanhMucPhuCapService, DanhMucPhuCapService>();
builder.Services.AddScoped<IDanhMucKhenThuongService, DanhMucKhenThuongService>();
builder.Services.AddScoped<IDanhMucKyLuatService, DanhMucKyLuatService>();
builder.Services.AddScoped<IHopDongService, HopDongService>();
builder.Services.AddScoped<IChamCongService, ChamCongService>();
builder.Services.AddScoped<IChiTietPhuCapService, ChiTietPhuCapService>();
builder.Services.AddScoped<IChiTietKhenThuongService, ChiTietKhenThuongService>();
builder.Services.AddScoped<IChiTietKyLuatService, ChiTietKyLuatService>();
builder.Services.AddScoped<ITongHopCongService, TongHopCongService>();
builder.Services.AddScoped<IBangTinhLuongService, BangTinhLuongService>();
builder.Services.AddScoped<IThamSoHeThongService, ThamSoHeThongService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();
builder.Services.AddScoped<IExcelExportService, ExcelExportService>();

// -------------------------------------------------------
// BUILD APP
// -------------------------------------------------------
var app = builder.Build();

// -------------------------------------------------------
// MIGRATION + SEED DỮ LIỆU
// -------------------------------------------------------
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     try
//     {
//         var db = services.GetRequiredService<BangLuongDbContext>();
//         db.Database.Migrate();

//         // Seed dữ liệu mẫu
//         DbInitializer.Seed(services);
//         await IdentitySeeder.SeedUsers(services);
//     }
//     catch (Exception ex)
//     {
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "❌ Lỗi khi migrate hoặc seed database.");
//     }
// }

// -------------------------------------------------------
// MIDDLEWARE
// -------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ✅ AUTHENTICATION & AUTHORIZATION & SESSION (CẦU HÌNH ĐÚNG THỨ TỰ)
app.UseAuthentication();    // Xác định người dùng
app.UseAuthorization();     // Phân quyền cho người dùng
app.UseSession();           // Kích hoạt Session

// -------------------------------------------------------
// SWAGGER
// -------------------------------------------------------
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BangLuong API V1");
});

// -------------------------------------------------------
// ROUTE
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();