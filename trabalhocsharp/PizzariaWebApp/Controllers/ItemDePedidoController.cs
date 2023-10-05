using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDePedidoController : ControllerBase
    {
        private readonly IItemDoPedidoRepository _itemdopedidoRepository;

        public ItemDePedidoController(IItemDoPedidoRepository itemdopedidoRepository)
        {
            _itemdopedidoRepository = itemdopedidoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDoPedido>>> GetItemDosPedidos()
        {
            var itemdospedidos = await _itemdopedidoRepository.GetAll();
            return Ok(itemdospedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDoPedido>> GetItemDoPedidoById(int id)
        {
            var itemdopedido = await _itemdopedidoRepository.GetById(id);

            if (itemdopedido == null)
                return NotFound();

            return itemdopedido;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDoPedido>> AddItemDoPedido([FromBody] ItemDoPedido itemdopedido)
        {
            if (itemdopedido == null)
                return BadRequest("A solicitação não contém dados válidos.");

            await _itemdopedidoRepository.Add(itemdopedido);
            return CreatedAtAction(nameof(GetItemDoPedidoById), new { id = itemdopedido.Id }, itemdopedido);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemDoPedido(int id, [FromBody] ItemDoPedido itemdopedido)
        {
            var existingItemDoPedido = await _itemdopedidoRepository.GetById(id);
            if (existingItemDoPedido == null)
                return NotFound();

            existingItemDoPedido.Pizzas = itemdopedido.Pizzas;
            existingItemDoPedido.PrecoUni = itemdopedido.PrecoUni;
            existingItemDoPedido.Quantidade = itemdopedido.Quantidade;

            await _itemdopedidoRepository.Update(existingItemDoPedido);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDoPedido(int id)
        {
            var itemdopedido = await _itemdopedidoRepository.GetById(id);
            if (itemdopedido == null)
                return NotFound();

            await _itemdopedidoRepository.Delete(id);
            return NoContent();
        }
    }
}
