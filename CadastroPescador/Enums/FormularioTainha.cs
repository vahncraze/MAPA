using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPescador.Enums
{
    public enum FormularioTainha
    {
        CercoTraineira = 1,
        EmalheAnilhado = 2,
        EntradaEmpresaPesqueira = 3
    }

    public static class FormularioTainhaExtension
    {

        public static string PegarValor(this FormularioTainha tipoFormulario)
        {
            switch (tipoFormulario)
            {
                case FormularioTainha.CercoTraineira:
                    return "Cerco Traineira";
                case FormularioTainha.EmalheAnilhado:
                    return "Emalhe Anilhado";
                case FormularioTainha.EntradaEmpresaPesqueira:
                    return "Empresa Pesqueira";
                default:
                    return "";

            }

        }

    }
}
