using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int? PessoaId { get; set; }
        public PessoaDto Pessoa { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? TipoUsuarioId { get; set; }
        public int? LojaId { get; set; }
        public List<VendaDto> Vendas { get; set; }
    }
}