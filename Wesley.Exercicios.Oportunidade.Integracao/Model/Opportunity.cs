using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.Oportunidade.Integracao.Model
{
    public class Opportunity
    {
        public IOrganizationService Service { get; set; }
        public Opportunity(IOrganizationService service) 
        { 
            Service = service;
        }
        public string GetUniqueKeyById(string entidade, string logicalName, Guid entityId)
        {
            return Service.Retrieve(entidade, entityId, new ColumnSet(logicalName)).Attributes[logicalName].ToString();
        }

        public Guid GetIdByUniqueCode(string entidade, string logicalName, string uniqueCode) 
        { 
            QueryExpression busca = new QueryExpression(entidade);
            busca.Criteria.AddCondition(logicalName, ConditionOperator.Equal, uniqueCode);
            EntityCollection colecao = Service.RetrieveMultiple(busca);
            return colecao.Entities.Count > 0 ? colecao.Entities.First().Id : Guid.Empty;
        }
    }
}
