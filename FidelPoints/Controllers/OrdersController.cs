using FidelPoints.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var client = await _context.Orders.ToListAsync();
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return BadRequest(new { message = "Id não encontrado" });
            return Ok(order);
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Order order)
        {
            if (order.Id != id) return BadRequest();
            if (await _context.Orders.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id) == null) return NotFound();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
