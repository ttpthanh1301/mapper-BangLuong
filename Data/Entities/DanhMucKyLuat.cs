using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DanhMucKyLuat
{
    [Key]
    public string MaKL { get; set; } = null!;

    public string TenKyLuat { get; set; } = null!;
    public decimal SoTienPhat { get; set; }

    public ICollection<ChiTietKyLuat>? ChiTietKyLuat { get; set; }
}
public class  DanhMucKyLuatConfiguration : IEntityTypeConfiguration< DanhMucKyLuat>
{
        public void Configure(EntityTypeBuilder< DanhMucKyLuat> builder)
    {
        builder.HasKey(e => e.MaKL);

        builder.HasMany(e => e.ChiTietKyLuat)
               .WithOne(e => e. DanhMucKyLuat)
               .HasForeignKey(e => e.MaKL)
               .HasPrincipalKey(e => e.MaKL);
    }
}
