using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[Table("BangTinhLuong")]
public class BangTinhLuong
{
    [Key]
    public int MaBL { get; set; }

    [Required]
    public int KyLuongThang { get; set; }

    [Required]
    public int KyLuongNam { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal LuongCoBan { get; set; }

    [Precision(18, 2)]
    public decimal? TongPhuCap { get; set; }

    [Precision(18, 2)]
    public decimal? TongKhenThuong { get; set; }

    [Precision(18, 2)]
    public decimal? LuongTangCa { get; set; }

    [Precision(18, 2)]
    public decimal? TongThuNhap { get; set; }

    [Precision(18, 2)]
    public decimal? GiamTruBHXH { get; set; }

    [Precision(18, 2)]
    public decimal? GiamTruBHYT { get; set; }

    [Precision(18, 2)]
    public decimal? GiamTruBHTN { get; set; }

    [Precision(18, 2)]
    public decimal? TongGiamTruKyLuat { get; set; }

    [Precision(18, 2)]
    public decimal? GiamTruThueTNCN { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal ThucLanh { get; set; }

    [Required]
    [StringLength(50)]
    public string TrangThai { get; set; } = null!;

    [Required]
    [StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}