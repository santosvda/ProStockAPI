using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataExclusao { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}