using System;
using System.Collections.Generic;

namespace ProStock.API.Dtos
{
    public class RelatorioBase
    {
        public List<RelatorioVendaDto> Vendas { get; set; } = new List<RelatorioVendaDto>();
        public decimal ValorTotalTotal { get; set; } = 0;
        public decimal DescontoTotal { get; set; } = 0;
        public decimal AcrescimoTotal { get; set; } = 0;
        public decimal FreteTotal { get; set; } = 0;
    }
}