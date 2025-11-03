using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class TongHopCong
{
    [Key]
    public int MaTHC { get; set; }

    [Required]
    public int KyLuongThang { get; set; }

    [Required]
    public int KyLuongNam { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal SoNgayCong { get; set; }
    [Precision(18, 2)]

    public decimal? SoGioTangCaNgayThuong { get; set; }
    [Precision(18, 2)]
    public decimal? SoGioTangCaCuoiTuan { get; set; }
    [Precision(18, 2)]
    public decimal? SoGioTangCaNgayLe { get; set; }
    public int? SoNgayNghiPhep { get; set; }

    [Required, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
