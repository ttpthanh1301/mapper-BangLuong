using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static BangLuong.ViewModels.BangTinhLuongViewModels;

namespace BangLuong.Services
{
    public class BangTinhLuongService : IBangTinhLuongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public BangTinhLuongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy tất cả bảng lương
        public async Task<IEnumerable<BangTinhLuongViewModel>> GetAllAsync()
        {
            var list = await _context.BangTinhLuong
                .Include(x => x.NhanVien)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BangTinhLuongViewModel>>(list);
        }

        // Lấy chi tiết theo id
        public async Task<BangTinhLuongViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.BangTinhLuong
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaBL == id);

            return _mapper.Map<BangTinhLuongViewModel>(entity);
        }

        // Thêm mới
        public async Task<bool> CreateAsync(BangTinhLuongRequest request)
        {
            var entity = _mapper.Map<BangTinhLuong>(request);
            _context.BangTinhLuong.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // Cập nhật
        public async Task<bool> UpdateAsync(int id, BangTinhLuongViewModel request)
        {
            var existing = await _context.BangTinhLuong.FirstOrDefaultAsync(x => x.MaBL == id);
            if (existing == null)
                return false;

            _mapper.Map(request, existing);
            _context.BangTinhLuong.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        // Xóa
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BangTinhLuong.FindAsync(id);
            if (entity == null)
                return false;

            _context.BangTinhLuong.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
