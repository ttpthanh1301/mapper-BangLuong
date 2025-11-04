using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class NguoiPhuThuoc
{
    [Key]
    public int MaNPT { get; set; }
    public string HoTen { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; }
    public string MoiQuanHe { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime ThoiGianBatDauGiamTru { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ThoiGianKetThucGiamTru { get; set; }
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
