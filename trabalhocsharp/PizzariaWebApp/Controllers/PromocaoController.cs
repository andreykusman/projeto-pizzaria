using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocaoController : ControllerBase
    {
        private readonly ILogger<PromocaoController> _logger;
        private readonly IPromocaoRepository _promocaoRepository;

        public PromocaoController(ILogger<PromocaoController> logger, IPromocaoRepository promocaoRepository)
        {
            _logger = logger;
            _promocaoRepository = promocaoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Promocao>>> GetPromocoes()
        {
            var promocoes = await _promocaoRepository.GetAll();
            return Ok(promocoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Promocao>> GetPromocaoById(int id)
        {
            var promocao = await _promocaoRepository.GetById(id);

            if (promocao == null)
                return NotFound();

            return promocao;
        }

        [HttpPost]
        public async Task<ActionResult<Promocao>> AddPromocao(Promocao promocao)
        {
            await _promocaoRepository.Add(promocao);
            return CreatedAtAction(nameof(GetPromocaoById), new { id = promocao.Id }, promocao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromocao(int id, Promocao promocao)
        {
            var existingPromocao = await _promocaoRepository.GetById(id);
            if (existingPromocao == null)
                return NotFound();

            existingPromocao.Descricao = promocao.Descricao;
            existingPromocao.Periodo = promocao.Periodo;
            existingPromocao.Desconto = promocao.Desconto;
            existingPromocao.PizzasIncluidas = promocao.PizzasIncluidas;

            await _promocaoRepository.Update(existingPromocao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromocao(int id)
        {
            var promocao = await _promocaoRepository.GetById(id);
            if (promocao == null)
                return NotFound();

            await _promocaoRepository.Delete(id);
            return NoContent();
        }
    }
}
