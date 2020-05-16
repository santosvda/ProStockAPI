namespace ProStock.Domain
{
    public class ProdutoVenda
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int VendaId { get; set; }
        public Venda Venda { get; set; }

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