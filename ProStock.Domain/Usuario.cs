using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public int? TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public int? LojaId { get; set; }
        public Loja Loja { get; set; }
        public List<Venda> Vendas { get; set; }
    }
}