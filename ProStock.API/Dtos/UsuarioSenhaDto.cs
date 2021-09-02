using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ProStock.API.Dtos
{
    public class UsuarioSenhaDto
    {
        public string ConfirmarSenha { get; set; }
        public string Senha { get; set; }        
        public int AdminId { get; set; }

    }
}