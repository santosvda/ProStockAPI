using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class RelatorioVendaDto
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Acrescimo { get; set; }
        public decimal Frete { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public int QtdProdutos { get;  set; }
    }
}