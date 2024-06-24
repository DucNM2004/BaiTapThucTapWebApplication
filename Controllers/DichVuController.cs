using BaiTapThucTapWebApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//hello
namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DichVuController(MyDbContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDichvu() {
            var dsDichvu = _context.DICHVU.ToList();
            return Ok(dsDichvu);
        }
        [HttpGet("{id}")]
        public IActionResult GetDichVuById(string id)
        {
            var lmh = _context.DICHVU.SingleOrDefault(lm => lm.Madichvu == id);
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
        public async Task<ActionResult<DichVu>> PostLoaiMatHang(DichVu dichVu)
        {
            _context.DICHVU.Add(dichVu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllDichvu", new
            {
                Madichvu = dichVu.Madichvu,
                Tendichvu = dichVu.Tendichvu
            }, dichVu);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutDichVu(string id, DichVu dichVu)
        {
            if (id != dichVu.Madichvu)
            {
                return BadRequest();
            }

            _context.Entry(dichVu).State = EntityState.Modified;
            var check = _context.DICHVU.SingleOrDefault(mk => mk.Madichvu == id);
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
        public async Task<IActionResult> DelDichVu(string id)
        {
            var LMHDel = await _context.DICHVU.FindAsync(id);
            if (LMHDel == null)
            {
                return NotFound();
            }

            _context.DICHVU.Remove(LMHDel);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
