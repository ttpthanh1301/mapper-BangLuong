using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BangLuong.Data;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.ChiTietKhenThuongViewModels;

namespace BangLuong.Services
{
    public class ChiTietKhenThuongService : IChiTietKhenThuongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietKhenThuongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ChiTietKhenThuongViewModel>> GetAllFilter(
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

            var query = from ct in _context.ChiTietKhenThuong
                        select ct;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(ct =>
                    ct.MaCTKT.ToString().Contains(searchString) ||
                    ct.MaNV.Contains(searchString) ||
                    ct.MaKT.Contains(searchString) ||
                    (ct.LyDo != null && ct.LyDo.Contains(searchString))
                );
            }

            query = sortOrder switch
            {
                "date_desc" => query.OrderByDescending(ct => ct.NgayKhenThuong),
                "date" => query.OrderBy(ct => ct.NgayKhenThuong),
                _ => query.OrderBy(ct => ct.NgayKhenThuong)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<ChiTietKhenThuongViewModel>>(list);

            return PaginatedList<ChiTietKhenThuongViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        public async Task<IEnumerable<ChiTietKhenThuongViewModel>> GetAllAsync()
        {
            var list = await _context.ChiTietKhenThuong
                .Include(x => x.DanhMucKhenThuong)
                .Include(x => x.NhanVien)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChiTietKhenThuongViewModel>>(list);
        }

        public async Task<ChiTietKhenThuongViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.ChiTietKhenThuong
                .Include(x => x.DanhMucKhenThuong)
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaCTKT == id);

            return _mapper.Map<ChiTietKhenThuongViewModel?>(entity);
        }

        public async Task<bool> CreateAsync(ChiTietKhenThuongRequest request)
        {
            var entity = _mapper.Map<ChiTietKhenThuong>(request);
            _context.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, ChiTietKhenThuongViewModel request)
        {
            if (id != request.MaCTKT) return false;
            var entity = _mapper.Map<ChiTietKhenThuong>(request);
            _context.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ChiTietKhenThuong.FindAsync(id);
            if (entity == null) return false;

            _context.ChiTietKhenThuong.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
