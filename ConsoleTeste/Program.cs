using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Security.Policy;

namespace ConsoleTeste
{
    public class Program
    {
        static void Main(string[] args)
        {
            Conexao conexao= new Conexao();
            //Guid novaOppId = new Guid("cc1dab9b-c8ce-43c7-a01f-c05a012e7e68");
            Guid oppId = new Guid("1b988b17-aeed-4120-88b3-7475b21f6138");
            QueryExpression busca = new QueryExpression("quote");
            busca.ColumnSet = new ColumnSet(true);
            busca.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, oppId);
            EntityCollection entidades = conexao.Service.RetrieveMultiple(busca);
            Entity cotacao = entidades.Entities.LastOrDefault();
            Console.Write(cotacao.Attributes["createdon"]);

            QueryExpression busca2 = new QueryExpression("quotedetail");
            Guid quoteId = cotacao.Id;
            busca2.ColumnSet = new ColumnSet(true);
            busca2.Criteria.AddCondition("quoteid", ConditionOperator.Equal, quoteId);
            EntityCollection products = conexao.Service.RetrieveMultiple(busca2);
            Console.WriteLine(products);

            QueryExpression busca3 = new QueryExpression("opportunity");
            busca3.ColumnSet.AddColumn("pricelevelid");
            busca3.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, oppId);
            EntityCollection registros = conexao.Service.RetrieveMultiple(busca3);
            Entity oportunidade = registros.Entities.FirstOrDefault();


            Entity novaOpp = new Entity("opportunity");
            novaOpp.Attributes["pricelevelid"] = new EntityReference("pricelevel", ((EntityReference)oportunidade.Attributes["pricelevelid"]).Id);
            Guid novaOppId = conexao.Service.Create(novaOpp);

            foreach (Entity item in products.Entities) 
            {
                Entity produto = new Entity("opportunityproduct"); 
                produto.Attributes["opportunityid"] = new EntityReference("opportunity", novaOppId);
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

                conexao.Service.Create(produto);
            }

            Console.ReadLine();
        }
    }
}
