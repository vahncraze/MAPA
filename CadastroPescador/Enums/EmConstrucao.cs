using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Enums
{
    public enum EmConstrucao
    {
        Sim = 1,
        Nao = 2
    }

    public static class GEmConstrucaoExtension
    {

        public static string PegarValor(this EmConstrucao emConstrucao)
        {
            switch (emConstrucao)
            {
                case EmConstrucao.Sim:
                    return "Sim";
                case EmConstrucao.Nao:
                    return "Nao";
                default:
                    return "";

            }

        }

    }
}
