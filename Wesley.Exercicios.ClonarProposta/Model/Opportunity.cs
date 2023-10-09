using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Exercicios.ClonarProposta.Model
{
    public class Opportunity
    {
        public IOrganizationService Service { get; set; }
        public Guid PriceLevelId { get; set; }
        public Guid OppId { get; set; }
        public Guid NewOppId { get; set; }

        public Opportunity(IOrganizationService service)
        {
            Service = service;
        }

        public Entity GetQuoteByOppid(Guid oppId)
        {
            QueryExpression busca = new QueryExpression("quote");
            busca.ColumnSet.AllColumns = true;
            busca.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, oppId);
            EntityCollection entidades = Service.RetrieveMultiple(busca);
            return entidades.Entities.Count() > 0 ? entidades.Entities.LastOrDefault() : null;

        }

        public void GetPriceLevelIdByOppId()
        {
            QueryExpression busca3 = new QueryExpression("opportunity");
            busca3.ColumnSet.AddColumn("pricelevelid");
            busca3.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, OppId);
            EntityCollection registros = Service.RetrieveMultiple(busca3);
            PriceLevelId = ((EntityReference)registros.Entities.FirstOrDefault().Attributes["pricelevelid"]).Id;
        }

        public void CreateNewOpp()
        {
            Entity novaOpp = new Entity("opportunity");
            novaOpp.Attributes["pricelevelid"] = new EntityReference("pricelevel", PriceLevelId);
            NewOppId = Service.Create(novaOpp);
        }

        public Guid ClonaProdutoOpp(Guid oppId, EntityCollection products)
        {
            OppId = oppId;
            GetPriceLevelIdByOppId();
            CreateNewOpp();

            foreach (Entity item in products.Entities)
            {
                Entity produto = new Entity("opportunityproduct");
                produto.Attributes["opportunityid"] = new EntityReference("opportunity", NewOppId);
                produto.Attributes["isproductoverridden"] = item.Attributes["isproductoverridden"];
                produto.Attributes["productdescription"] = item.Attributes["productdescription"];
                produto.Attributes["ispriceoverridden"] = item.Attributes["ispriceoverridden"];
                produto.Attributes["priceperunit"] = item.Attributes["priceperunit"];
                produto.Attributes["volumediscountamount"] = item.Attributes["volumediscountamount"];
                produto.Attributes["quantity"] = item.Attributes["quantity"];
                if (item.Contains("manualdiscountamount"))
                    produto.Attributes["manualdiscountamount"] = item.Attributes["manualdiscountamount"];
                if (item.Contains("tax"))
                    produto.Attributes["tax"] = item.Attributes["tax"];

                Service.Create(produto);
            }
            return NewOppId;
        }
    }
}
