using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRQ.Avalicacao.EderSESanto.BRQ.Avalicacao.Entity
{
    public class CandidatoEntity
    {

        public int id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }

        public string NomePai { get; set; }

        public string NomeMae { get; set; }

        public string RG { get; set; }
    }
}