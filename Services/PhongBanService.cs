using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.PhongBanViewModels;

namespace BangLuong.Services
{
    public class PhongBanService : IPhongBanService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public PhongBanService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedList<PhongBanViewModel>> GetAllFilter(
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

            var query = from pb in _context.PhongBan
                        select pb;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(pb =>
                    pb.MaPB.Contains(searchString) ||
                    pb.TenPB.Contains(searchString) ||
                    (pb.MoTa != null && pb.MoTa.Contains(searchString))
                );
            }

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(pb => pb.TenPB),
                "code" => query.OrderBy(pb => pb.MaPB),
                "code_desc" => query.OrderByDescending(pb => pb.MaPB),
                _ => query.OrderBy(pb => pb.TenPB)
            };

            var list = await query.ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<PhongBanViewModel>>(list);

            return PaginatedList<PhongBanViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        public async Task<int> Create(PhongBanRequest request)
        {
            var phongBan = _mapper.Map<PhongBan>(request);
            _context.PhongBan.Add(phongBan);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(string id)
        {
            var phongBan = await _context.PhongBan.FindAsync(id);
            if (phongBan != null)
            {
                _context.PhongBan.Remove(phongBan);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PhongBanViewModel>> GetAll()
        {
            var list = await _context.PhongBan.ToListAsync();
            return _mapper.Map<IEnumerable<PhongBanViewModel>>(list);
        }

        public async Task<PhongBanViewModel> GetById(string id)
        {
            var phongBan = await _context.PhongBan.FirstOrDefaultAsync(m => m.MaPB == id);
            return _mapper.Map<PhongBanViewModel>(phongBan);
        }

        public async Task<int> Update(PhongBanViewModel request)
        {
            if (!PhongBanExists(request.MaPB))
            {
                throw new KeyNotFoundException("Phòng ban không tồn tại");
            }

            _context.PhongBan.Update(_mapper.Map<PhongBan>(request));
            return await _context.SaveChangesAsync();
        }

        private bool PhongBanExists(string id)
        {
            return _context.PhongBan.Any(e => e.MaPB == id);
        }
    }
}
