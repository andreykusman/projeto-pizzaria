using System;
using System.Collections.Generic;
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

        // Adicionando construtor vazio
        public Entrega()
        {
            Status = string.Empty;
            DataEntrega = DateTime.MinValue;
            Pedido = null;
        }

        // Adicionando construtor com parâmetros
        public Entrega(string status, DateTime dataEntrega, Pedido pedido)
        {
            Status = status;
            DataEntrega = dataEntrega;
            Pedido = pedido;
        }
    }
}
