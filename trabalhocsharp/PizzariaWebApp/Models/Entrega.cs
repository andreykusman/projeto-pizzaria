using System;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class Entrega
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime DataEntrega { get; set; }

        [Required]
        public Pedido Pedido { get; set; }
    }
}
