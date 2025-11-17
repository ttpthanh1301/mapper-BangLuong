using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BangLuong.Data.Entities;
using Microsoft.AspNetCore.Identity;

public class NguoiDung : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string PhanQuyen { get; set; } = "Employee";

    [Required]
    [MaxLength(50)]
    public string TrangThai { get; set; } = "Active"; // "Active" hoặc "Inactive"

    // XÓA thuộc tính Password - IdentityUser đã có PasswordHash
    // [Required]
    // public string Password { get; set; } = null!;

    [NotMapped]
    public string MaNV
    {
        get => Id;
        set => Id = value;
    }

    [ForeignKey(nameof(MaNV))]
    public NhanVien NhanVien { get; set; } = null!;
}