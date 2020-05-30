using System;

namespace ProStock.Domain
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public string Pais { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public int? PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

    }
}