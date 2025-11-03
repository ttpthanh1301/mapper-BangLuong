using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;

public class NguoiDung
{
    [Key, StringLength(15)]
    public string MaNV { get; set; } = null!;

    [Required, StringLength(255)]
    public string MatKhau { get; set; } = null!;

    [Required, StringLength(50)]
    public string PhanQuyen { get; set; } = null!;

    [Required, StringLength(50)]
    public string TrangThai { get; set; } = null!;

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
