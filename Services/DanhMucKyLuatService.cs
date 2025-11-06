using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.DanhMucKyLuatViewModels;

namespace BangLuong.Services
{
    public class DanhMucKyLuatService : IDanhMucKyLuatService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public DanhMucKyLuatService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<DanhMucKyLuatViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize)
        {
            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            var query = from kl in _context.DanhMucKyLuat
                        select kl;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(kl =>
                    kl.MaKL.Contains(searchString) ||
                    kl.TenKyLuat.Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(kl => kl.TenKyLuat),
                "code" => query.OrderBy(kl => kl.MaKL),
                "code_desc" => query.OrderByDescending(kl => kl.MaKL),
                _ => query.OrderBy(kl => kl.TenKyLuat)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<DanhMucKyLuatViewModel>>(list);

            return PaginatedList<DanhMucKyLuatViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        // Lấy tất cả danh mục
        public async Task<IEnumerable<DanhMucKyLuatViewModel>> GetAllAsync()
        {
            var list = await _context.DanhMucKyLuat.ToListAsync();
            return _mapper.Map<IEnumerable<DanhMucKyLuatViewModel>>(list);
        }

        // Lấy chi tiết 1 danh mục
        public async Task<DanhMucKyLuatViewModel?> GetByIdAsync(string id)
        {
            var entity = await _context.DanhMucKyLuat.FirstOrDefaultAsync(x => x.MaKL == id);
            return _mapper.Map<DanhMucKyLuatViewModel>(entity);
        }

        // Tạo mới
        public async Task<bool> CreateAsync(DanhMucKyLuatRequest request)
        {
            var entity = _mapper.Map<DanhMucKyLuat>(request);
            _context.DanhMucKyLuat.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // Cập nhật
        public async Task<bool> UpdateAsync(DanhMucKyLuatViewModel request)
        {
            var existing = await _context.DanhMucKyLuat.FirstOrDefaultAsync(x => x.MaKL == request.MaKL);
            if (existing == null)
                return false;

            _mapper.Map(request, existing);
            _context.DanhMucKyLuat.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        // Xóa
        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.DanhMucKyLuat.FindAsync(id);
            if (entity == null)
                return false;

            _context.DanhMucKyLuat.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
