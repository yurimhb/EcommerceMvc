using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CasaDoCodigo.Models;

namespace ECommerceMVC.Areas.Catalogo.Data
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var produtos = GetProdutos();
            var categorias = produtos.Select(x => x.Categoria).Distinct();

            modelBuilder.Entity<Categoria>(b =>
            {
                b.HasKey(t => t.Id);
                b.HasData(categorias);
            });

            modelBuilder.Entity<Produto>(p =>
            {
                p.HasKey(t => t.Id);
                p.HasData(produtos
                    .Select(x=>
                    new 
                    {
                        x.Id,
                        x.Codigo,
                        x.Nome,
                        x.Preco,
                        CategoriaId = x.Categoria.Id
                    }));
            });
        }

        private List<Livro> GetLivros() 
        {
            var json = File.ReadAllText("data/livros.json");
            return JsonConvert.DeserializeObject<List<Livro>>(json);
        }

        private List<Produto> GetProdutos() 
        {
            var livros = GetLivros();

            var categorias = livros
                .Select(x => x.Categoria)
                .Distinct()
                .Select((n,i) =>
                {
                    var categoria= new Categoria(n);
                    categoria.Id = i+1;
                    return categoria;
                });

            var produtos =
                (from livro in livros
                 join categoria in categorias
                     on livro.Categoria equals categoria.Nome
                     select new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria)
                     )
                     .Select((produto, i) =>
                        {
                            produto.Id = i + 1;
                            return produto;
                        }
                     )
                     .ToList();

            return produtos;
        }
    }
}
