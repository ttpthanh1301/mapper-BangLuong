using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.ChamCongViewModels;

namespace BangLuong.Services
{
    public class ChamCongService : IChamCongService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public ChamCongService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ SỬA LẠI LOGIC PHÂN TRANG ĐÚNG
        public async Task<PaginatedList<ChamCongViewModel>> GetAllFilter(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,
            int pageSize)
        {
            // ✅ BỎ LOGIC NÀY ĐI - Controller đã xử lý rồi
            // Chỉ cần dùng searchString được truyền vào
            
            var query = _context.ChamCong
                .Include(cc => cc.NhanVien) // ✅ Include để hiển thị thông tin nhân viên
                .AsQueryable();

            // ✅ Filter theo searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(cc =>
                    cc.MaNV.Contains(searchString) ||
                    cc.MaCC.ToString().Contains(searchString)
                );
            }

            // ✅ Sorting
            query = sortOrder switch
            {
                "date_desc" => query.OrderByDescending(cc => cc.NgayChamCong),
                "date" => query.OrderBy(cc => cc.NgayChamCong),
                "overtime_desc" => query.OrderByDescending(cc => cc.SoGioTangCa),
                "overtime" => query.OrderBy(cc => cc.SoGioTangCa),
                _ => query.OrderByDescending(cc => cc.NgayChamCong) // Mặc định: mới nhất lên đầu
            };

            // ✅ QUAN TRỌNG: Dùng CreateAsync thay vì Create
            // Để tận dụng IQueryable và phân trang hiệu quả
            return await PaginatedList<ChamCongViewModel>.CreateAsync(
                query.Select(cc => new ChamCongViewModel
                {
                    MaCC = cc.MaCC,
                    MaNV = cc.MaNV,
                    NgayChamCong = cc.NgayChamCong,
                    GioVao = cc.GioVao,
                    GioRa = cc.GioRa,
                    SoGioTangCa = cc.SoGioTangCa
                }),
                pageNumber ?? 1,
                pageSize
            );
        }

        public async Task<IEnumerable<ChamCongViewModel>> GetAll()
        {
            var list = await _context.ChamCong.Include(c => c.NhanVien).ToListAsync();
            return _mapper.Map<IEnumerable<ChamCongViewModel>>(list);
        }

        public async Task<ChamCongViewModel> GetById(int id)
        {
            var chamCong = await _context.ChamCong
                .Include(c => c.NhanVien)
                .FirstOrDefaultAsync(c => c.MaCC == id);
            return _mapper.Map<ChamCongViewModel>(chamCong);
        }

        public async Task<int> Create(ChamCongRequest request)
        {
            var chamCong = _mapper.Map<ChamCong>(request);
            _context.ChamCong.Add(chamCong);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ChamCongViewModel request)
        {
            if (!ChamCongExists(request.MaCC))
            {
                throw new KeyNotFoundException("Bản ghi chấm công không tồn tại");
            }

            _context.ChamCong.Update(_mapper.Map<ChamCong>(request));
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var chamCong = await _context.ChamCong.FindAsync(id);
            if (chamCong != null)
            {
                _context.ChamCong.Remove(chamCong);
            }
            return await _context.SaveChangesAsync();
        }

        private bool ChamCongExists(int id)
        {
            return _context.ChamCong.Any(e => e.MaCC == id);
        }
    }
}