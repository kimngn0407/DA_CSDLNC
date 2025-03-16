using Microsoft.AspNetCore.Mvc;
using JobRecruitmentWeb.Data;
using JobRecruitmentWeb.Models;

namespace JobRecruitmentWeb.Controllers
{
    public class InterviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InterviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Schedule(int jobId, string mssv)
        {
            var interview = new PhongVan
            {
                MaVL = jobId,
                MSSV = mssv,
                NgayPhongVan = DateTime.Now.AddDays(3),
                GiamKhao = "HR Team"
            };

            _context.PhongVans.Add(interview);
            _context.SaveChanges();
            return RedirectToAction("Details", "Job", new { id = jobId });
        }

        public IActionResult Results(int id)
        {
            var interview = _context.PhongVans.FirstOrDefault(p => p.MaPV == id);
            if (interview == null) return NotFound();

            return View(interview);
        }
    }
}
