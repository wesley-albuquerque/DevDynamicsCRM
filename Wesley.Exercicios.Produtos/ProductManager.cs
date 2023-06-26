using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Produtos.Controller;
using Wesley.Exercicios.SharedClass;
using Wesley.Exercicios.SharedClass.Arquitetura;

namespace Wesley.Exercicios.Produtos.Integracao
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            //Create = PostOperation Update = PostOperation
            if (Context.MessageName.ToLower() == "create" || Context.MessageName.ToLower() == "update")
            {
                ConnectionWesExe2 conexaoAmb2 = new ConnectionWesExe2();

                Entity product1 = (Entity)Context.InputParameters["Target"];

                ProductController produtoController = new ProductController(Service);
                ProductController product2Controller = new ProductController(conexaoAmb2.Service);

                product1.Attributes["wes_integracao"] = true;

                if (product1.Contains("defaultuomscheduleid"))
                {
                    string grupoUnNome = produtoController.GetNameById(((EntityReference)product1.Attributes["defaultuomscheduleid"]).Id, "uomschedule");
                    Guid grupoUnId = product2Controller.GetIdByName(grupoUnNome, "uomschedule");
                    if (grupoUnId == Guid.Empty)
                        throw new InvalidPluginExecutionException("Grupo de unidades inexistente no WesleyExercício2, crie grupo de unidades: \"" + grupoUnNome + "\" no WesleyExecercício2\"");
                    product1.Attributes["defaultuomscheduleid"] = new EntityReference("uomschedule", grupoUnId);
                }

                if (product1.Contains("defaultuomid"))
                {
                    string unidadePadraoNome = produtoController.GetNameById(((EntityReference)product1.Attributes["defaultuomid"]).Id, "uom");
                    Guid unidadePadraoId = product2Controller.GetIdByName(unidadePadraoNome, "uom");
                    if (unidadePadraoId == Guid.Empty)
                        throw new InvalidPluginExecutionException("Unidade padrão inexistente no WesleyExercício2, crie a unidade padrão: \"" + unidadePadraoNome + "\" no WesleyExecercício2");
                    product1.Attributes["defaultuomid"] = new EntityReference("uom", unidadePadraoId);
                }

                if (product1.Contains("pricelevelid"))
                {
                  
                    string listaPrecoNome = produtoController.GetNameById(((EntityReference)product1.Attributes["pricelevelid"]).Id, "pricelevel");
                    Guid listaPrecoId = product2Controller.GetIdByName(listaPrecoNome, "pricelevel");
                    if (listaPrecoId == Guid.Empty)
                        throw new InvalidPluginExecutionException("Lista de Preços inexistente no WesleyExercício2, crie a lista de preços: \"" + listaPrecoNome + "\" no WesleyExecercício2");
                    product1.Attributes["pricelevelid"] = new EntityReference("pricelevel", listaPrecoId);
                }

                if (Context.MessageName.ToLower() == "update")
                {
                    string codProduto = produtoController.GetCodeProductById(product1.Id);
                    Guid productId = product2Controller.GetProductIdByCode(codProduto);
                    product1.Id = productId;

                    conexaoAmb2.Service.Update(product1);
                }
                else
                    conexaoAmb2.Service.Create(product1);

               

               
            }
            if (Context.MessageName.ToLower() == "delete")
            {
                Entity product = Context.PreEntityImages["PreImage"];
                ConnectionWesExe2 conexao = new ConnectionWesExe2();
                ProductController productController = new ProductController(conexao.Service);
                Guid productId = productController.GetProductIdByCode(product.Attributes["productnumber"].ToString());
                conexao.Service.Delete("product", productId);
            }

        }


    }
}
