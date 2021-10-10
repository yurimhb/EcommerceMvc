class Carrinho {
    clickIncremento(btn) {
        var data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn)
    {
        var data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    updateQuantidade(input)
    {
        var data = this.getData(input);
        this.postQuantidade(data);
    }

    postQuantidade(data) {
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response)
        {
            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']');
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subTotal).duasCasas());

            let carrinhoViewModel = response.carrinhoViewModel;

            $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens');

            $('[total]').html((carrinhoViewModel.total).duasCasas());


            if (itemPedido.quantidade == 0) {
                linhaDoItem.remove();
            }

            debugger;
        });
    }

    getData(elemento) {
        var linha = $(elemento).parents('[item-id]');
        var itemId = $(linha).attr('item-id');
        var novaQtd = $(linha).find('input').val();

        var data = {
            Id: itemId,
            Quantidade: novaQtd
        };
        return data;
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}