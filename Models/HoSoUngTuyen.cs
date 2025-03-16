using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobRecruitmentWeb.Models
{
    public class HoSoUngTuyen
    {
        [Key]
        public int MaHS { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string MSSV { get; set; }
        public SinhVien SinhVien { get; set; }

        [Required]
        [StringLength(255)]
        public string TenFile { get; set; }

        public DateTime NgayTaiLen { get; set; }
    }
}
