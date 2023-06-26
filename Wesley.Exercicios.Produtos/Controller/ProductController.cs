using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Produtos.Model;

namespace Wesley.Exercicios.Produtos.Controller
{
    public class ProductController
    {
        public Produto Produto { get; set; }
        public IOrganizationService Service { get; set; }

        public ProductController(IOrganizationService service) 
        {
            Service = service;
            Produto = new Produto(service);
        }

        public string GetNameById(Guid unidadeId, string entidade)
        {
            return Produto.GetNameId(unidadeId, entidade);
        }

        public Guid GetIdByName(string nome, string entidade)
        {
            return Produto.GetIdByName(nome, entidade);
        }
        public string GetCodeProductById(Guid productId)
        {
            return Produto.GetCodeProductById(productId);
        }
        public Guid GetProductIdByCode(string codigo)
        {
            return Produto.GetProductIdByCode(codigo);
        }
        
    }
}
