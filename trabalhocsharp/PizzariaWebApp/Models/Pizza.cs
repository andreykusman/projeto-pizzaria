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
        [ListaIngredientesRequerida]
        public List<string> Ingredientes { get; set; }

        [Required]
        public string NomeDaPizza { get; set; }

        [Required]
        public string TamanhoDaPizza { get; set; }
    }

    public class ListaIngredientesRequeridaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is List<string> ingredientes && ingredientes.Any())
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("A lista de ingredientes não pode estar vazia.");
        }
    }
}
