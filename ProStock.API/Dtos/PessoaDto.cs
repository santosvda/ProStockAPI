using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<EnderecoDto> Enderecos { get; set; }
    }
}