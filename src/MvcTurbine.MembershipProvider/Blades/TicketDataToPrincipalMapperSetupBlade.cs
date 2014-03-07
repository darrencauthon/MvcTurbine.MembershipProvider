using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcTurbine.Blades;
using MvcTurbine.ComponentModel;
using MvcTurbine.MembershipProvider.Mappers;

namespace MvcTurbine.MembershipProvider.Blades
{
    public class TicketDataToPrincipalMapperSetupBlade : Blade
    {
        private readonly IServiceLocator serviceLocator;

        public TicketDataToPrincipalMapperSetupBlade(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public override void Spin(IRotorContext context)
        {
            var implementers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(GetTypesThatImplementTheUnauthenticatedPrincipalCreator)
                .ToList();

            if (implementers.Count() > 1 && implementers.Contains(typeof (DefaultTicketDataToPrincipalMapper)))
                implementers.Remove(typeof (DefaultTicketDataToPrincipalMapper));

            if (implementers.Any())
                serviceLocator.Register<ITicketDataToPrincipalMapper>(implementers.First());
            else
                serviceLocator.Register<ITicketDataToPrincipalMapper, DefaultTicketDataToPrincipalMapper>();
        }

        private static IEnumerable<Type> GetTypesThatImplementTheUnauthenticatedPrincipalCreator(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes()
                    .Where(x => x.IsAbstract == false)
                    .Where(x => x.IsInterface == false)
                    .Where(x => x.GetInterfaces().Contains(typeof (ITicketDataToPrincipalMapper)));
            } catch
            {
                return new Type[] {};
            }
        }
    }
}