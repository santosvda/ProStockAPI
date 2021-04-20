using System;
using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Pessoa : EntityBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}