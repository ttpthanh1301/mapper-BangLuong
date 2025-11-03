
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BangLuong.Data.Entities;

public class NhanVien
{
    [Key]
    [StringLength(15)]
    public string MaNV { get; set; } = null!;
    public string HoTen { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? CCCD { get; set; }

    public string? DiaChi { get; set; }
    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }
    public string? MaSoThue { get; set; }


    public string? TaiKhoanNganHang { get; set; }

    public string? TenNganHang { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayVaoLam { get; set; }
    public string TrangThai { get; set; } = null!;

    [ForeignKey(nameof(PhongBan))]
    public string? MaPB { get; set; }

    [ForeignKey(nameof(ChucVu))]
    public string? MaCV { get; set; }

    // Navigation
    public PhongBan? PhongBan { get; set; }
    public ChucVu? ChucVu { get; set; }
    public ICollection<HopDong>? HopDong { get; set; }
    public ICollection<ChamCong>? ChamCong { get; set; }
    public ICollection<TongHopCong>? TongHopCong { get; set; }
    public ICollection<ChiTietPhuCap>? ChiTietPhuCap { get; set; }
    public ICollection<ChiTietKhenThuong>? ChiTietKhenThuong { get; set; }
    public ICollection<ChiTietKyLuat>? ChiTietKyLuat { get; set; }
    public ICollection<NguoiPhuThuoc>? NguoiPhuThuoc { get; set; }
    public ICollection<BangTinhLuong>? BangTinhLuong { get; set; }
    public NguoiDung? NguoiDung { get; set; }
}
public class NhanVienConfiguration : IEntityTypeConfiguration<NhanVien>
{
    public void Configure(EntityTypeBuilder<NhanVien> builder)
    {
        builder.HasMany(e => e.HopDong)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.ChamCong)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.TongHopCong)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.ChiTietKhenThuong)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.ChiTietKyLuat)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.ChiTietPhuCap)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasMany(e => e.NguoiPhuThuoc)
        .WithOne(e => e.NhanVien)
        .HasForeignKey(e => e.MaNV)
        .HasPrincipalKey(e => e.MaNV);
        builder.HasOne(e => e.NguoiDung)
        .WithOne(e => e.NhanVien)
        .HasForeignKey<NguoiDung>(e => e.MaNV)
        .HasPrincipalKey<NhanVien>(e => e.MaNV);
    }

}

