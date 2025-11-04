using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class TongHopCong
{
    [Key]
    public int MaTHC { get; set; }

    public int KyLuongThang { get; set; }

    public int KyLuongNam { get; set; }

    public decimal SoNgayCong { get; set; }

    public decimal? SoGioTangCaNgayThuong { get; set; }
    public decimal? SoGioTangCaCuoiTuan { get; set; }
    public decimal? SoGioTangCaNgayLe { get; set; }
    public int? SoNgayNghiPhep { get; set; }
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
