using Microsoft.AspNetCore.Mvc;
using JobRecruitmentWeb.Data;
using JobRecruitmentWeb.Models;

namespace JobRecruitmentWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Profile(string mssv)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MSSV == mssv);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public IActionResult UpdateProfile(SinhVien student)
        {
            var existingStudent = _context.SinhViens.FirstOrDefault(s => s.MSSV == student.MSSV);
            if (existingStudent == null) return NotFound();

            existingStudent.HoTen = student.HoTen;
            existingStudent.Nganh = student.Nganh;
            existingStudent.Email = student.Email;
            existingStudent.SoDienThoai = student.SoDienThoai;
            existingStudent.KyNang = student.KyNang;
            existingStudent.KinhNghiem = student.KinhNghiem;
            existingStudent.PortfolioLink = student.PortfolioLink;
            existingStudent.Certifications = student.Certifications;

            _context.SinhViens.Update(existingStudent);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { mssv = student.MSSV });
        }

        public IActionResult UploadCV(string mssv)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MSSV == mssv);
            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public IActionResult UploadCV(string mssv, string fileName)
        {
            var cv = new HoSoUngTuyen
            {
                MSSV = mssv,
                TenFile = fileName,
                NgayTaiLen = DateTime.Now
            };

            _context.HoSoUngTuyens.Add(cv);
            _context.SaveChanges();
            return RedirectToAction("Profile", new { mssv });
        }

        [HttpPost]
        public IActionResult UploadCV(string mssv, IFormFile cvFile)
        {
            if (cvFile != null && cvFile.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads/cvs", cvFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    cvFile.CopyTo(stream);
                }

                var cv = new HoSoUngTuyen
                {
                    MSSV = mssv,
                    TenFile = cvFile.FileName,
                    NgayTaiLen = DateTime.Now
                };

                _context.HoSoUngTuyens.Add(cv);
                _context.SaveChanges();
            }

            return RedirectToAction("Profile", new { mssv });
        }

        public IActionResult Dashboard(string mssv)
        {
            var student = _context.SinhViens.FirstOrDefault(s => s.MSSV == mssv);
            if (student == null) return NotFound();

            var applications = _context.UngTuyens.Where(u => u.MSSV == mssv).ToList();
            ViewData["Applications"] = applications;

            return View(student);
        }

        public IActionResult SearchJobs()
        {
            var jobs = _context.ViecLams.ToList();
            return View(jobs);
        }

        [HttpPost]
        public IActionResult ApplyForJob(int jobId, string mssv, IFormFile cvFile)
        {
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

            return RedirectToAction("Dashboard", new { mssv });
        }
    }
}
