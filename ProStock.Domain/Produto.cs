using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Produto : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public decimal ValorUnit { get; set; }
        public int? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<ProdutoVenda> ProdutosVendas { get; set; }

    }
}