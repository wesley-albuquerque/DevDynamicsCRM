using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Model;

namespace Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Controller
{
    public class OppController
    {
        public Opportunity Opportunity { get; set; }

        public OppController(IOrganizationService service, Guid organizationId) 
        { 
            Opportunity = new Opportunity(service, organizationId);
        }

        public string ObterCodeOpp()
        {
            return Opportunity.ObterCodeOpp();
        }
    }
}
