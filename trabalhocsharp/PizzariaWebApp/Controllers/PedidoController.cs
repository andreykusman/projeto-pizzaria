using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var pedidos = await _pedidoRepository.GetAll();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            var pedido = await _pedidoRepository.GetById(id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> AddPedido([FromBody] Pedido pedido)
        {
            // Aqui você pode acessar o conteúdo da solicitação HTTP no parâmetro 'pedido'
            
            if (pedido == null)
                return BadRequest("A solicitação não contém dados válidos.");

            // Continue com a lógica para adicionar o pedido ao banco de dados

            await _pedidoRepository.Add(pedido);
            return CreatedAtAction(nameof(GetPedidoById), new { id = pedido.Id }, pedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, [FromBody] Pedido pedido)
        {
            var existingPedido = await _pedidoRepository.GetById(id);
            if (existingPedido == null)
                return NotFound();

            existingPedido.StatusDoPedido = pedido.StatusDoPedido;
            existingPedido.Pizzas = pedido.Pizzas;
            existingPedido.PrecoTotal = pedido.PrecoTotal;
            existingPedido.Itens = pedido.Itens;

            await _pedidoRepository.Update(existingPedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _pedidoRepository.GetById(id);
            if (pedido == null)
                return NotFound();

            await _pedidoRepository.Delete(id);
            return NoContent();
        }
    }
}
