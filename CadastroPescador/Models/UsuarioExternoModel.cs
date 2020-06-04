using CadastroPescador.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Models
{
    public class UsuarioExternoModel
    {

        #region Dados de Usuario
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }
        public string Login
        {
            get
            {
                if (GrupoTrabalho.Equals(GruposTrabalho.EntidadeColaborativa))
                {
                    return CNPJ;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.Pescador))
                {
                    return CPF;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.Embarcacao))
                {
                    if (!string.IsNullOrEmpty(CNPJ))
                    {
                        return $"{CNPJ}ECT";
                    }
                    if (!string.IsNullOrEmpty(CPF))
                    {
                        return $"{CPF}ECT";
                    }
                    if (!string.IsNullOrEmpty(NumeroRGP))
                    {
                        return $"{NumeroRGP}ECT";
                    }
                    
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.Servidor) || GrupoTrabalho.Equals(GruposTrabalho.INSS))
                {
                    return CPF;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.EmpresaRastreadora))
                {
                    return CNPJ;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.PescadorAmador))
                {
                    return "PA" + CPF;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.SisTainha))
                {
                    if(Formulario == FormularioTainha.CercoTraineira || Formulario == FormularioTainha.EmalheAnilhado)
                    return "ST" + NumeroRGP;

                    if(Formulario == FormularioTainha.EntradaEmpresaPesqueira)
                        return "ST" + NumeroSif;
                }
                if (GrupoTrabalho.Equals(GruposTrabalho.MapaBordoOnline)) 
                {
                    return "MB" + TIE;
                }
                return null;
            }
            private set
            {
            }
        }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senha não confere")]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(11, ErrorMessage = "CPF Inválido")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(14, ErrorMessage = "CNPJ Inválido")]
        public string CNPJ { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Email { get; set; }
        [Display(Name = "Telefone para Contato")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Contato { get; set; }

        #endregion

        #region Endereço
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Nome do Endereço")]
        public string NomeEndereco { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Número")]
        public string NumeroEndereco { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MinLength(2, ErrorMessage = "Sigla Invalida")]
        [MaxLength(2, ErrorMessage = "Sigla Invalida")]
        public string Estado { get; set; }
        public string Complemento { get; set; }


        #endregion

        #region Dados do Pescador

        [Display(Name = "Número SIF")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NumeroSif { get; set; }

        [EnumDataType(typeof(FormularioTainha))]
        [Display(Name = "Tipo de Usuário")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public FormularioTainha Formulario { get; set; }

        [EnumDataType(typeof(GruposTrabalho))]
        [Display(Name = "Grupo de Trabalho")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public GruposTrabalho GrupoTrabalho { get; set; }
        [Display(Name = "Lotação")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Lotacao { get; set; }
        [Display(Name = "Matrícula ")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Matricula { get; set; }
        [Display(Name = "Número do RGP")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NumeroRGP { get; set; }
        [Display(Name = "Nome da Embarcação")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string NomeEmbarcacao { get; set; }
        [Display(Name = "Razão Social")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Inscrição na Marinha - TIE")]
        public string TIE { get; set; }
        [Display(Name = "Possui Título de Inscrição de Embarcação – TIE")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public EmConstrucao EmContrucao { get; set; }

        [Display(Name = "Natureza do Responsável Pela Embarcação")]
        public string TipoPessoa
        {
            get
            {
                if (!string.IsNullOrEmpty(CPF))
                {
                    return "CPF";
                }
                if (!string.IsNullOrEmpty(CNPJ))
                {
                    return "CNPJ";
                }
                return null;
            }
            private set
            {

            }
        }

        #endregion

        #region reCaptcha
        public string Token { get; set; }
        #endregion

        public string RetornaCpfCnpj()
        {
            if (!string.IsNullOrEmpty(CPF))
                return CPF;
            if (!string.IsNullOrEmpty(CNPJ))
                return CNPJ;
            if (!string.IsNullOrEmpty(TIE))
                return TIE;
            return null;
        }

        public object ChecarUsuario()
        {
            if (!string.IsNullOrEmpty(CPF))
                return new
                {
                    Id = 10,
                    TipoDocumentalId = 18,
                    Nome = "CPF",
                    ColunaTipo = "C",
                    Comparacao = 3,
                    Valor = CPF
                };
            if (!string.IsNullOrEmpty(CNPJ))
                return new
                {
                    Id = 11,
                    TipoDocumentalId = 18,
                    Nome = "CNPJ",
                    ColunaTipo = "C",
                    Comparacao = 3,
                    Valor = CNPJ
                };
            if (!string.IsNullOrEmpty(TIE))
            {
                return new
                {
                    Id = 6,
                    TipoDocumentalId = 18,
                    Nome = "Número do TIE",
                    ColunaTipo = "C",
                    Comparacao = 3,
                    Valor = TIE
                };
            }
            return null;
        }
        public object ChecarGrupo()
        {
            return new
            {
                Id = 9,
                TipoDocumentalId = 18,
                Nome = "GRUPO-DE-TRABALHO",
                ColunaTipo = "LST",
                Comparacao = 3,
                Valor = (GrupoTrabalho.PegarValor() == "SIS-TAINHA" ? RetornarSubGrupoSisTainha() : GrupoTrabalho.PegarValor())
            };
        }

        public string RetornarObservacao()
        {
            string observacao = "Lotação: " + Lotacao + " | Matricula: " + Matricula;

            if (GrupoTrabalho.Equals(GruposTrabalho.SisTainha))
            {
                if (Formulario == FormularioTainha.CercoTraineira || Formulario == FormularioTainha.EmalheAnilhado)
                    observacao = TIE;

                if (Formulario == FormularioTainha.EntradaEmpresaPesqueira)
                    observacao = CPF;
            }
            if (GrupoTrabalho.Equals(GruposTrabalho.MapaBordoOnline))
            {
                observacao = NumeroRGP;
            }

            return observacao;
        }

        public string RetornarSubGrupoSisTainha()
        {
            string subGrupo = "";
                if (Formulario == FormularioTainha.CercoTraineira)
                subGrupo =  "CERCO-TRAINEIRA";
                if (Formulario == FormularioTainha.EmalheAnilhado)
                subGrupo = "EMALHE-ANILHADO";
                if (Formulario == FormularioTainha.EntradaEmpresaPesqueira)
                subGrupo = "EMPRESA-PESQUEIRA";

            return subGrupo;
        }

        public string RetornarNome() 
        {
            string nome = Nome;

            if (GrupoTrabalho.Equals(GruposTrabalho.SisTainha))
            {
                if (Formulario.Equals(FormularioTainha.CercoTraineira) || Formulario.Equals(FormularioTainha.EmalheAnilhado))
                {
                    nome =  NomeEmbarcacao;
                }
                if (Formulario.Equals(FormularioTainha.EntradaEmpresaPesqueira))
                {
                    nome = RazaoSocial;
                }
            }

            return nome;
        }

        public string RetornarLotacao()
        {
            string lotacao = Lotacao;

            if (GrupoTrabalho.Equals(GruposTrabalho.SisTainha))
            {
                if (Formulario.Equals(FormularioTainha.CercoTraineira) || Formulario.Equals(FormularioTainha.EmalheAnilhado))
                    lotacao =  TIE;
            }

            return lotacao;
        }

    }
}
