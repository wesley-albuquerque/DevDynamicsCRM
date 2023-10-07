using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wesley.Exercicios.SharedClass.Arquitetura
{
    public class ConnectionWesExe2
    {
        public IOrganizationService Service { get; set; }

        public ConnectionWesExe2()
        {
            GetService();
        }
        public void GetService()
        {
            string url = "wesleyexercicio002";
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
