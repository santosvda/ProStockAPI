using System;

namespace ProStock.Domain
{
    public class Estoque
    {
        public int Id { get; set; }
        public int QtdAtual { get; set; }
        public int QtdMinima { get; set; }
        public int QtdReservada { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }
}