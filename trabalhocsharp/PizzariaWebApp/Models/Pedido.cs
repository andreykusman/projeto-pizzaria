using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{   
    public class Pedido
    {
        [Key]
        public int Id {get; set; }
        [Required]
        public List<Pizza> Pizzas {get; set; }
        [Required]
        public decimal PrecoTotal {get;set; }
        [Required]
        public string StatusDoPedido {get; set; }
        [Required]
        public List<ItemDePedido> Itens {get;set; }
    }
}
