using System.ComponentModel.DataAnnotations;

namespace PizzariaWebApp.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cargo { get; set; }

        // Adicionando construtor vazio
        public Funcionario()
        {
            Nome = string.Empty;
            Cargo = string.Empty;
        }

        // Adicionando construtor com parâmetros
        public Funcionario(string nome, string cargo)
        {
            Nome = nome;
            Cargo = cargo;
        }
    }
}
