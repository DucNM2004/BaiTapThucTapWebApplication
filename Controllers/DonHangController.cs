using BaiTapThucTapWebApplication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiTapThucTapWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DonHangController(MyDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonHang>>> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string orderCode = "", [FromQuery] string phoneNumber = "", [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var orders = _context.DONHANG_GIAOHANG.AsQueryable();
            orders = orders.OrderBy(o => o.Madonhanggiaohang);
            if (!string.IsNullOrEmpty(orderCode))
            {
                switch (orderCode)
                {
                    case "Desc":
                        orders = orders.OrderByDescending(o => o.Madonhanggiaohang);
                        break;
                    case "Asc":
                        orders = orders.OrderBy(o => o.Madonhanggiaohang);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                orders = orders.Where(o => o.Sodtnguoinhan.Contains(phoneNumber));
            }

            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.Ngaygiaohang >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.Ngaygiaohang <= endDate.Value);
            }

            var pagedOrders = await orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return pagedOrders;
        }
        [HttpGet("{id}")]
        public IActionResult GetAllDonhangById(string id)
        {
            var donhang = _context.DONHANG_GIAOHANG.SingleOrDefault(dh => dh.Madonhanggiaohang == id);
            if (donhang == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(donhang);
            }
        }
        [HttpPost]
        public async Task<ActionResult<DonHang>> PostOrder(DonHang order)
        {
            if (!_context.DICHVU.Any(s => s.Madichvu == order.Madichvu))
            {
                return BadRequest("ERROR_SERVICE_NOT_EXISTS");
            }

            if (!_context.THANHVIENGIAOHANG.Any(d => d.Mathanhviengh == order.Mathanhviengh))
            {
                return BadRequest("ERROR_MEMBER_NOT_EXISTS");
            }

            if (!_context.KHUVUC.Any(a => a.Makhuvuc == order.Makhuvucgiaohang))
            {
                return BadRequest("ERROR_AREA_NOT_EXISTS");
            }

            if (!_context.KHACHHANG.Any(c => c.Makhachhang == order.Makhachhang))
            {
                var newCustomer = new KhachHang
                {
                    Tenkhachhang = order.Tennguoinhan,
                    Sodtkhachhang = order.Sodtnguoinhan,
                    Makhachhang = order.Makhachhang,

                };
                _context.KHACHHANG.Add(newCustomer);
                await _context.SaveChangesAsync();
                order.Makhachhang = newCustomer.Makhachhang;
            }

            _context.DONHANG_GIAOHANG.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new {
                Madonhanggiaohang = order.Makhoangthoigiangiaohang,
                Makhachhang = order.Makhachhang,
                Mathanhviengh = order.Mathanhviengh,
                Madichvu = order.Madichvu,
                Tennguoinhan = order.Tennguoinhan,
                Diachinguoinhan = order.Diachinguoinhan,
                Sodtnguoinhan = order.Sodtnguoinhan,
                Makhoangthoigiangiaohang = order.Makhoangthoigiangiaohang,
                Ngaygiaohang = order.Ngaygiaohang,
                Phuongthucthanhtoan = order.Phuongthucthanhtoan,
                Trangthaipheduyet = order.Trangthaipheduyet,
                Trangthaigiaohang = order.Trangthaigiaohang
            }, order);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> ApproveOrder(string id, string trangthai)
        {
            var order = await _context.DONHANG_GIAOHANG.FindAsync(id);
            var check = _context.DONHANG_GIAOHANG.SingleOrDefault(dh => dh.Madonhanggiaohang == id);
            if (check == null)
            {
                return BadRequest("Khong thay id trong he thong");
            }
            _context.Entry(check).State = EntityState.Modified;
            try
            {
                order.Trangthaigiaohang = trangthai;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (check == null)
                {
                    return BadRequest("Khong thay id dang tim");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deldonhang(string id)
        {
            var check = await _context.DONHANG_GIAOHANG.FindAsync(id);
            if(check == null)
            {
                return NotFound();
            }
            _context.DONHANG_GIAOHANG.Remove(check);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
