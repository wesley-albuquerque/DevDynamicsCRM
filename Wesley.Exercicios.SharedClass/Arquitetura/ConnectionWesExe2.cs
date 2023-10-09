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
            string url = "org0c799842";
            string clientId = "021dd5e6-4d1d-4057-87ad-4efc57470530";
            string clientSecret = "jw18Q~3ER_QFkVBRPInvh4X3SJDfnN3BaLyzbboc";
            string stringConnection = $@"AuthType=ClientSecret;
                                      url=https://{url}.crm2.dynamics.com/;
                                      ClientId={clientId};
                                      ClientSecret={clientSecret};";

            Service = new CrmServiceClient(stringConnection);
            
        }
    }
}
