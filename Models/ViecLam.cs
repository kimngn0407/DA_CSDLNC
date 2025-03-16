using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobRecruitmentWeb.Models
{
    // Represents job postings targeted at IT students.
    public class ViecLam
    {
        [Key]
        public int MaVL { get; set; }

        [Required]
        public string TenCongViec { get; set; }

        public string MoTa { get; set; }
        public string YeuCau { get; set; }
        public decimal MucLuong { get; set; }
        public string LoaiHinhLamViec { get; set; }
        public string JobCategory { get; set; }
        public string DiaChi { get; set; }

        [ForeignKey("CongTy")]
        public int MaCT { get; set; }
        public CongTy CongTy { get; set; }

        public ICollection<UngTuyen> UngTuyens { get; set; }
        public ICollection<PhongVan> PhongVans { get; set; }
    }
}
