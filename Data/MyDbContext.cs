using Microsoft.EntityFrameworkCore;

namespace BaiTapThucTapWebApplication.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<LoaiMatHang> LOAIMATHANG { get; set; }
        public DbSet<KhachHang> KHACHHANG { get; set; }
        public DbSet<DichVu> DICHVU { get; set; }
        public DbSet<KhuVuc> KHUVUC { get; set; }
        public DbSet<ThanhVienGiaoHang> THANHVIENGIAOHANG {  get; set; }
        public DbSet<DonHang> DONHANG_GIAOHANG { get; set; }
    }
}
