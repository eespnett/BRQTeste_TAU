using BRQ.Avalicacao.EderSESanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using BRQ.Avalicacao.EderSESanto.BRQ.Avalicacao.Business;
using System.Threading;

namespace BRQ.Avalicacao.EderSESanto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["TotalResistrosAgoraNoBanco"]!=null)
            {

            }
            return View();
        }

        public ActionResult Download()
        {
            List<Candidato> myListCandidatosRetornoProcesso = new List<Candidato>();
            StringBuilder strBuilderResponse = new StringBuilder();
            StringBuilder sbRetornoProcesso;
            GerarArquivoRetorno(out myListCandidatosRetornoProcesso, out sbRetornoProcesso, strBuilderResponse);


            string strFName = "Report_" + "Candidatos" + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".csv";
              return File(Encoding.UTF8.GetBytes(sbRetornoProcesso.ToString()), "text/csv", strFName);
        }

        [HttpPost]
        public ActionResult Index(IndexModel myIndexModel, HttpPostedFileBase file)
        {
            //Criação de variáveis do processo
            List<Candidato> myListCandidatos = new List<Candidato>();
            List<Candidato> myListCandidatosRetornoProcesso = new List<Candidato>();
            StringBuilder strBuilderResponse = new StringBuilder();
            string strTmpDirectory = System.Configuration.ConfigurationManager.AppSettings["TempFolder"];

            //Verificar Existência do arquivo na pasta temporária
            VErificarExistenciaArquivoAtual();


            /// Carregar Arquivo Em Mémoria
            CarregarArivoEmMemoria(myListCandidatos);

            //gravar dados no banco de dados
            GravarDadosNoBancoDados(myListCandidatos, strBuilderResponse);

            // gerar arquivo de saida 
            StringBuilder sbRetornoProcesso;
            GerarArquivoRetorno(out myListCandidatosRetornoProcesso, out sbRetornoProcesso, strBuilderResponse);

            ViewData["RetornoMsg"] = strBuilderResponse.ToString();
            myIndexModel.strLogOperacao = strBuilderResponse.ToString();
            myIndexModel.strLogOperacao = strBuilderResponse.ToString();
        
            
            string strFName = "Report_" + "Candidatos" + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".csv";
    


            return View();
        }

        private static void GerarArquivoRetorno(out List<Candidato> myListCandidatosRetornoProcesso, out StringBuilder sbRetornoProcesso, StringBuilder strBuilderResponse)
        {
            myListCandidatosRetornoProcesso = new CanididatoBusiness().SelecionarCandidatos();

       
            sbRetornoProcesso = new StringBuilder();
            sbRetornoProcesso.Append(System.Configuration.ConfigurationManager.AppSettings["fileHeader"].ToString());
            sbRetornoProcesso.Append("\r\n");


            foreach (var item in myListCandidatosRetornoProcesso)
            {
                ///ID,Nome,Email,Idade,NomePai,NomeMae,RG
                sbRetornoProcesso.Append(item.Id + ';' + item.Nome + ';'+ item.Email + ';'+ item.Idade + ';'+ item.NomePai + ';'+ item.NomeMae + ';'+ item.RG + ';');
                sbRetornoProcesso.Append("\r\n");
            }
        
        }

        private void VErificarExistenciaArquivoAtual()
        {
            string fileBasePathVerificar = Server.MapPath(@"~/temp");
            string fileNameVerificar = Path.GetFileName(Request.Files[0].FileName);
            string fullFilePathVerificar = fileBasePathVerificar + "/" + fileNameVerificar;

            if (System.IO.File.Exists(fullFilePathVerificar))
            {
                System.IO.File.Delete(@fullFilePathVerificar);
            }
        }

        private void GravarDadosNoBancoDados(List<Candidato> myListCandidatos, StringBuilder strBuilderResponse)
        {
            if (myListCandidatos.Count > 0)
            {
                int totalRegistroBD = new CanididatoBusiness().SelecionarCandidatos().ToList().Count;

                Session["TotalResistrosAgoraNoBanco"] = totalRegistroBD;


                foreach (var item in myListCandidatos)
                {
                    // incluir no banco de dados
                    bool incluirRegistroCandidato = false;
                    item.Id += totalRegistroBD;
                 
                    incluirRegistroCandidato = new CanididatoBusiness().IncluirCandidato(item);

                    if (incluirRegistroCandidato)
                    {
                        strBuilderResponse.Append(string.Format("O Registro do {0}, filho de {1}. Foi incluido com sucesso.", item.Nome.ToString(), item.NomePai.ToString()));
                        strBuilderResponse.Append("<br/>");


                    }
                    else
                    {
                        strBuilderResponse.Append(string.Format("O Registro do {0}, filho de {1}. Teve erro no processo de inclusão.", item.Nome.ToString(), item.NomePai.ToString()));
                        strBuilderResponse.Append("</br>");

                    }

                    //Aguardar 6 Segundos
                    Thread.Sleep(6000);
                }
            }
            else
            {
                strBuilderResponse.Append("Não Existe dados no Arquivo");

            }
        }

        private void CarregarArivoEmMemoria(List<Candidato> myListCandidatos)
        {
            if (Request.Files[0].FileName != "")
            {
                string strTempHeader = System.Configuration.ConfigurationManager.AppSettings["fileHeader"];
                string strArquivoEntrada = Request.Files[0].FileName;
                HttpPostedFileBase filePostedArquivoEntrada = Request.Files[0];
                Request.Files[0].SaveAs(Server.MapPath(@"~/temp/" + strArquivoEntrada));

                string line;
                string fileBasePath = Server.MapPath(@"~/temp");
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string fullFilePath = fileBasePath + "/" + fileName;
                System.IO.StreamReader fileARquivoEntrada = new System.IO.StreamReader(@fullFilePath);
                var fileLines = new List<string>();
                int intContatoLinhas = 0;



                while ((line = fileARquivoEntrada.ReadLine()) != null)
                {
                    if (line.ToUpper().ToString() != strTempHeader.ToUpper().ToString())
                    {
                        // não é o cabeçario, e vai incluir (processar)
                        Candidato oCandidato = new Candidato();
                        string[] strConteudodaLinha = line.Split(',');
 intContatoLinhas += 1;
                        oCandidato.Id = intContatoLinhas;
                        oCandidato.Nome = strConteudodaLinha[1].ToString();
                        oCandidato.Email = strConteudodaLinha[2].ToString();
                        oCandidato.Idade = int.Parse(strConteudodaLinha[3].ToString());
                        oCandidato.NomePai = strConteudodaLinha[4].ToString();
                        oCandidato.NomeMae = strConteudodaLinha[5].ToString();
                        oCandidato.RG = strConteudodaLinha[6].ToString();
                        myListCandidatos.Add(oCandidato);
                       

                    }


                }
                fileARquivoEntrada.Close();
                System.IO.File.Delete(@fullFilePath);


            }
        }

        [HttpPost]
        public void StopProcess()
        {
           
        }

    }
}