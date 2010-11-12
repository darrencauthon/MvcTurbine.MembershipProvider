using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.MembershipProvider.Registration
{
    public class DefaultRegistration : DefaultConventionsServiceRegistration, IServiceRegistration
    {
        public DefaultRegistration()
            : base(typeof (DefaultRegistration).Assembly)
        {
            TypesToIgnore.Add(typeof (ITicketDataToPrincipalMapper));
            TypesToIgnore.Add(typeof (IUnauthenticatedPrincipalCreator));
        }
    }

    public abstract class DefaultConventionsServiceRegistration
    {
        private readonly Assembly assembly;

        protected DefaultConventionsServiceRegistration(Assembly assembly)
        {
            this.assembly = assembly;
            TypesToIgnore = new List<Type>();
        }

        protected IList<Type> TypesToIgnore { get; private set; }

        public virtual void Register(IServiceLocator locator)
        {
            var interfaces = GetInterfacesThatAreNotIgnored();
            foreach (var @interface in interfaces)
                RegisterTheInterfaceIfOneImplementerExists(@interface, locator);
        }

        private void RegisterTheInterfaceIfOneImplementerExists(Type @interface, IServiceLocator locator)
        {
            var implementers = GetAllImplementersOfThisInterface(@interface);
            if (ThereIsOneImplementationOfThisInterface(implementers))
                RegisterTheInterfaceWithTheImplementer(@interface, implementers, locator);
        }

        private static void RegisterTheInterfaceWithTheImplementer(Type @interface, IEnumerable<Type> implementers,
                                                                   IServiceLocator locator)
        {
            locator.Register(@interface, implementers.First());
        }

        private static bool ThereIsOneImplementationOfThisInterface(IEnumerable<Type> implementers)
        {
            return implementers.Count() == 1;
        }

        private IEnumerable<Type> GetAllImplementersOfThisInterface(Type @interface)
        {
            return assembly
                .GetTypes()
                .Where(x => x.IsInterface == false && x.IsAbstract == false)
                .Where(x => x.GetInterfaces().Contains(@interface));
        }

        private IEnumerable<Type> GetInterfacesThatAreNotIgnored()
        {
            return assembly.GetTypes()
                .Where(x => TypesToIgnore.Contains(x) == false)
                .Where(x => x.IsInterface);
        }
    }
}