using Microsoft.Xrm.Sdk.Deployment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Model
{
    public class RequestAPI
    {
        public HttpClient Client { get; set; }
        public string Ambiente { get; set; }
        public Guid OrganizationIdEx1 = new Guid("e2e88f9b-6bcb-ed11-aed0-6045bd3ad0ea");
        public string Token { get; set; }
        public RequestAPI(Guid organizationId) 
        { 
            Client = new HttpClient();
            if(organizationId == OrganizationIdEx1)
            {
                Ambiente = "wesleyexercicio2";
            }
            else
                Ambiente = "wesleyexercicio1";

        }
        public void GetToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/a7844c97-0e99-42e9-b118-8b4d7e2a8345/oauth2/token");
            request.Headers.Add("Cookie", "fpc=Avom-BhPSu5Hks3TxUYQeIbrNjZnAQAAAMEowtsOAAAAfBQIwwEAAADvKMLbDgAAAEDKk8sCAAAAHCnC2w4AAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("client_id", "17d7329f-d40e-4866-9b3d-10b9c433ebbb"));
            collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            collection.Add(new KeyValuePair<string, string>("client_secret", "mw38Q~KQSXLEM8J9wZSDFQPE794kMB7Z9wBY-b3d"));
            collection.Add(new KeyValuePair<string, string>("resource", $"https://{Ambiente}.crm2.dynamics.com/"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = Client.SendAsync(request).Result;
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                ResponseAPI resposta = JsonConvert.DeserializeObject<ResponseAPI>(response.Content.ReadAsStringAsync().Result);
                Token = $"{resposta.TokenType} {resposta.AccessToken}";
            }
            else
                throw new Exception("Erro ao obter o token");
        }
        public string BuscaMaiorCodOppNaAPI()
        {
            GetToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://{Ambiente}.crm2.dynamics.com/api/data/v9.2/opportunities?$select=wes_cod_opp&$orderby=wes_cod_opp desc&$top=1");
            request.Headers.Add("Authorization", Token);
            var response = client.SendAsync(request).Result;
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                ResponseAPI responseAPi = JsonConvert.DeserializeObject<ResponseAPI>(response.Content.ReadAsStringAsync().Result);
                return responseAPi.Value.Count > 0 ? responseAPi.Value[0].CodOpp.ToString() : null;
            }
            else throw new Exception("Não foi possível consultar a API");
        }
    }
}
