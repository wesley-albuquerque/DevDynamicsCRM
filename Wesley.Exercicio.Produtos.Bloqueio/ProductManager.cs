using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.SharedClass;

namespace Wesley.Exercicio.Produtos.Bloqueio
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
           if (Context.MessageName.ToLower() == "create")
            {
                Entity product = Context.InputParameters["Target"] as Entity;
                
                if (product.GetAttributeValue<bool>("wes_integracao") == false)
                {
                    throw new InvalidPluginExecutionException("Você está no WesleyExercicio2, esta ação não é permitida neste ambiente");
                }
            }
        }
    }
}
