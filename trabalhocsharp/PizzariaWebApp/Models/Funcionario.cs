using System.ComponentModel.DataAnnotations;
namespace PizzariaWebApp.Models
{
    public class Funcionario
    {
        [Key]
        public int Id {get;set;}
        [Required]
        public string Nome {get; set; }
        [Required]
        public string Cargo {get; set; }
    }
}