using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietPhuCap
{
    [Key]
    public int MaCTPC { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime NgayApDung { get; set; }

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [Required, StringLength(10)]
    public string MaPC { get; set; } = null!;

    [Required, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaPC))]
    public DanhMucPhuCap DanhMucPhuCap { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
