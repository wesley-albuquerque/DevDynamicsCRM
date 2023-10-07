using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.SharedClass;

namespace Wesley.Exercicios.Contato.ValidaDuplicidadeCPF
{
    public class ContatoManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            if (Context.MessageName.ToLower() == "create" || Context.MessageName.ToLower() == "update")
            {
                Entity contato = Context.InputParameters["Target"] as Entity;
                var cpf = contato.Attributes["wes_cpf"].ToString();
                QueryExpression busca = new QueryExpression("contact");
                busca.ColumnSet.AddColumns("firstname", "lastname");
                busca.Criteria.AddCondition("wes_cpf", ConditionOperator.Equal, cpf);
                EntityCollection contatos = Service.RetrieveMultiple(busca);
                if (contatos.Entities.Count > 0)
                {
                    string name = contatos.Entities.FirstOrDefault().Attributes["firstname"].ToString() + " " + contatos.Entities.FirstOrDefault().Attributes["lastname"].ToString();
                    throw new InvalidPluginExecutionException($"CPF já cadastrado no contato {name}");
                }

            }
        }
    }
}
