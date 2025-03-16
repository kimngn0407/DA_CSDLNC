using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobRecruitmentWeb.Models
{
    public class TaiKhoan
    {
        [Key]
        public int MaTK { get; set; }

        [Required]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }

        public string LoaiTaiKhoan { get; set; } // "Student" hoặc "HRManager"

        [ForeignKey("SinhVien")]
        public string? MSSV { get; set; }
        public SinhVien? SinhVien { get; set; }

        [ForeignKey("CongTy")]
        public int? MaCT { get; set; }
        public CongTy? CongTy { get; set; }

        public string TrangThai { get; set; }
        public DateTime NgayDangKy { get; set; }

        public string? CompanyName { get; set; } // For recruiters
        public string? TaxId { get; set; } // For recruiters
        public string? FullName { get; set; } // For students
    }
}
