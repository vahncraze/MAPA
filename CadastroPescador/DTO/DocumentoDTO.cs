using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.DTO
{
    public class DocumentoDTO
    {
        public int Id { get; set; } // id (Primary key)
        public string Nome { get; set; }
        public int TipoDocumentalId { get; set; }
        public string TipoDocumentalNome { get; set; }
        public int? DocumentoPaiId { get; set; }
        public int DocumentoVersaoId { get; set; }
        public int DocumentoReservado { get; set; }
        public string DocumentoReservadoNomeUsu { get; set; }
        public int DocumentoVersaoTotalAnexos { get; set; }
        public string Proprietario { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }

        public int TipoContainerId { get; set; }

        public string TipoContainerNome { get; set; }

        public List<CampoValor> CamposValor { get; set; }

        public List<dynamic> DocumentoVersaoCampos { get; set; }
        //processo
        public int InstanciaId { get; set; }
        public int ProcessoIdInicio { get; set; }
        public int ProcessoId { get; set; }
        public int ProcessoVersaoId { get; set; }
        public string ProcessoVersaoNome { get; set; }
        public int AtividadeId { get; set; }
        public int AtividadeTipoId { get; set; }
        public string AtividadeNome { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataTimeout { get; set; }
        public DateTime? DataTimeoutAviso { get; set; }
        public string DataTempoTotal { get; set; }
        public string DataTempoTotalNaAtividade { get; set; }

        public int InstanciaStatusId { get; set; }
        public string InstanciaStatusNome { get; set; }

        public int InstanciaUsuarioId { get; set; }
        public string InstanciaUsuarioNome { get; set; }

        public bool PossoAtenderAtividade { get; set; }

        public List<dynamic> AtividadeCamposVisualizacao { get; set; }

        public string Diretorio { get; set; }

        public int Excluido { get; set; }

        public int CaixaId { get; set; }
        public int LoteId { get; set; }
        public int Rascunho { get; set; }

        public DateTime? AprovacaoDataPublicacaoFutura { get; set; }
        public DateTime? AprovacaoData { get; set; }
        public int AprovacaoUsuarioIdAprovador { get; set; }
        public string AprovacaoUsuarioNomeAprovador { get; set; }
        public int AprovacaoUsuarioIdSolicitante { get; set; }
        public string AprovacaoUsuarioNomeSolicitante { get; set; }

        public List<dynamic> DocumentoVersaoAnexo { get; set; }
        public List<dynamic> DocumentoRelacionado { get; set; }
        public DocumentoDTO()
        {
            this.DocumentoVersaoAnexo = new List<dynamic>();
            this.CamposValor = new List<CampoValor>();
            this.AtividadeCamposVisualizacao = new List<dynamic>();
            this.DocumentoRelacionado = new List<dynamic>();
        }

        public override string ToString()
        {
            return Id > 0 ? string.Format("<b>[</b>Id:{0} / Nome:{1} / ID Superior{2}: <b>]</b>", Id, Nome, DocumentoPaiId) : "";
        }
    }

    public class CampoValor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }

    }
}
