using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalCreator
    {
        IPrincipal CreateUnauthenticatedPrincipal();
        IPrincipal CreatePrincipalFromTicket(FormsAuthenticationTicket ticket);
    }

    public class PrincipalCreator : IPrincipalCreator
    {
        private readonly IUnauthenticatedPrincipalCreator unauthenticatedPrincipalCreator;
        private readonly IPrincipalFromTicketCreator principalFromTicketCreator;

        public PrincipalCreator(IUnauthenticatedPrincipalCreator unauthenticatedPrincipalCreator,
                                IPrincipalFromTicketCreator principalFromTicketCreator)
        {
            this.unauthenticatedPrincipalCreator = unauthenticatedPrincipalCreator;
            this.principalFromTicketCreator = principalFromTicketCreator;
        }

        public IPrincipal CreateUnauthenticatedPrincipal()
        {
            return unauthenticatedPrincipalCreator.Create();
        }

        public IPrincipal CreatePrincipalFromTicket(FormsAuthenticationTicket ticket)
        {
            return principalFromTicketCreator.Create(ticket);
        }
    }
}