using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DanhMucPhuCap
{
    [Key, StringLength(10)]
    public string MaPC { get; set; } = null!;

    [Required, StringLength(100)]
    public string TenPhuCap { get; set; } = null!;
    [Precision(18, 2)]

    [Required]
    public decimal SoTien { get; set; }

    public ICollection<ChiTietPhuCap>? ChiTietPhuCap { get; set; }
}
public class  DanhMucPhuCapConfiguration : IEntityTypeConfiguration< DanhMucPhuCap>
{
        public void Configure(EntityTypeBuilder< DanhMucPhuCap> builder)
    {
        builder.HasKey(e => e.MaPC);

        builder.HasMany(e => e.ChiTietPhuCap)
               .WithOne(e => e. DanhMucPhuCap)
               .HasForeignKey(e => e.MaPC)
               .HasPrincipalKey(e => e.MaPC);
    }
}
