using BaiTapThucTapWebApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly MyDbContext _context;

        public KhachHangController(MyDbContext context) { 
            _context = context;
        }

        [HttpGet]
        //public IActionResult GetAllKhachHang()
        //{
        //    var dsKhachhang = _context.KHACHHANG.ToList();
        //    return Ok(dsKhachhang);
        //}
        public async Task<ActionResult<IEnumerable<KhachHang>>> GetAllKhachHang()
        {
            return await _context.KHACHHANG.ToListAsync();
        }
        [HttpGet("{id}")]
        public IActionResult GetKhachHangbyID(string id) 
        {
            var kh = _context.KHACHHANG.SingleOrDefault(kh => kh.Makhachhang == id);
            if(kh != null)
            {
                return Ok(kh);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<KhachHang>> PostKhachHang(KhachHang khachhang)
        {
            _context.KHACHHANG.Add(khachhang);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAllKhachHang", new
            {
                Makhachhang = khachhang.Makhachhang,
                Makhuvuc = khachhang.Makhuvuc,
                Tenkhachhang = khachhang.Tenkhachhang,
                Tencuahang = khachhang.Tencuahang,
                Sodtkhachhang = khachhang.Sodtkhachhang,
                Diachiemail = khachhang.Diachiemail,
                Diachinhanhang = khachhang.Diachinhanhang
            }, khachhang);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKhachhang(string id, KhachHang khachhang)
        {
            if(id != khachhang.Makhachhang)
            {
                return NotFound();
            }
            _context.Entry(khachhang).State = EntityState.Modified;
            var check = _context.KHACHHANG.SingleOrDefault(kh => kh.Makhachhang == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(check == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DelKhachhang(string id)
        {
            var khachhangdel = await _context.KHACHHANG.FindAsync(id);
            if(khachhangdel == null)
            {
                return NotFound();
            }

            _context.KHACHHANG.Remove(khachhangdel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
