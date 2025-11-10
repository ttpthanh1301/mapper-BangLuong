using BangLuong.Data;
using BangLuong.Services;
using BangLuong.Services.Implementations;
using BangLuong.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BangLuongDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BangLuongDbContext") 
                         ?? throw new InvalidOperationException("Connection string 'BangLuongDbContext' not found.")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// DI services
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

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    DbInitializer.Seed(services);
}
// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
