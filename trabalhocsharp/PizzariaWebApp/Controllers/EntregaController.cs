using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly IEntregaRepository _entregaRepository;

        public EntregaController(IEntregaRepository entregaRepository)
        {
            _entregaRepository = entregaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
            var entregas = await _entregaRepository.GetAll();
            return Ok(entregas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntregaById(int id)
        {
            var entrega = await _entregaRepository.GetById(id);

            if (entrega == null)
                return NotFound();

            return entrega;
        }

        [HttpPost]
        public async Task<ActionResult<Entrega>> AddEntrega(Entrega entrega)
        {
            await _entregaRepository.Add(entrega);
            return CreatedAtAction(nameof(GetEntregaById), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntrega(int id, Entrega entrega)
        {
            var existingEntrega = await _entregaRepository.GetById(id);
            if (existingEntrega == null)
                return NotFound();

            existingEntrega.Status = entrega.Status;
            existingEntrega.DataEntrega = entrega.DataEntrega;
            existingEntrega.Pedido = entrega.Pedido;

            await _entregaRepository.Update(existingEntrega);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            var entrega = await _entregaRepository.GetById(id);
            if (entrega == null)
                return NotFound();

            await _entregaRepository.Delete(id);
            return NoContent();
        }
    }
}
