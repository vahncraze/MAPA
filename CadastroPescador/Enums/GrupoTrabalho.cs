using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Enums
{
    public enum GruposTrabalho
    {
        EntidadeColaborativa = 1,
        Pescador = 2,
        Embarcacao = 3,
        Servidor = 4,
        INSS = 5,
        EntidadeValidadora = 6,
        EmpresaRastreadora = 7,
        PescadorAmador = 8,
        SisTainha = 9
    }
    public static class GruposTrabalhoExtension
    {

        public static string PegarValor(this GruposTrabalho gruposTrabalho)
        {
            switch (gruposTrabalho)
            {
                case GruposTrabalho.EntidadeColaborativa:
                    return "ENTIDADE";
                case GruposTrabalho.Pescador:
                    return "PESCADOR";
                case GruposTrabalho.Embarcacao:
                    return "EMBARCACAO";
                case GruposTrabalho.Servidor:
                    return "SERVIDOR";
                case GruposTrabalho.INSS:
                    return "INSS";
                case GruposTrabalho.EntidadeValidadora:
                    return "ENTIDADE-VALIDADORA";
                case GruposTrabalho.EmpresaRastreadora:
                    return "EMPRESA-RASTREADORA";
                case GruposTrabalho.PescadorAmador:
                    return "PESCADOR-AMADOR";
                case GruposTrabalho.SisTainha:
                    return "SIS-TAINHA";
                default:
                    return "";

            }

        }

    }

}
