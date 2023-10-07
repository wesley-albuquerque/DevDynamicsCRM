using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.SharedClass;

namespace Wesley.Exercicios.Conta.ValidaDuplicidadeCNPJ
{
    public class ContaManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            if (Context.MessageName.ToLower() == "create" || Context.MessageName.ToLower() == "update")
            {
                Entity conta = Context.InputParameters["Target"] as Entity;
                var cnpj = conta.Attributes["wes_cnpj"].ToString();
                QueryExpression busca = new QueryExpression("account");
                busca.ColumnSet.AddColumn("name");
                busca.Criteria.AddCondition("wes_cnpj", ConditionOperator.Equal, cnpj);
                EntityCollection contas = Service.RetrieveMultiple(busca);
                if(contas.Entities.Count > 0)
                {
                    var nome = contas.Entities.FirstOrDefault().Attributes["name"].ToString();
                    throw new InvalidPluginExecutionException($"CNPJ já existente para conta {nome}");
                }
            }
            
        }
    }
}
