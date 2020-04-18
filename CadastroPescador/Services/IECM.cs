using CadastroPescador.DTO;
using CadastroPescador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Services
{
    public interface IECM
    {
        TokenResponse ObterToken();
        bool CriarUsuario(UsuarioExternoModel usuario);
        List<DocumentoDTO> ExisteUsuario(UsuarioExternoModel usuario);
        bool CriarDocumento(UsuarioExternoModel usuario);
        void IniciarProcesso(int processoId, int documentoId);
        void SalvarLog(string mensagem);

    }
}
