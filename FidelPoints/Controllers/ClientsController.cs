using FidelPoints.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FidelPoints.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClientsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> Getall()
        {
            var client = await _context.Clients.ToListAsync();
            return Ok(client);
        }

        [HttpGet("cpf")]
        public async Task<ActionResult> GetClientByCpf(string cpf)
        {
            var client = await _context.Clients
                .FirstAsync(c => c.Cpf == cpf);
            return Ok(client);
        }

        [HttpGet("extrato")]
        public async Task<ActionResult> GetExtratoClientByCpf(string cpf)
        {
            var client = await _context.Orders
                .Include(c => c.Sellers)
                .Include(c => c.Clients)
                .FirstAsync(c => c.Clients.Cpf == cpf);
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Client client)
        {
            client.Password =BCrypt.Net.BCrypt.HashPassword(client.Password);
            await _context.AddAsync(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }
        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return BadRequest(new { message = "Id não encontrado" });
            return Ok(client);
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, Client client)
        {
            if (client.Id != id) return BadRequest();
            if (await _context.Clients.AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id) == null) return NotFound();
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
