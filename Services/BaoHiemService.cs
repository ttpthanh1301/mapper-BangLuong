using AutoMapper;
using BangLuong.Data;
using BangLuong.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BangLuong.ViewModels.BaoHiemViewModels;

namespace BangLuong.Services
{
    public class BaoHiemService : IBaoHiemService
    {
        private readonly BangLuongDbContext _context;
        private readonly IMapper _mapper;

        public BaoHiemService(BangLuongDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BaoHiemViewModel>> GetAllFilter(
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

            // Kết hợp với NhanVien để tìm kiếm và hiển thị tên nhân viên
            var query = from bh in _context.BaoHiem
                        join nv in _context.NhanVien on bh.MaNV equals nv.MaNV
                        select new { bh, nv };

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x =>
                    (x.bh.SoSoBHXH != null && x.bh.SoSoBHXH.Contains(searchString)) ||
                    (x.bh.MaTheBHYT != null && x.bh.MaTheBHYT.Contains(searchString)) ||
                    (x.bh.NoiDKKCB != null && x.bh.NoiDKKCB.Contains(searchString)) ||
                    x.nv.HoTen.Contains(searchString) ||
                    x.bh.MaNV.Contains(searchString)
                );
            }

            query = sortOrder switch
            {
                "ssbh_desc" => query.OrderByDescending(x => x.bh.SoSoBHXH),
                "manv" => query.OrderBy(x => x.bh.MaNV),
                "manv_desc" => query.OrderByDescending(x => x.bh.MaNV),
                _ => query.OrderBy(x => x.bh.MaBH) // Mặc định sắp xếp theo Mã BH
            };

            var list = await query.ToListAsync();
            
            // Ánh xạ kết quả sang ViewModel và thêm TenNhanVien
            var viewModels = list.Select(x => {
                var vm = _mapper.Map<BaoHiemViewModel>(x.bh);
                // ĐÃ SỬA: Sử dụng x.nv?.HoTen để loại bỏ cảnh báo CS8602
                vm.TenNhanVien = x.nv?.HoTen;
                return vm;
            });
            
            return PaginatedList<BaoHiemViewModel>.Create(viewModels, pageNumber ?? 1, pageSize);
        }

        public async Task<int> Create(BaoHiemRequest request)
        {
            // Kiểm tra ràng buộc duy nhất trên MaNV
            if (BaoHiemExistsByMaNV(request.MaNV))
            {
                throw new InvalidOperationException($"Nhân viên có mã '{request.MaNV}' đã có thông tin bảo hiểm. Mỗi nhân viên chỉ có một hồ sơ bảo hiểm.");
            }

            var baoHiem = _mapper.Map<BaoHiem>(request);
            _context.BaoHiem.Add(baoHiem);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var baoHiem = await _context.BaoHiem.FindAsync(id);
            if (baoHiem != null)
            {
                _context.BaoHiem.Remove(baoHiem);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BaoHiemViewModel>> GetAll()
        {
            var list = await _context.BaoHiem
                                    .Include(b => b.NhanVien)
                                    .ToListAsync();
                                    
            // Map và thêm tên nhân viên
            return list.Select(b => {
                var vm = _mapper.Map<BaoHiemViewModel>(b);
                vm.TenNhanVien = b.NhanVien?.HoTen;
                return vm;
            });
        }

        // ĐÃ SỬA: Chữ ký phương thức trả về BaoHiemViewModel?
        public async Task<BaoHiemViewModel?> GetById(int id) 
        {
            var baoHiem = await _context.BaoHiem
                                        .Include(b => b.NhanVien)
                                        .FirstOrDefaultAsync(m => m.MaBH == id);
            
            if (baoHiem == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<BaoHiemViewModel>(baoHiem);
            
            if (viewModel != null)
            {
                 // Dùng toán tử `.` an toàn vì đã kiểm tra baoHiem != null
                 viewModel.TenNhanVien = baoHiem.NhanVien?.HoTen;
            }
            return viewModel; 
        }

        public async Task<int> Update(BaoHiemViewModel request)
        {
            if (!BaoHiemExists(request.MaBH))
            {
                throw new KeyNotFoundException("Thông tin bảo hiểm không tồn tại");
            }

            var baoHiem = _mapper.Map<BaoHiem>(request);
            _context.BaoHiem.Update(baoHiem);
            return await _context.SaveChangesAsync();
        }

        private bool BaoHiemExists(int id)
        {
            return _context.BaoHiem.Any(e => e.MaBH == id);
        }

        private bool BaoHiemExistsByMaNV(string maNV)
        {
            return _context.BaoHiem.Any(e => e.MaNV == maNV);
        }
    }
}