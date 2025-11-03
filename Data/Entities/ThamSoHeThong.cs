using System;
using System.ComponentModel.DataAnnotations;

public class ThamSoHeThong
{
    [Key]
    [StringLength(50)]
    public string MaTS { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string TenThamSo { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string GiaTri { get; set; } = string.Empty;

    public DateTime? NgayApDung { get; set; }
}
