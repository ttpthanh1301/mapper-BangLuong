using BangLuong.Services;
using BangLuong.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace BangLuong.Controllers
{
    [Authorize] // Mặc định: tất cả user đã login
    public class NguoiDungController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly UserManager<NguoiDung> _userManager;

        public NguoiDungController(
            INguoiDungService nguoiDungService,
            SignInManager<NguoiDung> signInManager,
            UserManager<NguoiDung> userManager)
        {
            _nguoiDungService = nguoiDungService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // ======================= INDEX - CHỈ ADMIN/MANAGER =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _nguoiDungService.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi tải danh sách: {ex.Message}";
                return View(new List<NguoiDungViewModels.NguoiDungViewModel>());
            }
        }

        // ======================= REGISTER =======================
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Register(NguoiDungViewModels.NguoiDungRequest request)
        {
            if (string.IsNullOrEmpty(request.PhanQuyen))
                ModelState.Remove("PhanQuyen");

            if (!ModelState.IsValid)
                return View(request);

            try
            {
                var result = await _nguoiDungService.Register(request);
                if (!result)
                {
                    ModelState.AddModelError("", "Đăng ký thất bại");
                    return View(request);
                }

                TempData["SuccessMessage"] = "Đăng ký người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= LOGIN =======================
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            var login = new NguoiDungViewModels.LoginRequest
            {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(NguoiDungViewModels.LoginRequest login)
        {
            if (!ModelState.IsValid)
                return View(login);

            try
            {
                var appUser = await _userManager.FindByNameAsync(login.MaNV);

                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();

                    SignInResult result = await _signInManager.PasswordSignInAsync(
                        appUser,
                        login.Password,
                        login.RememberMe,
                        false
                    );

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Đăng nhập thành công!";
                        // Redirect về Welcome page chung cho mọi user
                        return RedirectToAction("Welcome", "Home");
                    }
                }

                ModelState.AddModelError(nameof(login.MaNV), "Đăng nhập thất bại: Mã NV hoặc mật khẩu không đúng");
                return View(login);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(login);
            }
        }

        // ======================= LOGOUT =======================
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "Đã đăng xuất thành công!";
            return RedirectToAction(nameof(Login));
        }

        // ======================= ACCESS DENIED =======================
        public IActionResult AccessDenied()
        {
            return View();
        }

        // ======================= EDIT =======================
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Mã nhân viên không được để trống");

            try
            {
                var user = await _nguoiDungService.GetById(id);
                if (user == null)
                    return NotFound("Không tìm thấy người dùng");

                var editModel = new NguoiDungViewModels.NguoiDungRequest
                {
                    MaNV = user.MaNV,
                    Email = user.Email,
                    PhanQuyen = user.PhanQuyen,
                    TrangThai = user.TrangThai
                };
                return View(editModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(NguoiDungViewModels.NguoiDungRequest request)
        {
            if (string.IsNullOrEmpty(request.Password))
                ModelState.Remove("Password");

            if (!ModelState.IsValid)
                return View(request);

            try
            {
                var result = await _nguoiDungService.Update(request);
                if (!result)
                {
                    ModelState.AddModelError("", "Cập nhật thất bại");
                    return View(request);
                }

                TempData["SuccessMessage"] = "Cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

        // ======================= DELETE =======================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Mã nhân viên không được để trống");

            try
            {
                var user = await _nguoiDungService.GetById(id);
                if (user == null)
                    return NotFound("Không tìm thấy người dùng");

                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var result = await _nguoiDungService.Delete(id);
                if (!result)
                {
                    TempData["ErrorMessage"] = "Xóa thất bại";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Xóa người dùng thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
