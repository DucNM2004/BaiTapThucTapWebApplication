using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTapThucTapWebApplication.Data
{
    public class KhachHang
    {
        [Key]
        public string Makhachhang { get; set; }
        public string? Tenkhachhang { get; set; }
        [ForeignKey("Makhuvuc")]
        public string? Makhuvuc { get; set; }
        public string? Sodtkhachhang { get; set; }
        public string? Diachiemail { get; set; }
        public string? Diachinhanhang { get; set; }
        public string? Tencuahang { get; set; }
        
        //public KhuVuc KHUVUC { get; set; }
    }
}
