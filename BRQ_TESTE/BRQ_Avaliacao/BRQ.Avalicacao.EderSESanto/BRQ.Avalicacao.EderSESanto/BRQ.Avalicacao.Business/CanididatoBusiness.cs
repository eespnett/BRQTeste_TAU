using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRQ.Avalicacao.EderSESanto.BRQ.Avalicacao.Business
{
    public class CanididatoBusiness : Data.CandidatoData
    {
        internal bool IncluirCandidato(Candidato item)
        {
            bool retornoIncluirCandidato = false;

            retornoIncluirCandidato = base.IncluirCandidato(item);

            return retornoIncluirCandidato;
        }

        internal List<Candidato> SelecionarCandidatos()
        {
            List<Candidato> retornoListCandidato = new List<Candidato>();

            retornoListCandidato = base.SelecionarCandidatos();

            return retornoListCandidato;
        }
    }
}