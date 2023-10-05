using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{   
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public List<Pizza> Pizzas { get; set; }

        [Required]
        public decimal PrecoTotal { get; set; }

        [Required]
        public string StatusDoPedido { get; set; }

        [Required]
        public List<ItemDePedido> Itens { get; set; }

        // Adicionando construtor vazio
        public Pedido()
        {
            Pizzas = new List<Pizza>();
            PrecoTotal = 0.0m;
            StatusDoPedido = string.Empty;
            Itens = new List<ItemDePedido>();
        }

        // Adicionando construtor com parâmetros
        public Pedido(List<Pizza> pizzas, decimal precoTotal, string statusDoPedido, List<ItemDePedido> itens)
        {
            Pizzas = pizzas;
            PrecoTotal = precoTotal;
            StatusDoPedido = statusDoPedido;
            Itens = itens;
        }
    }
}
