namespace ProStock.API.Dtos
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int Numero { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public string Pais { get; set; }
        public int? PessoaId { get; set; }
        //public PessoaDto Pessoa { get; set; }
    }
}