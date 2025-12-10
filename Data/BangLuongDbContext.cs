using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data.Entities;
using BangLuong.ViewModels;

namespace BangLuong.Data
{
public class BangLuongDbContext : IdentityDbContext<NguoiDung>
{
public BangLuongDbContext(DbContextOptions<BangLuongDbContext> options)
: base(options)
{
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {  
        // 1️⃣ Gọi base để cấu hình Identity mặc định  
        base.OnModelCreating(modelBuilder);  

        // 2️⃣ Áp dụng configuration các entity  
        modelBuilder.ApplyConfiguration(new PhongBanConfiguration());  
        modelBuilder.ApplyConfiguration(new DanhMucKhenThuongConfiguration());  
        modelBuilder.ApplyConfiguration(new DanhMucKyLuatConfiguration());  
        modelBuilder.ApplyConfiguration(new DanhMucPhuCapConfiguration());  
        modelBuilder.ApplyConfiguration(new ChucVuConfiguration());  
        modelBuilder.ApplyConfiguration(new NhanVienConfiguration());  

        // 3️⃣ Keyless ViewModels (không tạo table)  
        modelBuilder.Entity<BaoCaoNhanSuViewModel>().HasNoKey().ToView(null);  
        modelBuilder.Entity<BaoCaoTongHopCongViewModel>().HasNoKey().ToView(null);  
        modelBuilder.Entity<BaoCaoBangLuongChiTietViewModel>().HasNoKey().ToView(null);  
        modelBuilder.Entity<PhieuLuongCaNhanViewModel>().HasNoKey().ToView(null);  

        // 4️⃣ Customize Identity table names  
        modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");  
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");  
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");  
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");  
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");  
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");  
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");  

        // 5️⃣ Primary key cho các entity phụ Identity  
        modelBuilder.Entity<IdentityUserLogin<string>>()  
                    .HasKey(l => new { l.LoginProvider, l.ProviderKey });  

        modelBuilder.Entity<IdentityUserRole<string>>()  
                    .HasKey(r => new { r.UserId, r.RoleId });  

        modelBuilder.Entity<IdentityUserToken<string>>()  
                    .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });  

        modelBuilder.Entity<IdentityRoleClaim<string>>()  
                    .HasKey(rc => rc.Id);  

        modelBuilder.Entity<IdentityUserClaim<string>>()  
                    .HasKey(uc => uc.Id);  

        // 6️⃣ Tùy chỉnh Id không Unicode và maxLength  
        modelBuilder.Entity<NguoiDung>()  
                    .Property(u => u.Id)  
                    .HasMaxLength(50)  
                    .IsUnicode(false);  

        modelBuilder.Entity<IdentityRole>()  
                    .Property(r => r.Id)  
                    .HasMaxLength(50)  
                    .IsUnicode(false);  
    }  

    // 7️⃣ DbSet Keyless ViewModels (không tạo table)  
    public DbSet<BaoCaoNhanSuViewModel> BaoCaoNhanSuViewModels => Set<BaoCaoNhanSuViewModel>();  
    public DbSet<BaoCaoTongHopCongViewModel> BaoCaoTongHopCongViewModels => Set<BaoCaoTongHopCongViewModel>();  
    public DbSet<BaoCaoBangLuongChiTietViewModel> BaoCaoBangLuongChiTietViewModels => Set<BaoCaoBangLuongChiTietViewModel>();  
    public DbSet<PhieuLuongCaNhanViewModel> PhieuLuongCaNhanViewModels => Set<PhieuLuongCaNhanViewModel>();  

    // 8️⃣ DbSet các bảng thực tế  
    public DbSet<PhongBan> PhongBan { get; set; } = null!;  
    public DbSet<ChucVu> ChucVu { get; set; } = null!;  
    public DbSet<NhanVien> NhanVien { get; set; } = null!;  
    public DbSet<HopDong> HopDong { get; set; } = null!;  
    public DbSet<ChamCong> ChamCong { get; set; } = null!;  
    public DbSet<TongHopCong> TongHopCong { get; set; } = null!;  
    public DbSet<DanhMucPhuCap> DanhMucPhuCap { get; set; } = null!;  
    public DbSet<ChiTietPhuCap> ChiTietPhuCap { get; set; } = null!;  
    public DbSet<DanhMucKhenThuong> DanhMucKhenThuong { get; set; } = null!;  
    public DbSet<ChiTietKhenThuong> ChiTietKhenThuong { get; set; } = null!;  
    public DbSet<DanhMucKyLuat> DanhMucKyLuat { get; set; } = null!;  
    public DbSet<ChiTietKyLuat> ChiTietKyLuat { get; set; } = null!;  
    public DbSet<NguoiPhuThuoc> NguoiPhuThuoc { get; set; } = null!;  
    public DbSet<BangTinhLuong> BangTinhLuong { get; set; } = null!;  
    public DbSet<ThamSoHeThong> ThamSoHeThong { get; set; } = null!;  
    public DbSet<BaoHiem> BaoHiem { get; set; } = null!;  
}  

}
