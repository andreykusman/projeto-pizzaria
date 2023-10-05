using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteRepository.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteById(int id)
        {
            var cliente = await _clienteRepository.GetById(id);

            if (cliente == null)
                return NotFound();

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> AddCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("A solicitação não contém dados válidos.");

            await _clienteRepository.Add(cliente);
            return CreatedAtAction(nameof(GetClienteById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            var existingCliente = await _clienteRepository.GetById(id);
            if (existingCliente == null)
                return NotFound();

            existingCliente.Nome = cliente.Nome;
            existingCliente.Endereco = cliente.Endereco;
            existingCliente.Telefone = cliente.Telefone;

            await _clienteRepository.Update(existingCliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            if (cliente == null)
                return NotFound();

            await _clienteRepository.Delete(id);
            return NoContent();
        }
    }
}
