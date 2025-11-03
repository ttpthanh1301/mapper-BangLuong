using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class NguoiPhuThuoc
{
    [Key]
    public int MaNPT { get; set; }

    [Required, StringLength(100)]
    public string HoTen { get; set; } = null!;

    [Required, DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; }

    [Required, StringLength(50)]
    public string MoiQuanHe { get; set; } = null!;

    [Required, DataType(DataType.Date)]
    public DateTime ThoiGianBatDauGiamTru { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ThoiGianKetThucGiamTru { get; set; }

    [Required, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
