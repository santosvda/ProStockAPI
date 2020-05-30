using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Loja
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}