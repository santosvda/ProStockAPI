using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class VendaGetDto
    {
        public int Id { get; set; }
        public double ValorTotal { get; set; }
        public double Desconto { get; set; }
        public double Acrescimo { get; set; }
        public double Frete { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public int ClienteId { get; set; }
        public ClienteDto Cliente { get; set; }
        public int UsuarioId { get; set; }
        public UsuarioDto Usuario { get; set; }
        public List<ProdutoDto> Produtos { get;  set; }
    }
}