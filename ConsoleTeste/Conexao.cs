using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTeste
{
    public class Conexao
    {
        public IOrganizationService Service { get; set; }
        public Conexao() 
        {
            GetService();
        }

        public void GetService()
        {
            string url = "wesleyexercicio001";
            string clientId = "499b218e-07bb-4026-a6c6-ac1346f123e5";
            string clientSecret = "eFk8Q~A0cTKOqFqsYezSuClYv9CzOM7j4No0yder";
            string stringConnection = $@"AuthType=ClientSecret;
                                      url=https://{url}.crm2.dynamics.com/;
                                      ClientId={clientId};
                                      ClientSecret={clientSecret};";
            Service = new CrmServiceClient(stringConnection);
        }
    }
}
