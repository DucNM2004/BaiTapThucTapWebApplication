using BaiTapThucTapWebApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiMatHangController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaiMatHangController( MyDbContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiMatHang>>> GetAllLoaimathang()
        {
            var lmh = await _context.LOAIMATHANG.ToListAsync();
            return Ok(lmh);
        }
        //ssfsdfsdfsdfsdfsdfsdfsdfsdfsddfsfdsfdsfsdfsdfsdfsdfsdfsdf
        //hello from the orther side 
        [HttpGet("{id}")]
        public IActionResult GetLMHById(string id)
        {
            var lmh = _context.LOAIMATHANG.SingleOrDefault(lm => lm.Maloaimathang == id);
            if (lmh == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lmh);
            }
        }
        [HttpPost]
        public async Task<ActionResult<LoaiMatHang>> PostLoaiMatHang(LoaiMatHang loaimathang)
        {
            _context.LOAIMATHANG.Add(loaimathang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllLoaimathang", new { Maloaimathang = loaimathang.Maloaimathang,
                Tenloaimathang = loaimathang.Tenloaimathang }, loaimathang);
        }
        
        [HttpPut("{id}")]

        public async Task<IActionResult> PutLoaiMatHang(string id, LoaiMatHang loaimathang)
        {
            if (id != loaimathang.Maloaimathang)
            {
                return BadRequest();
            }

            _context.Entry(loaimathang).State = EntityState.Modified;
            var check = _context.LOAIMATHANG.SingleOrDefault(mk => mk.Maloaimathang == id);
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
        public async Task<IActionResult> DelLoaimathang(string id)
        {
            var LMHDel = await _context.LOAIMATHANG.FindAsync(id);
            if (LMHDel == null)
            {
                return NotFound();
            }

            _context.LOAIMATHANG.Remove(LMHDel);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
