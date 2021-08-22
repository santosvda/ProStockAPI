using System;

namespace ProStock.API.Dtos
{
    public class EstoqueDto
    {
         public int Id { get; set; }
        public int QtdAtual { get; set; }
        public int QtdMinima { get; set; }
        public int QtdReservada { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int ProdutoId { get; set; }
        //public ProdutoDto Produto { get;}
    }
}