using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.ClonarProposta.Model
{
    public class Quote
    {
        public IOrganizationService Service { get; set; }

        public Quote(IOrganizationService service)
        {
            Service = service;
        }

        public EntityCollection GetProductsByQuoteId(Guid quoteId)
        {
            QueryExpression busca = new QueryExpression("quotedetail");
            busca.ColumnSet = new ColumnSet(true);
            busca.Criteria.AddCondition("quoteid", ConditionOperator.Equal, quoteId);
            EntityCollection products = Service.RetrieveMultiple(busca);
            return products;
            
        }
    }
}
