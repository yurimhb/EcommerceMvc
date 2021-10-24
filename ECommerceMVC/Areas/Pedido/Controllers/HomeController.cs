using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Identity;
using ECommerceMVC.Areas.Identity.Data;

namespace ECommerceMVC.Areas.Pedido.Controllers
{
    [Area("Pedido")]
    public class HomeController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly UserManager<AppIdentityUser> userManager;

        public HomeController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, UserManager<AppIdentityUser> userManager)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CasaDoCodigo.Models.Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {

                var usuario = await userManager.GetUserAsync(this.User);
                usuario.Email = cadastro.Email;
                usuario.Telefone = cadastro.Telefone;
                usuario.Nome = cadastro.Nome;
                usuario.Endereco = cadastro.Endereco;
                usuario.Bairro = cadastro.Bairro;
                usuario.Complemento = cadastro.Complemento;
                usuario.Municipio = cadastro.Municipio;
                usuario.CEP = cadastro.CEP;
                usuario.UF = cadastro.UF;

                await userManager.UpdateAsync(usuario);

                return View(await pedidoRepository.UpdateCadastroAsync(cadastro));
            }
            return RedirectToAction("Cadastro");
        }
    }
}
