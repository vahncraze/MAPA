using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.DTO
{
    public class TokenResponse
    {
        public TokenResponse()
        {
        }

        public string token { get; set; }
        public int codigo { get; set; }
        public string mensagem { get; set; }

    }
}
