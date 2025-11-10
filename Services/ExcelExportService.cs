using ClosedXML.Excel;
using BangLuong.ViewModels;
using BangLuong.Services.Interfaces;

namespace BangLuong.Services.Implementations
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportBaoCaoNhanSu(List<BaoCaoNhanSuViewModel> data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Danh Sách Nhân Viên");

            // Header - Tên công ty
            worksheet.Cell(1, 1).Value = "CÔNG TY CỔ PHẦN CÔNG NGHỆ PROTON";
            worksheet.Range(1, 1, 1, 12).Merge().Style
                .Font.SetBold().Font.SetFontSize(13)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Tiêu đề báo cáo
            worksheet.Cell(3, 1).Value = "DANH SÁCH NHÂN VIÊN";
            worksheet.Range(3, 1, 3, 12).Merge().Style
                .Font.SetBold().Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Ngày xuất
            worksheet.Cell(1, 12).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy}";
            worksheet.Cell(1, 12).Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Column headers
            int headerRow = 5;
            var headers = new[] {
                "STT", "Mã NV", "Họ và Tên", "Giới tính", "Ngày sinh", "Phòng ban",
                "Chức vụ", "Ngày vào làm", "Thâm niên (Năm)", "Trạng thái HĐ",
                "Email", "SĐT"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style
                    .Font.SetBold()
                    .Fill.SetBackgroundColor(XLColor.LightGray)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            }

            // Data
            int row = headerRow + 1;
            int stt = 1;
            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = stt;
                worksheet.Cell(row, 2).Value = item.MaNV;
                worksheet.Cell(row, 3).Value = item.HoTen;
                worksheet.Cell(row, 4).Value = item.GioiTinh;
                worksheet.Cell(row, 5).Value = item.NgaySinh?.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 6).Value = item.PhongBan;
                worksheet.Cell(row, 7).Value = item.ChucVu;
                worksheet.Cell(row, 8).Value = item.NgayVaoLam?.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 9).Value = item.ThamNienNam;
                worksheet.Cell(row, 10).Value = item.TrangThaiHopDong;
                worksheet.Cell(row, 11).Value = item.Email;
                worksheet.Cell(row, 12).Value = item.SoDienThoai;

                // Border cho data rows
                worksheet.Range(row, 1, row, 12).Style
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

                row++;
                stt++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportBaoCaoTongHopCong(List<BaoCaoTongHopCongViewModel> data, int thang, int nam)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Tổng Hợp Công");

            // Header - Tên công ty
            worksheet.Cell(1, 1).Value = "CÔNG TY CỔ PHẦN CÔNG NGHỆ PROTON";
            worksheet.Range(1, 1, 1, 11).Merge().Style
                .Font.SetBold().Font.SetFontSize(13)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Ngày xuất
            worksheet.Cell(1, 11).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy}";
            worksheet.Cell(1, 11).Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Tiêu đề báo cáo
            worksheet.Cell(3, 1).Value = $"BẢNG TỔNG HỢP CÔNG THÁNG {thang} NĂM {nam}";
            worksheet.Range(3, 1, 3, 11).Merge().Style
                .Font.SetBold().Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Column headers
            int headerRow = 5;
            var headers = new[] {
                "STT", "Mã NV", "Họ và Tên", "Chức vụ",
                "Ngày công chuẩn", "Ngày công thực tế", "Nghỉ phép",
                "Tăng ca (Ngày thường)", "Tăng ca (Cuối tuần)", "Tăng ca (Ngày lễ)", "Ghi chú"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style
                    .Font.SetBold()
                    .Fill.SetBackgroundColor(XLColor.LightGray)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    .Alignment.SetWrapText();
            }

            // Data
            int row = headerRow + 1;
            int stt = 1;

            // Tính tổng
            decimal tongNgayCongChuan = 0;
            decimal tongNgayCongThucTe = 0;
            decimal tongNghiPhep = 0;
            decimal tongTCNgayThuong = 0;
            decimal tongTCCuoiTuan = 0;
            decimal tongTCNgayLe = 0;

            foreach (var item in data)
            {
                worksheet.Cell(row, 1).Value = stt;
                worksheet.Cell(row, 2).Value = item.MaNV;
                worksheet.Cell(row, 3).Value = item.HoTen;
                worksheet.Cell(row, 4).Value = item.ChucVu;

                // FIX: Thêm .GetValueOrDefault(0) khi gán giá trị vào ô Excel
                worksheet.Cell(row, 6).Value = item.NgayCongThucTe.GetValueOrDefault(0);     // SỬA
                worksheet.Cell(row, 7).Value = item.SoNgayNghiPhep.GetValueOrDefault(0);     // SỬA
                worksheet.Cell(row, 8).Value = item.SoGioTangCaNgayThuong.GetValueOrDefault(0); // SỬA
                worksheet.Cell(row, 9).Value = item.SoGioTangCaCuoiTuan.GetValueOrDefault(0);  // SỬA
                worksheet.Cell(row, 10).Value = item.SoGioTangCaNgayLe.GetValueOrDefault(0);   // SỬA
                worksheet.Cell(row, 11).Value = "";

                // Border cho data rows
                worksheet.Range(row, 1, row, 11).Style
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // FIX: Thêm .GetValueOrDefault(0) khi tính tổng       // SỬA
                tongNgayCongThucTe += item.NgayCongThucTe.GetValueOrDefault(0);      // SỬA
                tongNghiPhep += item.SoNgayNghiPhep.GetValueOrDefault(0);            // SỬA
                tongTCNgayThuong += item.SoGioTangCaNgayThuong.GetValueOrDefault(0); // SỬA
                tongTCCuoiTuan += item.SoGioTangCaCuoiTuan.GetValueOrDefault(0);    // SỬA
                tongTCNgayLe += item.SoGioTangCaNgayLe.GetValueOrDefault(0);          // SỬA

                row++;
                stt++;
            }

            // Dòng tổng
            worksheet.Cell(row, 1).Value = "Tổng";
            worksheet.Range(row, 1, row, 4).Merge();
            worksheet.Cell(row, 5).Value = tongNgayCongChuan;
            worksheet.Cell(row, 6).Value = tongNgayCongThucTe;
            worksheet.Cell(row, 7).Value = tongNghiPhep;
            worksheet.Cell(row, 8).Value = tongTCNgayThuong;
            worksheet.Cell(row, 9).Value = tongTCCuoiTuan;
            worksheet.Cell(row, 10).Value = tongTCNgayLe;

            worksheet.Range(row, 1, row, 11).Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightYellow)
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Set column widths
            worksheet.Column(1).Width = 5;
            worksheet.Column(2).Width = 10;
            worksheet.Column(3).Width = 20;
            worksheet.Column(4).Width = 15;
            worksheet.Columns(5, 10).Width = 12;
            worksheet.Column(11).Width = 15;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportBaoCaoBangLuong(List<BaoCaoBangLuongChiTietViewModel> data, int thang, int nam)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Bảng Lương");

            // Header - Tên công ty
            worksheet.Cell(1, 1).Value = "CÔNG TY CỔ PHẦN CÔNG NGHỆ PROTON";
            worksheet.Range(1, 1, 1, 16).Merge().Style
                .Font.SetBold().Font.SetFontSize(13)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Tiêu đề báo cáo
            worksheet.Cell(2, 1).Value = $"BẢNG LƯƠNG CHI TIẾT THÁNG {thang} NĂM {nam}";
            worksheet.Range(2, 1, 2, 16).Merge().Style
                .Font.SetBold().Font.SetFontSize(16)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Ngày xuất
            worksheet.Cell(1, 16).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy}";
            worksheet.Cell(1, 16).Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Column headers
            int headerRow = 4;
            var headers = new[] {
                "STT", "Mã NV", "Họ Tên", "Chức vụ",
                "Lương CB Hợp đồng", "Ngày công chuẩn", "Ngày công thực tế",
                "Lương Thực tế (A)", "Tổng Phụ Cấp (B)", "Lương Tăng Ca (C)",
                "Khen Thưởng (D)", "Tổng Thu Nhập (Gross) (E = A+B+C+D)",
                "BHXH (8%)", "BHYT (1.5%)", "BHTN (1%)", "Thuế TNCN"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, i + 1);
                cell.Value = headers[i];
                cell.Style
                    .Font.SetBold().Font.SetFontSize(9)
                    .Fill.SetBackgroundColor(XLColor.LightGray)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    .Alignment.SetWrapText();
            }

            // Tiếp tục header row 2
            var headers2 = new[] { "Kỷ luật", "Tổng Khấu Trừ (F)", "Thực Lãnh (E - F)" };
            for (int i = 0; i < headers2.Length; i++)
            {
                var cell = worksheet.Cell(headerRow, 17 + i);
                cell.Value = headers2[i];
                cell.Style
                    .Font.SetBold().Font.SetFontSize(9)
                    .Fill.SetBackgroundColor(XLColor.LightGray)
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                    .Alignment.SetWrapText();
            }

            // Data
            int row = headerRow + 1;
            int stt = 1;
            string currentPhongBan = "";

            // Biến tổng
            decimal tongLuongCB = 0, tongNgayCong = 0, tongLuongThucTe = 0;
            decimal tongPhuCap = 0, tongTangCa = 0, tongKhenThuong = 0, tongThuNhap = 0;
            decimal tongBHXH = 0, tongBHYT = 0, tongBHTN = 0, tongThue = 0;
            decimal tongKyLuat = 0, tongKhauTru = 0, tongThucLanh = 0;

            foreach (var item in data)
            {
                // Thêm dòng phòng ban
                if (currentPhongBan != item.PhongBan && !string.IsNullOrEmpty(item.PhongBan))
                {
                    currentPhongBan = item.PhongBan;
                    worksheet.Range(row, 1, row, 19).Merge();
                    worksheet.Cell(row, 1).Value = currentPhongBan;
                    worksheet.Range(row, 1, row, 19).Style
                        .Font.SetBold()
                        .Fill.SetBackgroundColor(XLColor.LightBlue);
                    row++;
                }

                worksheet.Cell(row, 1).Value = stt;
                worksheet.Cell(row, 2).Value = item.MaNV;
                worksheet.Cell(row, 3).Value = item.HoTen;
                worksheet.Cell(row, 4).Value = item.ChucVu;
                worksheet.Cell(row, 5).Value = item.LuongCoBan;
                worksheet.Cell(row, 6).Value = 21.5; // Ngày công chuẩn
                worksheet.Cell(row, 7).Value = item.NgayCongThucTe;
                worksheet.Cell(row, 8).Value = item.LuongThucTe;
                worksheet.Cell(row, 9).Value = item.TongPhuCap;
                worksheet.Cell(row, 10).Value = item.LuongTangCa;
                worksheet.Cell(row, 11).Value = 0; // Khen thưởng
                worksheet.Cell(row, 12).Value = item.TongThuNhap;
                worksheet.Cell(row, 13).Value = 0; // BHXH
                worksheet.Cell(row, 14).Value = 0; // BHYT
                worksheet.Cell(row, 15).Value = 0; // BHTN
                worksheet.Cell(row, 16).Value = 0; // Thuế
                worksheet.Cell(row, 17).Value = 0; // Kỷ luật
                worksheet.Cell(row, 18).Value = item.TongKhauTru;
                worksheet.Cell(row, 19).Value = item.ThucLanh;

                // Format số
                worksheet.Range(row, 5, row, 19).Style
                    .NumberFormat.SetFormat("#,##0");
                worksheet.Range(row, 5, row, 19).Style
                    .Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Range(row, 5, row, 19).Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

                // Tính tổng
                tongLuongCB += item.LuongCoBan;
                tongNgayCong += item.NgayCongThucTe;
                tongLuongThucTe += item.LuongThucTe;
                tongPhuCap += item.TongPhuCap;
                tongTangCa += item.LuongTangCa;
                tongThuNhap += item.TongThuNhap;
                tongKhauTru += item.TongKhauTru;
                tongThucLanh += item.ThucLanh;

                row++;
                stt++;
            }

            // Dòng tổng PHCTH
            worksheet.Cell(row, 1).Value = "Tổng PHCTH";
            worksheet.Range(row, 1, row, 4).Merge();
            worksheet.Cell(row, 5).Value = tongLuongCB;
            worksheet.Cell(row, 7).Value = tongNgayCong;
            worksheet.Cell(row, 8).Value = tongLuongThucTe;
            worksheet.Cell(row, 9).Value = tongPhuCap;
            worksheet.Cell(row, 10).Value = tongTangCa;
            worksheet.Cell(row, 11).Value = tongKhenThuong;
            worksheet.Cell(row, 12).Value = tongThuNhap;
            worksheet.Cell(row, 13).Value = tongBHXH;
            worksheet.Cell(row, 14).Value = tongBHYT;
            worksheet.Cell(row, 15).Value = tongBHTN;
            worksheet.Cell(row, 16).Value = tongThue;
            worksheet.Cell(row, 17).Value = tongKyLuat;
            worksheet.Cell(row, 18).Value = tongKhauTru;
            worksheet.Cell(row, 19).Value = tongThucLanh;

            worksheet.Range(row, 1, row, 19).Style
                .Font.SetBold();
            worksheet.Range(row, 1, row, 19).Style
                .Fill.SetBackgroundColor(XLColor.Yellow);
            worksheet.Range(row, 1, row, 19).Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            worksheet.Range(row, 1, row, 19).Style
                .NumberFormat.SetFormat("#,##0");
            worksheet.Range(row, 1, row, 19).Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

            // Set column widths
            worksheet.Column(1).Width = 5;
            worksheet.Column(2).Width = 10;
            worksheet.Column(3).Width = 18;
            worksheet.Column(4).Width = 12;
            worksheet.Columns(5, 19).Width = 12;

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportPhieuLuongCaNhan(PhieuLuongCaNhanViewModel data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Phiếu Lương");

            // Header
            worksheet.Cell(1, 1).Value = $"PHIẾU LƯƠNG (PAYSLIP) Kỳ lương: Tháng {data.KyLuongThang} năm {data.KyLuongNam}";
            worksheet.Range(1, 1, 1, 2).Merge().Style
                .Font.SetBold().Font.SetFontSize(14)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium);

            // Thông tin nhân viên
            int row = 3;
            worksheet.Cell(row, 1).Value = "THÔNG TIN NHÂN VIÊN";
            worksheet.Range(row, 1, row, 2).Merge().Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightGray);
            row += 2;

            worksheet.Cell(row, 1).Value = $"Họ và tên: {data.HoTen}";
            worksheet.Range(row, 1, row, 2).Merge();
            row++;

            worksheet.Cell(row, 1).Value = $"Mã NV: {data.MaNV}";
            worksheet.Range(row, 1, row, 2).Merge();
            row++;

            worksheet.Cell(row, 1).Value = $"Phòng ban: {data.PhongBan}";
            worksheet.Range(row, 1, row, 2).Merge();
            row++;

            worksheet.Cell(row, 1).Value = $"Chức vụ: {data.ChucVu}";
            worksheet.Range(row, 1, row, 2).Merge();
            row += 2;

            // Chi tiết lương
            worksheet.Cell(row, 1).Value = "CHI TIẾT LƯƠNG";
            worksheet.Cell(row, 2).Value = "số tiền";
            worksheet.Range(row, 1, row, 2).Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightBlue)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            row++;

            // 1. Lương & Phụ Cấp
            worksheet.Cell(row, 1).Value = "1. Lương & Phụ Cấp";
            worksheet.Range(row, 1, row, 2).Merge().Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightYellow);
            row++;

            var luongItems = new[]
            {
                ("Lương cơ bản HĐ", data.LuongCoBanHopDong),
                ($"Số ngày công chuẩn / Thực tế", data.NgayCongThucTe),
                ("Lương thực tế", data.LuongThucTe),
                ("Phụ cấp (Ăn trưa, Xăng xe,...)", data.TongPhuCap),
                ("Lương tăng ca", data.LuongTangCa),
                ("Khen thưởng/Phụ cấp khác", data.TongKhenThuong)
            };

            foreach (var item in luongItems)
            {
                worksheet.Cell(row, 1).Value = item.Item1;
                worksheet.Cell(row, 2).Value = item.Item2;
                worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
                row++;
            }

            worksheet.Cell(row, 1).Value = "TỔNG THU NHẬP (GROSS)";
            worksheet.Cell(row, 2).Value = data.TongThuNhap_GROSS;
            worksheet.Range(row, 1, row, 2).Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightGreen)
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row += 2;

            // 2. Các khoản Khấu trừ
            worksheet.Cell(row, 1).Value = "2. Các khoản Khấu trừ";
            worksheet.Range(row, 1, row, 2).Merge().Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightYellow);
            row++;

            // Lấy dữ liệu từ ViewModel (giả sử là item)
            var khautruItems = new (string Label, decimal Value)[]
            {
                ("BHXH (8%) ", data.BHXH),
                ("BHYT (1.5%) ", data.BHYT),
                ("BHTN (1%) ", data.BHTN),
                ("Kỷ luật ", data.KyLuat)
            };

            foreach (var kt in khautruItems)
            {
                worksheet.Cell(row, 1).Value = kt.Label;
                worksheet.Cell(row, 2).Value = kt.Value;
                worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
                row++;
            }

            row++;
            // 3. Thuế Thu nhập Cá nhân
            worksheet.Cell(row, 1).Value = "3. Thuế Thu nhập Cá nhân (TNCN)";
            worksheet.Range(row, 1, row, 2).Merge().Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightYellow);
            row++;

            worksheet.Cell(row, 1).Value = "Thu nhập chịu thuế";
            worksheet.Cell(row, 2).Value = data.ThuNhapChiuThue;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row++;

            worksheet.Cell(row, 1).Value = "Giảm trừ (BHXH, BHYT, BHTN)";
            worksheet.Cell(row, 2).Value = data.BHXH + data.BHYT + data.BHTN;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row++;

            worksheet.Cell(row, 1).Value = "Giảm trừ gia cảnh (Bản thân)";
            worksheet.Cell(row, 2).Value = data.GiamTruBanThan;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row++;

            worksheet.Cell(row, 1).Value = $"Giảm trừ người phụ thuộc ({data.SoNguoiPhuThuoc})";
            worksheet.Cell(row, 2).Value = data.GiamTruNguoiPhuThuoc;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row++;

            worksheet.Cell(row, 1).Value = "Thu nhập tính thuế";
            worksheet.Cell(row, 2).Value = data.ThuNhapTinhThue;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            worksheet.Cell(row, 2).Style.Font.SetItalic();
            row++;

            worksheet.Cell(row, 1).Value = "Thuế TNCN phải nộp";
            worksheet.Cell(row, 2).Value = data.ThueTNCNPhaiNop;
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            worksheet.Range(row, 1, row, 2).Style.Font.SetBold();
            row++;

            worksheet.Cell(row, 1).Value = "TỔNG KHẤU TRỪ";
            worksheet.Cell(row, 2).Value = data.TongKhauTru;
            worksheet.Range(row, 1, row, 2).Style
                .Font.SetBold()
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium);
            worksheet.Cell(row, 2).Style.NumberFormat.Format = "#,##0";
            row++;

            worksheet.Cell(row, 1).Value = "THỰC LÃNH (Chuyển khoản):";
            worksheet.Cell(row, 2).Value = $"{data.ThucLanh:N0} VNĐ";
            worksheet.Range(row, 1, row, 2).Style
                .Font.SetBold().Font.SetFontSize(13)
                .Fill.SetBackgroundColor(XLColor.LightBlue)
                .Border.SetOutsideBorder(XLBorderStyleValues.Medium);



            // Set column widths
            worksheet.Column(1).Width = 40;
            worksheet.Column(2).Width = 20;

            // Borders for all cells
            worksheet.Range(3, 1, row, 2).Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}