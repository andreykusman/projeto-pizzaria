using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public ICollection<string> Ingredientes { get; set; }

        // Adicionando construtor vazio
        public Fornecedor()
        {
            Nome = string.Empty;
            Ingredientes = new List<string>();
        }

        // Adicionando construtor com parâmetros
        public Fornecedor(string nome, ICollection<string> ingredientes)
        {
            Nome = nome;
            Ingredientes = ingredientes;
        }
    }
}
