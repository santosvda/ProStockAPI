using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ProStock.API.Dtos
{
    public class UsuarioGetDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int? PessoaId { get; set; }
        public PessoaDto Pessoa { get; set; }
        public int? LojaId { get; set; }
        public List<VendaDto> Vendas { get; }
    }
}