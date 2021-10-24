using CasaDoCodigo.Repositories;
using ECommerceMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class HomeController : Controller
    {
        private readonly IPedidoRepository pedidoRepository;
        private readonly UserManager<AppIdentityUser> userManager;

        public HomeController(IPedidoRepository pedidoRepository, UserManager<AppIdentityUser> userManager)
        {
            this.pedidoRepository = pedidoRepository;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var pedido = await pedidoRepository.GetPedidoAsync();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            var usuario = await userManager.GetUserAsync(this.User);
            pedido.Cadastro.Email = usuario.Email;
            pedido.Cadastro.Telefone = usuario.Telefone;
            pedido.Cadastro.Nome = usuario.Nome;
            pedido.Cadastro.Endereco = usuario.Endereco;
            pedido.Cadastro.Bairro = usuario.Bairro;
            pedido.Cadastro.Complemento = usuario.Complemento;
            pedido.Cadastro.Municipio = usuario.Municipio;
            pedido.Cadastro.CEP = usuario.CEP;
            pedido.Cadastro.UF = usuario.UF;


            return View(pedido.Cadastro);
        }

    }
}
