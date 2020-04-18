
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.DTO
{
    public class DocumentoUsuarioDTO
    {
        public DocumentoUsuarioDTO(int id, string texto)
        {
            Id = id;
            Texto = texto;
        }
        public int Id { get; set; }
        public string Texto { get; set; }
    }
    
}
