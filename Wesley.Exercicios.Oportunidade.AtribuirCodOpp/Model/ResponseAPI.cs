using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Model
{
    public class ResponseAPI
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public long ExtExpiresIn { get; set; }

        [JsonProperty("expires_on")]
        public long ExpiresOn { get; set; }

        [JsonProperty("not_before")]
        public long NotBefore { get; set; }

        [JsonProperty("resource")]
        public Uri Resource { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("value")]
        public List<Value> Value { get; set; }
    }

    public class Value
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("wes_cod_opp")]
        public string CodOpp { get; set; }

        [JsonProperty("opportunityid")]
        public Guid Opportunityid { get; set; }
    }
}
