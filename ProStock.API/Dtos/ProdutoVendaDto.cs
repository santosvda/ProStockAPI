namespace ProStock.API.Dtos
{
    public class ProdutoVendaDto
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public VendaDto Venda { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoDto Produto { get;  }
        public int Quantidade { get; set; }

        /*
                Venda ==== Produto
                1               1
                1               2
                1               3
                2               1
                2               3
        */
    }
}