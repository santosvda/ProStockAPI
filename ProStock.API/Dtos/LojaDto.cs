using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class LojaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int? EnderecoId { get; set; }
        public EnderecoDto Endereco { get; set;}
        //public List<UsuarioDto> Usuarios { get; set;}
    }
}