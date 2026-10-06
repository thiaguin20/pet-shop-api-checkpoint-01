using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_API.Data;
using Trabalho_API.Models;

namespace Trabalho_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly PetShopContext _context;

        public AgendamentosController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos(string? status, DateTime? data)
        {
            IQueryable<Agendamento> consulta = _context.Agendamentos;

            if (!string.IsNullOrWhiteSpace(status))
                consulta = consulta.Where(agendamento => agendamento.Status == status);

            if (data.HasValue)
                consulta = consulta.Where(agendamento => agendamento.DataHora.Date == data.Value.Date);

            return Ok(await consulta.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamento(int id)
        {
            Agendamento? agendamento = await _context.Agendamentos.FindAsync(id);
            return agendamento == null ? NotFound() : Ok(agendamento);
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> PostAgendamento(Agendamento agendamento)
        {
            if (!await _context.Pets.AnyAsync(pet => pet.Id == agendamento.PetId))
                return BadRequest("O pet informado não existe.");

            if (agendamento.DataHora <= DateTime.Now)
                return BadRequest("A data e a hora do agendamento devem estar no futuro.");

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAgendamento), new { id = agendamento.Id }, agendamento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgendamento(int id, Agendamento agendamento)
        {
            if (id != agendamento.Id)
                return BadRequest("O ID da rota deve ser igual ao ID do agendamento.");

            if (!await _context.Agendamentos.AnyAsync(item => item.Id == id))
                return NotFound();

            if (!await _context.Pets.AnyAsync(pet => pet.Id == agendamento.PetId))
                return BadRequest("O pet informado não existe.");

            if (agendamento.DataHora <= DateTime.Now)
                return BadRequest("A data e a hora do agendamento devem estar no futuro.");

            _context.Entry(agendamento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgendamento(int id)
        {
            Agendamento? agendamento = await _context.Agendamentos.FindAsync(id);

            if (agendamento == null)
                return NotFound();

            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
