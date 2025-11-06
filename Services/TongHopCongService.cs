using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Services
{
    public class TongHopCongService : ITongHopCongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public TongHopCongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TongHopCongViewModel>> GetAllFilter(
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

            var query = from th in _context.TongHopCong
                        select th;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(th =>
                    th.MaNV.Contains(searchString) ||
                    th.KyLuongThang.ToString().Contains(searchString) ||
                    th.KyLuongNam.ToString().Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "month_desc" => query.OrderByDescending(th => th.KyLuongThang),
                "year_desc" => query.OrderByDescending(th => th.KyLuongNam),
                _ => query.OrderBy(th => th.KyLuongNam).ThenBy(th => th.KyLuongThang)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<TongHopCongViewModel>>(list);

            return PaginatedList<TongHopCongViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        public async Task<IEnumerable<TongHopCongViewModel>> GetAllAsync()
        {
            var list = await _context.TongHopCong.Include(x => x.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<TongHopCongViewModel>>(list);
        }

        public async Task<TongHopCongViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.TongHopCong
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaTHC == id);

            return _mapper.Map<TongHopCongViewModel>(entity);
        }

        public async Task<bool> CreateAsync(TongHopCongRequest request)
        {
            var entity = _mapper.Map<TongHopCong>(request);
            _context.TongHopCong.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, TongHopCongViewModel request)
        {
            var existing = await _context.TongHopCong.FirstOrDefaultAsync(x => x.MaTHC == id);
            if (existing == null)
                return false;

            _mapper.Map(request, existing);
            _context.TongHopCong.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TongHopCong.FindAsync(id);
            if (entity == null)
                return false;

            _context.TongHopCong.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
