using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Venda
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public bool Ativo { get; set; }
        public string Status { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<ProdutoVenda> ProdutosVendas { get; set; }
    }
}