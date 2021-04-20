using System.Collections.Generic;

namespace ProStock.Domain
{
    public class Cliente : EntityBase
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public List<Venda> Vendas { get; set; }

    }
}