using Microsoft.AspNetCore.Mvc;

namespace PizzariaWebApp.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Retorna a página inicial do menu
        }

        // Ações para redirecionar para outros controladores

        public IActionResult Cliente()
        {
            return RedirectToAction("Index", "Cliente"); // Redireciona para o controlador "Cliente" e a ação "Index" dentro dele
        }

        public IActionResult Entrega()
        {
            return RedirectToAction("Index", "Entrega");
        }

        public IActionResult Fornecedor()
        {
            return RedirectToAction("Index", "Fornecedor");
        }

        public IActionResult Funcionario()
        {
            return RedirectToAction("Index", "Funcionario");
        }

        public IActionResult ItemDePedido()
        {
            return RedirectToAction("Index", "ItemDePedido");
        }

        public IActionResult Pedido()
        {
            return RedirectToAction("Index", "Pedido");
        }

        public IActionResult Pizza()
        {
            return RedirectToAction("Index", "Pizza");
        }

        public IActionResult Promocao()
        {
            return RedirectToAction("Index", "Promocao");
        }
    }
}
