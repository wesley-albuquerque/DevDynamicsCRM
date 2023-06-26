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
            string stringConnection;

            Service = new CrmServiceClient(stringConnection);
            
        }
    }
}
