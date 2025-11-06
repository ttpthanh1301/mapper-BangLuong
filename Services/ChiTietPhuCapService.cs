using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChiTietPhuCapViewModels;

namespace BangLuong.Services
{
    public class ChiTietPhuCapService : IChiTietPhuCapService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChiTietPhuCapService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChiTietPhuCapViewModel>> GetAll()
        {
            var list = await _context.ChiTietPhuCap
                .Include(c => c.DanhMucPhuCap)
                .Include(c => c.NhanVien)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ChiTietPhuCapViewModel>>(list);
        }

        public async Task<ChiTietPhuCapViewModel> GetById(int id)
        {
            var chiTietPhuCap = await _context.ChiTietPhuCap
                .Include(c => c.DanhMucPhuCap)
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(c => c.MaCTPC == id);

            return _mapper.Map<ChiTietPhuCapViewModel>(chiTietPhuCap);
        }

        public async Task<int> Create(ChiTietPhuCapRequest request)
        {
            var chiTietPhuCap = _mapper.Map<ChiTietPhuCap>(request);
            _context.ChiTietPhuCap.Add(chiTietPhuCap);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ChiTietPhuCapViewModel request)
        {
            if (!ChiTietPhuCapExists(request.MaCTPC))
            {
                throw new KeyNotFoundException("Chi tiết phụ cấp không tồn tại");
            }

            _context.ChiTietPhuCap.Update(_mapper.Map<ChiTietPhuCap>(request));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var chiTietPhuCap = await _context.ChiTietPhuCap.FindAsync(id);
            if (chiTietPhuCap != null)
            {
                _context.ChiTietPhuCap.Remove(chiTietPhuCap);
            }
            return await _context.SaveChangesAsync();
        }

        private bool ChiTietPhuCapExists(int id)
        {
            return _context.ChiTietPhuCap.Any(e => e.MaCTPC == id);
        }
    }
}
