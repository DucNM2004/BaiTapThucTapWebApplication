using BaiTapThucTapWebApplication.Data;
using BaiTapThucTapWebApplication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuVucController : ControllerBase
    {
        private readonly MyDbContext _context;

        public KhuVucController(MyDbContext context) {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllKhuvuc() {
            var dsKhuvuc = _context.KHUVUC.ToList();
            return Ok(dsKhuvuc);
        }
        [HttpGet("{id}")]
        public IActionResult GetKhuvucById(string id) {
            var khuvuc = _context.KHUVUC.SingleOrDefault(kv => kv.Makhuvuc == id);
            if (khuvuc == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(khuvuc);
            }
        }
        [HttpPost]
        public async Task<ActionResult<KhuVuc>> PostLoaiMatHang(KhuVuc khuVuc)
        {
            _context.KHUVUC.Add(khuVuc);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllKhuvuc", new { Makhuvuc = khuVuc.Makhuvuc, Tenkhuvuc = khuVuc.Tenkhuvuc }, khuVuc);
        }
        //public IActionResult CreateKhuvuc(KhuVucModel model)
        //{
        //    try
        //    {
        //        var khuvucNew = new KhuVuc
        //        {
        //            Makhuvuc = model.Makhuvuc,
        //            Tenkhuvuc = model.Tenkhuvuc,
        //        };
        //        _context.Add(khuvucNew);
        //        _context.SaveChanges();
        //        return Ok(khuvucNew);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        [HttpPut("{id}")]

        //public IActionResult UpdateKhuVuc(string id,KhuVucModel model)
        //{
        //        var khuvucUp = _context.KHUVUC.SingleOrDefault(mk => mk.Makhuvuc == id);
        //        if(khuvucUp != null)
        //        {
        //            khuvucUp.Tenkhuvuc = model.Tenkhuvuc;
        //            _context.SaveChanges();
        //            return NoContent();
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //}


        public async Task<IActionResult> PutLoaiMatHang(string id, KhuVuc khuvuc)
        {
            if (id != khuvuc.Makhuvuc)
            {
                return BadRequest();
            }

            _context.Entry(khuvuc).State = EntityState.Modified;
            var check = _context.KHUVUC.SingleOrDefault(mk => mk.Makhuvuc == id);
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
        public async Task<IActionResult> DelKhuVuc(string id)
        {
            var KhuvucDel = await _context.KHUVUC.FindAsync(id);
            if(KhuvucDel == null)
            {
                return NotFound();
            }
            
              _context.KHUVUC.Remove(KhuvucDel);
              await _context.SaveChangesAsync();
              return NoContent();
            
        }
    }
}
