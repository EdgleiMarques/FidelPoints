using FidelPoints.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var product = await _context.Products.ToListAsync();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return BadRequest(new { message = "Id não encontrado" });
            return Ok(product);
        }
        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Product product)
        {
            if (product.Id != id) return BadRequest();
            if (await _context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id) == null) return NotFound();
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
