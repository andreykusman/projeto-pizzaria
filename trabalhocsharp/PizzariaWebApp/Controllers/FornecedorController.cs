using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores()
        {
            var fornecedores = await _fornecedorRepository.GetAll();
            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> GetFornecedorById(int id)
        {
            var fornecedor = await _fornecedorRepository.GetById(id);

            if (fornecedor == null)
                return NotFound();

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<Fornecedor>> AddFornecedor(Fornecedor fornecedor)
        {
            await _fornecedorRepository.Add(fornecedor);
            return CreatedAtAction(nameof(GetFornecedorById), new { id = fornecedor.Id }, fornecedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFornecedor(int id, Fornecedor fornecedor)
        {
            var existingFornecedor = await _fornecedorRepository.GetById(id);
            if (existingFornecedor == null)
                return NotFound();

            existingFornecedor.Nome = fornecedor.Nome;
            existingFornecedor.Ingredientes = fornecedor.Ingredientes;

            await _fornecedorRepository.Update(existingFornecedor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(int id)
        {
            var fornecedor = await _fornecedorRepository.GetById(id);
            if (fornecedor == null)
                return NotFound();

            await _fornecedorRepository.Delete(id);
            return NoContent();
        }
    }
}