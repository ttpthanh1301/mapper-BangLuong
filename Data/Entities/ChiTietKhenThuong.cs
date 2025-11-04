using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class ChiTietKhenThuong
{
    [Key]
    public int MaCTKT { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayKhenThuong { get; set; }
    public string? LyDo { get; set; }
    public string MaKT { get; set; } = null!;

    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaKT))]
    public DanhMucKhenThuong DanhMucKhenThuong { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
