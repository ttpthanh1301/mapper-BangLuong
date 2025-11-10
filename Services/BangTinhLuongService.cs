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

        // ================================
        // 1️⃣ Lấy danh sách có phân trang, tìm kiếm, sắp xếp
        // ================================
        public async Task<PaginatedList<BangTinhLuongViewModel>> GetAllFilter(
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

            var query = from bl in _context.BangTinhLuong
                        select bl;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(bl =>
                    bl.MaNV.Contains(searchString) ||
                    bl.KyLuongThang.ToString().Contains(searchString) ||
                    bl.KyLuongNam.ToString().Contains(searchString) ||
                    bl.TrangThai.Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "month_desc" => query.OrderByDescending(bl => bl.KyLuongThang),
                "year_desc" => query.OrderByDescending(bl => bl.KyLuongNam),
                "salary_desc" => query.OrderByDescending(bl => bl.ThucLanh),
                _ => query.OrderBy(bl => bl.KyLuongNam).ThenBy(bl => bl.KyLuongThang)
            };

            var list = await query.Include(x => x.NhanVien).ToListAsync();
            var viewModels = _mapper.Map<IEnumerable<BangTinhLuongViewModel>>(list);

            return PaginatedList<BangTinhLuongViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        // ================================
        // 2️⃣ Lấy toàn bộ danh sách bảng lương
        // ================================
        public async Task<IEnumerable<BangTinhLuongViewModel>> GetAllAsync()
        {
            var list = await _context.BangTinhLuong
                .Include(x => x.NhanVien)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BangTinhLuongViewModel>>(list);
        }

        // ================================
        // 3️⃣ Lấy chi tiết theo ID
        // ================================
        public async Task<BangTinhLuongViewModel?> GetByIdAsync(int id)
        {
            var entity = await _context.BangTinhLuong
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.MaBL == id);

            return _mapper.Map<BangTinhLuongViewModel>(entity);
        }

        // ================================
        // 4️⃣ Thêm mới bảng lương
        // ================================
        public async Task<bool> CreateAsync(BangTinhLuongRequest request)
        {
            var entity = _mapper.Map<BangTinhLuong>(request);
            _context.BangTinhLuong.Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // ================================
        // 5️⃣ Cập nhật bảng lương
        // ================================
        public async Task<bool> UpdateAsync(int id, BangTinhLuongViewModel request)
        {
            var existing = await _context.BangTinhLuong.FirstOrDefaultAsync(x => x.MaBL == id);
            if (existing == null)
                return false;

            _mapper.Map(request, existing);
            _context.BangTinhLuong.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }

        // ================================
        // 6️⃣ Xóa bảng lương
        // ================================
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BangTinhLuong.FindAsync(id);
            if (entity == null)
                return false;

            _context.BangTinhLuong.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // ================================
        // 7️⃣ Tính lương theo kỳ (gọi stored procedure)
        // ================================
        public async Task<bool> TinhLuongTheoKyAsync(int kyLuongThang, int kyLuongNam)
        {
            try
            {
                // Gọi thủ tục SQL: sp_TinhLuongThang
                var sql = "EXEC dbo.sp_TinhLuongThang @KyLuongThang = {0}, @KyLuongNam = {1}";
                await _context.Database.ExecuteSqlRawAsync(sql, kyLuongThang, kyLuongNam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
