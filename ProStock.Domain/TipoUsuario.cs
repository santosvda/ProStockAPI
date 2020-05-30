using System.Collections.Generic;

namespace ProStock.Domain
{
    public class TipoUsuario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<Usuario> Usuarios { get; set; }

    }
}