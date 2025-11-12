using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.AspNetCore.Identity;

public class NguoiDung : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string PhanQuyen { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string TrangThai { get; set; } = null!;

    [NotMapped]
    public string MaNV
    {
        get => Id;   // Id của IdentityUser chính là MaNV
        set => Id = value;
    }

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}
