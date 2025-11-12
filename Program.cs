using System.Text;
using BangLuong.Data;
using BangLuong.Services;
using BangLuong.Services.Interfaces;
using BangLuong.Services.Implementations;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

// -------------------------------------------------------
// CẤU HÌNH ỨNG DỤNG
// -------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add MVC Controller & Views
builder.Services.AddControllersWithViews();

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
// CẤU HÌNH JWT AUTHENTICATION
// -------------------------------------------------------
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var jwtSection = builder.Configuration.GetSection("JwtOptions");
if (!jwtSection.Exists())
    throw new Exception("⚠️ Cấu hình 'JwtOptions' chưa được khai báo trong appsettings.json!");

var jwtOptions = jwtSection.Get<JwtOptions>() ?? throw new Exception("⚠️ Không thể đọc JwtOptions từ cấu hình!");
if (string.IsNullOrWhiteSpace(jwtOptions.SigningKey))
    throw new Exception("⚠️ JwtOptions.SigningKey không được để trống!");

var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddAuthorization();

// -------------------------------------------------------
// SWAGGER (HỖ TRỢ JWT + FILE UPLOAD)
// -------------------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BangLuong API", Version = "v1" });
    c.OperationFilter<SwaggerFileOperationFilter>();

    // Thêm Authorization Header
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Nhập token dạng: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -------------------------------------------------------
// AUTO MAPPER
// -------------------------------------------------------
builder.Services.AddAutoMapper(typeof(Program));

// -------------------------------------------------------
// ĐĂNG KÝ DEPENDENCY INJECTION CHO SERVICES
// -------------------------------------------------------
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<BangLuongDbContext>();
        db.Database.Migrate();

        // Seed dữ liệu mẫu
        DbInitializer.Seed(services);
        await IdentitySeeder.SeedUsers(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "❌ Lỗi khi migrate hoặc seed database.");
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

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
// -------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// -------------------------------------------------------
// LỚP PHỤ TRỢ CHO SWAGGER HIỂN THỊ FILE UPLOAD
// -------------------------------------------------------
public class SwaggerFileOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
{
    public void Apply(OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
    {
        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile));
        if (!fileParams.Any()) return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties =
                        {
                            ["file"] = new OpenApiSchema { Type = "string", Format = "binary" }
                        },
                        Required = new HashSet<string> { "file" }
                    }
                }
            }
        };
    }
}
