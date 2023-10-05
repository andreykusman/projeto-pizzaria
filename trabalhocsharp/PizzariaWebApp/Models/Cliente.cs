using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Telefone { get; set; }

        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

        #region Construtores
        public Cliente()
        {
            Cpf = string.Empty;
            Nome = string.Empty;
            Endereco = string.Empty;
            Telefone = string.Empty;
        }

        public Cliente(string cpf, string nome, string endereco, string telefone)
        {
            Cpf = cpf;
            Nome = nome;
            Endereco = endereco;
            Telefone = telefone;
        }
        #endregion

        #region CRUD
        public void Inserir()
        {
            clientes.Add(this);
        }
        #endregion
    }
}
