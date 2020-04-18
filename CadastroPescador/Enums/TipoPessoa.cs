using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Enums
{
    public enum TipoPessoa
    {
        PJ = 1,
        PF = 2
    }

    public static class TipoPessoaExtension
    {

        public static string PegarValor(this TipoPessoa tipoPessoa)
        {
            switch (tipoPessoa)
            {
                case TipoPessoa.PF:
                    return "CPF";
                case TipoPessoa.PJ:
                    return "CNPJ";
                default:
                    return "";

            }

        }

    }
}
