using CasaDoCodigo.Models;
using ECommerceMVC.Areas.Catalogo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceMVC.Areas.Catalogo.ViewComponents
{
    public class CategoriasViewComponent : ViewComponent
    {
        private const int TamanhoPagina = 4;

        public IViewComponentResult Invoke(IList<Produto> produtos)
        {
            var categorias =
                    produtos
                        .Select(m => m.Categoria)
                        .Distinct().ToList();

            return View("Default", new CategoriasViewModel(categorias, produtos, TamanhoPagina));
        }
    }
}
