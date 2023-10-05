using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzariaWebApp.Data;
using PizzariaWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizzas = await _pizzaRepository.GetAll();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizzaById(int id)
        {
            var pizza = await _pizzaRepository.GetById(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        [HttpPost]
        public async Task<ActionResult<Pizza>> AddPizza(Pizza pizza)
        {
            await _pizzaRepository.Add(pizza);
            return CreatedAtAction(nameof(GetPizzaById), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza(int id, Pizza pizza)
        {
            var existingPizza = await _pizzaRepository.GetById(id);
            if (existingPizza == null)
                return NotFound();
            existingPizza.NomeDaPizza = pizza.NomeDaPizza;
            existingPizza.TamanhoDaPizza = pizza.TamanhoDaPizza;
            existingPizza.Ingredientes = pizza.Ingredientes;

            await _pizzaRepository.Update(existingPizza);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _pizzaRepository.GetById(id);
            if (pizza == null)
                return NotFound();

            await _pizzaRepository.Delete(id);
            return NoContent();
        }
    }

    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetAll();
        Task<Pizza> GetById(int id);
        Task Add(Pizza pizza);
        Task Update(Pizza pizza);
        Task Delete(int id);
    }
}
