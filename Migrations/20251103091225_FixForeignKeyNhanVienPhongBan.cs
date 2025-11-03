using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyNhanVienPhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaPB",
                table: "NhanVien",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPB = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenPB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPB);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaPB",
                table: "NhanVien",
                column: "MaPB");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_PhongBan_MaPB",
                table: "NhanVien",
                column: "MaPB",
                principalTable: "PhongBan",
                principalColumn: "MaPB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_PhongBan_MaPB",
                table: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_MaPB",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaPB",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);
        }
    }
}
