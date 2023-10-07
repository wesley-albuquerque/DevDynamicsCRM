using Microsoft.Xrm.Sdk.Workflow;
using Newtonsoft.Json;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Wesley.Exercicios.BuscaCEP.Model;
using Wesley.Exercicios.SharedClass.Arquitetura;

namespace Wesley.Exercicios.BuscaCEP
{
    public class BuscaCEP : ActionCore
    {
        [Input("CEP")]
        public InArgument<string> CEP { get; set; }

        [Output("logradouro")]
        public OutArgument<string> Logradouro { get; set; }
        [Output("complemento")]
        public OutArgument<string> Complemento { get; set; }
        [Output("bairro")]
        public OutArgument<string> Bairro { get; set; }
        [Output("localidade")]
        public OutArgument<string> Localidade { get; set; }
        [Output("uf")]
        public OutArgument<string> UF { get; set; }
        [Output("ibge")]
        public OutArgument<string> IBGE { get; set; }
        [Output("ddd")]
        public OutArgument<string> DDD { get; set; }
        [Output("erro")]
        public OutArgument<bool> Erro { get; set; }
        public override void ExecuteAction(CodeActivityContext context)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{CEP.Get(context)}/json");
            var response = client.SendAsync(request).Result;
            ResponseApiCorreios endereco = JsonConvert.DeserializeObject<ResponseApiCorreios>(response.Content.ReadAsStringAsync().Result);
            
            if (!endereco.Erro)
            {
                Logradouro.Set(context, endereco.Logradouro);
                Complemento.Set(context, endereco.Complemento);
                Bairro.Set(context, endereco.Bairro);
                Localidade.Set(context, endereco.Localidade);
                UF.Set(context, endereco.UF);
                IBGE.Set(context, endereco.IBGE);
                DDD.Set(context, endereco.DDD);
            }
            else
            {
                Erro.Set(context, endereco.Erro);
                Logradouro.Set(context, string.Empty);
                Complemento.Set(context, string.Empty);
                Bairro.Set(context, string.Empty);
                Localidade.Set(context, string.Empty);
                UF.Set(context, string.Empty);
                IBGE.Set(context, string.Empty);
                DDD.Set(context, string.Empty);
            }

        }
    }
}
