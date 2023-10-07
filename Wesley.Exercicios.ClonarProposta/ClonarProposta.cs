using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.ClonarProposta.Controller;
using Wesley.Exercicios.SharedClass.Arquitetura;

namespace Wesley.Exercicios.ClonarProposta
{
    public class ClonarProposta : ActionCore
    {
        [Input("oppId")]
        public InArgument<string> OppId { get; set; }
        public override void ExecuteAction(CodeActivityContext context)
        {
            string log = "";
            //try
            //{
                
                Guid oppId = new Guid(OppId.Get(context));

                ClonarController clonarController = new ClonarController(Service);
                log += "Recebeu serviço";
                Entity proposta = clonarController.GetQuoteByOppid(oppId);
                log += " >Obteve o OppId" + proposta;
                if (proposta == null)
                    throw new InvalidWorkflowException("Não existe Cotação na Oportunidade selecionada. Crie a cotação antes de cloná-la.");
              
                EntityCollection produtos = clonarController.GetProductsByQuoteId(proposta.Id);
                clonarController.ClonaProdutoOpp(oppId, produtos);
 
                log += " >Obteve os produtos";
    

            //} catch(Exception ex)
            //{
            //    throw new Exception(log, ex);
            //}

            

        }
    }
}
