using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PizzariaWebApp.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ListaIngredientesRequerida(ErrorMessage = "A lista de ingredientes não pode estar vazia.")]
        public List<string> Ingredientes { get; set; }

        [Required]
        public string NomeDaPizza { get; set; }

        [Required]
        public string TamanhoDaPizza { get; set; }

        public Pizza()
        {
            Ingredientes = new List<string>();
        }

        public Pizza(List<string> ingredientes, string nomeDaPizza, string tamanhoDaPizza)
        {
            Ingredientes = ingredientes;
            NomeDaPizza = nomeDaPizza;
            TamanhoDaPizza = tamanhoDaPizza;
        }
    }

    public class ListaIngredientesRequeridaAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is List<string> ingredientes && ingredientes.Any())
            {
                return true;
            }

            return false;
        }
    }
}
