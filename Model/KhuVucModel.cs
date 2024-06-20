using System.ComponentModel.DataAnnotations;

namespace BaiTapThucTapWebApplication.Model
{
    public class KhuVucModel
    {
        [Key]
        public string Makhuvuc { get; set; }
        [Required]
        public string Tenkhuvuc {  get; set; }
    }
}
