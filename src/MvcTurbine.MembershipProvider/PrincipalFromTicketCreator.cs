using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalFromTicketCreator
    {
        IPrincipal Create(FormsAuthenticationTicket ticket);
    }

    public class PrincipalFromTicketCreator : IPrincipalFromTicketCreator
    {
        private readonly IPrincipalFromUserDataCreator principalFromUserDataCreator;

        public PrincipalFromTicketCreator(IPrincipalFromUserDataCreator principalFromUserDataCreator)
        {
            this.principalFromUserDataCreator = principalFromUserDataCreator;
        }

        public IPrincipal Create(FormsAuthenticationTicket ticket)
        {
            return principalFromUserDataCreator.CreatePrincipal(ticket.Name, ticket.UserData);
        }
    }
}