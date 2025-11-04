using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

public class ThamSoHeThong
{
    [Key]
    public string MaTS { get; set; } = string.Empty;
    public string TenThamSo { get; set; } = string.Empty;

    public string GiaTri { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime? NgayApDung { get; set; }
}
