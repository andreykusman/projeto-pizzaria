using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PizzariaWebApp.Data;
using PizzariaWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=pizzaria.db");
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => Results.Ok("Bem-vindo à Pizzaria!\n"));

app.MapGet("/menu", () => Results.Ok("Escolha uma opção:\n1. Cliente\n2. Entrega\n3. Fornecedor\n4. Funcionário\n5. Item de Pedido\n6. Pedido\n7. Pizza\n8. Promoção\n"));

app.MapPost("/menu", async context =>
{
    var userInput = await context.Request.ReadAsStringAsync();
    switch (userInput.Trim())
    {
        case "1":
            ClienteMenu();
            break;
        case "2":
            EntregaMenu();
            break;
        case "3":
            FornecedorMenu();
            break;
        case "4":
            FuncionarioMenu();
            break;
        case "5":
            ItemDePedidoMenu();
            break;
        case "6":
            PedidoMenu();
            break;
        case "7":
            PizzaMenu();
            break;
        case "8":
            PromocaoMenu();
            break;
        default:
            await context.Response.WriteAsync("Opção inválida.\n");
            break;
    }
});

app.Run();

static void ClienteMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Cliente:");
        Console.WriteLine("1. Mostrar Todos os Clientes");
        Console.WriteLine("2. Mostrar Cliente por ID");
        Console.WriteLine("3. Adicionar Novo Cliente");
        Console.WriteLine("4. Atualizar Cliente");
        Console.WriteLine("5. Deletar Cliente");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodosClientes();
                    break;
                case 2:
                    MostrarClientePorID();
                    break;
                case 3:
                    AdicionarNovoCliente();
                    break;
                case 4:
                    AtualizarCliente();
                    break;
                case 5:
                    DeletarCliente();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodosClientes()
{
    using var dbContext = new AppDbContext();
    var clientes = dbContext.Clientes.ToList();

    Console.WriteLine("Lista de Clientes:");
    foreach (var cliente in clientes)
    {
        Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, Endereço: {cliente.Endereco}, Telefone: {cliente.Telefone}");
    }
}

