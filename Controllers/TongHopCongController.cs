using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BangLuong.Services;
using static BangLuong.ViewModels.TongHopCongViewModels;

namespace BangLuong.Controllers
{
    public class TongHopCongController : Controller
    {
        private readonly ITongHopCongService _tongHopCongService;
        private readonly INhanVienService _nhanVienService; // để lấy danh sách nhân viên (nếu đã có service này)

        public TongHopCongController(ITongHopCongService tongHopCongService, INhanVienService nhanVienService)
        {
            _tongHopCongService = tongHopCongService;
            _nhanVienService = nhanVienService;
        }
        [HttpPost]
        public async Task<IActionResult> RunTongHopCong(int kyLuongThang, int kyLuongNam)
        {
            try
            {
                await _tongHopCongService.RunTongHopCongThangAsync(kyLuongThang, kyLuongNam);
                TempData["Success"] = $"✅ Tổng hợp công tháng {kyLuongThang}/{kyLuongNam} hoàn tất!";
            }
            catch
            {
                TempData["Error"] = "❌ Lỗi khi chạy tổng hợp công.";
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            int pageSize = 10; // Số bản ghi mỗi trang

            var list = await _tongHopCongService.GetAllFilter(sortOrder, currentFilter, searchString, pageNumber, pageSize);

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;

            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _tongHopCongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TongHopCongRequest request)
        {
            if (ModelState.IsValid)
            {
                await _tongHopCongService.CreateAsync(request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _tongHopCongService.GetByIdAsync(id);
            if (item == null) return NotFound();

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", item.MaNV);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TongHopCongViewModel request)
        {
            if (ModelState.IsValid)
            {
                await _tongHopCongService.UpdateAsync(id, request);
                return RedirectToAction(nameof(Index));
            }

            var nhanVienList = await _nhanVienService.GetAll();
            ViewData["MaNV"] = new SelectList(nhanVienList, "MaNV", "MaNV", request.MaNV);
            return View(request);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _tongHopCongService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tongHopCongService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
