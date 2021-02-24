using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int PessoaId { get; set; }
        public PessoaDto Pessoa { get; set; }
        public List<VendaDto> Vendas { get; set; }
    }
}