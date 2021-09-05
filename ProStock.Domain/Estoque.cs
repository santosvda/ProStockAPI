using System;

namespace ProStock.Domain
{
    public class Estoque : EntityBase
    {
        public int Id { get; set; }
        public int QtdAtual { get; set; }
        public int QtdMinima { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }
}