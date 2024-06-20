using System.ComponentModel.DataAnnotations;

namespace BaiTapThucTapWebApplication.Data
{
    public class DichVu
    {
        [Key]
         public string Madichvu { get; set; }
         public string Tendichvu { get; set; }
        
    }
}
