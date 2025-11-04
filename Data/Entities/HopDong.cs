using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities; 

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
    public decimal LuongCoBan { get; set; }
    public decimal? PhuCapAnTrua { get; set; }
    public decimal? PhuCapXangXe { get; set; }
    public decimal? PhuCapDienThoai { get; set; }
    public decimal? PhuCapTrachNhiem { get; set; }
    public decimal? PhuCapKhac { get; set; }
    public string TrangThai { get; set; } = null!;

    [Required, ForeignKey(nameof(NhanVien))]
    public string MaNV { get; set; } = null!;
    // Navigation
    public NhanVien NhanVien { get; set; } = null!;
}
