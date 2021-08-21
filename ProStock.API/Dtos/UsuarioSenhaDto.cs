using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ProStock.API.Dtos
{
    public class UsuarioSenhaDto
    {
        public int Id { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Senha { get; set; }        
        public int UsuarioId { get; set; }

    }
}