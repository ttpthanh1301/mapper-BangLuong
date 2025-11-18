using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.HopDongViewModels;

namespace BangLuong.Services
{
    public class HopDongService : IHopDongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public HopDongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<HopDongViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize,
            string? maNV = null) // giá trị mặc định null
        {
            if (searchString != null) pageNumber = 1;
            else searchString = currentFilter;

            var query = _context.HopDong.AsQueryable();

            // Chỉ lọc theo maNV nếu nhân viên bình thường
            if (!string.IsNullOrEmpty(maNV))
                query = query.Where(h => h.MaNV == maNV);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(hd =>
                    (hd.SoHopDong != null && hd.SoHopDong.Contains(searchString)) ||
                    hd.LoaiHD.Contains(searchString) ||
                    hd.MaNV.Contains(searchString) ||
                    hd.TrangThai.Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "date_desc" => query.OrderByDescending(hd => hd.NgayBatDau),
                "date" => query.OrderBy(hd => hd.NgayBatDau),
                "salary_desc" => query.OrderByDescending(hd => hd.LuongCoBan),
                _ => query.OrderBy(hd => hd.NgayBatDau)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<HopDongViewModel>>(list);

            return PaginatedList<HopDongViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }
        public async Task<IEnumerable<HopDongViewModel>> GetAll()
        {
            var list = await _context.HopDong.Include(h => h.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<HopDongViewModel>>(list);
        }

        public async Task<HopDongViewModel> GetById(int id)
        {
            var hopDong = await _context.HopDong
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(h => h.MaHD == id);
            return _mapper.Map<HopDongViewModel>(hopDong);
        }

        public async Task<int> Create(HopDongRequest request)
        {
            var hopDong = _mapper.Map<HopDong>(request);
            _context.HopDong.Add(hopDong);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(HopDongViewModel request)
        {
            if (!HopDongExists(request.MaHD))
            {
                throw new KeyNotFoundException("Hợp đồng không tồn tại");
            }

            _context.HopDong.Update(_mapper.Map<HopDong>(request));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDong.Remove(hopDong);
            }
            return await _context.SaveChangesAsync();
        }

        private bool HopDongExists(int id)
        {
            return _context.HopDong.Any(e => e.MaHD == id);
        }
    }
}
