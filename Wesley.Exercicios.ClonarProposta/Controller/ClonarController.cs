using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.ClonarProposta.Model;

namespace Wesley.Exercicios.ClonarProposta.Controller
{
    public class ClonarController
    {
        public Opportunity Opp { get; set; }
        public Quote Quote { get; set; }
        public IOrganizationService Service { get; set; }
        public ClonarController(IOrganizationService service)
        {
            Opp = new Opportunity(service);
            Quote = new Quote(service);
            Service = service;
        }

        public Entity GetQuoteByOppid(Guid oppId)
        {
            return Opp.GetQuoteByOppid(oppId);
        }

        public EntityCollection GetProductsByQuoteId(Guid quoteId)
        {
            return Quote.GetProductsByQuoteId(quoteId);
        }

        public void ClonaProdutoOpp(Guid oppId, EntityCollection products)
        {
            Opp.ClonaProdutoOpp(oppId, products);
        }


    }
}
