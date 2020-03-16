using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BRQ.Avalicacao.EderSESanto.Models
{
    public class IndexModel
    {

        [MaxLength(100)]
        public string strArquivoEntrada { get; set; }


        [MaxLength(100)]
        public string strARquvioSaida { get; set; }


        public string strLogOperacao { get; set; }

        public bool IsExecuting { get; set; }
    }
}