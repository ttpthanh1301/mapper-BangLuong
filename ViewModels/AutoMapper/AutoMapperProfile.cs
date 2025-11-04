namespace BangLuong.ViewModels.AutoMapper;
using AutoMapper;
using BangLuong.Data.Entities;
using global::AutoMapper;
using static BangLuong.ViewModels.BangTinhLuongViewModels;
using static BangLuong.ViewModels.NhanVienViewModels;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<NhanVien, NhanVienViewModel>();
        CreateMap<NhanVienViewModel, NhanVien>();
        CreateMap<NhanVienRequest, NhanVien>();
        CreateMap<BangTinhLuong, BangTinhLuongViewModel>();
        CreateMap<BangTinhLuongViewModel, BangTinhLuong>();
        CreateMap<BangTinhLuongRequest, BangTinhLuong>();
        
    }
}

