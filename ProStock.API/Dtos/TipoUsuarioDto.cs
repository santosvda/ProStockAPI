using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class TipoUsuarioDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<UsuarioDto> Usuarios { get; set; }
    }
}