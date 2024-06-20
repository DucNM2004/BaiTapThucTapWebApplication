using BaiTapThucTapWebApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhVienGiaoHangController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ThanhVienGiaoHangController (MyDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllTVGH() {
            var dsThanhviengiaohang = _context.THANHVIENGIAOHANG.ToList();
            return Ok(dsThanhviengiaohang);
        }

        [HttpPost]
        public async Task<ActionResult<ThanhVienGiaoHang>> PostThanhVienGH(ThanhVienGiaoHang thanhVienGiaoHang)
        {
            _context.THANHVIENGIAOHANG.Add(thanhVienGiaoHang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllTVGH", new
            {
                Mathangviengh = thanhVienGiaoHang.Mathanhviengh,
                Tenthanhviengiaohang = thanhVienGiaoHang.Tenthanhviengiaohang,
                Ngaysinh = thanhVienGiaoHang.Ngaysinh,
                Gioitinh = thanhVienGiaoHang.Gioitinh,
                Sodtthanhvien = thanhVienGiaoHang.Sodtthanhvien,
                Diachithanhvien = thanhVienGiaoHang.Diachithanhvien
            },thanhVienGiaoHang);
        }
        [HttpGet("{id}")]
        public IActionResult GetTHGHById(string id) 
        {
            var tttv = _context.THANHVIENGIAOHANG.SingleOrDefault(tv => tv.Mathanhviengh == id);
            if(tttv == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tttv);
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutTVGH(string id, ThanhVienGiaoHang thanhVienGiaoHang)
        {
            if (id != thanhVienGiaoHang.Mathanhviengh)
            {
                return BadRequest();
            }

            _context.Entry(thanhVienGiaoHang).State = EntityState.Modified;
            var check = _context.THANHVIENGIAOHANG.SingleOrDefault(mk => mk.Mathanhviengh == id);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (check == null)
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
        public async Task<IActionResult> DelTVGH(string id)
        {
            var tttv = _context.THANHVIENGIAOHANG.SingleOrDefault(tv=>tv.Mathanhviengh == id);
            if(tttv == null)
            {
                return NotFound();
            }
            
            _context.THANHVIENGIAOHANG.Remove(tttv);
            await _context.SaveChangesAsync();
            return NoContent();
            
        }
    }
}
