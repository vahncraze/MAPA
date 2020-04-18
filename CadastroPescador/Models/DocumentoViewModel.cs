using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CadastroPescador.Models
{
    public class DocumentoVersao
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int Id { get; set; } // id (Primary key)

        [DataMember(Order = 2, IsRequired = true)]
        public int DocumentoId { get; set; } // documento_id

        [DataMember(Order = 3, IsRequired = false)]
        public string Nome { get; set; } // nome

        [DataMember(Order = 4, IsRequired = false)]
        public DateTime? DataAlteracao { get; set; } // data_alteracao

        [DataMember(Order = 5, IsRequired = true)]
        public int DocumentoVersaoStatusId { get; set; } // documento_versao_status_id

        [DataMember(Order = 6, IsRequired = false)]
        public int? UsuarioIdAlteracao { get; set; } // usuario_id_alteracao

        public DocumentoVersao()
        {

        }

        public override string ToString()
        {
            var campos = "";
            var anexos = "";
            return Id > 0 ? string.Format("Documento Id:{0} / Nome: {1} / Versao Id:{2} / Campos:{3} / Anexos:{4} ", DocumentoId, Nome, Id, campos, anexos) : "";
        }
    }
}
