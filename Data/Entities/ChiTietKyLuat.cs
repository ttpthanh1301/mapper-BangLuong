using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietKyLuat
{
    [Key]
    public int MaCTKL { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayViPham { get; set; }

    public string? LyDo { get; set; }

    public string MaKL { get; set; } = null!;

    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaKL))]
    public DanhMucKyLuat DanhMucKyLuat { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
