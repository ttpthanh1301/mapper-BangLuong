using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class NguoiDung
{
    [Key]
    public string MaNV { get; set; } = null!;
    public string MatKhau { get; set; } = null!;
    public string PhanQuyen { get; set; } = null!;
    public string TrangThai { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
