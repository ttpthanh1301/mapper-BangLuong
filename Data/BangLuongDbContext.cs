using Microsoft.EntityFrameworkCore;
using BangLuong.Data.Entities;
using System.Collections.Generic;

namespace BangLuong.Data
{
    public class BangLuongDbContext : DbContext
    {
        public BangLuongDbContext(DbContextOptions<BangLuongDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PhongBanConfiguration());
            modelBuilder.ApplyConfiguration(new DanhMucKhenThuongConfiguration());
            modelBuilder.ApplyConfiguration(new  DanhMucKyLuatConfiguration());
            modelBuilder.ApplyConfiguration(new  DanhMucPhuCapConfiguration());
            modelBuilder.ApplyConfiguration(new ChucVuConfiguration());
            modelBuilder.ApplyConfiguration(new NhanVienConfiguration());
        }

        // 1.1 Phòng Ban
        public DbSet<PhongBan> PhongBan { get; set; } = null!;

        // 1.2 Chức Vụ
        public DbSet<ChucVu> ChucVu { get; set; } = null!;

        // 1.3 Nhân Viên
        public DbSet<NhanVien> NhanVien { get; set; } = null!;

        // 1.4 Hợp Đồng
        public DbSet<HopDong> HopDong { get; set; } = null!;

        // 2.1 Chấm Công
        public DbSet<ChamCong> ChamCong { get; set; } = null!;

        // 2.2 Tổng Hợp Công
        public DbSet<TongHopCong> TongHopCong { get; set; } = null!;

        // 2.3.1 Danh Mục Phụ Cấp
        public DbSet<DanhMucPhuCap> DanhMucPhuCap { get; set; } = null!;

        // 2.3.2 Chi Tiết Phụ Cấp
        public DbSet<ChiTietPhuCap> ChiTietPhuCap { get; set; } = null!;

        // 2.4.1 Danh Mục Khen Thưởng
        public DbSet<DanhMucKhenThuong> DanhMucKhenThuong { get; set; } = null!;

        // 2.4.2 Chi Tiết Khen Thưởng
        public DbSet<ChiTietKhenThuong> ChiTietKhenThuong { get; set; } = null!;

        // 2.5.1 Danh Mục Kỷ Luật
        public DbSet<DanhMucKyLuat> DanhMucKyLuat { get; set; } = null!;

        // 2.5.2 Chi Tiết Kỷ Luật
        public DbSet<ChiTietKyLuat> ChiTietKyLuat { get; set; } = null!;

        // 3.1 Người Phụ Thuộc
        public DbSet<NguoiPhuThuoc> NguoiPhuThuoc { get; set; } = null!;

        // 3.2 Bảng Tính Lương
        public DbSet<BangTinhLuong> BangTinhLuong { get; set; } = null!;

        // 4.1 Người Dùng
        public DbSet<NguoiDung> NguoiDung { get; set; } = null!;

        // 4.2 Tham Số Hệ Thống
        public DbSet<ThamSoHeThong> ThamSoHeThong { get; set; } = null!;
    }
}
