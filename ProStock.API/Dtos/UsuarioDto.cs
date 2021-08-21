using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain.Enums;

namespace ProStock.API.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set;}
        public TipoUsuario TipoUsuario { get; set; }
        public int? PessoaId { get; set; }
        public PessoaDto Pessoa { get; set; }
        public int? LojaId { get; set; }
        public List<VendaDto> Vendas { get; set; }
    }
}