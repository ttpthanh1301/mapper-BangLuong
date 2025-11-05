using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;

public class ChamCong
{
    [Key]
    public int MaCC { get; set; }

    [DataType(DataType.Date)]
    public DateTime NgayChamCong { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? GioVao { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? GioRa { get; set; }
    [Precision(18, 2)]

    public decimal? SoGioTangCa { get; set; }
    public string MaNV { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
