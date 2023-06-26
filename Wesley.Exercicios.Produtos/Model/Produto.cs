using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Wesley.Exercicios.Produtos.Model
{
    public class Produto
    {
        public IOrganizationService Service { get; set; }

        public Produto(IOrganizationService service)
        {
            Service = service;
        }

        public string GetNameId(Guid UnidadePadraoId, string entidade)
        {
            QueryExpression busca = new QueryExpression(entidade);
            busca.ColumnSet.AddColumn("name");
            busca.Criteria.AddCondition(entidade + "id", ConditionOperator.Equal, UnidadePadraoId);
            EntityCollection unidades = Service.RetrieveMultiple(busca);
            return unidades.Entities.FirstOrDefault().Attributes["name"].ToString();
        }
        public Guid GetIdByName(string nome, string entidade)
        {
            QueryExpression busca = new QueryExpression(entidade);
            busca.Criteria.AddCondition("name", ConditionOperator.Equal, nome);
            EntityCollection unidades = Service.RetrieveMultiple(busca);
            return unidades.Entities.Count() > 0 ? unidades.Entities.FirstOrDefault().Id : Guid.Empty;
        }
        public string GetCodeProductById(Guid productId)
        {
            QueryExpression busca = new QueryExpression("product");
            busca.ColumnSet.AddColumn("productnumber");
            busca.Criteria.AddCondition("productid", ConditionOperator.Equal, productId);
            EntityCollection produtos = Service.RetrieveMultiple(busca);
            return produtos.Entities.FirstOrDefault().Attributes["productnumber"].ToString();
        }
        public Guid GetProductIdByCode(string codigo)
        {
            QueryExpression busca = new QueryExpression("product");
            busca.Criteria.AddCondition("productnumber", ConditionOperator.Equal, codigo);
            EntityCollection produtos = Service.RetrieveMultiple(busca);
            return produtos.Entities.FirstOrDefault().Id;
        }
    }
}
