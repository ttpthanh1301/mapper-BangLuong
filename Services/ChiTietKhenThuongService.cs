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
