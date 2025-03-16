using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobRecruitmentWeb.Models;

namespace JobRecruitmentWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<CongTy> CongTys { get; set; }
        public DbSet<ViecLam> ViecLams { get; set; }
        public DbSet<HoSoUngTuyen> HoSoUngTuyens { get; set; }
        public DbSet<UngTuyen> UngTuyens { get; set; }
        public DbSet<PhongVan> PhongVans { get; set; }
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
    }
}
