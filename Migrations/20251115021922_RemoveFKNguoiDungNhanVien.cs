using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BangLuong.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFKNguoiDungNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NguoiDung_NhanVien_Id",
                table: "NguoiDung");

            migrationBuilder.AddColumn<string>(
                name: "NguoiDungId",
                table: "NhanVien",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_NguoiDungId",
                table: "NhanVien",
                column: "NguoiDungId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_NguoiDung_NguoiDungId",
                table: "NhanVien",
                column: "NguoiDungId",
                principalTable: "NguoiDung",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanVien_NguoiDung_NguoiDungId",
                table: "NhanVien");

            migrationBuilder.DropIndex(
                name: "IX_NhanVien_NguoiDungId",
                table: "NhanVien");

            migrationBuilder.DropColumn(
                name: "NguoiDungId",
                table: "NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_NguoiDung_NhanVien_Id",
                table: "NguoiDung",
                column: "Id",
                principalTable: "NhanVien",
                principalColumn: "MaNV",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
