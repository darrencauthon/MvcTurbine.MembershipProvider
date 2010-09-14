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
}