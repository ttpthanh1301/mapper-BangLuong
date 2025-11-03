using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Migrations
{
    /// <inheritdoc />
    public partial class Addrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChucVu_ChucVuMaCV",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_ChucVuMaCV",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "ChucVuMaCV",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaCV",
                table: "NhanVien",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaCV",
                table: "NhanVien",
                column: "MaCV");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChucVu_MaCV",
                table: "NhanVien",
                column: "MaCV",
                principalTable: "ChucVu",
                principalColumn: "MaCV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_ChucVu_MaCV",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_MaCV",
                table: "NhanVien");

            migrationBuilder.AlterColumn<string>(
                name: "MaCV",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChucVuMaCV",
                table: "NhanVien",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ChucVuMaCV",
                table: "NhanVien",
                column: "ChucVuMaCV");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_ChucVu_ChucVuMaCV",
                table: "NhanVien",
                column: "ChucVuMaCV",
                principalTable: "ChucVu",
                principalColumn: "MaCV");
        }
    }
}
