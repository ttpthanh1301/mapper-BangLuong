using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietPhuCap
{
    [Key]
    public int MaCTPC { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayApDung { get; set; }

    public string? GhiChu { get; set; }

    public string MaPC { get; set; } = null!;

    public string MaNV { get; set; } = null!;

    public DanhMucPhuCap DanhMucPhuCap { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
