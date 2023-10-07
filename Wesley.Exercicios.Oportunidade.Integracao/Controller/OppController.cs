using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Oportunidade.Integracao.Model;

namespace Wesley.Exercicios.Oportunidade.Integracao.Controller
{
    public class OppController
    {
        public Opportunity Opportunity { get; set; }
        public IOrganizationService Service { get; set; }
        public OppController(IOrganizationService service) 
        { 
            Service = service;
            Opportunity = new Opportunity(service);
        }

        public string GetUniqueKeyById(string entidade, string logicalName, Guid entityId)
        {
            return Opportunity.GetUniqueKeyById(entidade, logicalName, entityId);
        }
        public Guid GetIdByUniqueCode(string entidade, string logicalName, string uniqueCode)
        {
            return Opportunity.GetIdByUniqueCode(entidade, logicalName, uniqueCode);
        }
    }
}
