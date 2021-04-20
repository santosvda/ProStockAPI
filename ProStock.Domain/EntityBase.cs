using System;

namespace ProStock.Domain
{
    public class EntityBase
    {
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}