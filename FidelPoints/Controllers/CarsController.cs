using FidelPoints.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var client = await _context.Cars.ToListAsync();
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Car car)
        {
            await _context.AddAsync(car);
            await _context.SaveChangesAsync();
            return Ok(car);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car  == null) return BadRequest(new { message = "Id não encontrado" });
            return Ok(car);
        }

        [HttpGet("ProductForClientId")]
        public async Task<ActionResult> GetProductByClient(int clientId)
        {
            var rel = await _context.Cars
                .Include(p => p.Product)
                .Where(c => c.ClientId == clientId)
                .ToListAsync();
            return Ok(rel);
        }

        [HttpGet("SumPointClient")]
        public async Task<ActionResult> SumPointClient(int clienteId)
        {
            var totalPoints = await _context.Cars
                .Where(c => c.ClientId == clienteId)
                .Select(c => c.Product.Point)
                .SumAsync();
                return Ok(totalPoints);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Car car)
        {
            if (car.Id != id) return BadRequest(new {message = "Id conflitando"});
            if (await _context.Cars.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id) == null) return NotFound();
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
