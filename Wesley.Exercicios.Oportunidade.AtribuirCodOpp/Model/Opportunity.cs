using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Model
{
    public class Opportunity
    {
        public string CodOpp { get; set; }
        public string CodigoOppOrigem { get; set; }
        public string CodigoOppAPI { get; set; }
        public string Termo1 = "OPP";
        public int Termo2 = 12365;
        public string Termo3 = "A1A2";
        public IOrganizationService Service { get; set; }
        public RequestAPI RequestAPI { get; set; }
        public Opportunity(IOrganizationService service, Guid organizationId) 
        {
            Service = service;
            RequestAPI = new RequestAPI(organizationId);
        }
        public string ObterCodeOpp()
        {
            CodigoOppAPI = RequestAPI.BuscaMaiorCodOppNaAPI();
            ObterMaiorCodeOppOrigem();

            if (CodigoOppOrigem != null && CodigoOppAPI != null)
            {
                string[] codOppOrigem = CodigoOppOrigem.Split('-');
                string[] codOppApi = CodigoOppAPI.Split('-');
                int termo2Origem = int.Parse(codOppOrigem[1]);
                int termo2API = int.Parse(codOppApi[1]);
                if (termo2Origem >= termo2API)
                {
                    return AcrescerCodOpp(termo2Origem);
                }
                else
                return AcrescerCodOpp(termo2API);

            }
            else if(CodigoOppOrigem != null && CodigoOppAPI == null)
            {
                string[] codOrigem = CodigoOppOrigem.Split('-');
                return AcrescerCodOpp(int.Parse(codOrigem[1]));
            }
            else if(CodigoOppOrigem == null && CodigoOppAPI != null)
            {
                string[] codAPI = CodigoOppAPI.Split('-');
                return AcrescerCodOpp(int.Parse(codAPI[1]));
            }
            else
                return CodigoOppOrigem = $"{Termo1}-{Termo2}-{Termo3}";


        }
        public void ObterMaiorCodeOppOrigem() 
        {
            QueryExpression busca = new QueryExpression("opportunity");
            busca.ColumnSet.AddColumn("wes_cod_opp");
            busca.AddOrder("wes_cod_opp", OrderType.Descending);
            EntityCollection codigos = Service.RetrieveMultiple(busca);
            CodigoOppOrigem = codigos.Entities.Count > 0 ? codigos.Entities.FirstOrDefault().Attributes["wes_cod_opp"].ToString() : null;
        }

        public string AcrescerCodOpp(int termo2)
        {
            termo2++;
            CodOpp = $"{Termo1}-{termo2}-{Termo3}";
            return CodOpp;
        }

    }

}
