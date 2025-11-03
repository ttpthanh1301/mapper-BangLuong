using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietKyLuat
{
    [Key]
    public int MaCTKL { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime NgayViPham { get; set; }

    [StringLength(255)]
    public string? LyDo { get; set; }

    [Required, StringLength(10)]
    public string MaKL { get; set; } = null!;

    [Required, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaKL))]
    public DanhMucKyLuat DanhMucKyLuat { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
