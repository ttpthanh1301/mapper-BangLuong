using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using BangLuong.ViewModels;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.NhanVienViewModels;

namespace BangLuong.Services;

public class NhanVienService : INhanVienService
{
    private readonly BangLuongDbContext _context;
    private readonly IMapper _mapper;
    public NhanVienService(BangLuongDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
public async Task<PaginatedList<NhanVienViewModel>> GetAllFilter(
    string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber,
    int pageSize)
{
    if (searchString != null)
    {
        pageNumber = 1;
    }
    else
    {
        searchString = currentFilter;
    }

    var nhanVienQuery = from nv in _context.NhanVien
                        select nv;

    if (!string.IsNullOrEmpty(searchString))
    {
        nhanVienQuery = nhanVienQuery.Where(s =>
            s.HoTen.Contains(searchString) ||
            s.MaNV.Contains(searchString) ||
            (s.CCCD != null && s.CCCD.Contains(searchString))
        );
    }

    nhanVienQuery = sortOrder switch
    {
        "name_desc" => nhanVienQuery.OrderByDescending(s => s.HoTen),
        "gender" => nhanVienQuery.OrderBy(s => s.GioiTinh),
        "gender_desc" => nhanVienQuery.OrderByDescending(s => s.GioiTinh),
        "date" => nhanVienQuery.OrderBy(s => s.NgayVaoLam),
        "date_desc" => nhanVienQuery.OrderByDescending(s => s.NgayVaoLam),
        _ => nhanVienQuery.OrderBy(s => s.HoTen),
    };

    var nhanVienList = await nhanVienQuery.ToListAsync();
    var nhanVienViewModels = _mapper.Map<IEnumerable<NhanVienViewModel>>(nhanVienList);

    // ✅ Sửa lại dòng này
    return PaginatedList<NhanVienViewModel>.Create(nhanVienViewModels, pageNumber ?? 1, pageSize);
}


    public async Task<int> Create(NhanVienViewModels.NhanVienRequest request)
    {
        var nhanVien = _mapper.Map<NhanVien>(request);
        _context.NhanVien.Add(nhanVien);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(string id)
    {
        var nhanVien  = await _context.NhanVien.FindAsync(id);
       if (nhanVien == null)
            {
                throw new Exception("Nhan vien does not exist");
            }
            _context.NhanVien.Remove(nhanVien);
       return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NhanVienViewModels.NhanVienViewModel>> GetAll()
    {
        var nhanViens = await _context.NhanVien.ToListAsync();
        return _mapper.Map<IEnumerable<NhanVienViewModels.NhanVienViewModel>>(nhanViens);
    }

    public async Task<NhanVienViewModels.NhanVienViewModel> GetById(string id)
    {
        var nhanVien  = await _context.NhanVien
                .FirstOrDefaultAsync(m => m.MaNV == id);
        if (nhanVien == null)
            {
                throw new Exception("Course does not exist");
            }
            return _mapper.Map<NhanVienViewModel>(nhanVien);
    }

    public async Task<int> Update(NhanVienViewModels.NhanVienViewModel request)
    {
      if (!NhanVienExists(request.MaNV))
            {
                throw new Exception("Nhanvien does not exist");
            }
            _context.NhanVien.Update(_mapper.Map<NhanVien>(request));
            return await _context.SaveChangesAsync();
    }

    private bool NhanVienExists(string id)
    {
        return _context.NhanVien.Any(e => e.MaNV == id);
    }
}

