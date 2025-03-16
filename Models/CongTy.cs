using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JobRecruitmentWeb.Models
{
    // Represents a company offering job opportunities for IT students.
    public class CongTy
    {
        [Key]
        public int MaCT { get; set; }

        [Required]
        public string TenCongTy { get; set; }

        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string LinhVucHoatDong { get; set; }

        public ICollection<ViecLam> ViecLams { get; set; }
    }
}
