using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.BuscaCEP.Model
{ 
   public partial class ResponseApiCorreios
    {
        [JsonProperty("cep")]
        public string Cep { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        [JsonProperty("ibge")]
        public string IBGE { get; set; }

        [JsonProperty("ddd")]
        public string DDD { get; set; }

        [JsonProperty("erro")]
        public bool Erro { get; set; }
    }
}
