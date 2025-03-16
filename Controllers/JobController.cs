using Microsoft.AspNetCore.Mvc;
using JobRecruitmentWeb.Data;
using JobRecruitmentWeb.Models;
using Microsoft.AspNetCore.Http;

namespace JobRecruitmentWeb.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var jobs = _context.ViecLams.ToList();
            return View(jobs);
        }

        public IActionResult Details(int id)
        {
            var job = _context.ViecLams.FirstOrDefault(j => j.MaVL == id);
            if (job == null) return NotFound();

            return View(job);
        }

        public IActionResult Search(string keyword, string jobType, decimal? minSalary, decimal? maxSalary, string category, string location)
        {
            var jobs = _context.ViecLams
                .Where(j => (string.IsNullOrEmpty(keyword) || j.TenCongViec.Contains(keyword)) &&
                            (string.IsNullOrEmpty(jobType) || j.LoaiHinhLamViec == jobType) &&
                            (!minSalary.HasValue || j.MucLuong >= minSalary) &&
                            (!maxSalary.HasValue || j.MucLuong <= maxSalary) &&
                            (string.IsNullOrEmpty(category) || j.JobCategory == category) &&
                            (string.IsNullOrEmpty(location) || j.DiaChi.Contains(location)))
                .ToList();

            return View("Index", jobs);
        }

        [HttpPost]
        public IActionResult Apply(int jobId, string mssv, IFormFile cvFile)
        {
            if (string.IsNullOrEmpty(mssv))
            {
                ModelState.AddModelError("", "Student ID is required.");
                return RedirectToAction("Details", new { id = jobId });
            }

            if (cvFile != null && cvFile.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads/cvs", cvFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    cvFile.CopyTo(stream);
                }

                var application = new UngTuyen
                {
                    MaVL = jobId,
                    MSSV = mssv,
                    NgayDangKy = DateTime.Now,
                    TrangThai = "Pending",
                    CVPath = cvFile.FileName
                };

                _context.UngTuyens.Add(application);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", new { id = jobId });
        }

        [HttpGet]
        public IActionResult PostJob()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostJob(ViecLam job)
        {
            if (ModelState.IsValid)
            {
                _context.ViecLams.Add(job);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        public IActionResult ManageApplications(int jobId)
        {
            var applications = _context.UngTuyens.Where(u => u.MaVL == jobId).ToList();
            return View(applications);
        }

        [HttpPost]
        public IActionResult UpdateApplicationStatus(int applicationId, string status)
        {
            var application = _context.UngTuyens.FirstOrDefault(u => u.MaUT == applicationId);
            if (application == null) return NotFound();

            application.TrangThai = status;
            _context.UngTuyens.Update(application);
            _context.SaveChanges();

            return RedirectToAction("ManageApplications", new { jobId = application.MaVL });
        }
    }
}
