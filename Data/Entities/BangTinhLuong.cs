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
    public int KyLuongThang { get; set; }
    public int KyLuongNam { get; set; }
    public decimal LuongCoBan { get; set; }
    public decimal? TongPhuCap { get; set; }
    public decimal? TongKhenThuong { get; set; }
    public decimal? LuongTangCa { get; set; }
    public decimal? TongThuNhap { get; set; }
    public decimal? GiamTruBHXH { get; set; }
    public decimal? GiamTruBHYT { get; set; }
    public decimal? GiamTruBHTN { get; set; }
    public decimal? TongGiamTruKyLuat { get; set; }
    public decimal? GiamTruThueTNCN { get; set; }
    public decimal ThucLanh { get; set; }
    public string TrangThai { get; set; } = null!;
    public string MaNV { get; set; } = null!;
    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}