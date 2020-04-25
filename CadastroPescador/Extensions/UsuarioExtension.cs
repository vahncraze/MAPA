using CadastroPescador.Enums;
using CadastroPescador.Models;
using CadastroPescador.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Extensions
{
    public static class UsuarioExtension
    {
        public static List<DocumentoUsuarioDTO> ToDocumentoDTO(this UsuarioExternoModel usuarioExternoModel)
        {
            var dto = new List<DocumentoUsuarioDTO>();
            if (!usuarioExternoModel.Equals(null))
            {

                dto.Add(new DocumentoUsuarioDTO(1, usuarioExternoModel.RetornarNome()));
                dto.Add(new DocumentoUsuarioDTO(2, usuarioExternoModel.Email));
                dto.Add(new DocumentoUsuarioDTO(3, usuarioExternoModel.Login));
                dto.Add(new DocumentoUsuarioDTO(4, usuarioExternoModel.NomeEmbarcacao));
                dto.Add(new DocumentoUsuarioDTO(5, usuarioExternoModel.NumeroRGP));
                dto.Add(new DocumentoUsuarioDTO(6, usuarioExternoModel.TIE));               
                dto.Add(new DocumentoUsuarioDTO(8, usuarioExternoModel.Estado));
                dto.Add(new DocumentoUsuarioDTO(9, usuarioExternoModel.GrupoTrabalho.PegarValor() == "SIS-TAINHA" ? RetornarSubGrupoSisTainha(usuarioExternoModel) : usuarioExternoModel.GrupoTrabalho.PegarValor()));
                dto.Add(new DocumentoUsuarioDTO(10, usuarioExternoModel.CPF));
                dto.Add(new DocumentoUsuarioDTO(11, usuarioExternoModel.CNPJ));
                dto.Add(new DocumentoUsuarioDTO(12, usuarioExternoModel.NumeroRGP));
                dto.Add(new DocumentoUsuarioDTO(17, usuarioExternoModel.CEP));
                dto.Add(new DocumentoUsuarioDTO(18, usuarioExternoModel.Endereco));
                dto.Add(new DocumentoUsuarioDTO(19, usuarioExternoModel.NumeroEndereco));
                dto.Add(new DocumentoUsuarioDTO(20, usuarioExternoModel.Bairro));
                dto.Add(new DocumentoUsuarioDTO(21, usuarioExternoModel.Cidade));
                dto.Add(new DocumentoUsuarioDTO(22, usuarioExternoModel.Complemento));
                dto.Add(new DocumentoUsuarioDTO(23, usuarioExternoModel.Contato));
                dto.Add(new DocumentoUsuarioDTO(24, usuarioExternoModel.TipoPessoa));
                dto.Add(new DocumentoUsuarioDTO(28, usuarioExternoModel.EmContrucao.PegarValor()));
                dto.Add(new DocumentoUsuarioDTO(30, usuarioExternoModel.RazaoSocial));
                dto.Add(new DocumentoUsuarioDTO(26, usuarioExternoModel.RetornarLotacao()));
                dto.Add(new DocumentoUsuarioDTO(25, usuarioExternoModel.Matricula));
                dto.Add(new DocumentoUsuarioDTO(32, usuarioExternoModel.NumeroSif));

            }
            return dto;
        }

        public static DocumentoLoginModel ToDocumentoLoginModel(this DocumentoDTO documentoDTO)
        {
            var dto = new DocumentoLoginModel();
            if(documentoDTO.CamposValor.Count > 0)
            {
                dto.Id = documentoDTO.Id;
                foreach (var campo in documentoDTO.CamposValor)
                {
                    //login
                    if (campo.Id == 3)
                    {
                        dto.Login = campo.Valor;
                    }

                    //Nome
                    if (campo.Id == 1)
                    {
                        dto.Nome = campo.Valor;
                    }

                    //email
                    if (campo.Id == 2)
                    {
                        dto.Email = campo.Valor;
                    }
                    
                }
            }

            return dto;
        }

        public static string RetornarSubGrupoSisTainha(UsuarioExternoModel usuarioExternoModel)
        {
            string subGrupo = "";
            if (usuarioExternoModel.Formulario == FormularioTainha.CercoTraineira)
                subGrupo = "CERCO-TRAINEIRA";
            if (usuarioExternoModel.Formulario == FormularioTainha.EmalheAnilhado)
                subGrupo = "EMALHE-ANILHADO";
            if (usuarioExternoModel.Formulario == FormularioTainha.EntradaEmpresaPesqueira)
                subGrupo = "EMPRESA-PESQUEIRA";

            return subGrupo;
        }
    }
}
