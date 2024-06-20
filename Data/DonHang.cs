using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTapThucTapWebApplication.Data
{
    public class DonHang
    {
        [Key]
        public string Madonhanggiaohang {  get; set; }
        [ForeignKey("Makhachhang")]
        public string Makhachhang {  get; set; }
        [ForeignKey("Mathanhviengh")]
        public string Mathanhviengh {  get; set; }
        [ForeignKey("Madichvu")]
        public string Madichvu {  get; set; }
        [ForeignKey("Makhuvucgiaohang")]
        public string Makhuvucgiaohang {  get; set; }
        public string Tennguoinhan {  get; set; }
        public string Diachinguoinhan { get; set; }
        public string Sodtnguoinhan { get; set; }
        public string Makhoangthoigiangiaohang { get; set; }
        public DateTime Ngaygiaohang { get; set; }
        public string Phuongthucthanhtoan {  get; set; }
        public string Trangthaipheduyet {  get; set; }
        public string Trangthaigiaohang { get; set; }
    }
}
