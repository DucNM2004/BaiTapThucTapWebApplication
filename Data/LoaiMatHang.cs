using System.ComponentModel.DataAnnotations;

namespace BaiTapThucTapWebApplication.Data
{
    public class LoaiMatHang
    {
        [Key]
        public string Maloaimathang { get; set; }
        public string Tenloaimathang { get; set; }
    }
}
