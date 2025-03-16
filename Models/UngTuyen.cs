using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobRecruitmentWeb.Models
{
    public class UngTuyen
    {
        [Key]
        public int MaUT { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string MSSV { get; set; }
        public SinhVien SinhVien { get; set; }

        [Required]
        [ForeignKey("ViecLam")]
        public int MaVL { get; set; }
        public ViecLam ViecLam { get; set; }

        public DateTime NgayDangKy { get; set; }
        public string? TrangThai { get; set; }
        public string? KetQuaTuyenDung { get; set; }
        public string? CVPath { get; set; } // Path to the uploaded CV
    }
}
