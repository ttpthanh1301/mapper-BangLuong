using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BangLuong.Services;
using System.Threading.Tasks;

namespace BangLuong.Controllers
{
    [Authorize] // Cho tất cả user đã login
    public class HomeController : Controller
    {
        private readonly UserManager<NguoiDung> _userManager;
        private readonly IDashboardService _dashboardService;

        public HomeController(UserManager<NguoiDung> userManager, IDashboardService dashboardService)
        {
            _userManager = userManager;
            _dashboardService = dashboardService;
        }

        [Authorize] // mọi role
        public async Task<IActionResult> Welcome()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "NguoiDung");

            var roles = await _userManager.GetRolesAsync(user);

            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;

            // Admin/Manager redirect tới NguoiDung/Index
            if (roles.Contains("Admin") || roles.Contains("Manager"))
            {
                return RedirectToAction("Index", "Home");
            }

            // Employee xem Welcome
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Index()
        {
            var data = await _dashboardService.GetDashboardDataAsync();
            return View(data);
        }
    }
}
