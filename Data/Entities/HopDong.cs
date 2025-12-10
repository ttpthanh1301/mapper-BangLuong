using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BangLuong.Data.Entities;

public class HopDong
{
    [Key]
    public int MaHD { get; set; }
    public string? SoHopDong { get; set; }

    public string LoaiHD { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime NgayBatDau { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NgayKetThuc { get; set; }
     [Precision(18, 2)]
    public decimal LuongCoBan { get; set; }
     [Precision(18, 2)]
    public decimal? PhuCapAnTrua { get; set; }
     [Precision(18, 2)]
    public decimal? PhuCapXangXe { get; set; }
     [Precision(18, 2)]
    public decimal? PhuCapDienThoai { get; set; }
     [Precision(18, 2)]
    public decimal? PhuCapTrachNhiem { get; set; }
     [Precision(18, 2)]
    public decimal? PhuCapKhac { get; set; }
    public string TrangThai { get; set; } = null!;

    [Required, ForeignKey(nameof(NhanVien))]
    [StringLength(15)]
    public string MaNV { get; set; } = null!;
    // Navigation
    public NhanVien NhanVien { get; set; } = null!;
}
