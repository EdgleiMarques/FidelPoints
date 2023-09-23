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
/*
        [HttpGet("ClientId")]
        public async Task<ActionResult> GetPointsById(int clientId)
        {
            var totalPoints = await _context.Orders
                .Select(c => c.Point)
                .SumAsync();
            return Ok(totalPoints);
        }*/

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
