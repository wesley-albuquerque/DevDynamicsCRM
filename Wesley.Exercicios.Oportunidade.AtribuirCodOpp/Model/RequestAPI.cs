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
        public Guid OrganizationIdEx1 = new Guid("42aa77a4-7565-ee11-a383-002248debce1");
        public string ClientID = "021dd5e6-4d1d-4057-87ad-4efc57470530";
        public string ClientSecret = "jw18Q~3ER_QFkVBRPInvh4X3SJDfnN3BaLyzbboc";
        public string IdLocatario = "c7b424b2-74c7-4650-935a-850982cdca85";

        public string Token { get; set; }
        public RequestAPI(Guid organizationId) 
        { 
            Client = new HttpClient();
            if(organizationId == OrganizationIdEx1)
            {
                Ambiente = "org0c799842"; //Ambiente 2
            }
            else
                Ambiente = "org835d2368"; //Ambiente 1

        }
        public void GetToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://login.microsoftonline.com/{IdLocatario}/oauth2/token");
            request.Headers.Add("Cookie", "fpc=Avom-BhPSu5Hks3TxUYQeIbrNjZnAQAAAMEowtsOAAAAfBQIwwEAAADvKMLbDgAAAEDKk8sCAAAAHCnC2w4AAAA; stsservicecookie=estsfd; x-ms-gateway-slice=estsfd");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new KeyValuePair<string, string>("client_id", ClientID));
            collection.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            collection.Add(new KeyValuePair<string, string>("client_secret", ClientSecret));
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
