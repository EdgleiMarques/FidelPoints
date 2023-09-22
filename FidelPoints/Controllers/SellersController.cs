using FidelPoints.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SellersController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var seller = await _context.Sellers.ToListAsync();
            return Ok(seller);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Seller seller)
        {
            await _context.AddAsync(seller);
            await _context.SaveChangesAsync();
            return Ok(seller);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var veiculo = await _context.Sellers.FindAsync(id);
            if (veiculo == null) return BadRequest(new { message = "Id não encontrado" });
            return Ok(veiculo);
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Seller seller)
        {
            if (seller.Id != id) return BadRequest();
            if (await _context.Sellers.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id) == null) return NotFound();
            _context.Sellers.Update(seller);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return NotFound();
            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
