using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities; 

namespace BangLuong.Data.Entities;

public class HopDong
{
    [Key]
    public int MaHD { get; set; }

    [StringLength(50)]
    public string? SoHopDong { get; set; }

    [Required, StringLength(100)]
    public string LoaiHD { get; set; } = null!;

    [Required, DataType(DataType.Date)]
    public DateTime NgayBatDau { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NgayKetThuc { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LuongCoBan { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PhuCapAnTrua { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PhuCapXangXe { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PhuCapDienThoai { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PhuCapTrachNhiem { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PhuCapKhac { get; set; }

    [Required, StringLength(50)]
    public string TrangThai { get; set; } = null!;

    [Required, ForeignKey(nameof(NhanVien))]
    public string MaNV { get; set; } = null!;

    // Navigation
    public NhanVien NhanVien { get; set; } = null!;
}
