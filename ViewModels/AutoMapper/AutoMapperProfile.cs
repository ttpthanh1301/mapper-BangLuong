namespace BangLuong.ViewModels.AutoMapper;

using AutoMapper;
using BangLuong.Data.Entities;
using global::AutoMapper;
using static BangLuong.ViewModels.BangTinhLuongViewModels;
using static BangLuong.ViewModels.ChamCongViewModels;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;
using static BangLuong.ViewModels.ChiTietKyLuatViewModels;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;
using static BangLuong.ViewModels.ChucVuViewModels;
using static BangLuong.ViewModels.DanhMucKhenThuongViewModels;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels;
using static BangLuong.ViewModels.DanhMucPhuCapViewModels;
using static BangLuong.ViewModels.HopDongViewModels;
using static BangLuong.ViewModels.NguoiDungViewModels;
using static BangLuong.ViewModels.NguoiPhuThuocViewModels;
using static BangLuong.ViewModels.NhanVienViewModels;
using static BangLuong.ViewModels.PhongBanViewModels;
using static BangLuong.ViewModels.ThamSoHeThongViewModels;
using static BangLuong.ViewModels.TongHopCongViewModels;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // 1. Phòng ban
        CreateMap<PhongBan, PhongBanViewModel>();
        CreateMap<PhongBanViewModel, PhongBan>();
        CreateMap<PhongBanRequest, PhongBan>();

        // 2. Chức vụ
        CreateMap<ChucVu, ChucVuViewModel>();
        CreateMap<ChucVuViewModel, ChucVu>();
        CreateMap<ChucVuRequest, ChucVu>();

        // 3. Nhân viên
        CreateMap<NhanVien, NhanVienViewModel>();
        CreateMap<NhanVienViewModel, NhanVien>();
        CreateMap<NhanVienRequest, NhanVien>();

        // 4. Người phụ thuộc
        CreateMap<NguoiPhuThuoc, NguoiPhuThuocViewModel>();
        CreateMap<NguoiPhuThuocViewModel, NguoiPhuThuoc>();
        CreateMap<NguoiPhuThuocRequest, NguoiPhuThuoc>();
        
        // 5. Người dùng (Thường liên quan đến Authentication)
        CreateMap<NguoiDung, NguoiDungViewModel>();
        CreateMap<NguoiDungViewModel, NguoiDung>();
        CreateMap<NguoiDungRequest, NguoiDung>();

        // 6. Hợp đồng
        CreateMap<HopDong, HopDongViewModel>();
        CreateMap<HopDongViewModel, HopDong>();
        CreateMap<HopDongRequest, HopDong>();

        // 7. Danh mục phụ cấp
        CreateMap<DanhMucPhuCap, DanhMucPhuCapViewModel>();
        CreateMap<DanhMucPhuCapViewModel, DanhMucPhuCap>();
        CreateMap<DanhMucPhuCapRequest, DanhMucPhuCap>();

        // 8. Danh mục khen thưởng
        CreateMap<DanhMucKhenThuong, DanhMucKhenThuongViewModel>();
        CreateMap<DanhMucKhenThuongViewModel, DanhMucKhenThuong>();
        CreateMap<DanhMucKhenThuongRequest, DanhMucKhenThuong>();

        // 9. Danh mục kỷ luật
        CreateMap<DanhMucKyLuat, DanhMucKyLuatViewModel>();
        CreateMap<DanhMucKyLuatViewModel, DanhMucKyLuat>();
        CreateMap<DanhMucKyLuatRequest, DanhMucKyLuat>();
        
        // 10. Chi tiết phụ cấp
        CreateMap<ChiTietPhuCap, ChiTietPhuCapViewModel>();
        CreateMap<ChiTietPhuCapViewModel, ChiTietPhuCap>();
        CreateMap<ChiTietPhuCapRequest, ChiTietPhuCap>();

        // 11. Chi tiết khen thưởng
        CreateMap<ChiTietKhenThuong, ChiTietKhenThuongViewModel>();
        CreateMap<ChiTietKhenThuongViewModel, ChiTietKhenThuong>();
        CreateMap<ChiTietKhenThuongRequest, ChiTietKhenThuong>();

        // 12. Chi tiết kỷ luật
        CreateMap<ChiTietKyLuat, ChiTietKyLuatViewModel>();
        CreateMap<ChiTietKyLuatViewModel, ChiTietKyLuat>();
        CreateMap<ChiTietKyLuatRequest, ChiTietKyLuat>();

        // 13. Chấm công
        CreateMap<ChamCong, ChamCongViewModel>();
        CreateMap<ChamCongViewModel, ChamCong>();
        CreateMap<ChamCongRequest, ChamCong>();

        // 14. Tổng hợp công
        CreateMap<TongHopCong, TongHopCongViewModel>();
        CreateMap<TongHopCongViewModel, TongHopCong>();
        CreateMap<TongHopCongRequest, TongHopCong>();

        // 15. Bảng tính lương
        CreateMap<BangTinhLuong, BangTinhLuongViewModel>();
        CreateMap<BangTinhLuongViewModel, BangTinhLuong>();
        CreateMap<BangTinhLuongRequest, BangTinhLuong>();
        
        
        // 16. Tham số hệ thống
        CreateMap<ThamSoHeThong, ThamSoHeThongViewModel>();
        CreateMap<ThamSoHeThongViewModel, ThamSoHeThong>();
        CreateMap<ThamSoHeThongRequest, ThamSoHeThong>();
        
        CreateMap<BaoHiem, BaoHiemViewModels.BaoHiemViewModel>();
        CreateMap<BaoHiemViewModels.BaoHiemViewModel, BaoHiem>();
        CreateMap<BaoHiemViewModels.BaoHiemRequest, BaoHiem>();
    }
}