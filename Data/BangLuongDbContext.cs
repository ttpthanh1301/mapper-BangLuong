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
            // 1️⃣ Bắt buộc: gọi base để Identity cấu hình mặc định
            base.OnModelCreating(modelBuilder);

            // 2️⃣ Áp dụng cấu hình các entity khác
            modelBuilder.ApplyConfiguration(new PhongBanConfiguration());
            modelBuilder.ApplyConfiguration(new DanhMucKhenThuongConfiguration());
            modelBuilder.ApplyConfiguration(new DanhMucKyLuatConfiguration());
            modelBuilder.ApplyConfiguration(new DanhMucPhuCapConfiguration());
            modelBuilder.ApplyConfiguration(new ChucVuConfiguration());
            modelBuilder.ApplyConfiguration(new NhanVienConfiguration());

            // 3️⃣ Keyless ViewModels
            modelBuilder.Entity<BaoCaoNhanSuViewModel>().HasNoKey();
            modelBuilder.Entity<BaoCaoTongHopCongViewModel>().HasNoKey();
            modelBuilder.Entity<BaoCaoBangLuongChiTietViewModel>().HasNoKey().ToView(null);
            modelBuilder.Entity<PhieuLuongCaNhanViewModel>().HasNoKey();

            // 4️⃣ Customize Identity table names
            modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // 5️⃣ Primary key cho các entity phụ để tránh lỗi EF Core
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
                .Property(u => u.Id).HasMaxLength(50).IsUnicode(false);

            modelBuilder.Entity<IdentityRole>()
                .Property(r => r.Id).HasMaxLength(50).IsUnicode(false);
        }

        // 7️⃣ DbSet Keyless ViewModels
        public DbSet<BaoCaoNhanSuViewModel> BaoCaoNhanSuViewModels { get; set; }
        public DbSet<BaoCaoTongHopCongViewModel> BaoCaoTongHopCongViewModels { get; set; }
        public DbSet<BaoCaoBangLuongChiTietViewModel> BaoCaoBangLuongChiTietViewModels { get; set; }
        public DbSet<PhieuLuongCaNhanViewModel> PhieuLuongCaNhanViewModels { get; set; }

        // 8️⃣ DbSet các bảng khác
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
    }
}
