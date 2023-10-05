using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class Promocao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public DateTime Periodo { get; set; }

        [Required]
        public decimal Desconto { get; set; }

        [Required]
        public ICollection<Pizza> PizzasIncluidas { get; set; } // Ou List<Pizza>

        public Promocao()
        {
            PizzasIncluidas = new List<Pizza>();
        }

        public Promocao(string descricao, DateTime periodo, decimal desconto, List<Pizza> pizzasIncluidas)
        {
            Descricao = descricao;
            Periodo = periodo;
            Desconto = desconto;
            PizzasIncluidas = pizzasIncluidas;
        }
    }
}
