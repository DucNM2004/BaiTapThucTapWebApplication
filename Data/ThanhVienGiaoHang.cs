using System.ComponentModel.DataAnnotations;

namespace BaiTapThucTapWebApplication.Data
{
    public class ThanhVienGiaoHang
    {
        [Key]
        public string Mathanhviengh { get; set; }
        public string Tenthanhviengiaohang { get; set; }
        public DateTime Ngaysinh { get; set; }
        public string Gioitinh { get; set; }
        public string Sodtthanhvien { get; set; }
        public string Diachithanhvien { get; set; }
    }
}
