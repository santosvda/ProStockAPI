using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class RelatorioProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public int? Quantidade { get;  set; }
        public decimal ValorUnit { get;  set; }
    }
}