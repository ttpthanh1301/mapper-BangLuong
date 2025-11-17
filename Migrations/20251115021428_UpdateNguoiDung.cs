using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNguoiDung : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_NhanVien_MaNV",
                table: "NguoiDung");

            migrationBuilder.DropIndex(
                name: "IX_NguoiDung_MaNV",
                table: "NguoiDung");

            migrationBuilder.DropColumn(
                name: "MaNV",
                table: "NguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_NhanVien_Id",
                table: "NguoiDung",
                column: "Id",
                principalTable: "NhanVien",
                principalColumn: "MaNV",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_NhanVien_Id",
                table: "NguoiDung");

            migrationBuilder.AddColumn<string>(
                name: "MaNV",
                table: "NguoiDung",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaNV",
                table: "NguoiDung",
                column: "MaNV",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_NhanVien_MaNV",
                table: "NguoiDung",
                column: "MaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
