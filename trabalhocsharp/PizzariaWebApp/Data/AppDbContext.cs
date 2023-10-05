using Microsoft.EntityFrameworkCore;
using PizzariaWebApp.Models;

namespace PizzariaWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<ItemDePedido> ItensDePedido { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Entrega> Entregas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=pizzaria.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
