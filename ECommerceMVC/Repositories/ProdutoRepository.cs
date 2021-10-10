using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public IList<Produto> GetProdutos()
        {
            return dbSets.ToList();
        }

        public void saveProdutos(List<Livro> livros)
        {
            foreach (var item in livros)
            {
                if (!dbSets.Any(x => x.Codigo == item.Codigo)) 
                {
                    dbSets.Add(new Produto(item.Codigo,
                    item.Nome, item.Preco));
                }
            }
            contexto.SaveChanges();
        }

        public class Livro
        {
            public string Codigo { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }
        }

    }
}
