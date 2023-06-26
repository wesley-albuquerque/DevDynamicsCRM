using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace Wesley.Exercicios.SharedClass
{
    public abstract class PluginCore : IPlugin
    {
        public IPluginExecutionContext Context { get; set; }
        public IOrganizationServiceFactory ServiceFactory { get; set; }
        public IOrganizationService Service { get; set; }
        public ITracingService TracingService { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            Context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            ServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            Service = ServiceFactory.CreateOrganizationService(Context.UserId);
            TracingService = serviceProvider.GetService(typeof(ITracingService)) as ITracingService;

            ExecutePlugin(serviceProvider);
        }

        public abstract void ExecutePlugin(IServiceProvider serviceProvider);
    }
}
