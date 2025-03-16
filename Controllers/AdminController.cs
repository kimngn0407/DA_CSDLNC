using Microsoft.AspNetCore.Mvc;
using JobRecruitmentWeb.Data;
using Microsoft.AspNetCore.Identity;

namespace JobRecruitmentWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            var stats = new
            {
                TotalStudents = _context.SinhViens.Count(),
                TotalJobs = _context.ViecLams.Count(),
                TotalApplications = _context.UngTuyens.Count()
            };

            return View(stats);
        }

        public IActionResult ManageRoles()
        {
            // Logic for managing roles
            return View();
        }

        public IActionResult ManageUsers()
        {
            var users = _context.TaiKhoans.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            await _userManager.AddToRoleAsync(user, role);
            return RedirectToAction("ManageUsers");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Role name cannot be empty.");
                return RedirectToAction("ManageRoles");
            }

            var roleExists = _context.Roles.Any(r => r.Name == roleName);
            if (roleExists)
            {
                ModelState.AddModelError("", "Role already exists.");
                return RedirectToAction("ManageRoles");
            }

            await _context.Roles.AddAsync(new IdentityRole { Name = roleName });
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageRoles");
        }

        public IActionResult GetRoles()
        {
            var roles = _context.Roles.ToList();
            return Json(roles);
        }

        [HttpPost]
        public IActionResult UpdateUserStatus(int userId, string status)
        {
            var user = _context.TaiKhoans.FirstOrDefault(u => u.MaTK == userId);
            if (user == null) return NotFound();

            user.TrangThai = status;
            _context.TaiKhoans.Update(user);
            _context.SaveChanges();

            return RedirectToAction("ManageUsers");
        }
    }
}
