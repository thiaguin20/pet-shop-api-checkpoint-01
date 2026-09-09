using Microsoft.AspNetCore.Mvc;
using Trabalho_API.Models;

namespace Trabalho_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> _clientes = new List<Cliente>()
        {
            new Cliente
            {
                Id = 1,
                Nome = "Ana Souza",
                Telefone = "(18) 99999-1111",
                Email = "ana.souza@email.com"
            },
            new Cliente
            {
                Id = 2,
                Nome = "Bruno Lima",
                Telefone = "(18) 98888-2222",
                Email = "bruno.lima@email.com"
            },
            new Cliente
            {
                Id = 3,
                Nome = "Carla Mendes",
                Telefone = "(18) 97777-3333",
                Email = "carla.mendes@email.com"
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Cliente? cliente = _clientes.FirstOrDefault(cliente => cliente.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }
    }
}
