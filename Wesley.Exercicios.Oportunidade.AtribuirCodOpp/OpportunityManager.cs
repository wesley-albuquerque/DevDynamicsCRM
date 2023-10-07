using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Oportunidade.AtribuirCodOpp.Controller;
using Wesley.Exercicios.SharedClass;

namespace Wesley.Exercicios.Oportunidade.AtribuirCodOpp
{
    public class OpportunityManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            if (Context.MessageName.ToLower() == "create")
            {
                OppController oppController = new OppController(Service, Context.OrganizationId);
                Entity opportunity = Context.InputParameters["Target"] as Entity;
                opportunity.Attributes["wes_cod_opp"] = oppController.ObterCodeOpp();
                
            }
        }
    }
}
