using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido getItemPedido(int itemPedidoId);

        void removeItemPedido(int itemPedidoId);
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemPedido getItemPedido(int itemPedidoId)
        {
            return dbSets.FirstOrDefault(x => x.Id == itemPedidoId);
        }

        public void removeItemPedido(int itemPedidoId)
        {
            dbSets.Remove(getItemPedido(itemPedidoId));
        }
    }
}
