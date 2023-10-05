using Microsoft.AspNetCore.Mvc;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            var funcionarios = await _funcionarioRepository.GetAll();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionarioById(int id)
        {
            var funcionario = await _funcionarioRepository.GetById(id);

            if (funcionario == null)
                return NotFound();

            return funcionario;
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> AddFuncionario(Funcionario funcionario)
        {
            await _funcionarioRepository.Add(funcionario);
            return CreatedAtAction(nameof(GetFuncionarioById), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(int id, Funcionario funcionario)
        {
            var existingFuncionario = await _funcionarioRepository.GetById(id);
            if (existingFuncionario == null)
                return NotFound();

            existingFuncionario.Nome = funcionario.Nome;
            existingFuncionario.Cargo = funcionario.Cargo;

            await _funcionarioRepository.Update(existingFuncionario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await _funcionarioRepository.GetById(id);
            if (funcionario == null)
                return NotFound();

            await _funcionarioRepository.Delete(id);
            return NoContent();
        }
    }
}
