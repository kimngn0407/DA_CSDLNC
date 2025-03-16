using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobRecruitmentWeb.Models
{
    public class PhongVan
    {
        [Key]
        public int MaPV { get; set; }

        [Required]
        [ForeignKey("ViecLam")]
        public int MaVL { get; set; }
        public ViecLam ViecLam { get; set; }

        [Required]
        [ForeignKey("SinhVien")]
        public string MSSV { get; set; }
        public SinhVien SinhVien { get; set; }

        public DateTime NgayPhongVan { get; set; }
        public string? GiamKhao { get; set; }
        public string? DanhGia { get; set; }
    }
}
