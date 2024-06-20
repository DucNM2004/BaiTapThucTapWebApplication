using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BaiTapThucTapWebApplication.Data
{
    public class KhuVuc
    {
        [Key]
        public string Makhuvuc {  get; set; }
        public string Tenkhuvuc { get; set; }

        
    }
}