static void MostrarClientePorID()
{
    Console.Write("Digite o ID do cliente: ");
    if (int.TryParse(Console.ReadLine(), out var clienteId))
    {
        using var dbContext = new AppDbContext();
        var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == clienteId);

        if (cliente != null)
        {
            Console.WriteLine($"ID: {cliente.Id}, Nome: {cliente.Nome}, Endereço: {cliente.Endereco}, Telefone: {cliente.Telefone}");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovoCliente()
{
    Console.Write("Digite o nome do novo cliente: ");
    var nomeCliente = Console.ReadLine();

    Console.Write("Digite o endereço do novo cliente: ");
    var enderecoCliente = Console.ReadLine();

    Console.Write("Digite o telefone do novo cliente: ");
    var telefoneCliente = Console.ReadLine();

    using var dbContext = new AppDbContext();
    var novoCliente = new Cliente { Nome = nomeCliente, Endereco = enderecoCliente, Telefone = telefoneCliente, Pedidos = new List<Pedido>() };
    dbContext.Clientes.Add(novoCliente);
    dbContext.SaveChanges();
    Console.WriteLine("Novo cliente adicionado com sucesso.");
}

static void AtualizarCliente()
{
    Console.Write("Digite o ID do cliente que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var clienteId))
    {
        using var dbContext = new AppDbContext();
        var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == clienteId);

        if (cliente != null)
        {
            Console.Write("Digite o novo nome: ");
            cliente.Nome = Console.ReadLine();

            Console.Write("Digite o novo endereço: ");
            cliente.Endereco = Console.ReadLine();

            Console.Write("Digite o novo telefone: ");
            cliente.Telefone = Console.ReadLine();

            dbContext.SaveChanges();
            Console.WriteLine("Cliente atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarCliente()
{
    Console.Write("Digite o ID do cliente que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var clienteId))
    {
        using var dbContext = new AppDbContext();
        var cliente = dbContext.Clientes.FirstOrDefault(c => c.Id == clienteId);

        if (cliente != null)
        {
            dbContext.Clientes.Remove(cliente);
            dbContext.SaveChanges();
            Console.WriteLine("Cliente deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void EntregaMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Entrega:");
        Console.WriteLine("1. Mostrar Todas as Entregas");
        Console.WriteLine("2. Mostrar Entrega por ID");
        Console.WriteLine("3. Adicionar Nova Entrega");
        Console.WriteLine("4. Atualizar Entrega");
        Console.WriteLine("5. Deletar Entrega");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodasEntregas();
                    break;
                case 2:
                    MostrarEntregaPorId();
                    break;
                case 3:
                    AdicionarNovaEntrega();
                    break;
                case 4:
                    AtualizarEntrega();
                    break;
                case 5:
                    DeletarEntrega();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodasEntregas()
{
    using var dbContext = new AppDbContext();
    var entregas = dbContext.Entregas.ToList();

    Console.WriteLine("Lista de Entregas:");
    foreach (var entrega in entregas)
    {
        Console.WriteLine($"ID: {entrega.Id}, Status: {entrega.Status}, Data de Entrega: {entrega.DataEntrega}, Pedido ID: {entrega.Pedido.Id}");
    }
}

static void MostrarEntregaPorId()
{
    Console.Write("Digite o ID da entrega: ");
    if (int.TryParse(Console.ReadLine(), out var entregaId))
    {
        using var dbContext = new AppDbContext();
        var entrega = dbContext.Entregas.FirstOrDefault(e => e.Id == entregaId);

        if (entrega != null)
        {
            Console.WriteLine($"ID: {entrega.Id}, Status: {entrega.Status}, Data de Entrega: {entrega.DataEntrega}, Pedido ID: {entrega.Pedido.Id}");
        }
        else
        {
            Console.WriteLine("Entrega não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovaEntrega()
{
    Console.Write("Digite o status da entrega: ");
    var statusEntrega = Console.ReadLine();

    Console.Write("Digite a data de entrega (yyyy-MM-dd): ");
    if (DateTime.TryParse(Console.ReadLine(), out var dataEntrega))
    {
        using var dbContext = new AppDbContext();
        var pedidos = dbContext.Pedidos.ToList();
        
        if (pedidos.Count > 0)
        {
            Console.WriteLine("Pedidos disponíveis para entrega:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"ID: {pedido.Id}, Data do Pedido: {pedido.DataPedido}");
            }

            Console.Write("Digite o ID do pedido para esta entrega: ");
            if (int.TryParse(Console.ReadLine(), out var pedidoId))
            {
                var pedido = dbContext.Pedidos.FirstOrDefault(p => p.Id == pedidoId);

                if (pedido != null)
                {
                    var novaEntrega = new Entrega { Status = statusEntrega, DataEntrega = dataEntrega, Pedido = pedido };
                    dbContext.Entregas.Add(novaEntrega);
                    dbContext.SaveChanges();
                    Console.WriteLine("Nova entrega adicionada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID de pedido inválido.");
            }
        }
        else
        {
            Console.WriteLine("Não há pedidos disponíveis para esta entrega.");
        }
    }
    else
    {
        Console.WriteLine("Data de entrega inválida.");
    }
}

static void AtualizarEntrega()
{
    Console.Write("Digite o ID da entrega que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var entregaId))
    {
        using var dbContext = new AppDbContext();
        var entrega = dbContext.Entregas.FirstOrDefault(e => e.Id == entregaId);

        if (entrega != null)
        {
            Console.Write("Digite o novo status da entrega: ");
            entrega.Status = Console.ReadLine();

            Console.Write("Digite a nova data de entrega (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out var dataEntrega))
            {
                entrega.DataEntrega = dataEntrega;
                dbContext.SaveChanges();
                Console.WriteLine("Entrega atualizada com sucesso.");
            }
            else
            {
                Console.WriteLine("Data de entrega inválida.");
            }
        }
        else
        {
            Console.WriteLine("Entrega não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarEntrega()
{
    Console.Write("Digite o ID da entrega que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var entregaId))
    {
        using var dbContext = new AppDbContext();
        var entrega = dbContext.Entregas.FirstOrDefault(e => e.Id == entregaId);

        if (entrega != null)
        {
            dbContext.Entregas.Remove(entrega);
            dbContext.SaveChanges();
            Console.WriteLine("Entrega deletada com sucesso.");
        }
        else
        {
            Console.WriteLine("Entrega não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}
static void FornecedorMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Fornecedor:");
        Console.WriteLine("1. Mostrar Todos os Fornecedores");
        Console.WriteLine("2. Mostrar Fornecedor por ID");
        Console.WriteLine("3. Adicionar Novo Fornecedor");
        Console.WriteLine("4. Atualizar Fornecedor");
        Console.WriteLine("5. Deletar Fornecedor");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodosFornecedores();
                    break;
                case 2:
                    MostrarFornecedorPorId();
                    break;
                case 3:
                    AdicionarNovoFornecedor();
                    break;
                case 4:
                    AtualizarFornecedor();
                    break;
                case 5:
                    DeletarFornecedor();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodosFornecedores()
{
    using var dbContext = new AppDbContext();
    var fornecedores = dbContext.Fornecedores.ToList();

    Console.WriteLine("Lista de Fornecedores:");
    foreach (var fornecedor in fornecedores)
    {
        Console.WriteLine($"ID: {fornecedor.Id}, Nome: {fornecedor.Nome}");
        Console.WriteLine("Ingredientes:");
        foreach (var ingrediente in fornecedor.Ingredientes)
        {
            Console.WriteLine(ingrediente);
        }
        Console.WriteLine();
    }
}

static void MostrarFornecedorPorId()
{
    Console.Write("Digite o ID do fornecedor: ");
    if (int.TryParse(Console.ReadLine(), out var fornecedorId))
    {
        using var dbContext = new AppDbContext();
        var fornecedor = dbContext.Fornecedores.FirstOrDefault(f => f.Id == fornecedorId);

        if (fornecedor != null)
        {
            Console.WriteLine($"ID: {fornecedor.Id}, Nome: {fornecedor.Nome}");
            Console.WriteLine("Ingredientes:");
            foreach (var ingrediente in fornecedor.Ingredientes)
            {
                Console.WriteLine(ingrediente);
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Fornecedor não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovoFornecedor()
{
    Console.Write("Digite o nome do novo fornecedor: ");
    var nomeFornecedor = Console.ReadLine();

    var ingredientes = new List<string>();

    while (true)
    {
        Console.Write("Digite um ingrediente (ou deixe em branco para sair): ");
        var ingrediente = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(ingrediente))
        {
            break;
        }
        ingredientes.Add(ingrediente);
    }

    using var dbContext = new AppDbContext();
    var novoFornecedor = new Fornecedor { Nome = nomeFornecedor, Ingredientes = ingredientes };
    dbContext.Fornecedores.Add(novoFornecedor);
    dbContext.SaveChanges();
    Console.WriteLine("Novo fornecedor adicionado com sucesso.");
}

static void AtualizarFornecedor()
{
    Console.Write("Digite o ID do fornecedor que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var fornecedorId))
    {
        using var dbContext = new AppDbContext();
        var fornecedor = dbContext.Fornecedores.FirstOrDefault(f => f.Id == fornecedorId);

        if (fornecedor != null)
        {
            Console.Write("Digite o novo nome: ");
            fornecedor.Nome = Console.ReadLine();

            fornecedor.Ingredientes.Clear();

            var ingredientes = new List<string>();

            while (true)
            {
                Console.Write("Digite um ingrediente (ou deixe em branco para sair): ");
                var ingrediente = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingrediente))
                {
                    break;
                }
                ingredientes.Add(ingrediente);
            }

            fornecedor.Ingredientes = ingredientes;

            dbContext.SaveChanges();
            Console.WriteLine("Fornecedor atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Fornecedor não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarFornecedor()
{
    Console.Write("Digite o ID do fornecedor que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var fornecedorId))
    {
        using var dbContext = new AppDbContext();
        var fornecedor = dbContext.Fornecedores.FirstOrDefault(f => f.Id == fornecedorId);

        if (fornecedor != null)
        {
            dbContext.Fornecedores.Remove(fornecedor);
            dbContext.SaveChanges();
            Console.WriteLine("Fornecedor deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("Fornecedor não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}
static void FuncionarioMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Funcionário:");
        Console.WriteLine("1. Mostrar Todos os Funcionários");
        Console.WriteLine("2. Mostrar Funcionário por ID");
        Console.WriteLine("3. Adicionar Novo Funcionário");
        Console.WriteLine("4. Atualizar Funcionário");
        Console.WriteLine("5. Deletar Funcionário");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodosFuncionarios();
                    break;
                case 2:
                    MostrarFuncionarioPorID();
                    break;
                case 3:
                    AdicionarNovoFuncionario();
                    break;
                case 4:
                    AtualizarFuncionario();
                    break;
                case 5:
                    DeletarFuncionario();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodosFuncionarios()
{
    using var dbContext = new AppDbContext();
    var funcionarios = dbContext.Funcionarios.ToList();

    Console.WriteLine("Lista de Funcionários:");
    foreach (var funcionario in funcionarios)
    {
        Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Cargo: {funcionario.Cargo}");
    }
}

static void MostrarFuncionarioPorID()
{
    Console.Write("Digite o ID do funcionário: ");
    if (int.TryParse(Console.ReadLine(), out var funcionarioId))
    {
        using var dbContext = new AppDbContext();
        var funcionario = dbContext.Funcionarios.FirstOrDefault(f => f.Id == funcionarioId);

        if (funcionario != null)
        {
            Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Cargo: {funcionario.Cargo}");
        }
        else
        {
            Console.WriteLine("Funcionário não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovoFuncionario()
{
    Console.Write("Digite o nome do novo funcionário: ");
    var nomeFuncionario = Console.ReadLine();

    Console.Write("Digite o cargo do novo funcionário: ");
    var cargoFuncionario = Console.ReadLine();

    using var dbContext = new AppDbContext();
    var novoFuncionario = new Funcionario { Nome = nomeFuncionario, Cargo = cargoFuncionario };
    dbContext.Funcionarios.Add(novoFuncionario);
    dbContext.SaveChanges();
    Console.WriteLine("Novo funcionário adicionado com sucesso.");
}

static void AtualizarFuncionario()
{
    Console.Write("Digite o ID do funcionário que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var funcionarioId))
    {
        using var dbContext = new AppDbContext();
        var funcionario = dbContext.Funcionarios.FirstOrDefault(f => f.Id == funcionarioId);

        if (funcionario != null)
        {
            Console.Write("Digite o novo nome: ");
            funcionario.Nome = Console.ReadLine();

            Console.Write("Digite o novo cargo: ");
            funcionario.Cargo = Console.ReadLine();

            dbContext.SaveChanges();
            Console.WriteLine("Funcionário atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Funcionário não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarFuncionario()
{
    Console.Write("Digite o ID do funcionário que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var funcionarioId))
    {
        using var dbContext = new AppDbContext();
        var funcionario = dbContext.Funcionarios.FirstOrDefault(f => f.Id == funcionarioId);

        if (funcionario != null)
        {
            dbContext.Funcionarios.Remove(funcionario);
            dbContext.SaveChanges();
            Console.WriteLine("Funcionário deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("Funcionário não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void ItemDePedidoMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Item de Pedido:");
        Console.WriteLine("1. Mostrar Todos os Itens de Pedido");
        Console.WriteLine("2. Mostrar Item de Pedido por ID");
        Console.WriteLine("3. Adicionar Novo Item de Pedido");
        Console.WriteLine("4. Atualizar Item de Pedido");
        Console.WriteLine("5. Deletar Item de Pedido");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodosItensDePedido();
                    break;
                case 2:
                    MostrarItemDePedidoPorID();
                    break;
                case 3:
                    AdicionarNovoItemDePedido();
                    break;
                case 4:
                    AtualizarItemDePedido();
                    break;
                case 5:
                    DeletarItemDePedido();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodosItensDePedido()
{
    using var dbContext = new AppDbContext();
    var itensDePedido = dbContext.ItensDePedido.ToList();

    Console.WriteLine("Lista de Itens de Pedido:");
    foreach (var itemDePedido in itensDePedido)
    {
        Console.WriteLine($"ID: {itemDePedido.Id}, Quantidade: {itemDePedido.Quantidade}, Preço Unitário: {itemDePedido.PrecoUni:C}");

        // Mostrar as pizzas associadas a este item de pedido
        Console.WriteLine("Pizzas:");
        foreach (var pizza in itemDePedido.Pizzas)
        {
            Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.Nome}");
        }
    }
}

static void MostrarItemDePedidoPorID()
{
    Console.Write("Digite o ID do Item de Pedido: ");
    if (int.TryParse(Console.ReadLine(), out var itemDePedidoId))
    {
        using var dbContext = new AppDbContext();
        var itemDePedido = dbContext.ItensDePedido.FirstOrDefault(i => i.Id == itemDePedidoId);

        if (itemDePedido != null)
        {
            Console.WriteLine($"ID: {itemDePedido.Id}, Quantidade: {itemDePedido.Quantidade}, Preço Unitário: {itemDePedido.PrecoUni:C}");

            // Mostrar as pizzas associadas a este item de pedido
            Console.WriteLine("Pizzas:");
            foreach (var pizza in itemDePedido.Pizzas)
            {
                Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.Nome}");
            }
        }
        else
        {
            Console.WriteLine("Item de Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovoItemDePedido()
{
    using var dbContext = new AppDbContext();
    var novoItemDePedido = new ItemDePedido();

    Console.Write("Digite a quantidade: ");
    if (int.TryParse(Console.ReadLine(), out var quantidade))
    {
        novoItemDePedido.Quantidade = quantidade;
    }
    else
    {
        Console.WriteLine("Quantidade inválida.");
        return;
    }

    Console.Write("Digite o preço unitário: ");
    if (decimal.TryParse(Console.ReadLine(), out var precoUni))
    {
        novoItemDePedido.PrecoUni = precoUni;
    }
    else
    {
        Console.WriteLine("Preço unitário inválido.");
        return;
    }

    // Lógica para selecionar pizzas associadas
    Console.WriteLine("Selecione as Pizzas associadas (digite os IDs separados por vírgula): ");
    var pizzaIdsInput = Console.ReadLine();
    var pizzaIds = pizzaIdsInput.Split(',').Select(idStr => int.Parse(idStr.Trim())).ToList();

    // Verifica se as Pizzas existem no banco de dados
    var pizzas = dbContext.Pizzas.Where(p => pizzaIds.Contains(p.Id)).ToList();
    if (pizzas.Count != pizzaIds.Count)
    {
        Console.WriteLine("Uma ou mais Pizzas selecionadas não existem.");
        return;
    }

    // Associa as Pizzas ao Item de Pedido
    novoItemDePedido.Pizzas = pizzas;

    dbContext.ItensDePedido.Add(novoItemDePedido);
    dbContext.SaveChanges();
    Console.WriteLine("Novo item de pedido adicionado com sucesso.");
}

static void AtualizarItemDePedido()
{
    Console.Write("Digite o ID do Item de Pedido que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var itemDePedidoId))
    {
        using var dbContext = new AppDbContext();
        var itemDePedido = dbContext.ItensDePedido.Include(i => i.Pizzas).FirstOrDefault(i => i.Id == itemDePedidoId);

        if (itemDePedido != null)
        {
            Console.Write("Digite a nova quantidade: ");
            if (int.TryParse(Console.ReadLine(), out var novaQuantidade))
            {
                itemDePedido.Quantidade = novaQuantidade;
            }
            else
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            Console.Write("Digite o novo preço unitário: ");
            if (decimal.TryParse(Console.ReadLine(), out var novoPrecoUni))
            {
                itemDePedido.PrecoUni = novoPrecoUni;
            }
            else
            {
                Console.WriteLine("Preço unitário inválido.");
                return;
            }

            // Lógica para atualizar as Pizzas associadas (adicionar/remover)
            Console.WriteLine("Selecione as Pizzas associadas (digite os IDs separados por vírgula): ");
            var pizzaIdsInput = Console.ReadLine();
            var pizzaIds = pizzaIdsInput.Split(',').Select(idStr => int.Parse(idStr.Trim())).ToList();

            // Verifica se as Pizzas existem no banco de dados
            var pizzas = dbContext.Pizzas.Where(p => pizzaIds.Contains(p.Id)).ToList();
            if (pizzas.Count != pizzaIds.Count)
            {
                Console.WriteLine("Uma ou mais Pizzas selecionadas não existem.");
                return;
            }

            // Atualiza a lista de Pizzas associadas ao Item de Pedido
            itemDePedido.Pizzas.Clear();
            itemDePedido.Pizzas.AddRange(pizzas);

            dbContext.SaveChanges();
            Console.WriteLine("Item de Pedido atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Item de Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}


static void DeletarItemDePedido()
{
    Console.Write("Digite o ID do Item de Pedido que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var itemDePedidoId))
    {
        using var dbContext = new AppDbContext();
        var itemDePedido = dbContext.ItensDePedido.FirstOrDefault(i => i.Id == itemDePedidoId);

        if (itemDePedido != null)
        {
            dbContext.ItensDePedido.Remove(itemDePedido);
            dbContext.SaveChanges();
            Console.WriteLine("Item de Pedido deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("Item de Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void PedidoMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Pedido:");
        Console.WriteLine("1. Mostrar Todos os Pedidos");
        Console.WriteLine("2. Mostrar Pedido por ID");
        Console.WriteLine("3. Adicionar Novo Pedido");
        Console.WriteLine("4. Atualizar Pedido");
        Console.WriteLine("5. Deletar Pedido");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodosPedidos();
                    break;
                case 2:
                    MostrarPedidoPorID();
                    break;
                case 3:
                    AdicionarNovoPedido();
                    break;
                case 4:
                    AtualizarPedido();
                    break;
                case 5:
                    DeletarPedido();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodosPedidos()
{
    using var dbContext = new AppDbContext();
    var pedidos = dbContext.Pedidos.ToList();

    Console.WriteLine("Lista de Pedidos:");
    foreach (var pedido in pedidos)
    {
        Console.WriteLine($"ID: {pedido.Id}, Preço Total: {pedido.PrecoTotal:C}, Status do Pedido: {pedido.StatusDoPedido}");

        // Mostrar as pizzas associadas a este pedido
        Console.WriteLine("Pizzas do Pedido:");
        foreach (var pizza in pedido.Pizzas)
        {
            Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.Nome}");
        }

        // Mostrar os itens de pedido associados a este pedido
        Console.WriteLine("Itens de Pedido do Pedido:");
        foreach (var itemDePedido in pedido.Itens)
        {
            Console.WriteLine($" - ID do Item de Pedido: {itemDePedido.Id}, Quantidade: {itemDePedido.Quantidade}, Preço Unitário: {itemDePedido.PrecoUni:C}");
        }
    }
}

static void MostrarPedidoPorID()
{
    Console.Write("Digite o ID do Pedido: ");
    if (int.TryParse(Console.ReadLine(), out var pedidoId))
    {
        using var dbContext = new AppDbContext();
        var pedido = dbContext.Pedidos.FirstOrDefault(p => p.Id == pedidoId);

        if (pedido != null)
        {
            Console.WriteLine($"ID: {pedido.Id}, Preço Total: {pedido.PrecoTotal:C}, Status do Pedido: {pedido.StatusDoPedido}");

            // Mostrar as pizzas associadas a este pedido
            Console.WriteLine("Pizzas do Pedido:");
            foreach (var pizza in pedido.Pizzas)
            {
                Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.Nome}");
            }

            // Mostrar os itens de pedido associados a este pedido
            Console.WriteLine("Itens de Pedido do Pedido:");
            foreach (var itemDePedido in pedido.Itens)
            {
                Console.WriteLine($" - ID do Item de Pedido: {itemDePedido.Id}, Quantidade: {itemDePedido.Quantidade}, Preço Unitário: {itemDePedido.PrecoUni:C}");
            }
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovoPedido()
{
    using var dbContext = new AppDbContext();
    
    // Coleta das informações do pedido
    var novoPedido = new Pedido();

    Console.Write("Digite o Preço Total do Pedido: ");
    if (decimal.TryParse(Console.ReadLine(), out var precoTotal))
    {
        novoPedido.PrecoTotal = precoTotal;
    }
    else
    {
        Console.WriteLine("Preço Total inválido. Pedido não adicionado.");
        return;
    }

    Console.Write("Digite o Status do Pedido: ");
    novoPedido.StatusDoPedido = Console.ReadLine();

    // Coleta das informações das Pizzas associadas
    Console.WriteLine("Adicionar Pizzas ao Pedido (separadas por vírgula, por exemplo: 1,2,3): ");
    var pizzaIdsInput = Console.ReadLine();
    var pizzaIds = pizzaIdsInput.Split(',').Select(int.Parse).ToList();

    foreach (var pizzaId in pizzaIds)
    {
        var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
        if (pizza != null)
        {
            novoPedido.Pizzas.Add(pizza);
        }
    }

    // Coleta das informações dos Itens de Pedido associados
    Console.WriteLine("Adicionar Itens de Pedido (ID da Pizza, Quantidade, Preço Unitário separados por vírgula, por exemplo: 1,2,10.99): ");
    var itensInput = Console.ReadLine();
    var itensInfo = itensInput.Split(',');

    for (int i = 0; i < itensInfo.Length; i += 3)
    {
        if (int.TryParse(itensInfo[i], out var pizzaId) &&
            int.TryParse(itensInfo[i + 1], out var quantidade) &&
            decimal.TryParse(itensInfo[i + 2], out var precoUnitario))
        {
            var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
            if (pizza != null)
            {
                novoPedido.Itens.Add(new ItemDePedido
                {
                    Pizzas = new List<Pizza> { pizza },
                    Quantidade = quantidade,
                    PrecoUni = precoUnitario
                });
            }
        }
    }

    dbContext.Pedidos.Add(novoPedido);
    dbContext.SaveChanges();
    Console.WriteLine("Novo pedido adicionado com sucesso.");
}

static void AtualizarPedido()
{
    Console.Write("Digite o ID do Pedido que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var pedidoId))
    {
        using var dbContext = new AppDbContext();
        var pedido = dbContext.Pedidos
            .Include(p => p.Pizzas)
            .Include(p => p.Itens)
            .FirstOrDefault(p => p.Id == pedidoId);

        if (pedido != null)
        {
            // Coleta das informações atualizadas do pedido
            Console.Write("Digite o novo Preço Total do Pedido: ");
            if (decimal.TryParse(Console.ReadLine(), out var novoPrecoTotal))
            {
                pedido.PrecoTotal = novoPrecoTotal;
            }

            Console.Write("Digite o novo Status do Pedido: ");
            pedido.StatusDoPedido = Console.ReadLine();

            // Coleta das informações atualizadas das Pizzas associadas
            Console.WriteLine("Atualizar Pizzas do Pedido (separadas por vírgula, por exemplo: 1,2,3): ");
            var pizzaIdsInput = Console.ReadLine();
            var pizzaIds = pizzaIdsInput.Split(',').Select(int.Parse).ToList();

            pedido.Pizzas.Clear();

            foreach (var pizzaId in pizzaIds)
            {
                var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
                if (pizza != null)
                {
                    pedido.Pizzas.Add(pizza);
                }
            }

            // Coleta das informações atualizadas dos Itens de Pedido associados
            Console.WriteLine("Atualizar Itens de Pedido (ID da Pizza, Quantidade, Preço Unitário separados por vírgula, por exemplo: 1,2,10.99): ");
            var itensInput = Console.ReadLine();
            var itensInfo = itensInput.Split(',');

            pedido.Itens.Clear();

            for (int i = 0; i < itensInfo.Length; i += 3)
            {
                if (int.TryParse(itensInfo[i], out var pizzaId) &&
                    int.TryParse(itensInfo[i + 1], out var quantidade) &&
                    decimal.TryParse(itensInfo[i + 2], out var precoUnitario))
                {
                    var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);
                    if (pizza != null)
                    {
                        pedido.Itens.Add(new ItemDePedido
                        {
                            Pizzas = new List<Pizza> { pizza },
                            Quantidade = quantidade,
                            PrecoUni = precoUnitario
                        });
                    }
                }
            }

            dbContext.SaveChanges();
            Console.WriteLine("Pedido atualizado com sucesso.");
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}


static void DeletarPedido()
{
    Console.Write("Digite o ID do Pedido que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var pedidoId))
    {
        using var dbContext = new AppDbContext();
        var pedido = dbContext.Pedidos.FirstOrDefault(p => p.Id == pedidoId);

        if (pedido != null)
        {
            dbContext.Pedidos.Remove(pedido);
            dbContext.SaveChanges();
            Console.WriteLine("Pedido deletado com sucesso.");
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void PizzaMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Pizza:");
        Console.WriteLine("1. Mostrar Todas as Pizzas");
        Console.WriteLine("2. Mostrar Pizza por ID");
        Console.WriteLine("3. Adicionar Nova Pizza");
        Console.WriteLine("4. Atualizar Pizza");
        Console.WriteLine("5. Deletar Pizza");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodasPizzas();
                    break;
                case 2:
                    MostrarPizzaPorID();
                    break;
                case 3:
                    AdicionarNovaPizza();
                    break;
                case 4:
                    AtualizarPizza();
                    break;
                case 5:
                    DeletarPizza();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodasPizzas()
{
    using var dbContext = new AppDbContext();
    var pizzas = dbContext.Pizzas.ToList();

    Console.WriteLine("Lista de Pizzas:");
    foreach (var pizza in pizzas)
    {
        Console.WriteLine($"ID: {pizza.Id}, Nome: {pizza.NomeDaPizza}, Tamanho: {pizza.TamanhoDaPizza}");
        Console.WriteLine("Ingredientes:");

        foreach (var ingrediente in pizza.Ingredientes)
        {
            Console.WriteLine($" - {ingrediente}");
        }
    }
}

static void MostrarPizzaPorID()
{
    Console.Write("Digite o ID da Pizza: ");
    if (int.TryParse(Console.ReadLine(), out var pizzaId))
    {
        using var dbContext = new AppDbContext();
        var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

        if (pizza != null)
        {
            Console.WriteLine($"ID: {pizza.Id}, Nome: {pizza.NomeDaPizza}, Tamanho: {pizza.TamanhoDaPizza}");
            Console.WriteLine("Ingredientes:");

            foreach (var ingrediente in pizza.Ingredientes)
            {
                Console.WriteLine($" - {ingrediente}");
            }
        }
        else
        {
            Console.WriteLine("Pizza não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovaPizza()
{
    Console.Write("Digite o nome da nova pizza: ");
    var nomePizza = Console.ReadLine();

    Console.Write("Digite o tamanho da nova pizza: ");
    var tamanhoPizza = Console.ReadLine();

    var ingredientesPizza = new List<string>();

    while (true)
    {
        Console.Write("Digite um ingrediente da pizza (ou deixe em branco para parar): ");
        var ingrediente = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(ingrediente))
        {
            break;
        }

        ingredientesPizza.Add(ingrediente);
    }

    using var dbContext = new AppDbContext();
    var novaPizza = new Pizza { NomeDaPizza = nomePizza, TamanhoDaPizza = tamanhoPizza, Ingredientes = ingredientesPizza };
    dbContext.Pizzas.Add(novaPizza);
    dbContext.SaveChanges();
    Console.WriteLine("Nova pizza adicionada com sucesso.");
}

static void AtualizarPizza()
{
    Console.Write("Digite o ID da Pizza que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var pizzaId))
    {
        using var dbContext = new AppDbContext();
        var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

        if (pizza != null)
        {
            Console.Write("Digite o novo nome: ");
            pizza.NomeDaPizza = Console.ReadLine();

            Console.Write("Digite o novo tamanho: ");
            pizza.TamanhoDaPizza = Console.ReadLine();

            var novosIngredientes = new List<string>();

            while (true)
            {
                Console.Write("Digite um novo ingrediente (ou deixe em branco para parar): ");
                var ingrediente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingrediente))
                {
                    break;
                }

                novosIngredientes.Add(ingrediente);
            }

            pizza.Ingredientes = novosIngredientes;

            dbContext.SaveChanges();
            Console.WriteLine("Pizza atualizada com sucesso.");
        }
        else
        {
            Console.WriteLine("Pizza não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarPizza()
{
    Console.Write("Digite o ID da Pizza que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var pizzaId))
    {
        using var dbContext = new AppDbContext();
        var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

        if (pizza != null)
        {
            dbContext.Pizzas.Remove(pizza);
            dbContext.SaveChanges();
            Console.WriteLine("Pizza deletada com sucesso.");
        }
        else
        {
            Console.WriteLine("Pizza não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void PromocaoMenu()
{
    while (true)
    {
        Console.WriteLine("Menu de Promoção:");
        Console.WriteLine("1. Mostrar Todas as Promoções");
        Console.WriteLine("2. Mostrar Promoção por ID");
        Console.WriteLine("3. Adicionar Nova Promoção");
        Console.WriteLine("4. Atualizar Promoção");
        Console.WriteLine("5. Deletar Promoção");
        Console.WriteLine("0. Voltar");

        int choice;
        if (int.TryParse(Console.ReadLine(), out choice))
        {
            switch (choice)
            {
                case 1:
                    MostrarTodasPromocoes();
                    break;
                case 2:
                    MostrarPromocaoPorID();
                    break;
                case 3:
                    AdicionarNovaPromocao();
                    break;
                case 4:
                    AtualizarPromocao();
                    break;
                case 5:
                    DeletarPromocao();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
        }
    }
}

static void MostrarTodasPromocoes()
{
    using var dbContext = new AppDbContext();
    var promocoes = dbContext.Promocoes.ToList();

    Console.WriteLine("Lista de Promoções:");
    foreach (var promocao in promocoes)
    {
        Console.WriteLine($"ID: {promocao.Id}, Descrição: {promocao.Descricao}");
        Console.WriteLine($"Período: {promocao.Periodo}, Desconto: {promocao.Desconto}%");

        Console.WriteLine("Pizzas Incluídas:");

        foreach (var pizza in promocao.PizzasIncluidas)
        {
            Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.NomeDaPizza}");
        }
    }
}

static void MostrarPromocaoPorID()
{
    Console.Write("Digite o ID da Promoção: ");
    if (int.TryParse(Console.ReadLine(), out var promocaoId))
    {
        using var dbContext = new AppDbContext();
        var promocao = dbContext.Promocoes.FirstOrDefault(p => p.Id == promocaoId);

        if (promocao != null)
        {
            Console.WriteLine($"ID: {promocao.Id}, Descrição: {promocao.Descricao}");
            Console.WriteLine($"Período: {promocao.Periodo}, Desconto: {promocao.Desconto}%");

            Console.WriteLine("Pizzas Incluídas:");

            foreach (var pizza in promocao.PizzasIncluidas)
            {
                Console.WriteLine($" - ID: {pizza.Id}, Nome: {pizza.NomeDaPizza}");
            }
        }
        else
        {
            Console.WriteLine("Promoção não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void AdicionarNovaPromocao()
{
    Console.Write("Digite a descrição da nova promoção: ");
    var descricaoPromocao = Console.ReadLine();

    Console.Write("Digite o período da promoção (ex: yyyy-MM-dd HH:mm:ss): ");
    if (DateTime.TryParse(Console.ReadLine(), out var periodoPromocao))
    {
        Console.Write("Digite o desconto da promoção (%): ");
        if (decimal.TryParse(Console.ReadLine(), out var descontoPromocao))
        {
            var pizzasIncluidasPromocao = new List<Pizza>();

            while (true)
            {
                Console.Write("Digite o ID da pizza incluída na promoção (ou deixe em branco para parar): ");
                var idPizza = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idPizza))
                {
                    break;
                }

                if (int.TryParse(idPizza, out var pizzaId))
                {
                    var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

                    if (pizza != null)
                    {
                        pizzasIncluidasPromocao.Add(pizza);
                    }
                    else
                    {
                        Console.WriteLine("Pizza não encontrada.");
                    }
                }
                else
                {
                    Console.WriteLine("ID de pizza inválido.");
                }
            }

            using var dbContext = new AppDbContext();
            var novaPromocao = new Promocao
            {
                Descricao = descricaoPromocao,
                Periodo = periodoPromocao,
                Desconto = descontoPromocao,
                PizzasIncluidas = pizzasIncluidasPromocao
            };

            dbContext.Promocoes.Add(novaPromocao);
            dbContext.SaveChanges();
            Console.WriteLine("Nova promoção adicionada com sucesso.");
        }
        else
        {
            Console.WriteLine("Desconto inválido.");
        }
    }
    else
    {
        Console.WriteLine("Período inválido.");
    }
}

static void AtualizarPromocao()
{
    Console.Write("Digite o ID da Promoção que deseja atualizar: ");
    if (int.TryParse(Console.ReadLine(), out var promocaoId))
    {
        using var dbContext = new AppDbContext();
        var promocao = dbContext.Promocoes.FirstOrDefault(p => p.Id == promocaoId);

        if (promocao != null)
        {
            Console.Write("Digite a nova descrição: ");
            promocao.Descricao = Console.ReadLine();

            Console.Write("Digite o novo período (ex: yyyy-MM-dd HH:mm:ss): ");
            if (DateTime.TryParse(Console.ReadLine(), out var novoPeriodo))
            {
                promocao.Periodo = novoPeriodo;

                Console.Write("Digite o novo desconto (%): ");
                if (decimal.TryParse(Console.ReadLine(), out var novoDesconto))
                {
                    promocao.Desconto = novoDesconto;

                    var novasPizzasIncluidas = new List<Pizza>();

                    while (true)
                    {
                        Console.Write("Digite o ID de uma pizza incluída na promoção (ou deixe em branco para parar): ");
                        var idPizza = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(idPizza))
                        {
                            break;
                        }

                        if (int.TryParse(idPizza, out var pizzaId))
                        {
                            var pizza = dbContext.Pizzas.FirstOrDefault(p => p.Id == pizzaId);

                            if (pizza != null)
                            {
                                novasPizzasIncluidas.Add(pizza);
                            }
                            else
                            {
                                Console.WriteLine("Pizza não encontrada.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID de pizza inválido.");
                        }
                    }

                    promocao.PizzasIncluidas = novasPizzasIncluidas;

                    dbContext.SaveChanges();
                    Console.WriteLine("Promoção atualizada com sucesso.");
                }
                else
                {
                    Console.WriteLine("Desconto inválido.");
                }
            }
            else
            {
                Console.WriteLine("Período inválido.");
            }
        }
        else
        {
            Console.WriteLine("Promoção não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

static void DeletarPromocao()
{
    Console.Write("Digite o ID da Promoção que deseja deletar: ");
    if (int.TryParse(Console.ReadLine(), out var promocaoId))
    {
        using var dbContext = new AppDbContext();
        var promocao = dbContext.Promocoes.FirstOrDefault(p => p.Id == promocaoId);

        if (promocao != null)
        {
            dbContext.Promocoes.Remove(promocao);
            dbContext.SaveChanges();
            Console.WriteLine("Promoção deletada com sucesso.");
        }
        else
        {
            Console.WriteLine("Promoção não encontrada.");
        }
    }
    else
    {
        Console.WriteLine("ID inválido.");
    }
}

