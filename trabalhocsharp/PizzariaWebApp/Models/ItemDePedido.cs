using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class ItemDePedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ICollection<Pizza> Pizzas { get; set; } 

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [DataType(DataType.Currency)] 
        public decimal PrecoUni { get; set; }
    }
}
