using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_API.Data;
using Trabalho_API.Models;

namespace Trabalho_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly PetShopContext _context;

        public PetsController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets(string? especie, int? clienteId)
        {
            IQueryable<Pet> consulta = _context.Pets;

            if (!string.IsNullOrWhiteSpace(especie))
                consulta = consulta.Where(pet => pet.Especie.Contains(especie));

            if (clienteId.HasValue)
                consulta = consulta.Where(pet => pet.ClienteId == clienteId.Value);

            return Ok(await consulta.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            Pet? pet = await _context.Pets.FindAsync(id);
            return pet == null ? NotFound() : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            if (!await _context.Clientes.AnyAsync(cliente => cliente.Id == pet.ClienteId))
                return BadRequest("O cliente informado não existe.");

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.Id)
                return BadRequest("O ID da rota deve ser igual ao ID do pet.");

            if (!await _context.Pets.AnyAsync(item => item.Id == id))
                return NotFound();

            if (!await _context.Clientes.AnyAsync(cliente => cliente.Id == pet.ClienteId))
                return BadRequest("O cliente informado não existe.");

            _context.Entry(pet).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            Pet? pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound();

            bool possuiAgendamentos = await _context.Agendamentos
                .AnyAsync(agendamento => agendamento.PetId == id);

            if (possuiAgendamentos)
                return BadRequest("O pet não pode ser excluído porque possui agendamentos cadastrados.");

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
