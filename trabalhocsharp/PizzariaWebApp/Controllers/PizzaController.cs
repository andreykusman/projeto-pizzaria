using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzariaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzariaWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaController(ILogger<PizzaController> logger, IPizzaRepository pizzaRepository)
        {
            _logger = logger;
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
}
