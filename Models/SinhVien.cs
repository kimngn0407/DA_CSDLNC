using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JobRecruitmentWeb.Models
{
    // Represents an IT student applying for jobs.
    public class SinhVien
    {
        [Key]
        public string MSSV { get; set; }
        
        [Required]
        public string HoTen { get; set; }

        public string Nganh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string KyNang { get; set; }
        public string KinhNghiem { get; set; }
        public string? PortfolioLink { get; set; }
        public string? Certifications { get; set; }

        public ICollection<HoSoUngTuyen> HoSoUngTuyens { get; set; }
        public ICollection<UngTuyen> UngTuyens { get; set; }
        public ICollection<PhongVan> PhongVans { get; set; }
    }
}
