using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class VendaDto
    {
        public int Id { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public int ClienteId { get; set; }
        public ClienteDto Cliente { get; }
        public int UsuarioId { get; set; }
        public UsuarioDto Usuario { get; }
        public List<ProdutoDto> Produtos { get;  }
    }
}