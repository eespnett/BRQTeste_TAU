using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRQ.Avalicacao.EderSESanto.BRQ.Avalicacao.Entity;

namespace BRQ.Avalicacao.EderSESanto.BRQ.Avalicacao.Data
{
    public class CandidatoData
    {
        internal bool IncluirCandidato(Candidato item)
        {
            bool retornoIncluirCandidato = false;

            try
            {
                using (Database1Entities myContext = new Database1Entities())
                {
                    myContext.Candidatoes.Add(item);

                    myContext.SaveChanges();

                    retornoIncluirCandidato = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return retornoIncluirCandidato;
        }

        internal List<Candidato> SelecionarCandidatos()
        {
            List<Candidato> retornoListCandidato = new List<Candidato>();


            try
            {
                using (Database1Entities myContext = new Database1Entities())
                {
                    retornoListCandidato = myContext.Candidatoes.ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex ;
            }
            return retornoListCandidato;
        }
    }
}