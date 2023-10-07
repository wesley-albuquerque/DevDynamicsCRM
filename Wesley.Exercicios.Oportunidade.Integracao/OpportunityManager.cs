using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wesley.Exercicios.Oportunidade.Integracao.Controller;
using Wesley.Exercicios.SharedClass;
using Wesley.Exercicios.SharedClass.Arquitetura;

namespace Wesley.Exercicios.Oportunidade.Integracao
{
    public class OpportunityManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            if (Context.MessageName.ToLower() == "create" || Context.MessageName.ToLower() == "update")
            {

                Entity opportunity = Context.PostEntityImages["PostImage"];

                ConnectionWesExe2 ambiente2 = new ConnectionWesExe2();

                OppController controllerAmb1 = new OppController(Service);
                //OppController controllerAmb2 = new OppController(Service);
                OppController controllerAmb2 = new OppController(ambiente2.Service);

                //Campos obrigatórios
                var codMoeda = controllerAmb1.GetUniqueKeyById("transactioncurrency", "isocurrencycode", ((EntityReference)opportunity.Attributes["transactioncurrencyid"]).Id);
                var MoedaId = controllerAmb2.GetIdByUniqueCode("transactioncurrency", "isocurrencycode", codMoeda);

                var emailUsuario = controllerAmb1.GetUniqueKeyById("systemuser", "domainname", ((EntityReference)opportunity.Attributes["ownerid"]).Id);
                var userId = controllerAmb2.GetIdByUniqueCode("systemuser", "domainname", emailUsuario);

                //Campos não obrigatórios
                if (opportunity.Contains("parentcontactid"))
                {
                    var cpfContato = controllerAmb1.GetUniqueKeyById("contact", "wes_cpf", ((EntityReference)opportunity.Attributes["parentcontactid"]).Id);
                    var contactId = controllerAmb2.GetIdByUniqueCode("contact", "wes_cpf", cpfContato);
                    if (contactId != Guid.Empty)
                    {
                        opportunity.Attributes["parentcontactid"] = new EntityReference("contact", contactId);
                        opportunity.Attributes["customerid"] = new EntityReference("contact", contactId);

                    }
                    else
                    {
                        opportunity.Attributes.Remove("parentcontactid");
                        opportunity.Attributes.Remove("customerid");
                    }
                }
                if (opportunity.Contains("pricelevelid"))
                {
                    var nomeLista = controllerAmb1.GetUniqueKeyById("pricelevel", "name", ((EntityReference)opportunity.Attributes["pricelevelid"]).Id);
                    Guid ListaId = controllerAmb2.GetIdByUniqueCode("pricelevel", "name", nomeLista);
                    if (ListaId != Guid.Empty)
                    {
                        opportunity.Attributes["pricelevelid"] = new EntityReference("pricelevel", ListaId);
                    }
                    else
                        throw new InvalidPluginExecutionException($"Lista de preços inexistente no WesleyExercício2. Cadastrar a lista: \"{nomeLista}\" em WesleyExercício2");
                }

                opportunity.Attributes["transactioncurrencyid"] = new EntityReference("transactioncurrency", MoedaId);
                opportunity.Attributes["ownerid"] = new EntityReference("systemuser", userId);
                opportunity.Attributes["wes_integracao"] = true;

                if(Context.MessageName.ToLower() == "create")
                {
                    ambiente2.Service.Create(opportunity);

                }
                else
                {
                    Guid oppId = controllerAmb2.GetIdByUniqueCode("opportunity", "wes_cod_opp", opportunity.Attributes["wes_cod_opp"].ToString());
                    opportunity.Id = oppId;
                    ambiente2.Service.Update(opportunity);
                }

                
            }
            if (Context.MessageName.ToLower() == "delete")
            {
                Entity oppotunity = Context.PreEntityImages["PreImage"];
                ConnectionWesExe2 ambiente2 = new ConnectionWesExe2();
                OppController controllerAmb2 = new OppController(ambiente2.Service);
                Guid oppId = controllerAmb2.GetIdByUniqueCode("opportunity", "wes_cod_opp", oppotunity.Attributes["wes_cod_opp"].ToString());
                ambiente2.Service.Delete("opportunity", oppId);
            }
        }
    }
}
