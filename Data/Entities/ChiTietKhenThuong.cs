using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietKhenThuong
{
    [Key]
    public int MaCTKT { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime NgayKhenThuong { get; set; }

    [StringLength(255)]
    public string? LyDo { get; set; }

    [Required, StringLength(10)]
    public string MaKT { get; set; } = null!;

    [Required, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaKT))]
    public DanhMucKhenThuong DanhMucKhenThuong { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
