using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trabalho_API.Data;
using Trabalho_API.Models;

namespace Trabalho_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly PetShopContext _context;

        public ClientesController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes(string? nome, string? email)
        {
            IQueryable<Cliente> consulta = _context.Clientes;

            if (!string.IsNullOrWhiteSpace(nome))
                consulta = consulta.Where(cliente => cliente.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(email))
                consulta = consulta.Where(cliente => cliente.Email.Contains(email));

            return Ok(await consulta.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            Cliente? cliente = await _context.Clientes.FindAsync(id);

            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            bool emailJaCadastrado = await _context.Clientes.AnyAsync(item => item.Email == cliente.Email);

            if (emailJaCadastrado)
                return BadRequest("Já existe um cliente cadastrado com esse e-mail.");

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest("O ID da rota deve ser igual ao ID do cliente.");

            if (!await _context.Clientes.AnyAsync(item => item.Id == id))
                return NotFound();

            bool emailJaCadastrado = await _context.Clientes
                .AnyAsync(item => item.Email == cliente.Email && item.Id != id);

            if (emailJaCadastrado)
                return BadRequest("Já existe outro cliente cadastrado com esse e-mail.");

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            Cliente? cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            bool possuiPets = await _context.Pets.AnyAsync(pet => pet.ClienteId == id);

            if (possuiPets)
                return BadRequest("O cliente não pode ser excluído porque possui pets cadastrados.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
