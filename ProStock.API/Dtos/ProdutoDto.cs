using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public double ValorUnit { get; set; }
        public int? UsuarioId { get; set; }
        //public UsuarioDto Usuario { get; set; }
        public List<VendaDto> Vendas { get; }
    }
}